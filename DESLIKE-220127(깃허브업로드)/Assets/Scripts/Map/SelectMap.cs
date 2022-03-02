using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectMap : MonoBehaviour
{
    public GameObject oneTrack, twoTrack, threeTrack;
    [SerializeField]
    Text HPText, Sel1_1Text, Sel2_1Text, Sel2_2Text, Sel3_1Text, Sel3_2Text, Sel3_3Text, CurrentDayText;

    GameObject hero;
    HeroInfo heroInfo;
    int curDay = 1; // ���� ��¥
    int sel1Day, sel2Day, sel3Day;  // �Ҹ� ��¥
    int sel1Evnt, sel2Evnt, sel3Evnt;   // ���� �ܰ� ����
    bool midBossCheck = false, villageCheck = false, organCheck = false; // �߰� ����, ����, ���� ����

    void Start()
    {
        CommonSetting();
        int mapSelect = 0;

        if (sel1Evnt != 0 && sel1Evnt != 1) // �Ϲ����� ��Ȳ(�̺�Ʈ or ����)�� �ƴ϶��
            mapSelect = 3;  // �ܱ�
        else mapSelect = Random.Range(0, 2); // �Ϲ����� ��Ȳ(�̺�Ʈ or ����)�̸� �ΰ������ �������� ����

        if (mapSelect == 0)
        {
            oneTrack.SetActive(false);
            twoTrack.SetActive(true);
            threeTrack.SetActive(false);
            TwoTrackSet();
        }
        else if(mapSelect == 1)
        {
            oneTrack.SetActive(false);
            twoTrack.SetActive(false);
            threeTrack.SetActive(true);
            ThreeTrackSet();
        }
        else    // �ܱ�, Ư�� ��Ȳ
        {
            oneTrack.SetActive(true);
            twoTrack.SetActive(false);
            threeTrack.SetActive(false);
            OneTrackSet();
        }
    }
   
    void CommonSetting()    // ���� ����
    {
        if (curDay != 0)
        {
            hero = GameObject.Find("Hero");
            heroInfo = hero.GetComponent<HeroInfo>();
            HPText.text = heroInfo.cur_Hp + "/" + heroInfo.castleData.hp;
        }
        NextEventChoice(); // ���� ���� ����
        CurrentDayText.text = curDay + " / 30";
    }

    void NextEventChoice()  // 0 : ����, 1 : �̺�Ʈ, 2 : �߰� ����, 3 : ����, 4 : ����, 5 : ���� ����
    {
        if (curDay == 0) // ù ����� ������ ����
        {
            sel1Evnt = 0;
            sel2Evnt = 0;
            sel3Evnt = 0;
        }
        else if((curDay >= 14) && (midBossCheck == false))  // �߰� ���� ����
        {
            sel1Evnt = 2;
            sel2Evnt = 2;
            sel3Evnt = 2;
            midBossCheck = true;
        }
        else if ((curDay >= 15) && (villageCheck == false))  // �߰� ���� ����
        {   // �߰� ���� ������ �ٷ� ���� ���̸� �ʿ����?
            sel1Evnt = 3;
            sel2Evnt = 3;
            sel3Evnt = 3;
            villageCheck = true;
        }
        else if ((curDay >= 29) && (villageCheck == false))  // ���� ����
        { 
            sel1Evnt = 4;
            sel2Evnt = 4;
            sel3Evnt = 4;
            organCheck = true;
        }
        else if(organCheck == true) // ���� ���� ����
        {
            sel1Evnt = 5;
            sel2Evnt = 5;
            sel3Evnt = 5;
        }
        else    // �Ϲ� ���� ��� 0 : ����, 1 : �̺�Ʈ 
        {
            sel1Evnt = Random.Range(0, 2);
            sel2Evnt = Random.Range(0, 2);
            sel3Evnt = Random.Range(0, 2);
        }
    }

    void OneTrackSet()  // �ܱ� ����
    {
        sel1Day = 1;   // ��¥ 1�� ����
        switch(sel1Evnt)
        {
            case 2: // �߰� ����
                Sel1_1Text.text = "�߰� ����\n1";
                break;
            case 3: // ����
                Sel1_1Text.text = "����\n1";
                break;
            case 4: // ����
                Sel1_1Text.text = "����\n1";
                break;
            case 5: // ���� ����
                Sel1_1Text.text = "���� ����\n1";
                break;
            default:
                Sel1_1Text.text = "����\n1";
                break;
        }
    }

    void TwoTrackSet()  // �ΰ����� ����
    {
        if (curDay == 0) // ù ����
        {
            sel1Day = 1;
            sel2Day = 1;
            Sel2_1Text.text = "����\n" + sel1Day;
            Sel2_2Text.text = "����\n" + sel2Day;
        }
        else // �̿�
        {
            sel1Day = Random.Range(0, 3) + 1;   // ��¥ ���� ����
            if (sel1Evnt == 0)
                Sel2_1Text.text = "����\n" + sel1Day;
            else Sel2_1Text.text = "�̺�Ʈ\n" + sel1Day;

            sel2Day = Random.Range(0, 3) + 1;
            if (sel2Evnt == 0)
                Sel2_2Text.text = "����\n" + sel2Day;
            else Sel2_2Text.text = "�̺�Ʈ\n" + sel2Day;
        }
    }

    void ThreeTrackSet()    // �������� ����
    {
        if (curDay == 0) // ù ����
        {
            sel1Day = 1;
            sel2Day = 1;
            sel3Day = 1;
            Sel3_1Text.text = "����\n" + sel1Day;
            Sel3_2Text.text = "����\n" + sel2Day;
            Sel3_3Text.text = "����\n" + sel3Day;
        }
        else // �̿�
        {
            sel1Day = Random.Range(0, 3) + 1;   // ��¥ ���� ����
            if (sel1Evnt == 0)
                Sel3_1Text.text = "����\n" + sel1Day;
            else Sel3_1Text.text = "�̺�Ʈ\n" + sel1Day;

            sel2Day = Random.Range(0, 3) + 1;
            if (sel2Evnt == 0)
                Sel3_2Text.text = "����\n" + sel2Day;
            else Sel3_2Text.text = "�̺�Ʈ\n" + sel2Day;

            sel3Day = Random.Range(0, 3) + 1;
            if (sel3Evnt == 0)
                Sel3_3Text.text = "����\n" + sel1Day;
            else Sel3_3Text.text = "�̺�Ʈ\n" + sel1Day;
        }
    }

    void Select1Btn()   // 1�� ������
    {
        curDay += sel1Day;
        // ���� �ܰ� �̵�
    }

    void Select2Btn()   // 2�� ������
    {
        curDay += sel2Day;
        // ���� �ܰ� �̵�
    }

    void Select3Btn()   // 3�� ������
    {
        curDay += sel2Day;
        // ���� �ܰ� �̵�
    }
}