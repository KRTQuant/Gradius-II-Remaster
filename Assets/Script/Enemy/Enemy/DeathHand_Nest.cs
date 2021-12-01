using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHand_Nest : MonoBehaviour
{
    [SerializeField] DeathHand_Enemy deathHand_Enemy;

    private void OnBecameVisible()
    {
        deathHand_Enemy.currentBehav = DeathHand_Behavior.INIT;
    }
}
