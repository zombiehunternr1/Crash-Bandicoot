using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameEvent DestroyedCrate;
    [HideInInspector]
    public bool HasBounced = false;
    [HideInInspector]
    public int JumpAmount;

    private Bounce Bouncing;
    private BreakAmount BreakOverTime;
    private Tnt TntCrate;
    private Nitro NitroCrate;
    private CheckPoint CheckpointCrate;
    private CrateBase Crate;

    private float CurrentHeight;
    private float PreviousHeight;
    public bool FallingDown = false;

    void Awake()
    {
        Crate = GetComponent<CrateBase>();
        Bouncing = GetComponent<Bounce>();
        BreakOverTime = GetComponent<BreakAmount>();
        TntCrate = GetComponent<Tnt>();
        NitroCrate = GetComponent<Nitro>();
        CheckpointCrate = GetComponent<CheckPoint>();
    }

    private void FixedUpdate()
    {
        if (PreviousHeight > transform.position.y)
        {
            FallingDown = true;
        }
        else if (PreviousHeight == transform.position.y)
        {
            FallingDown = false;
        }
        CurrentHeight = transform.position.y;
        PreviousHeight = CurrentHeight;
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
            //Forward
            case 3:
                Forward();
                break;
            //Back
            case 4:
                Back();
                break;
            //Left
            case 5:
                Left();
                break;
            //Right
            case 6:
                Right();
                break;
            //Spin
            case 7:
                Attack();
                break;
            case 8:
                EntityOrEffect();
                break;
        }          
    }

    void Top()
    {
        if (Bouncing)
        {
            if (BreakOverTime)
            {
                JumpAmount++;
                Bouncing.Up();
                BreakOverTime.BreakOverTime(JumpAmount);
                return;
            }
            Bouncing.Up();
            return;
        }
        else if (TntCrate)
        {
            if (!HasBounced)
            {
                TntCrate.Activate();
                Crate.BounceUpPlayer();
                HasBounced = true;
                return;
            }
            return;
        }
        else if (NitroCrate)
        {
            NitroCrate.ExplodeCrate();
        }
        else if (CheckpointCrate)
        {
            CheckpointCrate.SetCheckpoint();
        }
        else
        {
            Crate.BounceUpPlayer();
            DestroyedCrate.Raise();
            StartCoroutine(DelayDeactivating());
        }
    }

    void Bottom()
    {
        if (Bouncing)
        {
            JumpAmount++;
            Bouncing.Down();
            BreakOverTime.BreakOverTime(JumpAmount);
            return;
        }
        else if (TntCrate)
        {
            if (!HasBounced)
            {
                TntCrate.Activate();
                Crate.BounceDownPlayer();
                HasBounced = true;
                return;
            }
            return;
        }
        else if (NitroCrate)
        {
            NitroCrate.ExplodeCrate();
        }
        else
        {
            Crate.BounceDownPlayer();
            DestroyedCrate.Raise();
            StartCoroutine(DelayDeactivating());
        }
    }

    void Forward()
    {
        if (NitroCrate)
        {
            NitroCrate.ExplodeCrate();
        }
    }

    void Back()
    {
        if (NitroCrate)
        {
            NitroCrate.ExplodeCrate();
        }
    }

    void Left()
    {
        if (NitroCrate)
        {
            NitroCrate.ExplodeCrate();
        }
    }

    void Right()
    {
        if (NitroCrate)
        {
            NitroCrate.ExplodeCrate();
        }
    }

    void Attack()
    {
        if (TntCrate)
        {
            TntCrate.ExplodeCrate();
        }
        else if (NitroCrate)
        {
            NitroCrate.ExplodeCrate();
        }
        else if (Bouncing)
        {
            if (BreakOverTime)
            {
                BreakOverTime.BreakCrate();
            }
            else
            {
                DestroyedCrate.Raise();
                gameObject.SetActive(false);
            }
        }
        else if (CheckpointCrate)
        {
            CheckpointCrate.SetCheckpoint();
        }
        else
        {
            DestroyedCrate.Raise();
            gameObject.SetActive(false);
        }
    }

    void EntityOrEffect()
    {
        if (!CheckpointCrate)
        {
            if (TntCrate)
            {
                TntCrate.ExplodeCrate();
            }
            else if (NitroCrate)
            {
                NitroCrate.ExplodeCrate();
            }

            else if (BreakOverTime)
            {
                BreakOverTime.BreakCrate();
            }
            else if (Bouncing)
            {
                Bouncing.breakCrate();
            }
            else
            {
                breakCrate();
            }
        }      
    }

    //This coroutine lets the player bounce of the crate before getting deactivated.
    private IEnumerator DelayDeactivating()
    {
        yield return new WaitForSeconds(.1f);
        gameObject.SetActive(false);
    }

    //Once this function gets called it raises the crate destroyed event and disables the gameobject.
    public void breakCrate()
    {
        DestroyedCrate.Raise();
        gameObject.SetActive(false);
    }
}
