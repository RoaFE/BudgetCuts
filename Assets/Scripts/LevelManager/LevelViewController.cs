using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelViewController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_moneyText;
    [SerializeField] private TextMeshProUGUI m_moneyChangedAmountText;
    [SerializeField] private TextMeshProUGUI m_moneyChangeText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateMoneyText(int money, int changeAmount)
    {
        m_moneyText.SetText($"{money}");
    }

    public void UpdateMoneyChangeText(int changeAmount)
    {
        string changeSymbol = (changeAmount < 0) ? "-" : "+";
        m_moneyChangedAmountText.SetText($"{changeSymbol}{Mathf.Abs(changeAmount)}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
