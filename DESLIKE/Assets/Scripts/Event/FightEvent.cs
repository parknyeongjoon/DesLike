using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FightEvent : EventBasic
{
    public BattleNode battleNode;
    int enPortLevel;
    int phyNorRelC, speNorRelC, comNorRelC, phyEpicRelC, speEpicRelC, comEpicRelC;

    public void EBattleSet()
    {
        DataSet();
        LevelSet();
        RewardSet();
    }

    void DataSet()
    {
        phyNorRelC = map.physicNorRel.Count;
        speNorRelC = map.spellNorRel.Count;
        comNorRelC = map.commonNorRel.Count;
        phyEpicRelC = map.physicEpicRel.Count;
        speEpicRelC = map.spellEpicRel.Count;
        comEpicRelC = map.commonEpicRel.Count;
    }

    void LevelSet()
    {
        switch (curStage)    // 총 6단계
        {
            case 0:
                if (curDay <= 15) enPortLevel = 0;
                else enPortLevel = 1;
                break;
            case 1:
                if (curDay <= 15) enPortLevel = 2;
                else enPortLevel = 3;
                break;
            case 2:
                if (curDay <= 15) enPortLevel = 4;
                else enPortLevel = 5;
                break;
        }
        battleNode.enemyPortOption = battleNode.enemyPortsOptions[enPortLevel];
    }

    void RewardSet()    // 희귀 유물 보상만 
    {
        battleNode.SetAbleReward();

        int norTotal, epicTotal;
        if (battleNode.kingdom == Kingdom.Physic)
        {
            norTotal = phyNorRelC + comNorRelC;
            epicTotal = phyEpicRelC + comEpicRelC;
        }
        else
        {
            norTotal = speNorRelC + comNorRelC;
            epicTotal = speEpicRelC + comEpicRelC;
        }
    reroll:
        int rand = Random.Range(0, epicTotal) + norTotal;
        for (int i = 0; i < RelicManager.instance.relicList.Count; i++)
        {
            if (battleNode.ableRelicRewards[rand].relicData.code == RelicManager.instance.relicList[i].relicData.code)
                goto reroll;
        }   // 기존 가지고 있는 유물이면 리롤
        battleNode.reward.relic.Add(battleNode.ableRelicRewards[rand]);
    }

    public void BattleBtn() // 3일 소모, 전투 시작
    {
        curDay += 3;
        CurDaySave();
        battleNode.Play_BattleNode();
    }

    public void ThroughButton() // 1일 소모, 전투X
    {
        curDay += 1;
        for(int i = 0; i<2; i++)
            Buttons[i].gameObject.SetActive(false);
        EndButton.gameObject.SetActive(true);
    }
}
