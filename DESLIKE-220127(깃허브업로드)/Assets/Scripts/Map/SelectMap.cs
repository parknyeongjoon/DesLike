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
    int curDay = 0; // ���� ��¥
    int sel1Day, sel2Day, sel3Day;  // �Ҹ� ��¥
    int sel1Evnt, sel2Evnt, sel3Evnt;   // ���� �ܰ� ����
    bool midBossCheck = false, villageCheck = false, organCheck = false; // �߰� ����, ����, ���� ����

    [SerializeField]
    GameObject OneTrackNode, TwoTrackNode, ThreeTrackNode;
    [SerializeField]
    GameObject BossNode, VillageNode, EliteNode, BattleNode2_1, BattleNode2_2, BattleNode3_1, BattleNode3_2, BattleNode3_3, 
        EventNode2_1, EventNode2_2, EventNode3_1, EventNode3_2, EventNode3_3;   // ���� ����?

    SaveManager saveManager = SaveManager.Instance; // SaveManager ���� ���

    void Start()
    {
        curDay = saveManager.gameData.heroSaveData.curDay;
        CommonSetting();
     
        int mapSelect = 0; // mapSelect ���� �� �ʱ�ȭ��

        if (sel1Evnt != 0 && sel1Evnt != 1) // �Ϲ����� ��Ȳ(�̺�Ʈ or ����)�� �ƴ϶��
            mapSelect = 3;  // �ܱ�
        else mapSelect = Random.Range(0, 2); // �Ϲ����� ��Ȳ(�̺�Ʈ or ����)�̸� �ΰ������ �������� ����

        if (mapSelect == 0)
        {
            if (twoTrack.activeSelf == false)  // 2Ʈ�� ����������
            {
                oneTrack.SetActive(false);
                twoTrack.SetActive(true);
                threeTrack.SetActive(false);
                TwoTrackSet();
            }
        }
        else if (mapSelect == 1)
        {
            if (threeTrack.activeSelf == false)  // 3Ʈ�� ����������
            {
                oneTrack.SetActive(false);
                twoTrack.SetActive(false);
                threeTrack.SetActive(true);
                ThreeTrackSet();
            }
        }
        else    // �ܱ�, Ư�� ��Ȳ
        {
            if (oneTrack.activeSelf == false)  // 1Ʈ�� ����������
            {
                oneTrack.SetActive(true);
                twoTrack.SetActive(false);
                threeTrack.SetActive(false);
                OneTrackSet();
            }
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
        if(OneTrackNode.activeSelf == false)
        {
            OneTrackNode.SetActive(true);
            TwoTrackNode.SetActive(false);
            ThreeTrackNode.SetActive(false);
        }

        sel1Day = 1;   // ��¥ 1�� ����
        switch(sel1Evnt)
        {
            case 2: // �߰� ����
                if (BossNode.activeSelf == false)
                {
                    BossNode.SetActive(true);
                    VillageNode.SetActive(false);
                    EliteNode.SetActive(false);
                }
                Sel1_1Text.text = "�߰� ����\n1";
                break;
            case 3: // ����
                if (VillageNode.activeSelf == false)
                {
                    BossNode.SetActive(false);
                    VillageNode.SetActive(true);
                    EliteNode.SetActive(false);
                }
                Sel1_1Text.text = "����\n1";
                break;
            case 4: // ����
                // ��� �߰� �ʿ�
                Sel1_1Text.text = "����\n1";
                break;
            case 5: // ���� ����
                if (EliteNode.activeSelf == false)
                {
                    BossNode.SetActive(false);
                    VillageNode.SetActive(false);
                    EliteNode.SetActive(true);
                }
                Sel1_1Text.text = "���� ����\n1";
                break;
            default:
                Sel1_1Text.text = "����\n1";
                break;
        }
    }

    void TwoTrackSet()  // �ΰ����� ����
    {
        if (TwoTrackNode.activeSelf == false)
        {
            OneTrackNode.SetActive(false);
            TwoTrackNode.SetActive(true);
            ThreeTrackNode.SetActive(false);
        }

        if (curDay == 0) // ù ����
        {
            sel1Day = 1;    // ù ���� �Ҹ��� 1 ����
            sel2Day = 1;

            if (BattleNode2_1.activeSelf == false)
            {
                BattleNode2_1.SetActive(true);
                EventNode2_1.SetActive(false);
                Debug.Log("����2_1�� ��������");
            }
            Sel2_1Text.text = "����\n" + sel1Day;

            if (BattleNode2_2.activeSelf == false)
            {
                BattleNode2_2.SetActive(true);
                EventNode2_2.SetActive(false);
                Debug.Log("����2_2�� ��������");
            }
            Sel2_2Text.text = "����\n" + sel2Day;
        }
        else // �̿�
        {
            sel1Day = Random.Range(0, 3) + 1;   // ��¥ ���� ����
            if (sel1Evnt == 0)
            {
                if (BattleNode2_1.activeSelf == false)
                {
                    BattleNode2_1.SetActive(true);
                    EventNode2_1.SetActive(false);
                }
                Sel2_1Text.text = "����\n" + sel1Day;
            }
            else
            {
                if (EventNode2_1.activeSelf == false)
                {
                    BattleNode2_1.SetActive(false);
                    EventNode2_1.SetActive(true);
                }
                Sel2_1Text.text = "�̺�Ʈ\n" + sel1Day;
            }
            sel2Day = Random.Range(0, 3) + 1;
            if (sel2Evnt == 0)
            {
                if (BattleNode2_2.activeSelf == false)
                {
                    BattleNode2_2.SetActive(true);
                    EventNode2_2.SetActive(false);
                }
                Sel2_2Text.text = "����\n" + sel2Day;
            }
            else
            {
                if (EventNode2_2.activeSelf == false)
                {
                    BattleNode2_2.SetActive(false);
                    EventNode2_2.SetActive(true);
                }
                Sel2_2Text.text = "�̺�Ʈ\n" + sel2Day;
            }
        }
    }

    void ThreeTrackSet()    // �������� ����
    {
        if (TwoTrackNode.activeSelf == false)
        {
            OneTrackNode.SetActive(false);
            TwoTrackNode.SetActive(false);
            ThreeTrackNode.SetActive(true);
        }

        if (curDay == 0) // ù ����
        {
            sel1Day = 1;
            sel2Day = 1;
            sel3Day = 1;

            if (BattleNode3_1.activeSelf == false)
            {
                BattleNode3_1.SetActive(true);
                EventNode3_1.SetActive(false);
            }
            Sel3_1Text.text = "����\n" + sel1Day;

            if (BattleNode3_2.activeSelf == false)
            {
                BattleNode3_2.SetActive(true);
                EventNode3_2.SetActive(false);
            }
            Sel3_2Text.text = "����\n" + sel2Day;

            if (BattleNode3_3.activeSelf == false)
            {
                BattleNode3_3.SetActive(true);
                EventNode3_3.SetActive(false);
            }
            Sel3_3Text.text = "����\n" + sel3Day;
        }
        else // �̿�
        {
            sel1Day = Random.Range(0, 3) + 1;   // ��¥ ���� ����
            if (sel1Evnt == 0)
            {
                if (BattleNode3_1.activeSelf == false)
                {
                    BattleNode3_1.SetActive(true);
                    EventNode3_1.SetActive(false);
                }
                Sel3_1Text.text = "����\n" + sel1Day;
            }
            else
            {
                if (EventNode3_1.activeSelf == false)
                {
                    BattleNode3_1.SetActive(false);
                    EventNode3_1.SetActive(true);
                }
                Sel3_1Text.text = "�̺�Ʈ\n" + sel1Day;
            }

            sel2Day = Random.Range(0, 3) + 1;
            if (sel2Evnt == 0)
            {
                if (BattleNode3_2.activeSelf == false)
                {
                    BattleNode3_2.SetActive(true);
                    EventNode3_2.SetActive(false);
                }
                Sel3_2Text.text = "����\n" + sel2Day;
            }
            else
            {
                if (EventNode3_2.activeSelf == false)
                {
                    BattleNode3_2.SetActive(false);
                    EventNode3_2.SetActive(true);
                }
                Sel3_2Text.text = "�̺�Ʈ\n" + sel2Day;
            }

            sel3Day = Random.Range(0, 3) + 1;
            if (sel3Evnt == 0)
            {
                if (BattleNode3_3.activeSelf == false)
                {
                    BattleNode3_3.SetActive(true);
                    EventNode3_3.SetActive(false);
                }
                Sel3_3Text.text = "����\n" + sel1Day;
            }
            else
            {
                if (EventNode3_3.activeSelf == false)
                {
                    BattleNode3_3.SetActive(false);
                    EventNode3_3.SetActive(true);
                }
                Sel3_3Text.text = "�̺�Ʈ\n" + sel1Day;
            }
        }
    }

    public void Select1Btn()   // 1�� ������
    {
        saveManager.gameData.heroSaveData.curDay += sel1Day;
        // ���� �ܰ� �̵� => by ���
    }

    public void Select2Btn()   // 2�� ������
    {
        saveManager.gameData.heroSaveData.curDay += sel2Day;
        // ���� �ܰ� �̵� => by ���
    }

    public void Select3Btn()   // 3�� ������
    {
        saveManager.gameData.heroSaveData.curDay += sel2Day;
        // ���� �ܰ� �̵� => by ���
    }
}