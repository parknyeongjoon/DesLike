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
    bool midBossCheck1 = false, midBossCheck2 = false, villageCheck = false, organCheck = false, isContinue; // �߰� ����, ����, ���� ����
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
    bool[] isChallenge = new bool[3];

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
        for (int i = 0; i < 6; i++)
        {
            if (i < 3)
            {
                TrackNode[i].SetActive(false);  // �� ���
                S1T1B1[i].gameObject.SetActive(false);
                saveManager.gameData.mapData.isEventSet[i] = false;
            }
            S1T2B1[i].gameObject.SetActive(false);
            S1T2B2[i].gameObject.SetActive(false);
            S1T3B1[i].gameObject.SetActive(false);
            S1T3B2[i].gameObject.SetActive(false);
            S1T3B3[i].gameObject.SetActive(false);
        }
    }

    void FindData() // �ܺ� ������ ��������
    {

        curDay = saveManager.gameData.mapData.curDay;
        isContinue = saveManager.gameData.mapData.isContinue;
        if (curDay != saveManager.gameData.mapData.checkDay || curDay == 0)    // �̹� ���� �ʿ� ó�� �´ٸ�
        {
            newSet = true;
            saveManager.gameData.mapData.checkDay = curDay;
        }
        curStage = saveManager.gameData.mapData.curStage;
        curTrack = saveManager.gameData.mapData.curTrack;
        nextEvent = saveManager.gameData.mapData.nextEvent;
        midBossCheck1 = saveManager.gameData.mapData.midBossCheck1;
        midBossCheck2 = saveManager.gameData.mapData.midBossCheck2;
        villageCheck = saveManager.gameData.mapData.villageCheck;
        organCheck = saveManager.gameData.mapData.organCheck;
        goodsCollection = saveManager.gameData.goodsCollection;
        for (int i = 0; i < 3; i++)
        {
            selEvnt[i] = saveManager.gameData.mapData.selEvent[i];
            isChallenge[i] = saveManager.gameData.mapData.isChallenge[i];
        }
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
            int cRandom;
            for (int i = 0; i < 3; i++) // ��,�������� ����(����, �̺�Ʈ) ����
            {
                selEvnt[i] = Random.Range(0, 2);
                saveManager.gameData.mapData.selEvent[i] = selEvnt[i];  // ������ ����

                isChallenge[i] = false; // �ʱ�ȭ
                cRandom = Random.Range(0, 5);   // 20�� Ȯ��
                if (cRandom == 4)   // ������� ����
                {
                    isChallenge[i] = true;
                    saveManager.gameData.mapData.isChallenge[i] = isChallenge[i];   // ������ ����
                    Debug.Log( i+1 + "������� Ȱ��ȭ");
                }
            }

            // Ư�� ��Ȳ
            if (curDay >= 10 && midBossCheck1 == false)    // 1�� �߰� ���� ����
            {
                selEvnt[0] = 0;
                curTrack = 0;
                saveManager.gameData.mapData.curTrack = 0;
                saveManager.gameData.mapData.midBossCheck1 = true;
            }
            else if (curDay >= 15 && villageCheck == false) // ���� ���� 
            {
                selEvnt[0] = 3;
                curTrack = 0;
                saveManager.gameData.mapData.curTrack = 0;
                saveManager.gameData.mapData.villageCheck = true;
            }
            else if (curDay >= 20 && midBossCheck2 == false) // 2�� �߰� ���� ����
            {
                selEvnt[0] = 1;
                curTrack = 0;
                saveManager.gameData.mapData.curTrack = 0;
                saveManager.gameData.mapData.midBossCheck2 = true;

            }
            else if (curDay >= 29 && villageCheck == true)  // ���� ����
            {
                Debug.Log("29�̻�");
                if (organCheck == false)
                {
                    selEvnt[0] = 4;
                    curTrack = 0;
                    saveManager.gameData.mapData.curTrack = 0;
                    saveManager.gameData.mapData.organCheck = true;
                }
                else
                {
                    Debug.Log("��������");
                    selEvnt[0] = 2;    // ���� ����
                    saveManager.gameData.mapData.curTrack = 0;
                }
            }
        }
    }

    void OneTrackSet()  // �ܱ� ����
    {
        TrackNode[0].SetActive(true);
        
        switch (selEvnt[0])
        {
            case 0: // 1�� �߰� ����
                SelText[0].text = "�߰� ����";
                BattleNodeSet(0);
                break;
            case 1: // 2�� �߰� ����
                SelText[0].text = "�߰� ����";
                BattleNodeSet(0);
                break;

            case 2: // ���� ����
                SelText[0].text = "���� ����";
                BattleNodeSet(0);
                break;

            case 3: // ����
                SelText[0].text = "����";
                EventNodeSet(0);
                break;

            case 4: // ����
                SelText[0].text = "����";
                EventNodeSet(0);
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
                if (isChallenge[i] == true) Debug.Log("�������" + i + " ��Ȱ��ȭ");
                isChallenge[i] = false;
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
                if (isChallenge[i] == true) Debug.Log("�������" + i + " ��Ȱ��ȭ");
                isChallenge[i] = false;
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
                    if (btn == 0)
                    {
                        if (isChallenge[0] == true) S1T2B1[(curDay / 5) + 1].gameObject.SetActive(true);
                        else S1T2B1[(curDay / 5)].gameObject.SetActive(true); // ��ư1
                    }
                    else
                    {
                        if (isChallenge[1] == true) S1T2B2[(curDay / 5) + 1].gameObject.SetActive(true);
                        else S1T2B2[(curDay / 5)].gameObject.SetActive(true); // ��ư2
                    }
                }
                else if (curTrack == 2)   // 3Ʈ��
                {
                    if (btn == 0)
                    {
                        if (isChallenge[0] == true) S1T3B1[(curDay / 5) + 1].gameObject.SetActive(true);
                        else S1T3B1[(curDay / 5)].gameObject.SetActive(true); // ��ư1
                    }
                    else if (btn == 1)
                    {
                        if (isChallenge[1] == true) S1T3B2[(curDay / 5) + 1].gameObject.SetActive(true);
                        else S1T3B2[(curDay / 5)].gameObject.SetActive(true); // ��ư2
                    }
                    else
                    {
                        if (isChallenge[2] == true) S1T3B3[(curDay / 5) + 1].gameObject.SetActive(true);
                        else S1T3B3[(curDay / 5)].gameObject.SetActive(true); // ��ư3
                    }
                }
                else // 1Ʈ��
                    S1T1B1[selEvnt[0]].gameObject.SetActive(true);    // 0 : 1�� �߰� / 1 : 2�� �߰� / 2 : ����
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
                    Button1_1[0].SetActive(true);
                    Title[0].gameObject.SetActive(true);
                    break;

                case 1: // ����
                    // EventButton[0].SetActive(true);
                    break;

                case 2: // 2�� �߰� ����
                    Button1_1[1].SetActive(true);
                    Title[0].gameObject.SetActive(true);
                    break;

                case 3: // ����
                    // EventButton[1].SetActive(true);
                    break;

                case 4: // ���� ����
                    Button1_1[2].SetActive(true);
                    Title[0].gameObject.SetActive(true);
                    break;
            }
        }

        else if (curTrack == 1) // 2Ʈ��
        {
            if (curDay == 0 || selEvnt[0] == 0)
            {
                if (isChallenge[0] == true) Button2_1[(curDay / 5) + 1].SetActive(true);
                else Button2_1[curDay / 5].SetActive(true);
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
                if (isChallenge[0] == true) Button3_1[curDay / 5 + 1].SetActive(true);
                else Button3_1[curDay / 5].SetActive(true);
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
                if (isChallenge[1] == true) Button2_2[curDay / 5 + 1].SetActive(true);
                else Button2_2[curDay / 5].SetActive(true);
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
                if (isChallenge[1] == true) Button3_2[curDay / 5 + 3].SetActive(true);
                else Button3_2[curDay / 5].SetActive(true);
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
            if (isChallenge[2] == true) Button3_3[curDay / 5 + 3].SetActive(true);
            else Button3_3[curDay / 5].SetActive(true);
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
        
        for(int i = 0; i<6; i++)
        {
            if (i < 2) Title[i].SetActive(false);
            if (i < 5) Button1_1[i].SetActive(false);
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
        switch (curTrack)    // curDay �߰�
        {
            case 0:
                if (selEvnt[0] % 2 == 0)
                {
                    if (isChallenge[0] == true) saveManager.gameData.mapData.challengeCount += 1;
                    saveManager.gameData.mapData.curDay += 2;
                }
                break;

            case 1: // 2Ʈ��
                if (curDay == 0 || selEvnt[selectNum - 1] == 0)
                {
                    if (isChallenge[1] == true) saveManager.gameData.mapData.challengeCount += 1;
                    saveManager.gameData.mapData.curDay += 2;
                }
                break;

            case 2: // 3Ʈ��
                if (curDay == 0 || selEvnt[selectNum - 1] == 0)
                {
                    if (isChallenge[2] == true) saveManager.gameData.mapData.challengeCount += 1;
                    saveManager.gameData.mapData.curDay += 2;
                }
                break;
        }
        InfoPanelClose();
    }
}