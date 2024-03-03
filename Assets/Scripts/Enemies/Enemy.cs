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
    private List<Vector3> m_currentPath;
    private int m_pathIndex = 0;
    public void Spawn(List<Vector3> path)
    {
        m_currentPath = path;
        m_pathIndex = 0;

    }

    public void Init()
    {
        m_health = m_enemyDefinition.Health;
    }
    

    private void Update() {
        if(gameObject.activeInHierarchy)
        {
            Move();
        }
    }

    public void Move()
    {
        float speed = m_enemyDefinition.Speed;
        if((transform.position - m_currentPath[m_pathIndex]).sqrMagnitude < 9)
        {
            m_pathIndex++;
            if(m_pathIndex == m_currentPath.Count)
            {
                gameObject.SetActive(false);
            }
        }
        Vector3 newPos = Vector3.MoveTowards(transform.position, m_currentPath[m_pathIndex], speed * Time.deltaTime);
        transform.position = newPos;
    }

    public void Damage(int amount)
    {
        m_health -= amount;
        if(m_health < 0)
        {
            //Die
            gameObject.SetActive(false);
            transform.position = m_currentPath[0];
        }
    }
}
