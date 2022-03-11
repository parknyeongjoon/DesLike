using UnityEngine;
using UnityEngine.UI;

public class SelectMap : MonoBehaviour
{
    public GameObject[] Track = new GameObject[3];
    [SerializeField] Text HPText, CurrentDayText, MoneyText;
    [SerializeField] Text[] SelText = new Text[6]; // 0 : 1_1 / 1,2 : 2_1, 2 / 3, 4, 5 = 3_1, 2, 3 
    GameObject hero;
    HeroInfo heroInfo;

    int curDay = 0; // ���� ��¥
    bool midBossCheck = false, villageCheck = false, organCheck = false; // �߰� ����, ����, ���� ����
    int[] selDay = new int[3];
    int[] selEvnt = new int[6]; // 0 : 1_1 / 1, 2 : 2_1, 2 / 3, 4, 5 : 3_1, 2, 3
    int nodeNum;   // ��� ������(selEvnt[nodeNum]��)
    int curTrack = 0;

    [SerializeField] GameObject MyTeamPanel, MorePanel;
    [SerializeField] GameObject InfoPanel;
    [SerializeField] GameObject[] TrackNode = new GameObject[3];
    [SerializeField] GameObject[] StartBtn = new GameObject[14];
    [SerializeField] GameObject[] Nodes = new GameObject[14];
    /*
    - 0~3 => �ܱ� { 0 : �߰� ����; 1 : ����; 2 : ����; 3 : ���� ���� }
    - 4~7 => �ΰ����� { 4,5 : 2_1, 2 ����; 6, 7 : 2_1, 2 �̺�Ʈ }
    - 8~13 => �������� { 8, 9, 10 : 3_1, 2, 3 ����; 11, 12, 12 : 3_1, 2, 3 �̺�Ʈ }
    */
    

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
      
        if ((selEvnt[0] != 0) && (selEvnt[0] != 1)) // �Ϲ����� ��Ȳ(�̺�Ʈ or ����)�� �ƴ϶��
            curTrack = 0;   // �ܱ�
        else curTrack = Random.Range(0, 2) + 1; // �Ϲ����� ��Ȳ(�̺�Ʈ or ����)�̸� �ΰ������ �������� ����

        for (int i = 0; i < 3; i++)
            Track[i].SetActive(false);

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
        Track[curTrack].SetActive(true);
    }

    void ObjectInactive()   // �� �ʱ�ȭ
    {
        InfoPanel.gameObject.SetActive(false);
        for (int i = 0; i < 14; i++)
        {
            if (i < 3) TrackNode[i].SetActive(false);  // �� ���
            Nodes[i].SetActive(false);
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
        // hero ü�� ��������
        NextEventChoice(); // ���� ���� ����
        CurrentDayText.text = curDay + " / 30";
        if (curDay == 0)
            MoneyText.text = "- �ķ� : 0 \n- ��� : 0";
        else
            MoneyText.text = "- �ķ� : " + goodsCollection.food + "\n- ��� : " + goodsCollection.gold;
    }
    
    void NextEventChoice()  // 0 : ����, 1 : �̺�Ʈ, 2 : �߰� ����, 3 : ����, 4 : ����, 5 : ���� ����
    {
        for (int i = 1; i < 6; i++) // ��,�������� ����(����, �̺�Ʈ) ����
            selEvnt[i] = Random.Range(0, 2);
        
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

   
    void OneTrackSet()  // �ܱ� ����
    {
        TrackNode[0].SetActive(true);

        switch (selEvnt[0] - 2)
        {
            case 0: // �߰� ����
                SelText[0].text = "�߰� ����";
                Nodes[0].SetActive(true);
                // �߰� ���� ������ ����
                break;
            case 1: // ����
                SelText[0].text = "����";
                Nodes[1].SetActive(true);
                // ���� ������ ����
                break;
            case 2: // ����
                SelText[0].text = "����";
                Nodes[2].SetActive(true);
                // ���� ������ ����
                break;
            case 3: // ���� ����
                SelText[0].text = "���� ����";
                Nodes[3].SetActive(true);
                // ���� ���� ������ ����
                break;
            default:
                SelText[0].text = "����";
                break;
        }
    }

    void TwoTrackSet()  // �ΰ����� ����
    {
        TrackNode[1].SetActive(true);

    for (int i = 0; i < 2; i++)
        {
            if (curDay == 0 || selEvnt[i+1] == 0)
            {
                SelText[i + 1].text = "����";
                Nodes[i+4].SetActive(true);
                // ���� ������ ����
            }
            else
            {
                SelText[i + 1].text = "�̺�Ʈ";
                Nodes[i + 6].SetActive(true);
                // �̺�Ʈ ������ ����
            }
        }
    }

    void ThreeTrackSet()    // �������� ����
    {
        TrackNode[2].SetActive(true);

        for (int i = 0; i < 3; i++)
        {
            int j = i + 3;
            if (curDay == 0 || selEvnt[i] == 0)
            {
                SelText[i + 3].text = "����";
                Nodes[i + 8].SetActive(true);
                // ���� ������ ����
            }
            else
            {
                SelText[i + 3].text = "�̺�Ʈ";
                Nodes[i + 11].SetActive(true);
                // �̺�Ʈ ������ ����
            }
        }
    }

    /* 
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
    */

    public void Button1()
    {
        switch(curTrack)
        {
            case 0: // 1_1
                switch(selEvnt[0])
                {
                    case 0:
                        nodeNum = 0;    // �߰� ����
                        break;

                    case 1:
                        nodeNum = 1;    // ����
                        break;

                    case 2:
                        nodeNum = 2;    // ���� ��� �߰� �ʿ�
                        break;

                    case 3:
                        nodeNum = 3;    // ���� ����
                        break;

                    default:
                        Debug.Log("���� �߻�");
                        break;
                }
                break;

            case 1: // 2_1
                if(selEvnt[1] == 0 || curDay == 0)
                    nodeNum = 4;
                else nodeNum = 6;
                break;

            case 2: // 3_1
                if (selEvnt[3] == 0 || curDay == 0)
                    nodeNum = 8;
                else nodeNum = 11;
                break;

            default:
                Debug.Log("���� �߻�");
                break;
        }
        Nodes[nodeNum].gameObject.SetActive(true);
        InfoPanel.gameObject.SetActive(true);
        StartBtn[nodeNum].gameObject.SetActive(true);
    }

    public void Button2()
    {
        switch (curTrack)
        {   
            case 1: // 2_2
                if (selEvnt[2] == 0 || curDay == 0)
                    nodeNum = 5;
                else nodeNum = 7;
                break;
                
            case 2: // 3_2
                if (selEvnt[4] == 0 || curDay == 0)
                    nodeNum = 9;
                else nodeNum = 12;
                break;
        }
        Nodes[nodeNum].gameObject.SetActive(true);
        InfoPanel.gameObject.SetActive(true);
        StartBtn[nodeNum].gameObject.SetActive(true);
    }


    public void Button3()
    {
        if(selEvnt[5] == 0)
            nodeNum = 10;    // ����
        else nodeNum = 13;    // �̺�Ʈ
        Nodes[nodeNum].gameObject.SetActive(true);
        InfoPanel.gameObject.SetActive(true);
        StartBtn[nodeNum].gameObject.SetActive(true);
    }

    public void InfoPanelClose()
    {
        StartBtn[nodeNum].SetActive(false);   // �̺�Ʈ ���
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
        if (nodeNum >= 0 && nodeNum <=3)
            saveManager.gameData.map.curDay += 1;  // Ư�� �̺�Ʈ 1�� �߰�
        else if (nodeNum <= 10)
            if(nodeNum !=6 && nodeNum !=7)
                saveManager.gameData.map.curDay += 2;  // ���� �̺�Ʈ 2�� �߰�
        InfoPanelClose();
    }
}