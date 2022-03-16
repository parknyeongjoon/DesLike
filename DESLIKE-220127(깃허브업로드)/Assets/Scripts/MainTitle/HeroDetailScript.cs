using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroDetailScript : MonoBehaviour
{
    public Image Hero_Img;
    [SerializeField]
    Button Start_Btn;
    [SerializeField]
    GameObject previewPrefab;
    public GameObject optionPreviewPanel;
    public GameObject heroDetailPanel;
    public GameObject campRelicPreviewPanel;

    public void Close_HeroDetail_Panel()
    {
        heroDetailPanel.SetActive(false);
    }

    public void CreateOptionPreview(Option option)
    {
        GameObject createPrefab;
        createPrefab = Instantiate(previewPrefab, optionPreviewPanel.transform);
        createPrefab.GetComponentInChildren<Image>().sprite = option.soldierData.sprite;
        createPrefab.GetComponentInChildren<Text>().text = option.portNum.Length.ToString();
    }

    public void ShowCampRelic(GameObject campRelic)
    {
        if (campRelic)
        {
            GameObject createPrefab;
            RelicData relicData = campRelic.GetComponent<Relic>().relicData;
            createPrefab = Instantiate(previewPrefab, campRelicPreviewPanel.transform);
            createPrefab.GetComponent<Image>().sprite = relicData.relicImg;
        }
    }
}
