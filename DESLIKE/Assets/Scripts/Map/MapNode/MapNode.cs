using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapNode", menuName = "ScriptableObject/MapNode")]
public class MapNode : ScriptableObject
{
    public Map map;
    public bool isNode = false;
    public List<SoldierData> ableSoldierRewards = new List<SoldierData>();    // 위 3개를 조합해서 나오는 리워드 리스트
    public Dictionary<string, RelicData> ableRelicRewards = new Dictionary<string, RelicData>();
    public Kingdom kingdom;
    public Reward reward;
    public int[] relicLevelCount = new int[3];
    
    public void SetAbleReward()
    {
        kingdom = Kingdom.Physic;   // 수정 필요
        int comNorSolC, comEpicSolC;

        if (map.commonNorSol != null) comNorSolC = 0;
        else comNorSolC = map.commonNorSol.Count;
        if (map.commonEpicSol == null) comEpicSolC = 0;
        else comEpicSolC = map.commonEpicSol.Count;

        int comNorRelC = map.commonNorRel.Count;
        int comEpicRelC = map.commonEpicRel.Count;
        int comLegendRelC = map.commonLegendRel.Count;

        int kingdomNorSolC, kingdomEpicSolC, kingdomNorRelC, kingdomEpicRelC, kingdomLegendRelC;
        int sum1, sum2, sum3, sum4, sum5;

        if (kingdom == Kingdom.Physic)  // ableSoldierReward 세팅
        {
            kingdomNorSolC = map.physicNorSol.Count;
            kingdomEpicSolC = map.physicEpicSol.Count;
            sum1 = kingdomNorSolC + comNorSolC;
            sum2 = sum1 + kingdomEpicSolC;
            sum3 = sum2 + comEpicSolC;
            if (ableSoldierRewards.Count < sum3)
            {
                for (int i = 0; i < kingdomNorSolC; i++)
                    ableSoldierRewards.Add(map.physicNorSol[i]);    // 국가별 일반 유닛 추가
                for (int i = kingdomNorSolC; i < sum1; i++)
                    ableSoldierRewards.Add(map.commonNorSol[i - kingdomNorSolC]); // 공통 일반 유닛 추가
                for (int i = sum1; i < sum2; i++)
                    ableSoldierRewards.Add(map.physicEpicSol[i - sum1]);    // 국가별 희귀 유닛 추가
                for (int i = sum2; i < sum3; i++)
                    ableSoldierRewards.Add(map.commonEpicSol[i - sum2]);    // 공통 희귀 유닛 추가
            }
        }
        else if (kingdom == Kingdom.Spell)
        {
            kingdomNorSolC = map.spellNorSol.Count;
            kingdomEpicSolC = map.spellEpicSol.Count;
            sum1 = kingdomNorSolC + comNorSolC;
            sum2 = sum1 + kingdomEpicSolC;
            sum3 = sum2 + comEpicSolC;
            if (ableSoldierRewards.Count < sum3)
            {
                for (int i = 0; i < kingdomNorSolC; i++)
                    ableSoldierRewards.Add(map.spellNorSol[i]);    // 국가별 일반 유닛 추가
                for (int i = kingdomNorSolC; i < sum1; i++)
                    ableSoldierRewards.Add(map.commonNorSol[i - kingdomNorSolC]); // 공통 일반 유닛 추가
                for (int i = sum1; i < sum2; i++)
                    ableSoldierRewards.Add(map.spellEpicSol[i - sum1]);    // 국가별 희귀 유닛 추가
                for (int i = sum2; i < sum3; i++)
                    ableSoldierRewards.Add(map.commonEpicSol[i - sum2]);    // 공통 희귀 유닛 추가
            }
        }

        ableRelicRewards = new Dictionary<string, RelicData>();
        if (kingdom == Kingdom.Physic)  // ableRelicReward 세팅
        {
            kingdomNorRelC = map.physicNorRel.Count;
            kingdomEpicRelC = map.physicEpicRel.Count;
            kingdomLegendRelC = map.physicLegendRel.Count;

            sum1 = kingdomNorRelC + comNorRelC;
            sum2 = sum1 + kingdomEpicRelC;
            sum3 = sum2 + comEpicRelC;
            sum4 = sum3 + kingdomLegendRelC;
            sum5 = sum4 + comLegendRelC;

            relicLevelCount[0] = kingdomNorRelC + comNorRelC;
            relicLevelCount[1] = kingdomEpicRelC + comEpicRelC;
            relicLevelCount[2] = kingdomLegendRelC + comLegendRelC;
            
            if (ableRelicRewards.Count < sum3)
            {
                for (int i = 0; i < kingdomNorRelC; i++)
                    ableRelicRewards.Add(map.physicNorRel[i]);    // 국가별 일반 유물 추가
                for (int i = kingdomNorRelC; i < sum1; i++)
                    ableRelicRewards.Add(map.commonNorRel[i - kingdomNorRelC]); // 공통 일반 유물 추가
                for (int i = sum1; i < sum2; i++)
                    ableRelicRewards.Add(map.physicEpicRel[i - sum1]);    // 국가별 희귀 유물 추가
                for (int i = sum2; i < sum3; i++)
                    ableRelicRewards.Add(map.commonEpicRel[i - sum2]);    // 공통 희귀 유물 추가
                for (int i = sum3; i < sum4; i++)
                    ableRelicRewards.Add(map.physicLegendRel[i - sum3]);    // 공통 전설 유물 추가
                for (int i = sum4; i < sum5; i++)
                    ableRelicRewards.Add(map.commonLegendRel[i - sum4]);    // 공통 전설 유물 추가
            }
        }
        else if (kingdom == Kingdom.Spell)
        {
            kingdomNorRelC = map.spellNorRel.Count;
            kingdomEpicRelC = map.spellEpicRel.Count;
            kingdomLegendRelC = map.spellLegendRel.Count;

            sum1 = kingdomNorRelC + comNorRelC;
            sum2 = sum1 + kingdomEpicRelC;
            sum3 = sum2 + comEpicRelC;
            sum4 = sum3 + kingdomLegendRelC;
            sum5 = sum4 + comLegendRelC;

            relicLevelCount[0] = kingdomNorRelC + comNorRelC;
            relicLevelCount[1] = kingdomEpicRelC + comEpicRelC;
            relicLevelCount[2] = kingdomLegendRelC + comLegendRelC;

            if (ableRelicRewards.Count < sum3)
            {

                for (int i = 0; i < kingdomNorRelC; i++)
                    ableRelicRewards.Add(map.spellNorRel[i]);    // 국가별 일반 유물 추가
                for (int i = kingdomNorRelC; i < sum1; i++)
                    ableRelicRewards.Add(map.commonNorRel[i - kingdomNorRelC]); // 공통 일반 유물 추가
                for (int i = sum1; i < sum2; i++)
                    ableRelicRewards.Add(map.spellEpicRel[i - sum1]);    // 국가별 희귀 유물 추가
                for (int i = sum2; i < sum3; i++)
                    ableRelicRewards.Add(map.commonEpicRel[i - sum2]);    // 공통 희귀 유물 추가
                for (int i = sum3; i < sum4; i++)
                    ableRelicRewards.Add(map.spellLegendRel[i - sum3]);    // 공통 희귀 유물 추가
                for (int i = sum4; i < sum5; i++)
                    ableRelicRewards.Add(map.commonLegendRel[i - sum4]);    // 공통 희귀 유물 추가
            }
        }
    }
}