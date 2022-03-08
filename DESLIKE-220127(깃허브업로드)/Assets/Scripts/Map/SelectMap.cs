using UnityEngine;
using UnityEngine.UI;

public class SelectMap : MonoBehaviour
{
    public GameObject[] Track = new GameObject[3];
    [SerializeField] Text HPText, CurrentDayText, MoneyText;
    [SerializeField] Text[] SelText = new Text[6]; // 0 : 1_1 / 1,2 : 2_1, 2 / 3, 4, 5 = 3_1, 2, 3 
    [SerializeField] Text[] InfoText = new Text[6]; // 0 : 1_1 / 1,2 : 2_1, 2 / 3, 4, 5 = 3_1, 2, 3 

    GameObject hero;
    HeroInfo heroInfo;

    int curDay = 0; // ���� ��¥
    bool midBossCheck = false, villageCheck = false, organCheck = false; // �߰� ����, ����, ���� ����
    int[] selDay = new int[3];
    int[] selEvnt = new int[3];

    [SerializeField] GameObject[] TrackNode = new GameObject[3];
    [SerializeField] GameObject[] BattleNode = new GameObject[5]; // 0, 1 : 2_1, 2 / 2, 3, 4 = 3_1, 2, 3 
    [SerializeField] GameObject[] EvntNode = new GameObject[5];// 0, 1 : 2_1, 2 / 2, 3, 4 = 3_1, 2, 3 
    [SerializeField] GameObject[] ExtraNode = new GameObject[4]; // 0 : �߰� ����, 1 : ����, 2 : ����, 3 : ���� ����
    [SerializeField] GameObject[] Info = new GameObject[6]; // 0 : 1_1 / 1, 2 : 2_1, 2 / 3, 4, 5 : 3_1, 2, 3


    public Map map;
    SaveManager saveManager;

    [SerializeField]
    PortDatas portDatas;
    GoodsCollection goodsCollection;

    void Start()
    {
        saveManager = SaveManager.Instance;
        ObjectInactive();   // �� �ʱ�ȭ
        FindData(); // ������ ã��
        CommonSetting(); // �Ϲ� ����
        
        int mapSelect = 0; // mapSelect ���� �� �ʱ�ȭ��
       
        if ((selEvnt[0] != 0) && (selEvnt[0] != 1)) // �Ϲ����� ��Ȳ(�̺�Ʈ or ����)�� �ƴ϶��
            mapSelect = 3;   // �ܱ�
        else mapSelect = Random.Range(0, 2); // �Ϲ����� ��Ȳ(�̺�Ʈ or ����)�̸� �ΰ������ �������� ����

        for (int i = 0; i < 3; i++)
            Track[i].SetActive(false);

        switch(mapSelect)
        {
            case 0: // �ΰ�����
                Track[1].SetActive(true);
                TwoTrackSet();
                break;
            case 1: // ��������
                Track[2].SetActive(true);
                ThreeTrackSet();
                break;
            default:    // Ư�� ��Ȳ �ܱ�
                Track[0].SetActive(true);
                OneTrackSet();
                break;
        }
    }

    void FindData() // �ܺ� ������ ��������
    {
        curDay = saveManager.gameData.map.curDay;
        midBossCheck = saveManager.gameData.map.midBossCheck;
        villageCheck = saveManager.gameData.map.villageCheck;
        organCheck = saveManager.gameData.map.organCheck;
        goodsCollection = saveManager.gameData.goodsCollection;
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
        MoneyText.text = "- �ķ� : " + goodsCollection.food + "\n- ��� : " + goodsCollection.gold;
    }

    void ObjectInactive()   // �� �ʱ�ȭ
    {
        for(int i = 0; i<5; i++)
        {
            if (i < 3) TrackNode[i].SetActive(false);  // �� ���
            if (i < 4) ExtraNode[i].SetActive(false);  // ��Ÿ ���
            BattleNode[i].SetActive(false); // ��Ʋ ���
            EvntNode[i].SetActive(false);   // �̺�Ʈ ���
        }
    }

    void NextEventChoice()  // 0 : ����, 1 : �̺�Ʈ, 2 : �߰� ����, 3 : ����, 4 : ����, 5 : ���� ����
    {
        // �⺻ ���� ����
        for (int i = 0; i < 3; i++)
            selEvnt[i] = Random.Range(0, 2);

        if (curDay == 0) // ù ����� ������ ����
            for (int i = 0; i < 3; i++)
                selEvnt[i] = 0;
        else
        {
            if (curDay >= 14)
            {
                if (midBossCheck == false)    // �߰� ���� ����
                {
                    selEvnt[0] = 2;
                    saveManager.gameData.map.midBossCheck = true;
                }
                else if (villageCheck == false)
                {
                    selEvnt[0] = 3;
                    saveManager.gameData.map.villageCheck = true;
                }
                else if (curDay >= 29 && villageCheck == true)  // ���� ����
                {
                    if (organCheck == false)
                    {
                        selEvnt[0] = 4;
                        saveManager.gameData.map.organCheck = true;
                    }
                    else selEvnt[0] = 5;
                }
            }
        }
    }

    void OneTrackSet()  // �ܱ� ����
    {
        TrackNode[0].SetActive(true);

        for (int i = 0; i < 4; i++)
            ExtraNode[i].SetActive(false);

        selDay[0] = 1;   // ��¥ 1�� ����
        switch (selEvnt[0]-2)
        {
            case 0: // �߰� ����
                ExtraNode[0].SetActive(true);
                SelText[0].text = "�߰� ����";
                InfoText[0].text = "�߰����� ����";
                break;
            case 1: // ����
                ExtraNode[1].SetActive(true);
                SelText[0].text = "����";
                InfoText[0].text = "���� ����";
                break;
            case 2: // ����
                    // ��� �߰� �ʿ�
                ExtraNode[2].SetActive(true);
                SelText[0].text = "����";
                InfoText[0].text = "���� ����";
                break;
            case 3: // ���� ����
                ExtraNode[3].SetActive(true);
                SelText[0].text = "���� ����";
                InfoText[0].text = "���� ���� ����";
                break;
            default:
                SelText[0].text = "����";
                break;
        }
    }

    void TwoTrackSet()  // �ΰ����� ����
    {
        TrackNode[1].SetActive(true);
            
        if (curDay == 0) // ù ����
        {
            for(int i = 0; i<2; i++)
            {
                selDay[i] = 1;
                BattleNode[i].SetActive(true);
                SelText[i+1].text = "����";
                InfoText[i + 1].text = "���� ���� ����";
            }
        }
        else // �̿�
        {
            for(int i = 0; i<2; i++)
            {
                if (selEvnt[i] == 0)
                {
                    BattleNode[i].SetActive(true);
                    SelText[i+1].text = "����";
                    InfoText[i + 1].text = "���� ���� ����";
                }
                else
                {
                    EvntNode[i].SetActive(true);
                    SelText[i+1].text = "�̺�Ʈ";
                    InfoText[i + 1].text = "�̺�Ʈ ���� ����";
                }
            }
        }
    }

    void ThreeTrackSet()    // �������� ����
    {
            TrackNode[2].SetActive(true);

        if (curDay == 0) // ù ����
        {
            for(int i = 0; i<3; i++)
            {
                BattleNode[i+2].SetActive(true);
                SelText[i+3].text = "����";
                InfoText[i + 3].text = "���� ���� ����";
            }
        }
        else // �̿�
        {
            for(int i = 0; i<3; i++)
            {
                int j = i + 3;
                if (selEvnt[i] == 0)
                {
                    BattleNode[i+2].SetActive(true);
                    SelText[i+3].text = "����";
                    InfoText[i+3].text = "���� ���� ����";
                }
                else
                {
                    EvntNode[i+2].SetActive(true);
                    SelText[i+3].text = "�̺�Ʈ";
                    InfoText[i+3].text = "�̺�Ʈ ���� ����";
                }
            }
        }
    }

    public void ButtonDown1_1()
    {
        Info[0].SetActive(true);
    }
    
    public void ButtonDown2_1()
    {
        Info[1].SetActive(true);
    }

    public void ButtonDown2_2()
    {
        Info[2].SetActive(true);
    }

    public void ButtonDown3_1()
    {
        Info[3].SetActive(true);
    }

    public void ButtonDown3_2()
    {
        Info[4].SetActive(true);
    }

    public void ButtonDown3_3()
    {
        Info[5].SetActive(true);
    }

    public void ButtonOut()
    {
        for (int i = 0; i < 6; i++)
            Info[i].SetActive(false);
    }

    public void Select1Btn()   // 1�� ������
    {
        map.level = curDay;
        if (selEvnt[0] == 0)
            saveManager.gameData.map.curDay += 2;  // ���� �ÿ��� 2�� �߰�
        // ���� �ܰ� �̵� => by ���
    }

    public void Select2Btn()   // 2�� ������
    {
        map.level = curDay;
        if (selEvnt[1] == 0)
            saveManager.gameData.map.curDay += 2;  // ���� �ÿ��� 2�� �߰�
        // ���� �ܰ� �̵� => by ���
    }

    public void Select3Btn()   // 3�� ������
    {
        map.level = curDay;
        if (selEvnt[2] == 0)
            saveManager.gameData.map.curDay += 2;  // ���� �ÿ��� 2�� �߰�
        // ���� �ܰ� �̵� => by ���
    }
}