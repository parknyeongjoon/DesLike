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

    void Start()
    {
        relicScript = SaveManager.Instance.map.curMapNode.reward.relicReward(0).GetComponent<Relic>();
        relicData = relicScript.relicData;
        //toolTipPanel.SetActive(false);
        transform.GetChild(0).GetComponent<TMP_Text>().text = relicData.relicName.ToString();
        transform.GetChild(1).GetComponent<Image>().sprite = relicData.relicImg;
    }

    public void GetReward()
    {
        RelicManager.Instance.relicList.Add(relicScript);
        RelicManager.Instance.AddRelicInCanvas();
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
