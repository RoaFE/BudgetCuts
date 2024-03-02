using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private TowerDefinition m_towerDefinition;

    private float m_timer;
    // Start is called before    the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_timer += Time.deltaTime;
        
    }

    private void Shoot()
    {

    }

    private void DetectEnemy()
    {
        float radius = m_towerDefinition.Range;
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        List<Enemy> enemiesInRange = new List<Enemy>();
        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].CompareTag("Enemy"))
            {
                enemiesInRange.Add(colliders[i].GetComponent<Enemy>());
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Handles.color = new Color(0.7f, 0.5f, 0.2f, 0.3f);
        Gizmos.DrawSphere(transform.position, m_towerDefinition.Range);
    }
}
