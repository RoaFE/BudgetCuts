using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelViewController m_viewController;
    [SerializeField] private int m_money;
    private int m_upkeep;
    private int m_income;
    private Spawner[] m_spawners;

    private int m_roundCounter = 1;
    // Start is called before the first frame update
    void Start()
    {
        m_spawners = FindObjectsOfType<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginRound()
    {
        if(m_money + m_income - m_upkeep < 0)
        {
            //Need to balance the budget
            return;
        }
        UpdateMoney(m_income - m_upkeep);
        foreach(Spawner spawner in m_spawners)
        {
            spawner.SpawnWave();
        }
    }

    public void UpdateMoney(int changeAmount)
    {
        m_money += changeAmount;
        m_viewController.UpdateMoneyText(m_money, changeAmount);
    }

    public void IncrementRound()
    {
        m_roundCounter++;
    }
}
