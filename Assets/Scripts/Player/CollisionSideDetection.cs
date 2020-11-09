using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSideDetection : MonoBehaviour
{
    //Enums to help check which side the player hit a certain object or with his attack.
    private enum HitDirection { None, Top, Bottom, Forward, Back, Left, Right, Spin }

    public int value;

    public GameEventInt Hit;

    private BoxCollider SpinAttack;

    void Start()
    {
        SpinAttack = GetComponentInChildren<BoxCollider>();
    }

    //Checks if the player collides with any gameobject that has the script crate attached to it.
    void OnCollisionEnter(Collision collision)
    {
        //Checks if the collision is with an object that has the Crate script attached to it.
        //If so so it checks if the player did a spinattack when colliding.
        //If so it sets the int value to the enum Spin, else it means the player only hit one of the sides of the object.
        if(collision.transform.GetComponent<Crate>() != null)
        {
            if (!SpinAttack.enabled)
            {
                ReturnDirection(collision.gameObject, this.gameObject);
            }
            else
            {
                HitDirection SpinAttack = HitDirection.Spin;
                value = Convert.ToInt32(SpinAttack);
                Hit.RaiseInt(value);
            }
            
        }     
    }

    //This Enum function checks which side the player hits a certain object and returns this information.
    private HitDirection ReturnDirection(GameObject Object, GameObject ObjectHit)
    {
        HitDirection hitDirection = HitDirection.None;
        RaycastHit MyRayHit;
        Vector3 direction = (Object.transform.position - ObjectHit.transform.position).normalized;
        Ray MyRay = new Ray(ObjectHit.transform.position, direction);

        if (Physics.Raycast(MyRay, out MyRayHit))
        {
            if (MyRayHit.collider != null)
            {
                Vector3 MyNormal = MyRayHit.normal;
                MyNormal = MyRayHit.transform.TransformDirection(MyNormal);

                if (MyNormal == MyRayHit.transform.up)
                {
                    hitDirection = HitDirection.Top;
                    value = Convert.ToInt32(hitDirection);
                    Hit.RaiseInt(value);
                }
                if (MyNormal == -MyRayHit.transform.up)
                {
                    hitDirection = HitDirection.Bottom;
                    value = Convert.ToInt32(hitDirection);
                    Hit.RaiseInt(value);
                }
                if (MyNormal == MyRayHit.transform.forward)
                {
                    hitDirection = HitDirection.Forward;
                    value = Convert.ToInt32(hitDirection);
                    Hit.RaiseInt(value);
                }
                if (MyNormal == -MyRayHit.transform.forward)
                {
                    hitDirection = HitDirection.Back;
                    value = Convert.ToInt32(hitDirection);
                    Hit.RaiseInt(value);
                }
                if (MyNormal == MyRayHit.transform.right)
                {
                    hitDirection = HitDirection.Right;
                    value = Convert.ToInt32(hitDirection);
                    Hit.RaiseInt(value);
                }
                if (MyNormal == -MyRayHit.transform.right)
                {
                    hitDirection = HitDirection.Left;
                    value = Convert.ToInt32(hitDirection);
                    Hit.RaiseInt(value);
                }
            }
        }
        return hitDirection;
    }
}
