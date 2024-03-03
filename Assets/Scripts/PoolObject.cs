using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "PoolObject", menuName = "BudgetDefence/PoolObject", order = 0)]
public class PoolObject : ScriptableObject
{
    public string tag;
    public GameObject prefab;
    public int count;
}