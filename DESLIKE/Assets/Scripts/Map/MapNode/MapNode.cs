using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapNode", menuName = "ScriptableObject/MapNode")]
public class MapNode : ScriptableObject
{
    int rand;
    public Kingdom kingdom;
    public Map map;
    public bool isNode = false;
    public List<SoldierData> ableSoldierRewards = new List<SoldierData>();    // 위 3개를 조합해서 나오는 리워드 리스트
    // public Dictionary<string, Relic> ableRelicRewards = new Dictionary<string, Relic>();
    public Reward reward;
    public int[] relicLevelCount = new int[3];

    public void SetAbleReward() // 병사만 설정
    {
        kingdom = map.kingdom;
        int comNorSolC, comEpicSolC;
        
        int kingdomNorSolC, kingdomEpicSolC;
        int sum;

        if (kingdom == Kingdom.Physic)  // ableSoldierReward 세팅
        {
            kingdomNorSolC = map.physicNorSol.Count;
            kingdomEpicSolC = map.physicEpicSol.Count;
            sum = kingdomNorSolC + kingdomEpicSolC;
            if (ableSoldierRewards.Count < sum)
            {
                for (int i = 0; i < kingdomNorSolC; i++)
                    ableSoldierRewards.Add(map.physicNorSol[i]);    // 국가별 일반 유닛 추가
                for (int i = kingdomNorSolC; i < sum; i++)
                    ableSoldierRewards.Add(map.physicEpicSol[i - kingdomNorSolC]);    // 국가별 희귀 유닛 추가
            }
        }
        else if (kingdom == Kingdom.Spell)
        {
            kingdomNorSolC = map.spellNorSol.Count;
            kingdomEpicSolC = map.spellEpicSol.Count;
            sum = kingdomNorSolC + kingdomEpicSolC;
            if (ableSoldierRewards.Count < sum)
            {
                for (int i = 0; i < kingdomNorSolC; i++)
                    ableSoldierRewards.Add(map.spellNorSol[i]);    // 국가별 일반 유닛 추가
                for (int i = kingdomNorSolC; i < sum; i++)
                    ableSoldierRewards.Add(map.spellEpicSol[i - kingdomNorSolC]);    // 국가별 희귀 유닛 추가
            }
        }
    }

    public void ClearReward()
    {
        reward.soldierReward.Clear();
        reward.relicReward.Clear();
    }

    public string SetNorRel()  // 일반 유물 랜덤 생성 후 코드 전달
    {
        int norRelCount;
        kingdom = map.kingdom;

        if (kingdom == Kingdom.Physic)   // 무투국 일반 유물 세팅
        {
            norRelCount = map.physicNorRel.Count + map.commonNorRel.Count;  // 총 일반 유물의 갯수

        reroll:
            rand = Random.Range(0, norRelCount-1);  // 국가 유물 vs 공통 유물 중 어떤 걸로 할지
            if(rand < map.physicNorRel.Count)   // 국가 유물이라면
            {
                rand = Random.Range(0, map.physicNorRel.Count); // 국가 유물 내 랜덤값
                if (reward.relicReward.ContainsKey("PNR" + rand))   // 가지고 있는 유물인지
                {
                    InfiniteLoopDetector.Run();
                    goto reroll;
                }
                return ("PNR" + rand);
            }
            else // 일반 유물
            {
                rand = Random.Range(0, map.commonNorRel.Count);
                if (reward.relicReward.ContainsKey("CNR" + rand))   // 가지고 있는 유물인지
                {
                    InfiniteLoopDetector.Run();
                    goto reroll;
                }
                return ("CNR" + rand);
            }
        }
        else
        {
            norRelCount = map.spellNorRel.Count + map.commonNorRel.Count;  // 총 일반 유물의 갯수

        reroll:
            rand = Random.Range(0, norRelCount - 1);  // 국가 유물 vs 공통 유물 중 어떤 걸로 할지
            if (rand < map.spellNorRel.Count)   // 국가 유물이라면
            {
                rand = Random.Range(0, map.spellNorRel.Count);  // 국가 유물 내 랜덤값
                if (reward.relicReward.ContainsKey("SNR" + rand))   // 가지고 있는 유물인지
                {
                    InfiniteLoopDetector.Run();
                    goto reroll;
                }
                return ("SNR" + rand);
            }
            else // 일반 유물
            {
                rand = Random.Range(0, map.commonNorRel.Count);
                if (reward.relicReward.ContainsKey("CNR" + rand))   // 가지고 있는 유물인지
                {
                    InfiniteLoopDetector.Run();
                    goto reroll;
                }
                return ("CNR" + rand);
            }
        }
    }

    public string SetEpicRel()  // 일반 유물 랜덤 생성 후 코드 전달
    {
        int epicRelCount;
        kingdom = map.kingdom;

        if (kingdom == Kingdom.Physic)   // 무투국 일반 유물 세팅
        {
            epicRelCount = map.physicEpicRel.Count + map.commonEpicRel.Count;  // 총 일반 유물의 갯수

        reroll:
            rand = Random.Range(0, epicRelCount - 1);  // 국가 유물 vs 공통 유물 중 어떤 걸로 할지
            if (rand < map.physicNorRel.Count)   // 국가 유물이라면
            {
                rand = Random.Range(0, map.physicEpicRel.Count); // 국가 유물 내 랜덤값
                if (reward.relicReward.ContainsKey("PER" + rand))   // 가지고 있는 유물인지
                {
                    InfiniteLoopDetector.Run();
                    goto reroll;
                }
                return ("PER" + rand);
            }
            else // 일반 유물
            {
                rand = Random.Range(0, map.commonEpicRel.Count);
                if (reward.relicReward.ContainsKey("CER" + rand))   // 가지고 있는 유물인지
                {
                    InfiniteLoopDetector.Run();
                    goto reroll;
                }
                return ("CER" + rand);
            }
        }
        else
        {
            epicRelCount = map.spellNorRel.Count + map.commonNorRel.Count;  // 총 일반 유물의 갯수

        reroll:
            rand = Random.Range(0, epicRelCount - 1);  // 국가 유물 vs 공통 유물 중 어떤 걸로 할지
            if (rand < map.spellNorRel.Count)   // 국가 유물이라면
            {
                rand = Random.Range(0, map.spellEpicRel.Count);  // 국가 유물 내 랜덤값
                if (reward.relicReward.ContainsKey("SER" + rand))   // 가지고 있는 유물인지
                {
                    InfiniteLoopDetector.Run();
                    goto reroll;
                }
                return ("SER" + rand);
            }
            else // 일반 유물
            {
                rand = Random.Range(0, map.commonEpicRel.Count);
                if (reward.relicReward.ContainsKey("CER" + rand))   // 가지고 있는 유물인지
                {
                    InfiniteLoopDetector.Run();
                    goto reroll;
                }
                return ("CER" + rand);
            }
        }
    }

    public string SetLegRel()  // 일반 유물 랜덤 생성 후 코드 전달
    {
        int legRelCount;
        kingdom = map.kingdom;

        if (kingdom == Kingdom.Physic)   // 무투국 일반 유물 세팅
        {
            legRelCount = map.physicLegendRel.Count + map.commonLegendRel.Count;  // 총 일반 유물의 갯수

        reroll:
            rand = Random.Range(0, legRelCount - 1);  // 국가 유물 vs 공통 유물 중 어떤 걸로 할지
            if (rand < map.physicNorRel.Count)   // 국가 유물이라면
            {
                rand = Random.Range(0, map.physicLegendRel.Count); // 국가 유물 내 랜덤값
                if (reward.relicReward.ContainsKey("PLR" + rand))   // 가지고 있는 유물인지
                {
                    InfiniteLoopDetector.Run();
                    goto reroll;
                }
                return ("PLR" + rand);
            }
            else // 일반 유물
            {
                rand = Random.Range(0, map.commonEpicRel.Count);
                if (reward.relicReward.ContainsKey("CLR" + rand))   // 가지고 있는 유물인지
                {
                    InfiniteLoopDetector.Run();
                    goto reroll;
                }
                return ("CLR" + rand);
            }
        }
        else
        {
            legRelCount = map.spellNorRel.Count + map.commonNorRel.Count;  // 총 일반 유물의 갯수

        reroll:
            rand = Random.Range(0, legRelCount - 1);  // 국가 유물 vs 공통 유물 중 어떤 걸로 할지
            if (rand < map.spellNorRel.Count)   // 국가 유물이라면
            {
                rand = Random.Range(0, map.spellEpicRel.Count);  // 국가 유물 내 랜덤값
                if (reward.relicReward.ContainsKey("SLR" + rand))   // 가지고 있는 유물인지
                {
                    InfiniteLoopDetector.Run();
                    goto reroll;
                }
                return ("SLR" + rand);
            }
            else // 일반 유물
            {
                rand = Random.Range(0, map.commonLegendRel.Count);
                if (reward.relicReward.ContainsKey("CLR" + rand))   // 가지고 있는 유물인지
                {
                    InfiniteLoopDetector.Run();
                    goto reroll;
                }
                return ("CLR" + rand);
            }
        }
    }
}