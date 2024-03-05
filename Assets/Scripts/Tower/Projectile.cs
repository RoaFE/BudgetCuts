using System.Collections;
using System.Collections.Generic;
using Row.Utils;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] ProjectileDefinition m_definition;
    public IEnumerator Shoot(Enemy enemy)
    {
        Vector3 startPos = transform.position;
        Vector3 position = startPos;
        Vector3 enemyPos = enemy.transform.position;
        float t = 0;
        //v = d / t
        while (t < 1)
        {
            if (enemy.gameObject.activeInHierarchy)
                enemyPos = enemy.transform.position;
            position = Vector3.MoveTowards(position, enemyPos, m_definition.Speed * Time.deltaTime);
            t = Utils.InverseVector3Lerp(startPos, enemyPos, position);
            float y = position.y + m_definition.ProjectilePath.Evaluate(t);

            transform.position = position + (Vector3.up * y);
            yield return null;
        }
        if (enemy.gameObject.activeInHierarchy)
            enemy.Damage(m_definition.Damage);

        
        gameObject.SetActive(false);

    }
}
