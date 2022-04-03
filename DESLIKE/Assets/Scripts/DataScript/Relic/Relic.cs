using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Relic : MonoBehaviour
{
    public RelicData relicData;
    [SerializeField]
    GameObject toolTipPanel;
    [SerializeField]
    Text toolTip;
    [SerializeField]
    Image relicImg;

    void Start()
    {
        relicImg.sprite = relicData.relicImg;
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

    public IEnumerator ConditionEffect()//Relic이 발동될 조건이면 아이콘 깜빡거리게
    {
        float time = 0;

        while (true)
        {
            if (time < 0.5f)
            {
                relicImg.color = new Color(1, 1, 1, 1 - time);
            }
            else
            {
                relicImg.color = new Color(1, 1, 1, time);
                if (time > 1.0f)
                {
                    break;
                }
            }
            time += Time.deltaTime;
            yield return null;
        }
    }
}
