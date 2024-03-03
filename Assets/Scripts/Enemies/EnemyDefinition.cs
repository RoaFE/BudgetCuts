using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "BudgetDefence/Enemy")]
public class EnemyDefinition : ScriptableObject
{
    public string Name;
    public float Speed;
    public int Health;
    public int Damage;
    public int Value;
}
