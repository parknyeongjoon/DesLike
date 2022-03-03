using UnityEngine;
using UnityEngine.UI;

public class SelectMap : MonoBehaviour
{
    public GameObject oneTrack, twoTrack, threeTrack;
    [SerializeField]
    Text HPText, Sel1_1Text, Sel2_1Text, Sel2_2Text, Sel3_1Text, Sel3_2Text, Sel3_3Text, CurrentDayText;

    GameObject hero;
    HeroInfo heroInfo;
    int curDay = 0; // ���� ��¥
    int selDay1, selDay2, selDay3;  // �Ҹ� ��¥
    int selEvnt1, selEvnt2, selEvnt3;   // ���� �ܰ� ����
    
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

        if ((selEvnt1 != 0) && (selEvnt1 != 1)) // �Ϲ����� ��Ȳ(�̺�Ʈ or ����)�� �ƴ϶��
            mapSelect = 3;   // �ܱ�
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
            selEvnt1 = 0;
            selEvnt2 = 0;
            selEvnt3 = 0;
        }
        else if((curDay >= 14) && (midBossCheck == false))  // �߰� ���� ����
        {
            selEvnt1 = 2;
            selEvnt2 = 2;
            selEvnt3 = 2;
            midBossCheck = true;
        }
        else if ((curDay >= 15) && (villageCheck == false))  // �߰� ���� ����
        {   // �߰� ���� ������ �ٷ� ���� ���̸� �ʿ����?
            selEvnt1 = 3;
            selEvnt2 = 3;
            selEvnt3 = 3;
            villageCheck = true;
        }
        else if ((curDay >= 29) && (organCheck == false))  // ���� ����
        { 
            selEvnt1 = 4;
            selEvnt2 = 4;
            selEvnt3 = 4;
            organCheck = true;
        }
        else if(organCheck == true) // ���� ���� ����
        {
            selEvnt1 = 5;
            selEvnt2 = 5;
            selEvnt3 = 5;
        }
        else    // �Ϲ� ���� ��� 0 : ����, 1 : �̺�Ʈ 
        {
            selEvnt1 = Random.Range(0, 2);
            selEvnt2 = Random.Range(0, 2);
            selEvnt3 = Random.Range(0, 2);
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

        selDay1 = 1;   // ��¥ 1�� ����
        switch(selEvnt1)
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
            selDay1 = 1;    // ù ���� �Ҹ��� 1 ����
            selDay2 = 1;

            if (BattleNode2_1.activeSelf == false)
            {
                BattleNode2_1.SetActive(true);
                EventNode2_1.SetActive(false);
            }
            Sel2_1Text.text = "����\n" + selDay1;

            if (BattleNode2_2.activeSelf == false)
            {
                BattleNode2_2.SetActive(true);
                EventNode2_2.SetActive(false);
            }
            Sel2_2Text.text = "����\n" + selDay2;
        }
        else // �̿�
        {
            selDay1 = Random.Range(0, 3) + 1;   // ��¥ ���� ����
            if (selEvnt1 == 0 || saveManager.gameData.heroSaveData.EvntStream == 3) // �̺�Ʈ 3�� �����̸� ������ ����
            {
                if (BattleNode2_1.activeSelf == false)
                {
                    BattleNode2_1.SetActive(true);
                    EventNode2_1.SetActive(false);
                }
                Sel2_1Text.text = "����\n" + selDay1;
            }
            else
            {
                if (BattleNode2_1.activeSelf == true)
                {
                    BattleNode2_1.SetActive(false);
                    EventNode2_1.SetActive(true);
                }
                Sel2_1Text.text = "�̺�Ʈ\n" + selDay1;
            }

            selDay2 = Random.Range(0, 3) + 1;
            if (selEvnt2 == 0 || saveManager.gameData.heroSaveData.EvntStream == 3)
            {
                if (BattleNode2_2.activeSelf == false)
                {
                    BattleNode2_2.SetActive(true);
                    EventNode2_2.SetActive(false);
                }
                Sel2_2Text.text = "����\n" + selDay2;
            }
            else
            {
                if (BattleNode2_2.activeSelf == true)
                {
                    BattleNode2_2.SetActive(false);
                    EventNode2_2.SetActive(true);
                }
                Sel2_2Text.text = "�̺�Ʈ\n" + selDay2;
            }
        }
    }

    void ThreeTrackSet()    // �������� ����
    {
        if (ThreeTrackNode.activeSelf == false)
        {
            OneTrackNode.SetActive(false);
            TwoTrackNode.SetActive(false);
            ThreeTrackNode.SetActive(true);
        }

        if (curDay == 0) // ù ����
        {
            selDay1 = 1;
            selDay2 = 1;
            selDay3 = 1;

            if (BattleNode3_1.activeSelf == false)
            {
                BattleNode3_1.SetActive(true);
                EventNode3_1.SetActive(false);
            }
            Sel3_1Text.text = "����\n" + selDay1;

            if (BattleNode3_2.activeSelf == false)
            {
                BattleNode3_2.SetActive(true);
                EventNode3_2.SetActive(false);
            }
            Sel3_2Text.text = "����\n" + selDay2;

            if (BattleNode3_3.activeSelf == false)
            {
                BattleNode3_3.SetActive(true);
                EventNode3_3.SetActive(false);
            }
            Sel3_3Text.text = "����\n" + selDay3;
        }
        else // �̿�
        {
            selDay1 = Random.Range(0, 3) + 1;   // ��¥ ���� ����
            if (selEvnt1 == 0 || saveManager.gameData.heroSaveData.EvntStream == 3)
            {
                if (BattleNode3_1.activeSelf == false)
                {
                    BattleNode3_1.SetActive(true);
                    EventNode3_1.SetActive(false);
                }
                Sel3_1Text.text = "����\n" + selDay1;
            }
            else
            {
                if (BattleNode3_1.activeSelf == true)
                {
                    BattleNode3_1.SetActive(false);
                    EventNode3_1.SetActive(true);
                }
                Sel3_1Text.text = "�̺�Ʈ\n" + selDay1;
            }

            selDay2 = Random.Range(0, 3) + 1;
            if (selEvnt2 == 0 || saveManager.gameData.heroSaveData.EvntStream == 3)
            {
                if (BattleNode3_2.activeSelf == false)
                {
                    BattleNode3_2.SetActive(true);
                    EventNode3_2.SetActive(false);
                }
                Sel3_2Text.text = "����\n" + selDay2;
            }
            else
            {
                if (BattleNode3_2.activeSelf == true)
                {
                    BattleNode3_2.SetActive(false);
                    EventNode3_2.SetActive(true);
                }
                Sel3_2Text.text = "�̺�Ʈ\n" + selDay2;
            }

            selDay3 = Random.Range(0, 3) + 1;
            if (selEvnt3 == 0 || saveManager.gameData.heroSaveData.EvntStream == 3)
            {
                if (BattleNode3_3.activeSelf == false)
                {
                    BattleNode3_3.SetActive(true);
                    EventNode3_3.SetActive(false);
                }
                Sel3_3Text.text = "����\n" + selDay3;
            }
            else
            {
                if (BattleNode3_3.activeSelf == true)
                {
                    BattleNode3_3.SetActive(false);
                    EventNode3_3.SetActive(true);
                }
                Sel3_3Text.text = "�̺�Ʈ\n" + selDay3;
            }
        }
    }

    public void Select1Btn()   // 1�� ������
    {
        saveManager.gameData.heroSaveData.curDay += selDay1;
        if(selEvnt1 == 0)   // ����
            saveManager.gameData.heroSaveData.EvntStream = 0;   // �ʱ�ȭ
        else if(selEvnt1 == 1)  // �̺�Ʈ
            saveManager.gameData.heroSaveData.EvntStream++; // �߰�
        
        // ���� �ܰ� �̵� => by ���
    }

    public void Select2Btn()   // 2�� ������
    {
        saveManager.gameData.heroSaveData.curDay += selDay2;
        if (selEvnt2 == 0)
            saveManager.gameData.heroSaveData.EvntStream = 0;
        else if (selEvnt2 == 1)
            saveManager.gameData.heroSaveData.EvntStream++;
        // ���� �ܰ� �̵� => by ���
    }

    public void Select3Btn()   // 3�� ������
    {
        saveManager.gameData.heroSaveData.curDay += selDay2;
        if (selEvnt3 == 0)
            saveManager.gameData.heroSaveData.EvntStream = 0;
        else if (selEvnt3 == 1)
            saveManager.gameData.heroSaveData.EvntStream++;
        // ���� �ܰ� �̵� => by ���
    }
}