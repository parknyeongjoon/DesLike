using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//enum 모음
public enum Mouse_State { Idle, Target, Grenade }
public enum Port_State { Idle, SetSoldier, SetMutant, Drag, Sell }

public enum Team { Ally = 1, Enemy = 2, Neutral = 4 }
public enum AttackArea { None = 0, Ground = 1 << 8, Sky = 1 << 11, Dual = AttackArea.Ground + AttackArea.Sky }//공격 범위
public enum BodyArea { Ground = 1 << 8, Sky = 1 << 11 }
public enum Soldier_State { Idle, Detect, Stun, Battle, Charge, Taunt, Dead }//유닛들 상태
public enum AnimState { Idle, Move, Atk }
public enum Soldier_Action { Idle, Move, Attack, Skill, End_Delay }
public enum Soldier_Type { Tanker, Soldier, Healer, Buffer, Debuffer, Ranger, Catapult, Flight, Monster}
public enum Kingdom { Common, Physic, Spell}
public enum Tribe { Bear, Kangaroo, Rat, Frog, Undead, Mech, Ghost }
public enum Rarity { Normal, Rare, Epic }

public enum SkillType { TargetSkill, GrenadeSkill, InstanceSkill, PassiveSkill, Etc}

[System.Serializable]
public class Buff_Stat
{
    public Buff_Stat()
    {
        this.speed = 0;
        this.mp = 0;
        this.mp_Re = 0;
        this.atk = 0;
        this.atk_Speed = 0;
        this.hp = 0;
        this.hp_Re = 0;
        this.def = 0;
    }

    bool isPercent;

    public float speed;
    public float mp;
    public float mp_Re;
    public float atk;
    public float atk_Speed;
    public float hp;
    public float hp_Re;
    public float def;

    public void Add_Stat(Buff_Stat buff_Stat)
    {
        this.speed += buff_Stat.speed;
        this.mp += buff_Stat.mp;
        this.mp_Re += buff_Stat.mp_Re;
        this.atk += buff_Stat.atk;
        this.atk_Speed += buff_Stat.atk_Speed;
        this.hp += buff_Stat.hp;
        this.hp_Re += buff_Stat.hp_Re;
        this.def += buff_Stat.def;
    }

    public void Remove_Stat(Buff_Stat buff_Stat)
    {
        this.speed -= buff_Stat.speed;
        this.mp -= buff_Stat.mp;
        this.mp_Re -= buff_Stat.mp_Re;
        this.atk -= buff_Stat.atk;
        this.atk_Speed -= buff_Stat.atk_Speed;
        this.hp -= buff_Stat.hp;
        this.hp_Re -= buff_Stat.hp_Re;
        this.def -= buff_Stat.def;
    }

    public void Percent_Stat(HeroData heroData)
    {
        this.speed *= heroData.speed;
        this.mp *= heroData.mp;
        this.mp_Re *= heroData.mp_Re;
        //this.atk *= heroData.atk;
        //this.atk_Speed *= heroData.atk_Speed;
        this.hp *= heroData.hp;
        this.hp_Re *= heroData.hp_Re;
        this.def *= heroData.def;
    }
}

[System.Serializable]
public class Reward
{
    public List<SoldierReward> soldierReward;
    public GameObject relic;
    public int gold;//골드
    public int magicalStone;//진영화폐
}

[System.Serializable]
public struct SoldierReward //soldier랑 mutant 한 쌍으로 묶일 수 있게 만들어서 저장하기
{
    public SoldierData soldier;
    public GameObject mutant;
}

[System.Serializable]
public struct Option
{
    public SoldierData soldierData;
    public int[] portNum;
    public GameObject mutant;
}