using UnityEngine;
//enum 모음
public enum Mouse_State { Idle, Target, Grenade }

public enum Team { Ally = 1, Enemy = 2, Neutral = 4 }//soldierData로 옮기기?
public enum AttackArea { Ground = 1 << 8, Sky = 1 << 11, Dual = AttackArea.Ground + AttackArea.Sky }//공격 범위
public enum BodyArea { Ground = 1 << 8, Sky = 1 << 11 }
public enum Soldier_State { Idle, Detect, Siege, Battle, Dead}//유닛들 상태
public enum Soldier_Action { Start_Delay, Attack, Skill, End_Delay, Stun }
public enum Atk_Type { 무속성 = 1, 물리 = 2, 마법 = 4, 공허 = 8}
public enum Def_Type { 가죽 = 1, 천 = 2, 판금 = 4}
public enum Soldier_Type { Tanker, Soldier, Healer, Ranger, Catapult, Flight, Monster}
public enum Tribe { Undead, Mech, Ghost }
public enum Rarity { normal, rare, epic, unique, legend }

public enum SkillType { targetSkill, grenadeSkill, passiveSkill}

[System.Serializable]
public class Reward
{
    public Reward()
    {

    }
    public SoldierData soldierData;
    public GameObject relic;
    public int gold;
    public int scrap;
    public int magicalStone;
    public int soldierRemain;
}

[System.Serializable]
public struct Option
{
    public SoldierData soldierData;
    public int[] portNum;
    public GameObject mutant;
}