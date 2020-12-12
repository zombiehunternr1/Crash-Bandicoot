using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSideDetection : MonoBehaviour
{
    //Enums to help check which side the player hit a certain object or with his attack.
    private enum HitPlayerDirection { None, Top, Bottom, Forward, Back, Left, Right, Spin }

    [HideInInspector]
    public int SideHitValue;
    private CrateBase Crate;
    private EnemyBase Enemy;
    private BoxCollider SpinAttack;

    void Start()
    {
        Crate = GetComponent<CrateBase>();
        Enemy = GetComponent<EnemyBase>();
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.transform.GetComponent<PlayerActions>() != null)
        {
            gameObject.GetComponent<BoxCollider>().material.staticFriction = 0;
            if (gameObject.GetComponent<MetalCrate>())
            {
                gameObject.GetComponent<MetalCrate>().GetComponent<Rigidbody>().mass = 1.35f;
            }          
        }
    }

    //Checks if the player collides with it.
    void OnCollisionEnter(Collision collision)
    {
        //checks if the player is colliding.
        //If so it checks ifthe player did a spin attack when colliding.
        //If not than the player just either walked or jumped when colliding.
        //Afterwards it checks if the player collided with either an crate or an enemy.
        //In either crate or enemy we check if the gameobject is still active. If not it means the player has already interacted with it.
        //This is to prevent double firing the same event.
        if (collision.transform.GetComponent<PlayerActions>() != null)
        {
            //gets the boxcollider of the player and stores it in SpinAttack.
            SpinAttack = collision.gameObject.GetComponent<BoxCollider>();

            if (!SpinAttack.enabled)
            {
                ReturnDirection(collision.gameObject, this.gameObject);
                if (Crate)
                {
                    Crate.CrateDirectionHit(SideHitValue, collision.gameObject.GetComponent<Rigidbody>().velocity);
                }
                if (Enemy)
                {
                    Enemy.EnemyDirectionHit(SideHitValue);
                }
            }
            else
            {
                HitPlayerDirection SpinAttack = HitPlayerDirection.Spin;
                SideHitValue = Convert.ToInt32(SpinAttack);
                if (Crate)
                {
                    if (Crate.gameObject.activeSelf)
                    {
                        Crate.CrateDirectionHit(SideHitValue, collision.gameObject.GetComponent<Rigidbody>().velocity);
                    }                  
                }
                if (Enemy)
                {
                    if (Enemy.gameObject.activeSelf)
                    {
                        Enemy.EnemyDirectionHit(SideHitValue);
                    }                  
                }
            }                            
        }
        else if(collision.gameObject.GetComponent<CrateBase>() != null)
        {
            if (Crate.GravityEnabled)
            {
                if (this.gameObject.GetComponent<Interactable>())
                {
                    if (this.gameObject.GetComponent<MetalCrate>())
                    {
                        this.gameObject.GetComponent<CrateBase>().BounceUpCrate();
                        return;
                    }
                }
                if (collision.gameObject.GetComponent<Bounce>())
                {
                    if (!this.gameObject.GetComponent<Breakable>().FallingDown)
                    {
                        if (collision.gameObject.GetComponent<BreakAmount>())
                        {
                            collision.gameObject.GetComponent<CrateBase>().BounceUpCrate();
                        }                       
                    }
                    else
                    {
                        if (this.gameObject.GetComponent<Tnt>())
                        {
                            this.gameObject.GetComponent<Tnt>().Activate();
                        }
                        if (this.gameObject.GetComponent<Nitro>())
                        {
                            if (!collision.gameObject.GetComponent<BreakAmount>())
                            {
                                collision.gameObject.GetComponent<CrateBase>().BounceUpCrate();
                            }
                        }
                        else
                        {
                            collision.gameObject.GetComponent<CrateBase>().BounceUpCrate();
                        }
                    }
                }
                else if (collision.gameObject.GetComponent<Tnt>() && !this.gameObject.GetComponent<Nitro>())
                {
                    collision.gameObject.GetComponent<Tnt>().Activate();
                }
                else if (collision.gameObject.GetComponent<Nitro>())
                {
                    if (this.gameObject.GetComponent<Breakable>().FallingDown)
                    {
                        collision.gameObject.GetComponent<Nitro>().ExplodeCrate();
                    }                        
                }
                else if(collision.gameObject.GetComponent<Breakable>())
                {
                    if (this.gameObject.GetComponent<Tnt>())
                    {
                        if (this.gameObject.GetComponent<Breakable>().FallingDown)
                        {
                            this.gameObject.GetComponent<Tnt>().Activate();
                        }                 
                    }
                    else
                    {
                        if (!this.gameObject.GetComponent<Nitro>())
                        {
                            if (!this.gameObject.GetComponent<Breakable>())
                            {
                                this.gameObject.GetComponent<CrateBase>().BounceUpCrate();
                            }                            
                        }                       
                    }
                }
            }          
        }
    }

    //This Enum function checks which side the player hits a certain object and returns this information.
    private HitPlayerDirection ReturnDirection(GameObject Object, GameObject ObjectHit)
    {
        HitPlayerDirection hitDirection = HitPlayerDirection.None;
        RaycastHit MyRayHit;
        Vector3 direction = (ObjectHit.transform.position - Object.transform.position).normalized;
        Ray MyRay = new Ray(Object.transform.position, direction);

        if (Physics.Raycast(MyRay, out MyRayHit))
        {
            if (MyRayHit.collider != null)
            {
                Vector3 MyNormal = MyRayHit.normal;
                MyNormal = MyRayHit.transform.TransformDirection(MyNormal);

                if (MyNormal == MyRayHit.transform.up)
                {
                    hitDirection = HitPlayerDirection.Top;
                    SideHitValue = Convert.ToInt32(hitDirection);
                }
                if (MyNormal == -MyRayHit.transform.up)
                {
                    hitDirection = HitPlayerDirection.Bottom;
                    SideHitValue = Convert.ToInt32(hitDirection);
                }
                if (MyNormal == MyRayHit.transform.forward)
                {
                    hitDirection = HitPlayerDirection.Forward;
                    SideHitValue = Convert.ToInt32(hitDirection);
                }
                if (MyNormal == -MyRayHit.transform.forward)
                {
                    hitDirection = HitPlayerDirection.Back;
                    SideHitValue = Convert.ToInt32(hitDirection);
                }
                if (MyNormal == MyRayHit.transform.right)
                {
                    hitDirection = HitPlayerDirection.Right;
                    SideHitValue = Convert.ToInt32(hitDirection);
                }
                if (MyNormal == -MyRayHit.transform.right)
                {
                    hitDirection = HitPlayerDirection.Left;
                    SideHitValue = Convert.ToInt32(hitDirection);
                }
            }
        }
        return hitDirection;
    }
}
