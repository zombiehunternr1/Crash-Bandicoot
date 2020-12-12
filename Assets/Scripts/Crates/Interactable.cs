using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Bounce BouncingCrate;
    private CrateBase Crate;
    private CheckPoint CheckpointCrate;
    private MetalCrate MetalCrate;
    private NitroDetonator NitroDetonatorCrate;
    private Activator ActivatorCrate;
    private bool HasBounced = false;

    void Awake()
    {
        BouncingCrate = GetComponent<Bounce>();
        Crate = GetComponent<CrateBase>();
        CheckpointCrate = GetComponent<CheckPoint>();
        MetalCrate = GetComponent<MetalCrate>();
        NitroDetonatorCrate = GetComponent<NitroDetonator>();
        ActivatorCrate = GetComponent<Activator>();
    }

    public void CrateAction(int CrateSide)
    {
        switch (CrateSide)
        {
            //Top
            case 1:
                Top();
                break;
            //Botom
            case 2:
                Bottom();
                break;
            //Spin
            case 7:
                Attack();
                break;
            //Effect or Enemy
            case 8:
                EntityOrEffect();
                break;
        }        
    }

    void Top()
    {
        if (BouncingCrate)
        {
            BouncingCrate.Up();
        }
        else if (CheckpointCrate)
        {
            CheckpointCrate.SetCheckpoint();
        }
        else if (NitroDetonatorCrate)
        {
            NitroDetonatorCrate.DestroyAllNitroCrates();
        }
        else if (ActivatorCrate)
        {
            ActivatorCrate.ActivateCrates();
        }
        else if (MetalCrate)
        {
            MetalCrate.GetComponent<Rigidbody>().mass = 0.4f;
        }
        else
        {
            if (!HasBounced)
            {
                Crate.BounceUpPlayer();
                HasBounced = true;
            }            
        }
    }

    void Bottom()
    {
        if (BouncingCrate)
        {
            BouncingCrate.Down();
        }
        if (MetalCrate)
        {
            MetalCrate.IsFallingDown();
        }
    }

    void Attack()
    {
        if (CheckpointCrate)
        {
            CheckpointCrate.SetCheckpoint();
        }
        else if (NitroDetonatorCrate)
        {
            NitroDetonatorCrate.DestroyAllNitroCrates();
        }
        else if (ActivatorCrate)
        {
            ActivatorCrate.ActivateCrates();
        }
    }

    void EntityOrEffect()
    {
        if (ActivatorCrate)
        {
            ActivatorCrate.ActivateCrates();
        }
    }
}
