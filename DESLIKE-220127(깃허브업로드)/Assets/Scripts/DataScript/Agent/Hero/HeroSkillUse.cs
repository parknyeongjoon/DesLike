using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSkillUse: MonoBehaviour
{
    MouseManager mouseManager;

    HeroInfo heroInfo;
    public List<SkillBehaviour> skillBehaviours = new List<SkillBehaviour>();

    Coroutine skillCoroutine;

    GameObject skillFocus;
    [SerializeField]
    GameObject skillRange;

    int atkArea, atkLayer;

    void Start()
    {
        mouseManager = MouseManager.Instance;

        heroInfo = transform.GetComponent<HeroInfo>();

        for (int i = 0;i < heroInfo.heroData.skillList.Count; i++)
        {
            AddSkillBehaviour(heroInfo.heroData.skillList[i]);
        }
    }

    void Update()
    {
        QSkill();
        WSkill();
        ESkill();
        RSkill();
        StopSkillCoroutine();
    }

    void QSkill()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (heroInfo.heroData.skillList.Count > 0)
            {
                UseSkillBehaviour(heroInfo.heroData.skillList[0], skillBehaviours[0]);
            }
        }
    }

    void WSkill()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (heroInfo.heroData.skillList.Count > 1)
            {
                UseSkillBehaviour(heroInfo.heroData.skillList[1], skillBehaviours[1]);
            }
        }
    }

    void ESkill()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heroInfo.heroData.skillList.Count > 2)
            {
                UseSkillBehaviour(heroInfo.heroData.skillList[2], skillBehaviours[2]);
            }
        }
    }

    void RSkill()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (heroInfo.heroData.skillList.Count > 3)
            {
                UseSkillBehaviour(heroInfo.heroData.skillList[3], skillBehaviours[3]);
            }
        }
    }

    void AddSkillBehaviour(SkillData skillData)
    {
        if (skillData.sort == "SingleAttack")
        {
            SingleAttackBehaviour createBehaviour;
            createBehaviour = gameObject.AddComponent<SingleAttackBehaviour>();
            createBehaviour.singleAttackSkillData = (SingleAttackData)skillData;
            skillBehaviours.Add(createBehaviour);
        }
        else if (skillData.sort == "GrenadeAttack")
        {
            GrenadeAttackBehaviour createBehaviour;
            createBehaviour = gameObject.AddComponent<GrenadeAttackBehaviour>();
            createBehaviour.grenadeAttackSkillData = (GrenadeAttackData)skillData;
            skillBehaviours.Add(createBehaviour);
        }
    }
    //할 때마다 찾지말고 지정해놓고 쓰는 방법 찾아보기
    void UseSkillBehaviour(SkillData skillData, SkillBehaviour skillBehaviour)
    {
        if(skillCoroutine != null)
        {
            StopCoroutine(skillCoroutine);
        }
        if (skillData.sort == "SingleAttack")
        {
            skillCoroutine = StartCoroutine(UseSingleAttack(skillBehaviour));
        }
        else if(skillData.sort == "GrenadeAttack")
        {
            skillCoroutine = StartCoroutine(UseGrenadeAttack(skillBehaviour));
        }
    }

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

    void StopSkillCoroutine()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (skillCoroutine != null)
            {
                StopCoroutine(skillCoroutine);
                mouseManager.SkillTarget = null;
                heroInfo.target = null;
                if (skillFocus != null)
                {
                    skillFocus.SetActive(false);
                }
                mouseManager.mouseState = MouseState.Idle;
                Cursor.visible = true;
                mouseManager.grenadeExtent.SetActive(false);
                skillRange.SetActive(false);
                mouseManager.SetIdleCursorTexture();
            }
        }
    }
}
