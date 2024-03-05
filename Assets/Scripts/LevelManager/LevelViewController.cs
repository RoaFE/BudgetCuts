using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class LevelViewController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_moneyText;
    [SerializeField] private TextMeshProUGUI m_moneyChangedAmountText;
    [SerializeField] private TextMeshProUGUI m_moneyChangeText;


    [SerializeField] private Color m_positiveColour;
    [SerializeField] private Color m_negativeColour;
    [SerializeField] private Button m_beginRound;
    public Button BeginRound { get => m_beginRound;}

    // Start is called before the first frame update
    void Start()
    {
        m_beginRound.onClick.AddListener(() => SetRoundButtonActive(false));
    }

    public void UpdateMoneyText(int money, int changeAmount)
    {
        m_moneyText.SetText($"{money}");
    }

    public void UpdateMoneyChangeText(int changeAmount)
    {
        string changeSymbol = (changeAmount < 0) ? "-" : "+";
        m_moneyChangedAmountText.SetText($"{changeSymbol}{Mathf.Abs(changeAmount)}");
        m_moneyChangedAmountText.color = (changeAmount < 0) ? m_negativeColour : m_positiveColour;
    }
    
    public void SetRoundButtonActive(bool active)
    {
        m_beginRound.gameObject.SetActive(active);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
