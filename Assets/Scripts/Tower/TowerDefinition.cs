using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "BudgetDefence/Tower")]
public class TowerDefinition : ScriptableObject
{
    public string Name;
    public float Cost;
    public float UpKeep;
    public float Range;
    public float RateOfFire;
    public TowerDefinition DowngradeTower;
    public TowerDefinition[] UpgradeTowers;
    public GameObject Prefab;
    public ProjectileDefinition ProjectileDefinition;
}
