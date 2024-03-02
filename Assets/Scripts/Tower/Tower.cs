using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private TowerDefinition m_towerDefinition;

    private Enemy m_closestEnemy;

    private float m_timer;
    // Start is called before    the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectEnemy();

        if(m_timer < m_towerDefinition.RateOfFire)
            m_timer += Time.deltaTime;
        if(m_timer >= m_towerDefinition.RateOfFire && m_closestEnemy != null)
        {
            Shoot();
            m_timer = 0;
        }
    }

    private void Shoot()
    {
        GameObject projectile = ObjectPooler.Instance.SpawnFromPool(m_towerDefinition.ProjectileDefinition.Name, transform.position, Quaternion.identity);
        StartCoroutine(projectile.GetComponent<Projectile>().Shoot(m_closestEnemy.transform));
    }

    private void DetectEnemy()
    {
        float radius = m_towerDefinition.Range;
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        if (colliders.Length == 0)
        {
            m_closestEnemy = null;
            return;
        }

        
        List<Enemy> enemiesInRange = new List<Enemy>();
        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].CompareTag("Enemy"))
            {
                enemiesInRange.Add(colliders[i].GetComponent<Enemy>());
            }
        }
        if (enemiesInRange.Count == 0)
        {
            m_closestEnemy = null;
            return;
        }
        Enemy closest = enemiesInRange[0];
        float dstSqr = float.MaxValue;
        Vector3 position = transform.position;
        for (int i = 0; i < enemiesInRange.Count; i++)
        {
            float curSqrDst = (enemiesInRange[i].transform.position - position).sqrMagnitude;
            if(curSqrDst < dstSqr)
            {
                dstSqr = curSqrDst;
                closest = enemiesInRange[i];
            }
        }

        m_closestEnemy = closest;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(0.7f, 0.5f, 0.2f, 0.3f);
        Gizmos.DrawWireSphere(transform.position, m_towerDefinition.Range);
    }
}
