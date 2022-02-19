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
        if(skillScript.skillData as ActiveSkillData)
        {
            if (skillScript.skillData.skillType == SkillType.targetSkill)
            {
                mouseManager.mouseState = Mouse_State.Target;
            }
            else if (skillScript.skillData.skillType == SkillType.grenadeSkill)
            {
                mouseManager.mouseState = Mouse_State.Grenade;
                mouseManager.grenadeExtent.transform.localScale = new Vector2(((GrenadeAttackData)skillScript.skillData).extent, ((GrenadeAttackData)skillScript.skillData).extent);
            }
            skillRange.transform.localScale = new Vector2(((ActiveSkillData)skillScript.skillData).range, ((ActiveSkillData)skillScript.skillData).range);
            yield return StartCoroutine(SetTarget());
            skillCoroutine = StartCoroutine(skillScript.UseSkill((HeroInfo)heroInfo.targetInfo));
        }
    }

    IEnumerator SetTarget()
    {
        heroInfo.targetInfo = null;
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

    /*
    IEnumerator UseSingleAttack(SkillBehaviour skillBehaviour)
    {
        SingleAttackBehaviour singleAttackBehaviour = ((SingleAttackBehaviour)skillBehaviour);
        if (singleAttackBehaviour.cur_cooltime > 0)
        {
            //쿨타임 출력
        }
        else if (heroInfo.cur_Mp < singleAttackBehaviour.singleAttackSkillData.mp)
        {
            //마나 부족 출력
        }
        else
        {
            mouseManager.mouseState = MouseState.Skill;
            mouseManager.SetAimCursorTexture();
            skillRange.transform.localScale = new Vector2(singleAttackBehaviour.singleAttackSkillData.range, singleAttackBehaviour.singleAttackSkillData.range);
            skillRange.SetActive(true);
            while (mouseManager.SkillTarget == null)
            {
                yield return null;
            }
            heroInfo.target = mouseManager.SkillTarget;
            skillRange.SetActive(false);
            if (skillFocus != null)
            {
                skillFocus.SetActive(false);
            }
            skillFocus = heroInfo.target.transform.Find("skillFocus").gameObject;
            skillFocus.SetActive(true);
            mouseManager.mouseState = MouseState.Idle;
            mouseManager.SetIdleCursorTexture();
            while (mouseManager.SkillTarget != null)
            {
                if (singleAttackBehaviour.CanSkillCheck())
                {
                    WaitWhile waitWhile = new WaitWhile(() => GameManager.Instance.gamePause);
                    yield return waitWhile;
                    CastleInfo targetInfo = mouseManager.SkillTarget.GetComponent<CastleInfo>();
                    StartCoroutine(singleAttackBehaviour.SkillCoroutine(targetInfo));
                    break;
                }
                else
                {
                    heroInfo.moveDir = (heroInfo.target.transform.position - transform.position).normalized;
                    transform.Translate(Time.deltaTime * heroInfo.heroData.speed * heroInfo.moveDir);
                }
                yield return null;
            }
            skillFocus.SetActive(false);
            mouseManager.SkillTarget = null;
            heroInfo.target = null;
        }
    }

    IEnumerator UseGrenadeAttack(SkillBehaviour skillBehaviour)
    {
        GrenadeAttackBehaviour grenadeAttackBehaviour = ((GrenadeAttackBehaviour)skillBehaviour);
        if (grenadeAttackBehaviour.cur_cooltime > 0)
        {
            //쿨타임 출력
        }
        else if (heroInfo.cur_Mp < grenadeAttackBehaviour.grenadeAttackSkillData.mp)
        {
            //마나 부족 출력
        }
        else
        {
            Vector3 grenadePos;
            mouseManager.mouseState = MouseState.Grenade;
            Cursor.visible = false;
            skillRange.transform.localScale = new Vector2(grenadeAttackBehaviour.grenadeAttackSkillData.range, grenadeAttackBehaviour.grenadeAttackSkillData.range);
            skillRange.SetActive(true);
            mouseManager.grenadeExtent.SetActive(true);
            mouseManager.grenadeExtent.transform.localScale = new Vector2(grenadeAttackBehaviour.grenadeAttackSkillData.extent * 3, grenadeAttackBehaviour.grenadeAttackSkillData.extent * 3);
            while (!Input.GetMouseButtonDown(0))
            {
                yield return null;
            }
            grenadePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseManager.mouseState = MouseState.Idle;
            Cursor.visible = true;
            skillRange.SetActive(false);
            mouseManager.grenadeExtent.SetActive(false);
            while (true)
            {
                float distance = Vector2.Distance(transform.position, grenadePos);
                if (distance < grenadeAttackBehaviour.grenadeAttackSkillData.range)
                {
                    WaitWhile waitWhileResume = new WaitWhile(() => GameManager.Instance.gamePause);
                    yield return waitWhileResume;
                    //grenadeBehaviour안에 함수로 넣기
                    Collider2D[] targetColliders = Physics2D.OverlapCircleAll(grenadePos, grenadeAttackBehaviour.grenadeAttackSkillData.extent, grenadeAttackBehaviour.atkArea ^ grenadeAttackBehaviour.atkLayer);//스킬 범위로 변경
                    CastleInfo[] targetInfos = new CastleInfo[grenadeAttackBehaviour.grenadeAttackSkillData.max_Target];
                    for (int i = 0; i < targetColliders.Length && i < grenadeAttackBehaviour.grenadeAttackSkillData.max_Target; i++)
                    {
                        targetInfos[i] = targetColliders[i].GetComponent<CastleInfo>();
                    }
                    StartCoroutine(grenadeAttackBehaviour.SkillCoroutine(targetInfos));
                    break;
                }
                else
                {
                    heroInfo.moveDir = (grenadePos - transform.position).normalized;
                    transform.Translate(Time.deltaTime * heroInfo.heroData.speed * heroInfo.moveDir);
                }
                yield return null;
            }
        }
    }
    */
}