using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private PlayerActions Player;

    private void Awake()
    {
        Player = GetComponentInParent<PlayerActions>();
    }

    public void AnimationIdleEnd()
    {
        Player.StandingIdle = true;
        Player.StartIdle = 0;
    }
}
