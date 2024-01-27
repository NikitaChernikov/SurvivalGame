using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    private int _health = 100;

    public bool IsDead()
    {
        if (_health > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void GetDamage(int amount)
    {
        Debug.Log("Damage to Monster: " + amount);
    }
}
