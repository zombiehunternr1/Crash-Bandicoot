using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameEvent DestroyedCrate;
    public GameObject Woompa;
    public GameObject life;
    public GameObject WoompaSFX;
    public GameObject LifeSFX;
    public PlayerInfo Player;
    [HideInInspector]
    public bool HasBounced = false;
    [HideInInspector]
    public int JumpAmount;
    [HideInInspector]
    public bool FallingDown = false;

    public bool AutoAdd = false;

    private Bounce Bouncing;
    private BreakAmount BreakOverTime;
    private Tnt TntCrate;
    private Nitro NitroCrate;
    private CheckPoint CheckpointCrate;
    private Questionmark QuestionmarkCrate;
    private AkuAkuCrate AkuAkuCrate;
    private CrateBase Crate;
    private float CurrentHeight;
    private float PreviousHeight;   

    void Awake()
    {
        Crate = GetComponent<CrateBase>();
        Bouncing = GetComponent<Bounce>();
        BreakOverTime = GetComponent<BreakAmount>();
        TntCrate = GetComponent<Tnt>();
        NitroCrate = GetComponent<Nitro>();
        CheckpointCrate = GetComponent<CheckPoint>();
        QuestionmarkCrate = GetComponent<Questionmark>();
        AkuAkuCrate = GetComponent<AkuAkuCrate>();
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
            //Invincibility
            case 8:
                Invincibility();
                break;
            //Entity
            case 9:
                Entity();
                break;
            //Explosion
            case 10:
                Explosion();
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
        else if (QuestionmarkCrate)
        {
            QuestionmarkCrate.DropItems();
        }
        else if (AkuAkuCrate)
        {
            if (AkuAkuCrate.AutoAdd)
            {
                AkuAkuCrate.AddItem();
            }
            else
            {
                AkuAkuCrate.DropItem();
            }
        }
        else
        {
            Crate.BounceUpPlayer();
            StartCoroutine(DelayDeactivating());
        }
    }

    void Bottom()
    {
        if (Bouncing)
        {
            if (BreakOverTime)
            {
                JumpAmount++;
                Bouncing.Down();
                BreakOverTime.BreakOverTime(JumpAmount);
                return;
            }
            Bouncing.Down();
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
        else if (QuestionmarkCrate)
        {
            QuestionmarkCrate.DropItems();
        }
        else if (AkuAkuCrate)
        {
            if (AkuAkuCrate.AutoAdd)
            {
                AkuAkuCrate.AddItem();
            }
            else
            {
                AkuAkuCrate.DropItem();
            }
        }
        else
        {
            Crate.BounceDownPlayer();
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
                breakCrate();
            }
        }
        else if (CheckpointCrate)
        {
            CheckpointCrate.SetCheckpoint();
        }
        else if (QuestionmarkCrate)
        {
            QuestionmarkCrate.DropItems();
        }
        else if (AkuAkuCrate)
        {
            if (AkuAkuCrate.AutoAdd)
            {
                AkuAkuCrate.AddItem();
            }
            else
            {
                AkuAkuCrate.DropItem();
            }
        }
        else
        {
            breakCrate();
        }
    }

    void Explosion()
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
            else if (QuestionmarkCrate)
            {
                QuestionmarkCrate.BreakCrate();
            }
            else if (AkuAkuCrate)
            {
                if (AkuAkuCrate.AutoAdd)
                {
                    AkuAkuCrate.AddItem();
                }
                else
                {
                    AkuAkuCrate.DropItem();
                }
            }
            else
            {
                DestroyedCrate.Raise();
                gameObject.SetActive(false);
            }
        }      
    }

    void Entity()
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
                Player.Woompa++;
                BreakOverTime.BreakCrate();
            }
            else if (Bouncing)
            {
                Player.Woompa++;
                breakCrate();
            }
            else if (QuestionmarkCrate)
            {
                QuestionmarkCrate.DropItems();
            }
            else if (AkuAkuCrate)
            {
                AkuAkuCrate.AddItem();
            }
            else
            {
                Player.Woompa++;
                breakCrate();
            }
        }
    }

    void Invincibility()
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
                for(int i = 0; i < 5; i++)
                {
                    Instantiate(WoompaSFX);
                    Player.Woompa++;
                }
                BreakOverTime.BreakCrate();
            }
            else if (Bouncing)
            {
                Instantiate(WoompaSFX);
                Player.Woompa++;
                Bouncing.breakCrate();
            }
            else if (QuestionmarkCrate)
            {
                QuestionmarkCrate.AutoAdd = true;
                QuestionmarkCrate.DropItems();
            }
            else if (AkuAkuCrate)
            {
                if (AkuAkuCrate.AutoAdd)
                {
                    AkuAkuCrate.AddItem();
                }
                else
                {
                    AkuAkuCrate.DropItem();
                }
            }
            else
            {
                AutoAdd = true;
                breakCrate();
            }
        }
        else
        {
            CheckpointCrate.SetCheckpoint();
        }
    }

    //This coroutine lets the player bounce of the crate before getting deactivated.
    private IEnumerator DelayDeactivating()
    {        
        yield return new WaitForSeconds(.1f);
        breakCrate();
    }

    //Once this function gets called it checks if either the Woompa gameobject or the life object isn't null, if it isn't it either spawns in the Woompa or a life.
    //Else it just raises the crate destroyed event and disables the gameobject.
    public void breakCrate()
    {
        if (AutoAdd)
        {
            if (Woompa != null)
            {
                Instantiate(WoompaSFX);
                Player.Woompa++;
            }
            if (life != null)
            {
                Instantiate(LifeSFX);
                Player.Lives++;
            }
        }
        else
        {
            if (Woompa != null)
            {
                Instantiate(Woompa, transform.position, transform.rotation);
            }
            if (life != null)
            {
                Instantiate(life, transform.position, transform.rotation);
            }
        }
        DestroyedCrate.Raise();
        gameObject.SetActive(false);
    }
}
