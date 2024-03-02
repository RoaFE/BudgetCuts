using System.Collections;
using System.Collections.Generic;
using Row.Utils;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] ProjectileDefinition m_definition;
    public IEnumerator Shoot(Transform enemy)
    {
        Vector3 startPos = transform.position;
        Vector3 position = startPos;
        float t = 0;
        //v = d / t
        while (t < 1)
        {
            position = Vector3.MoveTowards(position, enemy.position, m_definition.speed * Time.deltaTime);
            t = Utils.InverseVector3Lerp(startPos, enemy.position, position);
            float y = position.y + m_definition.ProjectilePath.Evaluate(t);

            transform.position = position + (Vector3.up * y);
            yield return null;
        }

        gameObject.SetActive(false);

    }
}
