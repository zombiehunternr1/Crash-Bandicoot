using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform PlayerTransform;
    public float SmoothFactor;

    private Vector3 CameraOffset;

    //Gets the CameraOffset from it's own position minus the position of the player and stores it in the variable CameraOffset.
    void Start()
    {
        CameraOffset = transform.position - PlayerTransform.transform.position;
    }

    //Repositions the camera to the players current position with the offset and make the transations look smooth.
    void LateUpdate()
    {
        Vector3 NewPosition = PlayerTransform.transform.position + CameraOffset;
        transform.position = Vector3.Slerp(transform.position, NewPosition, SmoothFactor);
        transform.LookAt(PlayerTransform);
    }
}
