using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeroSkillUse: MonoBehaviour
{
    MouseManager mouseManager;

    [SerializeField]
    HeroInfo heroInfo;

    GameObject skillFocus;
    [SerializeField]
    GameObject skillRange;

    public Skill[] skillScripts = new Skill[3];

    Coroutine skillCoroutine;

    void Start()
    {
        mouseManager = MouseManager.Instance;
        SetSkillHandler();
    }

    void Update()
    {
        Skill1();
        SKill2();
        SKill3();
        StopSkillCoroutine();
    }

    void SetSkillHandler()
    {
        skillScripts = GetComponents<Skill>();
    }

    void Skill1()
    {
        if (Input.GetKeyDown(KeyCode.Z) && skillScripts[0])
        {
            if (CheckMpNCool(skillScripts[0]))
            {
                skillCoroutine = StartCoroutine(UseSkill(skillScripts[0]));
            }
        }
    }

    void SKill2()
    {
        if (Input.GetKeyDown(KeyCode.X) && skillScripts[1])
        {
            if (CheckMpNCool(skillScripts[1]))
            {
                skillCoroutine = StartCoroutine(UseSkill(skillScripts[1]));
            }
        }
    }

    void SKill3()
    {
        if (Input.GetKeyDown(KeyCode.C) && skillScripts[2])
        {
            if (CheckMpNCool(skillScripts[2]))
            {
                skillCoroutine = StartCoroutine(UseSkill(skillScripts[2]));
            }
        }
    }

    bool CheckMpNCool(Skill skillScript)
    {
        if (skillScript as ActiveSkill)
        {
            if(((ActiveSkill)skillScript).cur_cooltime > 0)
            {
                Debug.Log("쿨타임");
                return false;
            }
            else if(heroInfo.cur_Mp <= ((ActiveSkillData)skillScript.skillData).mp)
            {
                Debug.Log("마나 부족");
                return false;
            }
            else
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator UseSkill(Skill skillScript)
    {
        heroInfo.targetInfo = null;
        if(skillScript.skillData.skillType == SkillType.InstanceSkill)
        {
            skillCoroutine = StartCoroutine(skillScript.UseSkill(heroInfo));
        }
        else if (skillScript as ActiveSkill)
        {
            if (skillScript.skillData.skillType == SkillType.TargetSkill)
            {
                mouseManager.mouseState = Mouse_State.Target;
            }
            else if (skillScript.skillData.skillType == SkillType.GrenadeSkill)
            {
                mouseManager.mouseState = Mouse_State.Grenade;
                mouseManager.grenadeExtent.transform.localScale = new Vector2(((GrenadeAttackData)skillScript.skillData).extent, ((GrenadeAttackData)skillScript.skillData).extent);
            }
            skillRange.transform.localScale = new Vector2(((ActiveSkillData)skillScript.skillData).range, ((ActiveSkillData)skillScript.skillData).range);
            yield return StartCoroutine(SetTarget());
            yield return MoveToSkill(heroInfo.targetInfo, ((ActiveSkillData)skillScript.skillData).range);
            skillCoroutine = StartCoroutine(skillScript.UseSkill((HeroInfo)heroInfo.targetInfo));
        }
    }

    IEnumerator SetTarget()
    {
        MouseManager.Instance.SetAimCursorTexture();
        skillRange.SetActive(true);
        Collider2D hit;
        while (mouseManager.mouseState != Mouse_State.Idle)
        {
            if (mouseManager.mouseState == Mouse_State.Grenade && Input.GetKey(KeyCode.LeftAlt))
            {
                mouseManager.grenadeExtent.transform.parent = mouseManager.transform;
                mouseManager.grenadeExtent.SetActive(true);
                while (Input.GetKey(KeyCode.LeftAlt))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        mouseManager.skillPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        Set_Idle();
                        break;
                    }
                    yield return null;
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                hit = MouseManager.Instance.CastRay();
                if (hit != null)
                {
                    if (hit.gameObject.layer == 9 && hit.gameObject.tag != "Castle")
                    {
                        ActiveSkillFocus(hit.gameObject);
                        heroInfo.targetInfo = hit.gameObject.GetComponent<CastleInfo>();
                        Set_Idle();
                        break;
                    }
                }
            }
            yield return null;
        }
    }

    IEnumerator MoveToSkill(CastleInfo targetInfo, float range)
    {
        Vector3 destination;
        if (targetInfo)//타켓이 있다면 타켓쪽으로 없다면 논타켓 위치로
        {
            destination = targetInfo.transform.position;
            while(!heroInfo.TargetCheck(range - targetInfo.castleData.size))
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * ((HeroData)heroInfo.castleData).speed);
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            destination = mouseManager.skillPos;
            while (Vector3.Distance(transform.position, destination) > range)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * ((HeroData)heroInfo.castleData).speed);
                yield return new WaitForFixedUpdate();
            }
        }
    }

    void ActiveSkillFocus(GameObject gameObject)
    {
        if (skillFocus != null)
        {
            skillFocus.SetActive(false);
        }
        skillFocus = gameObject.transform.Find("skillFocus").gameObject;
        skillFocus.SetActive(true);
    }

    void StopSkillCoroutine()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape) && heroInfo.action != Soldier_Action.End_Delay)
        {
            if (skillCoroutine != null)
            {
                StopCoroutine(skillCoroutine);
                heroInfo.targetInfo = null;
                heroInfo.target = null;
                if (skillFocus != null)
                {
                    skillFocus.SetActive(false);
                }
                Set_Idle();
            }
        }
    }

    void Set_Idle()
    {
        mouseManager.mouseState = Mouse_State.Idle;
        mouseManager.grenadeExtent.SetActive(false);
        skillRange.SetActive(false);
        mouseManager.SetIdleCursorTexture();
    }
}