using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private TowerMenuViewController m_viewController;
    [SerializeField] private TowerDefinition m_towerDefinition;
    public TowerDefinition TowerDefinition => m_towerDefinition;

    private GameObject m_childObject;

    private Enemy m_closestEnemy;

    private float m_timer;
    // Start is called before    the first frame update
    public void Initialise()
    {
        m_viewController = FindObjectOfType<TowerMenuViewController>();
        UpdatePrefab();
        if(m_towerDefinition)
            LevelManager.Instance.UpdateUpKeep(m_towerDefinition.UpKeep);
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
        StartCoroutine(projectile.GetComponent<Projectile>().Shoot(m_closestEnemy));
    }

    private void OnMouseDown() {
        m_viewController.Disable();
        m_viewController.Enable(this);
    }

    public void OnUpgrade(int index = 0)
    {
        if (m_towerDefinition.UpgradeTowers == null)
            return;
        if(m_towerDefinition.UpgradeTowers[index] != null)
        {
            //Money stuff
            //...
            LevelManager.Instance.UpdateMoney(-m_towerDefinition.UpgradeTowers[index].Cost);
            LevelManager.Instance.UpdateUpKeep(-m_towerDefinition.UpKeep);
            m_towerDefinition = m_towerDefinition.UpgradeTowers[index];
            LevelManager.Instance.UpdateUpKeep(m_towerDefinition.UpKeep);
            UpdatePrefab();
            m_viewController.Disable();
            m_viewController.Enable(this);
        }
    }

    public void OnDowngrade()
    {
        if (m_towerDefinition.DowngradeTower == null)
            return;
        LevelManager.Instance.UpdateMoney(Mathf.FloorToInt(m_towerDefinition.Cost * 0.8f));
        LevelManager.Instance.UpdateUpKeep(-m_towerDefinition.UpKeep);
        m_towerDefinition = m_towerDefinition.DowngradeTower;
        LevelManager.Instance.UpdateUpKeep(m_towerDefinition.UpKeep);
        UpdatePrefab();
        m_viewController.Disable();
        m_viewController.Enable(this);
    }
    
    private void UpdatePrefab()
    {
        if (m_childObject != null)
            Destroy(m_childObject);
        if(m_towerDefinition != null)
        {
            m_childObject = Instantiate(m_towerDefinition.Prefab, transform);
        }
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
