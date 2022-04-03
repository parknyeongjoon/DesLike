using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardPanel : MonoBehaviour
{
    [SerializeField]
    GameObject rewardSoldierBtnPrefab, rewardRelicBtnPrefab;
    Map map;

    public void SetRewardPanel()
    {
        map = SaveManager.Instance.gameData.map;
        //선택지 가능하게 만들기, 각 버튼들 enable로 옮기기
        GameObject createBtn;
        Reward reward = map.curMapNode.reward;
        //soldierBtn
        for(int i = 0; i < 2; i++)//선택지 추가 옵션 넣기
        {
            createBtn = Instantiate(rewardSoldierBtnPrefab, transform);
            createBtn.GetComponent<SoldierRewardBtn>().soldierCode = reward.soldierReward.soldier[i].code;
            createBtn.transform.GetChild(0).GetComponent<TMP_Text>().text = reward.soldierReward.soldier[i].soldier_name.ToString();
            createBtn.transform.GetChild(1).GetComponent<Image>().sprite = reward.soldierReward.soldier[i].sprite;
        }
        
        //relicBtn
        if(reward.relic != null)
        {
            RelicData relicData = reward.relic.GetComponent<Relic>().relicData;
            createBtn = Instantiate(rewardRelicBtnPrefab, transform);
            createBtn.GetComponent<RelicRewardBtn>().relic = reward.relic;
            createBtn.transform.GetChild(0).GetComponent<TMP_Text>().text = relicData.relicName.ToString();
            createBtn.transform.GetChild(1).GetComponent<Image>().sprite = relicData.relicImg;
        }
    }
}
