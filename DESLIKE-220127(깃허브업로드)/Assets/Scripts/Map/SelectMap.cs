using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectMap : MonoBehaviour
{
    public GameObject[] Track = new GameObject[3];
    [SerializeField] TMP_Text HPText, CurrentDayText, MoneyText;
    [SerializeField] TMP_Text[] SelText = new TMP_Text[6]; // 0 : 1_1 / 1,2 : 2_1, 2 / 3, 4, 5 = 3_1, 2, 3 
    GameObject hero;
    HeroInfo heroInfo;

    int curDay = 0, curStage; // ���� ��¥
    bool midBossCheck1 = false, midBossCheck2 = false, villageCheck = false, organCheck = false; // �߰� ����, ����, ���� ����
    int[] selDay = new int[3];
    int[] selEvnt = new int[3];
    int nodeNum;   // ��� ������(selEvnt[nodeNum]��)
    int curTrack = 0;
    bool newSet = false;

    [SerializeField] GameObject MyTeamPanel, MorePanel;
    [SerializeField] GameObject InfoPanel;
    [SerializeField] GameObject[] TrackNode = new GameObject[3];
    [SerializeField] GameObject[] Button1_1;    // 0-2 1�� �߰� / 3-5 2�� �߰� / 6-8 ���� / 9 ���� / 10 ����
    [SerializeField] GameObject[] Button2_1;
    [SerializeField] GameObject[] Button2_2;
    [SerializeField] GameObject[] Button3_1;
    [SerializeField] GameObject[] Button3_2;
    [SerializeField] GameObject[] Button3_3;
    [SerializeField] GameObject[] EventButton;
    [SerializeField] GameObject[] Title;    // 0 ���� / 1 �̺�Ʈ

    [SerializeField] BattleNodeScript[] S1T1B1; // Stage1, Track1, Button1
    [SerializeField] BattleNodeScript[] S1T2B1; // Stage1, Track2, Button1
    [SerializeField] BattleNodeScript[] S1T2B2; // Stage1, Track2, Button2
    [SerializeField] BattleNodeScript[] S1T3B1; // Stage1, Track3, Button1
    [SerializeField] BattleNodeScript[] S1T3B2; // Stage1, Track3, Button2
    [SerializeField] BattleNodeScript[] S1T3B3; // Stage1, Track3, Button3
    [SerializeField] GameObject[] EventNode = new GameObject[7]; // 0:���� / 1:���� / 2,3 : 2_1,2�̺�Ʈ / 4,5,6 : 3_1,2,3 �̺�Ʈ
    int[] nextEvent;   // [0~2] : Btn1~3, int : 0~2 => ������ ����(���� ����)
    
    public Map map;
    SaveManager saveManager;

    [SerializeField] PortDatas portDatas;
    GoodsCollection goodsCollection;
    int selectNum = 0;

    void Start()
    {
        
        saveManager = SaveManager.Instance;
        ObjectInactive();   // �� �ʱ�ȭ
        FindData(); // ������ ã��
        CommonSetting(); // �Ϲ� ����
        NextEventChoice(); // ���� ���� ����

        for (int i = 0; i < 3; i++)
            Track[i].SetActive(false);

        Track[curTrack].SetActive(true);

        switch (curTrack)
        {
            case 0: // �ܱ�
                OneTrackSet();
                break;
            case 1: // �ΰ�����
                TwoTrackSet();
                break;
            case 2: // ��������
                ThreeTrackSet();
                break;
            default:    // Ư�� ��Ȳ �ܱ�
                Debug.Log("���� �߻�");
                break;
        }
    }

    void ObjectInactive()   // �� �ʱ�ȭ
    {
        InfoPanel.gameObject.SetActive(false);
        for (int i = 0; i < 18; i++)
        {
            if (i < 3) TrackNode[i].SetActive(false);  // �� ���
            if (i < 9) S1T1B1[i].gameObject.SetActive(false);
            S1T2B1[i].gameObject.SetActive(false);
            S1T2B2[i].gameObject.SetActive(false);
            S1T3B1[i].gameObject.SetActive(false);
            S1T3B2[i].gameObject.SetActive(false);
            S1T3B3[i].gameObject.SetActive(false);
        }
    }

    void FindData() // �ܺ� ������ ��������
    {
        curDay = saveManager.gameData.map.curDay;

        if (curDay != saveManager.gameData.map.checkDay || curDay == 0)    // �̹� ���� �ʿ� ó�� �´ٸ�
        {
            newSet = true;
            saveManager.gameData.map.checkDay = curDay;
        }
        curStage = saveManager.gameData.map.curStage;
        curTrack = saveManager.gameData.map.curTrack;
        nextEvent = saveManager.gameData.map.nextEvent;
        midBossCheck1 = saveManager.gameData.map.midBossCheck1;
        midBossCheck2 = saveManager.gameData.map.midBossCheck2;
        villageCheck = saveManager.gameData.map.villageCheck;
        organCheck = saveManager.gameData.map.organCheck;
        goodsCollection = saveManager.gameData.goodsCollection;
        for(int i = 0; i<3; i++) selEvnt[i] = saveManager.gameData.map.selEvent[i];
    }

    void CommonSetting()    // ���� ����
    {
        // hero ü�� ��������
        CurrentDayText.text = curDay + " / 30";
        if (curDay == 0)
            MoneyText.text = "- ��� : 0";
        else
            MoneyText.text = "- ��� : " + goodsCollection.gold;

        if (newSet == true)
        {
            if ((selEvnt[0] != 0) && (selEvnt[0] != 1)) // �Ϲ����� ��Ȳ(�̺�Ʈ or ����)�� �ƴ϶��
                curTrack = 0;   // �ܱ�
            else curTrack = Random.Range(0, 2) + 1; // �Ϲ����� ��Ȳ(�̺�Ʈ or ����)�̸� �ΰ������ �������� ����
        }
    }

    void NextEventChoice()  // 0 : ����, 1 : �̺�Ʈ, 2 : �߰� ����, 3 : ����, 4 : ����, 5 : ���� ����
    {
        if (newSet == true)
        {
            for (int i = 0; i < 3; i++) // ��,�������� ����(����, �̺�Ʈ) ����
            {
                selEvnt[i] = Random.Range(0, 2);
                saveManager.gameData.map.selEvent[i] = selEvnt[i];  // ������ ����
            }

            // Ư�� ��Ȳ
            if (curDay >= 10 && midBossCheck1 == false)    // 1�� �߰� ���� ����
            {
                selEvnt[0] = 2;
                curTrack = 0;
                saveManager.gameData.map.curTrack = 0;
                saveManager.gameData.map.midBossCheck1 = true;
            }
            else if (curDay >= 15 && villageCheck == false) // ���� ���� 
            {
                selEvnt[0] = 3;
                curTrack = 0;
                saveManager.gameData.map.curTrack = 0;
                saveManager.gameData.map.villageCheck = true;
            }
            else if (curDay >= 20 && midBossCheck2 == false) // 2�� �߰� ���� ����
            {
                selEvnt[0] = 4;
                curTrack = 0;
                saveManager.gameData.map.curTrack = 0;
                saveManager.gameData.map.midBossCheck2 = true;

            }
            else if (curDay >= 29 && villageCheck == true)  // ���� ����
            {
                if (organCheck == false)
                {
                    selEvnt[0] = 5;
                    curTrack = 0;
                    saveManager.gameData.map.curTrack = 0;
                    saveManager.gameData.map.organCheck = true;
                }
                else selEvnt[0] = 6;    // ���� ����
            }

            for (int i = 0; i < 3; i++)
            {
                nextEvent[i] = Random.Range(0, 3);  // ������ ����
                saveManager.gameData.map.nextEvent[i] = nextEvent[i];   // ������ ����
            }
        }
    }

    void OneTrackSet()  // �ܱ� ����
    {
        
        TrackNode[0].SetActive(true);
        
        switch (selEvnt[0] - 2)
        {
            case 0: // �߰� ����
                SelText[0].text = "�߰� ����";
                BattleNodeSet(0);
                break;
            case 1: // ����
                SelText[0].text = "����";
                EventNodeSet(0);
                break;
            case 2:
                SelText[0].text = "�߰� ����";
                BattleNodeSet(0);
                break;

            case 3: // ����
                SelText[0].text = "����";
                EventNodeSet(0);
                break;

            case 4: // ���� ����
                SelText[0].text = "���� ����";
                BattleNodeSet(0);
                break;
        }
    }

    void TwoTrackSet()  // �ΰ����� ����
    {
        TrackNode[1].SetActive(true);
        
        for (int i = 0; i < 2; i++)
        {
            if (curDay == 0 || selEvnt[i] == 0)
            {
                SelText[i + 1].text = "����";
                BattleNodeSet(i);
            }
            else
            {
                SelText[i + 1].text = "�̺�Ʈ";
                EventNodeSet(i);
            }
        }
    }

    void ThreeTrackSet()    // �������� ����
    {
        TrackNode[2].SetActive(true);
        
        for (int i = 0; i < 3; i++)
        {
            if (curDay == 0 || selEvnt[i] == 0)
            {
                SelText[i + 3].text = "����";
                BattleNodeSet(i);
            }
            else
            {
                SelText[i + 3].text = "�̺�Ʈ";
                EventNodeSet(i);
            }
        }
    }

    void BattleNodeSet(int btn)
    {
        switch (curStage)
        {
            case 0: // ��������1
                if (curTrack == 1) // 2Ʈ��
                {
                    if (btn == 0) S1T2B1[nextEvent[btn] + 3 * (curDay / 5)].gameObject.SetActive(true); // ��ư1
                    else S1T2B2[nextEvent[btn] + 3 * (curDay / 5)].gameObject.SetActive(true);  // ��ư2
                }
                else if (curTrack == 2)   // 3Ʈ��
                {
                    if (btn == 0) S1T3B1[nextEvent[btn] + 3 * (curDay / 5)].gameObject.SetActive(true); // ��ư1
                    else if (btn == 1) S1T3B2[nextEvent[btn] + 3 * (curDay / 5)].gameObject.SetActive(true);    // ��ư2
                    else S1T3B3[nextEvent[btn] + 3 * (curDay / 5)].gameObject.SetActive(true);  // ��ư3
                }
                else // 1Ʈ��
                    S1T1B1[nextEvent[btn] + 3 * (selEvnt[0] / 2 - 1)].gameObject.SetActive(true);    // 0~2 : 1�� �߰� / 3~5 : 2�� �߰� / 6~8 : 3�� �߰�
                break;

            case 1: // 2��������
                break;

            case 2: // 3��������
                break;
        }
    }

    void EventNodeSet(int btn)
    {
        switch (curStage)
        {
            case 0: // ��������1
                if (curTrack == 1) // 2Ʈ��
                {
                    if (btn == 0) EventNode[2].gameObject.SetActive(true);  // ��ư1
                    else EventNode[3].gameObject.SetActive(true); // ��ư2
                }
                else if (curTrack == 2)   // 3Ʈ��
                {
                    if (btn == 0) EventNode[4].gameObject.SetActive(true);
                    else if (btn == 1) EventNode[5].gameObject.SetActive(true);
                    else EventNode[6].gameObject.SetActive(true);
                }
                else // 1Ʈ��
                {
                    if (selEvnt[0] == 3) EventNode[0].gameObject.SetActive(true);
                    else EventNode[1].gameObject.SetActive(true);
                }
                break;

            case 1: // 2��������
                break;

            case 2: // 3��������
                break;
        }
    }

    public void Button1()
    {
        selectNum = 1;
        InfoPanel.SetActive(true);
        if (curTrack == 0) // 1Ʈ��
        {
            switch (selEvnt[0] - 2)
            {
                case 0: // 1�� �߰� ����
                    Button1_1[nextEvent[0]].SetActive(true);
                    Title[0].gameObject.SetActive(true);
                    break;

                case 1: // ����
                    Button1_1[9].SetActive(true);
                    break;

                case 2: // 2�� �߰� ����
                    Button1_1[nextEvent[0] + 3].SetActive(true);
                    Title[0].gameObject.SetActive(true);
                    break;

                case 3: // ����
                    Button1_1[10].SetActive(true);
                    break;

                case 4: // ���� ����
                    Button1_1[nextEvent[0] + 6].SetActive(true);
                    Title[0].gameObject.SetActive(true);
                    break;
            }
        }

        else if (curTrack == 1) // 2Ʈ��
        {
            if (curDay == 0 || selEvnt[0] == 0)
            {
                Button2_1[nextEvent[0] + 3 * (curDay / 5)].SetActive(true);
                Title[0].SetActive(true);
            }
            else
            {
                EventButton[0].SetActive(true);
                Title[1].SetActive(true);
            }
        }
        else    // 3Ʈ��
        {
            if (curDay == 0 || selEvnt[0] == 0)
            {
                Button3_1[nextEvent[0] + 3 * (curDay / 5)].SetActive(true);
                Title[0].SetActive(true);
            }
            else
            {
                EventButton[2].SetActive(true);
                Title[1].SetActive(true);
            }
        }
    }

    public void Button2()
    {
        selectNum = 2;
        InfoPanel.SetActive(true);
        if (curTrack == 1) // 2_2
        {
            if (curDay == 0 || selEvnt[1] == 0)
            {
                Button2_2[nextEvent[1] + 3 * (curDay / 5)].SetActive(true);
                Title[0].SetActive(true);
            }
            else
            {
                EventButton[1].SetActive(true);
                Title[1].SetActive(true);
            }
        }
        else    // 3_2
        {
            if (curDay == 0 || selEvnt[1] == 0)
            {
                Button3_2[nextEvent[1] + 3 * (curDay / 5)].SetActive(true);
                Title[0].SetActive(true);
            }
            else
            {
                EventButton[3].SetActive(true);
                Title[1].SetActive(true);
            }
        }
    }

    public void Button3()
    {
        selectNum = 3;            
        InfoPanel.SetActive(true);
        if (curDay == 0 || selEvnt[2] == 0)
        {
            Button3_3[nextEvent[2] + 3 * (curDay / 5)].SetActive(true);
            Title[0].SetActive(true);
        }
        else
        {
            EventButton[4].SetActive(true);
            Title[1].SetActive(true);
        }
    }

    public void InfoPanelClose()
    {
        
        for(int i = 0; i<18; i++)
        {
            if (i < 2) Title[i].SetActive(false);
            if (i < 11) Button1_1[i].SetActive(false);
            Button2_1[i].SetActive(false);
            Button2_2[i].SetActive(false);
            Button3_1[i].SetActive(false);
            Button3_2[i].SetActive(false);
            Button3_3[i].SetActive(false);
        }
        InfoPanel.gameObject.SetActive(false);
    }

    public void MorePanelClose()
    {
        MorePanel.SetActive(false);
    }

    public void MyTeamPanelOpen()
    {
        MyTeamPanel.gameObject.SetActive(true);
    }
    
    public void MyTeamPanelClose()
    {
        MyTeamPanel.gameObject.SetActive(false);
    }
    
    public void GameStart()
    {
        map.level = curDay;
        switch(curTrack)    // curDay �߰�
        {
            case 0:
                if (selEvnt[0] % 2 == 0)
                    saveManager.gameData.map.curDay += 2;
                break;

            case 1: // 2Ʈ��
                if (curDay == 0 || selEvnt[selectNum-1] == 0)
                    saveManager.gameData.map.curDay += 2;
                break;

            case 2: // 3Ʈ��
                if (curDay == 0 || selEvnt[selectNum-1] == 0)
                    saveManager.gameData.map.curDay += 2;
                break;
        }
        InfoPanelClose();
    }
}