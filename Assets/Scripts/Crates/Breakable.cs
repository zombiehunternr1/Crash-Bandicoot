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

    void Awake()
    {
        Crate = GetComponent<CrateBase>();
        Bouncing = GetComponent<Bounce>();
        BreakOverTime = GetComponent<BreakAmount>();
        TntCrate = GetComponent<Tnt>();
        NitroCrate = GetComponent<Nitro>();
        CheckpointCrate = GetComponent<CheckPoint>();
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
                Crate.BounceUp();
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
            Crate.BounceUp();
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
                Crate.BounceDown();
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
            Crate.BounceDown();
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
    }

    //This coroutine lets the player bounce of the crate before getting deactivated.
    private IEnumerator DelayDeactivating()
    {
        yield return new WaitForSeconds(.1f);
        gameObject.SetActive(false);
    }
}
