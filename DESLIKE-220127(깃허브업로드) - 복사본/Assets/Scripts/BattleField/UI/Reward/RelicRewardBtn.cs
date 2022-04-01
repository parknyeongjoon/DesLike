using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicRewardBtn : MonoBehaviour
{
    RelicManager relicManager;
    BattleUIManager battleUIManager;

    public GameObject relic;

    [SerializeField]
    Button button;
    [SerializeField]
    GameObject toolTipPanel;
    [SerializeField]
    Text toolTip;

    RelicData relicData;
    Relic relicScript;

    void Start()
    {
        relicManager = RelicManager.Instance;
        relicScript = relic.GetComponent<Relic>();
        relicData = relicScript.relicData;
        toolTipPanel.SetActive(false);
    }

    public void GetReward()
    {
        relicManager.relicList.Add(relicScript);
        Instantiate(relic, relicManager.relicCanvas.transform.GetChild(0).transform);
        button.interactable = false;
    }

    public void SetToolTip()
    {
        toolTipPanel.SetActive(true);
        toolTip.text = relicData.toopTip;
    }

    public void CloseToolTip()
    {
        toolTipPanel.SetActive(false);
    }
}
