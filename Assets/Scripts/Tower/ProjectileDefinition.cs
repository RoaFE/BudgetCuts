using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "BudgetDefence/Projectile")]
public class ProjectileDefinition : ScriptableObject
{
    public string Name;
    public float speed;
    public AnimationCurve ProjectilePath;
}
