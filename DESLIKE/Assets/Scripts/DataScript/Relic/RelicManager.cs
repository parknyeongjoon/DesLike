using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RelicManager : MonoBehaviour
{
    public static RelicManager instance;

    public Dictionary<string, Relic> relicList = new Dictionary<string, Relic>();

    public Canvas relicCanvas;

    public Action<HeroData> soldierConditionCheck;

    public static RelicManager Instance
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
        //�̱��� ����
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void GetRelic(string relicKey)//relicKey�� �ش��ϴ� ������ ŉ���ϴ� �Լ�
    {
        if (SaveManager.Instance.dataSheet.relicDataSheet[relicKey])//������ ��Ʈ�� �ִ� �������� �˻�
        {
            GameObject relicObject = SaveManager.Instance.dataSheet.relicObjectSheet[relicKey];
            relicList.Add(relicKey, relicObject.GetComponent<Relic>());
            Instantiate(relicObject, relicCanvas.transform.GetChild(0).transform);
            relicList[relicKey].DoEffect();
        }
        else
        {
            Debug.Log("���� Ű ����");
        }
    }

    public void LoadRelic(string relicKey)
    {
        if (SaveManager.Instance.dataSheet.relicDataSheet[relicKey])//������ ��Ʈ�� �ִ� �������� �˻�
        {
            GameObject relicObject = SaveManager.Instance.dataSheet.relicObjectSheet[relicKey];
            relicList.Add(relicKey, relicObject.GetComponent<Relic>());
            Instantiate(relicObject, relicCanvas.transform.GetChild(0).transform);
            if (relicList[relicKey].relicData.continueReuse)//�ε��ϸ� �ٽ� ����ؾ��ϴ� �����̶�� ����
            {
                relicList[relicKey].DoEffect();
            }
        }
        else
        {
            Debug.Log("���� Ű ����");
        }
    }
}