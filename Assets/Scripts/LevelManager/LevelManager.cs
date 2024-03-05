using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] private LevelViewController m_viewController;
    [SerializeField] private int m_money;
    private int m_upkeep;
    private int m_income;
    private Spawner[] m_spawners;

    [SerializeField] private int[] m_incomeAmounts;
    [SerializeField] private int m_maxRounds = 5;
    private int m_roundCounter = 1;

    [SerializeField]
    private int m_enemiesInWave = 0;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        m_income = m_incomeAmounts[m_roundCounter - 1];
        UpdateChangeAmount();
        m_spawners = FindObjectsOfType<Spawner>();
        m_viewController.UpdateMoneyText(m_money, 0);
        m_viewController.BeginRound.onClick.AddListener(BeginRound);
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
        foreach(Spawner spawner in m_spawners)
        {
            m_enemiesInWave += spawner.SpawnWave(m_roundCounter - 1);
        }
    }

    public void EnemyDestroyed()
    {
        m_enemiesInWave--;
        if(m_enemiesInWave == 0)
        {
            EndRound();
        }
    }

    public void EndRound()
    {
        m_viewController.SetRoundButtonActive(true);
        UpdateMoney(m_income - m_upkeep);
        if (m_roundCounter == m_maxRounds)
            EndGame();
        //Change income amount
        IncrementRound();
        
    }

    private void EndGame()
    {
        throw new NotImplementedException();
    }

    public void UpdateMoney(int changeAmount)
    {
        m_money += changeAmount;
        m_viewController.UpdateMoneyText(m_money, changeAmount);
    }

    public void IncrementRound()
    {
        m_roundCounter++;
        m_income = m_incomeAmounts[m_roundCounter - 1];
        UpdateChangeAmount();
    }

    private void UpdateChangeAmount()
    {
        m_viewController.UpdateMoneyChangeText(m_income - m_upkeep);
    }
}
