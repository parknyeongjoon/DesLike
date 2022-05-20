using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RelicRewardBtn : MonoBehaviour
{
    [SerializeField]
    GameObject toolTipPanel;
    [SerializeField]
    TMP_Text toolTip;

    RelicData relicData;
    Relic relicScript;
    SaveManager saveManager;

    void Start()
    {
        saveManager = SaveManager.Instance;
        relicData = saveManager.dataSheet.relicDataSheet[saveManager.map.curMapNode.SetNorRel()];
        //toolTipPanel.SetActive(false);
        transform.GetChild(0).GetComponent<TMP_Text>().text = relicData.relicName;
        transform.GetChild(1).GetComponent<Image>().sprite = relicData.relicImg;
    }

    public void GetReward()
    {
        RelicManager.Instance.GetRelic(relicData.code);
        Destroy(gameObject);
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
