using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//��Ʈ �Ŵ����� ������Ʈ �����
public class PortManager : MonoBehaviour
{
    static PortManager instance;
    public static PortManager Instance
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

    [SerializeField] PortDatas allyPortDatas;
    [SerializeField] GameObject curSellBtn;

    public Coroutine setPortCoroutine;
    public bool isSet = false;
    public string soldierCode;

    public 

    void Awake()
    {
        instance = this;
    }

    public IEnumerator SetPortCoroutine()
    {
        isSet = true;
        SetPortImg();
        while (!Input.GetKeyDown(KeyCode.Escape) && isSet)
        {
            yield return null;
        }
        ReturnPortImg();
    }

    public void SetPortImg()
    {
        for (int i = 0; i < allyPortDatas.portDatas.Length; i++)
        {
            if (allyPortDatas.portDatas[i].unlock && allyPortDatas.portDatas[i].soldierCode == null)
            {
                allyPortDatas.portDatas[i].portImg.color = new Color(0, 0.7f, 0);
            }
            else if (!allyPortDatas.portDatas[i].unlock || allyPortDatas.portDatas[i].soldierCode != null)
            {
                allyPortDatas.portDatas[i].portImg.color = new Color(0.7f, 0, 0);
            }
        }
    }

    public void ReturnPortImg()
    {
        for (int i = 0; i < allyPortDatas.portDatas.Length; i++)
        {
            if (allyPortDatas.portDatas[i].unlock) { allyPortDatas.portDatas[i].portImg.color = new Color(1, 1, 1); }//��Ʈ�� �����Ǿ��ִٸ� ���
            else { allyPortDatas.portDatas[i].portImg.color = new Color(0.3f, 0.3f, 0.3f); }//�ƴ϶�� ȸ��
        }
    }
}
