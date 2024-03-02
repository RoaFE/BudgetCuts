using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveEntry", menuName = "BudgetDefence/Wave")]
public class Wave : ScriptableObject
{
    [SerializeField] public WaveEntry[] WaveEntries;
}


[System.Serializable]
public class WaveEntry
{
    public EnemyDefinition Enemy;
    public int Count;
}
