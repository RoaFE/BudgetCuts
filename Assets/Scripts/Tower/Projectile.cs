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
        float t = 0;
        //v = d / t
        while (t < 1)
        {
            position = Vector3.MoveTowards(position, enemy.transform.position, m_definition.Speed * Time.deltaTime);
            t = Utils.InverseVector3Lerp(startPos, enemy.transform.position, position);
            float y = position.y + m_definition.ProjectilePath.Evaluate(t);

            transform.position = position + (Vector3.up * y);
            yield return null;
        }
        enemy.Damage(m_definition.Damage);
        gameObject.SetActive(false);

    }
}
