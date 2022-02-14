using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardPanel : MonoBehaviour
{
    [SerializeField]
    GameObject rewardSoldierBtnPrefab, rewardRelicBtnPrefab;
    [SerializeField]
    GameObject rewardPanel;
    Map map;

    public void SetRewardPanel()
    {
        map = SaveManager.Instance.gameData.map;
        //선택지 가능하게 만들기, 각 버튼들 enable로 옮기기
        GameObject createBtn;
        Reward reward = map.curMapNode.reward;
        //soldierBtn
        createBtn = Instantiate(rewardSoldierBtnPrefab, rewardPanel.transform);
        createBtn.GetComponent<SoldierRewardBtn>().soldierCode = reward.soldierReward.soldier.code;
        createBtn.GetComponent<SoldierRewardBtn>().soldierRemain = reward.soldierReward.remain;
        createBtn.transform.GetChild(0).GetComponent<Text>().text = reward.soldierReward.soldier.soldier_name.ToString();
        createBtn.transform.GetChild(1).GetComponent<Image>().sprite = reward.soldierReward.soldier.sprite;
        //relicBtn
        RelicData relicData = reward.relic.GetComponent<Relic>().relicData;
        createBtn = Instantiate(rewardRelicBtnPrefab, rewardPanel.transform);
        createBtn.GetComponent<RelicRewardBtn>().relic = reward.relic;
        createBtn.transform.GetChild(0).GetComponent<Text>().text = relicData.relicName.ToString();
        createBtn.transform.GetChild(1).GetComponent<Image>().sprite = relicData.relicImg;
    }
}
