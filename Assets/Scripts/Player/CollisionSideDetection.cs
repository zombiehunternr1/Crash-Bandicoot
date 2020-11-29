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

    void Start()
    {
        Crate = GetComponent<CrateBase>();
        Enemy = GetComponent<EnemyBase>();
    }

    private BoxCollider SpinAttack;

    //Checks if the player collides with it.
    void OnCollisionEnter(Collision collision)
    {
        //checks if the player is colliding.
        //If so it checks ifthe player did a spin attack when colliding.
        //If not than the player just either walked or jumped when colliding.
        //Afterwards it checks if the player collided with either an crate or an enemy.
        if (collision.transform.GetComponent<PlayerActions>() != null)
        {
            //gets the boxcollider of the player and stores it in SpinAttack.
            SpinAttack = collision.gameObject.GetComponent<BoxCollider>();

            if (!SpinAttack.enabled)
            {
                ReturnDirection(collision.gameObject, this.gameObject);
                if (Crate)
                {
                    Crate.CrateDirectionHit(SideHitValue);
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
                    Crate.CrateDirectionHit(SideHitValue);
                }
                if (Enemy)
                {
                    Enemy.EnemyDirectionHit(SideHitValue);
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
