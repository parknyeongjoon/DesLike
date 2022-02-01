using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour
{
    static MouseManager instance;

    public BattleUIManager battleUIManager;
    [SerializeField]
    Texture2D aimCursorTexture;

    public GameObject grenadeExtent;
    GameObject mouseFocus;

    public Mouse_State mouseState;

    //전역변수로써 manager에 접근할 수 있게 만들기
    public static MouseManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        SetIdleCursorTexture();
        mouseState = Mouse_State.Idle;
    }

    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if(mouseState == Mouse_State.Idle)
            {
                Collider2D hit;
                hit = CastRay();
                if (hit != null)
                {
                    if (hit.CompareTag("Soldier"))
                    {
                        if (mouseFocus != null)
                        {
                            mouseFocus.SetActive(false);
                        }
                        mouseFocus = hit.transform.Find("mouseFocus").gameObject;
                        mouseFocus.SetActive(true);
                        battleUIManager.cur_Soldier = hit.GetComponent<SoldierInfo>();
                        battleUIManager.SetMidPanel(0);
                    }
                    else if(hit.CompareTag("Challenge"))
                    {
                        battleUIManager.SetMidPanel(4);
                    }
                }

            }
            else if(mouseState == Mouse_State.Grenade)
            {

            }
        }
    }

    public Collider2D CastRay()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
        if (hit.collider != null)
        {
            return hit.collider;
        }
        return null;
    }

    public void SetAimCursorTexture()
    {
        Vector2 hotSpot;
        hotSpot.x = aimCursorTexture.width / 2;
        hotSpot.y = aimCursorTexture.height / 2;
        Cursor.SetCursor(aimCursorTexture, hotSpot, CursorMode.Auto);
    }

    public void SetIdleCursorTexture()
    {
        Cursor.SetCursor(null, new Vector2(0, 0), CursorMode.Auto);
    }
}