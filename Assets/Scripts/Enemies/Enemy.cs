using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyDefinition m_enemyDefinition;
    // Start is called before the first frame update
    [SerializeField] private int m_health;
    [SerializeField] private uint m_damage;

    //Path

    public void Init()
    {
        m_health = m_enemyDefinition.Health;
    }

    public void Move()
    {
        throw new NotImplementedException();
    }

    public void Damage(int amount)
    {
        m_health -= amount;
        if(m_health < 0)
        {
            //Die
            gameObject.SetActive(false);
        }
    }
}
