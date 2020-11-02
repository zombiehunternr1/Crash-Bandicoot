using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private BoxCollider SpinCollider;
    private bool IsSpinning;

    //Gets the BoxCollider component and stores it in the variable SpinCollider.
    void Awake()
    {
        SpinCollider = GetComponentInChildren<BoxCollider>();
    }

    //Once the player presses the attack button the Coroutine SpinAttack will start.
    private void OnSpin()
    {
        StartCoroutine(SpinAttack());
    }

    //Checks if the player isn't already using the attack, if not that means the player can perform the spin attack.
    IEnumerator SpinAttack()
    {
        if (!IsSpinning)
        {
            IsSpinning = true;
            SpinCollider.enabled = true;
            yield return new WaitForSeconds(1);
            SpinCollider.enabled = false;
            IsSpinning = false;
        }
    }
}
