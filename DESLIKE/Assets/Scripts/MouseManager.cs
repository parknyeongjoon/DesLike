using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour
{
    static MouseManager instance;

    [SerializeField]
    Texture2D aimCursorTexture;

    public GameObject grenadeExtent;
    public GameObject mouseFocus;

    public Vector3 skillPos;

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
        SetIdleCursorTexture();
        mouseState = Mouse_State.Idle;
    }

    void Update()//배틀 밖에서 할 필요 없음(지우기)
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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

    public void SetGrenadeExtent(Transform parentTrans)
    {
        grenadeExtent.transform.parent = parentTrans;
        grenadeExtent.transform.position = new Vector3(parentTrans.position.x, parentTrans.position.y, 0);
        grenadeExtent.SetActive(true);
    }
}