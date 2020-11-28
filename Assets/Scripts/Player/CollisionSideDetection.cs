﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSideDetection : MonoBehaviour
{
    //Enums to help check which side the player hit a certain object or with his attack.
    private enum HitPlayerDirection { None, Top, Bottom, Forward, Back, Left, Right, Spin }

    [HideInInspector]
    public int SideHitValue;
    public CrateBase Crate;

    //public GameEventInt PlayerHit;

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
        if(collision.transform.GetComponent<CrateBase>() != null)
        {
            if (!SpinAttack.enabled)
            {
                ReturnDirection(collision.gameObject, this.gameObject);
            }
            else
            {
                HitPlayerDirection SpinAttack = HitPlayerDirection.Spin;
                SideHitValue = Convert.ToInt32(SpinAttack);;
                Crate.CrateDirectionHit(SideHitValue);
            }         
        }     
    }

    //This Enum function checks which side the player hits a certain object and returns this information.
    private HitPlayerDirection ReturnDirection(GameObject Object, GameObject ObjectHit)
    {
        HitPlayerDirection hitDirection = HitPlayerDirection.None;
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
                    hitDirection = HitPlayerDirection.Top;
                    SideHitValue = Convert.ToInt32(hitDirection);
                    Crate.CrateSide = SideHitValue;
                    Crate.CrateDirectionHit(SideHitValue);
                }
                if (MyNormal == -MyRayHit.transform.up)
                {
                    hitDirection = HitPlayerDirection.Bottom;
                    SideHitValue = Convert.ToInt32(hitDirection);
                    Crate.CrateDirectionHit(SideHitValue);
                }
                if (MyNormal == MyRayHit.transform.forward)
                {
                    hitDirection = HitPlayerDirection.Forward;
                    SideHitValue = Convert.ToInt32(hitDirection);
                    Crate.CrateDirectionHit(SideHitValue);
                }
                if (MyNormal == -MyRayHit.transform.forward)
                {
                    hitDirection = HitPlayerDirection.Back;
                    SideHitValue = Convert.ToInt32(hitDirection);
                    Crate.CrateDirectionHit(SideHitValue);
                }
                if (MyNormal == MyRayHit.transform.right)
                {
                    hitDirection = HitPlayerDirection.Right;
                    SideHitValue = Convert.ToInt32(hitDirection);
                    Crate.CrateDirectionHit(SideHitValue);
                }
                if (MyNormal == -MyRayHit.transform.right)
                {
                    hitDirection = HitPlayerDirection.Left;
                    SideHitValue = Convert.ToInt32(hitDirection);
                    Crate.CrateDirectionHit(SideHitValue);
                }
            }
        }
        return hitDirection;
    }
}
