using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerMenuViewController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_titleText;
    [SerializeField]
    private Transform m_upgradeParent;
    [SerializeField]
    private Button m_close;
    [SerializeField]
    private List<Button> m_upgradeButtons;
    [SerializeField]
    private GameObject m_upgradePrefab;
    [SerializeField]
    private Button m_downgrade;

    private void Start() {
        m_close.onClick.AddListener(Disable);
        gameObject.SetActive(false);
    }

    public void Enable(Tower tower)
    {
        gameObject.SetActive(true);
        m_titleText.SetText($"{tower.TowerDefinition.Name}- Upkeep:{tower.TowerDefinition.UpKeep}");
        m_downgrade.gameObject.SetActive(tower.TowerDefinition.DowngradeTower != null);
        m_downgrade.onClick.AddListener(tower.OnDowngrade);
        m_upgradeButtons = new List<Button>(tower.TowerDefinition.UpgradeTowers.Length);
        for(int i = 0; i < tower.TowerDefinition.UpgradeTowers.Length; i++)
        {
            Button button = Instantiate(m_upgradePrefab, m_upgradeParent).GetComponent<Button>();
            AssignUpgradeCallback(button, i, tower);
            button.interactable = LevelManager.Instance.CanBuy(tower.TowerDefinition.UpgradeTowers[i].Cost);
            string buttonText = $"{tower.TowerDefinition.UpgradeTowers[i].Name} : {tower.TowerDefinition.UpgradeTowers[i].Cost}";
            m_upgradeButtons.Add(button);
            button.GetComponentInChildren<TextMeshProUGUI>()?.SetText(buttonText);
        }
    }

    private void AssignUpgradeCallback(Button button , int i, Tower tower)
    {
        button.onClick.AddListener(() => tower.OnUpgrade(i));
    }

    
    public void Disable()
    {
        gameObject.SetActive(false);
        m_downgrade.onClick.RemoveAllListeners();
        if (m_upgradeButtons != null)
        {
            foreach (Button button in m_upgradeButtons)
            {
                Destroy(button.gameObject);
            }
            m_upgradeButtons.Clear();
        }
        m_upgradeButtons = null;
    }
}
