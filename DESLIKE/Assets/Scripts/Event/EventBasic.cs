using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EventBasic : MonoBehaviour
{
    // public HeroInfo heroInfo;
    // public HeroData heroData;
    public Map map;
    public EventNode eventNode;
    public Button EndButton;
    public Button[] Buttons = new Button[3];
    public TMP_Text[] OptionText = new TMP_Text[3];
    public EventData eventData;
    SaveManager saveManager;

    public bool eventEnd;
    public int curDay, curGold, curStage;
    public int[] optionNum = new int[3];
    public float cur_HP;
    public bool isEventSet; // set : ������ ���� �� �� �̺�Ʈ�� ó������ üũ
    public bool isAlreadySelect; // �̹� �̺�Ʈ �����ߴ��� �Լ�(�̺�Ʈ ���������)

    // 1. relic : optionNum[0, 1, 2], isEventSet, isAlreadySelect
    // 2. heal : isAlreadySelect, isEventSet
    // 3. ����ȭ��
    public int[] areaGolds = new int[3];
    public int areaGold;
    // 4. �г�Ƽ
    public int ranPenalty;  // 0 : ü��, 1 : ��Ʈ, 2 : Ưȭ �ս�, 3, ��� �ս�
    // 5. ����
    public int[] relicLevelCount = new int[3];
    public int[] randRelic = new int[6];   // ��Ϻ� �����ѹ�
    public int[] relicPrice = new int[6];  // ��Ϻ� ����
    public bool isNewSet;
    public bool[] isSoldOut = new bool[6];
    public int gold;
    // 6. ����
    public bool[] stepCheck = new bool[5];
    public int[] choiNum = new int[3];  // 0 : 1��° ������(���, ü��, X), 1 : 2��° ������(�׼�), 2 : ���� ����
    public int rewardNum, comBox;  // �������� ����ϴ� ���/ü���� �׼�

    // 7. ����

    void Awake()
    {
        saveManager = SaveManager.Instance;
        DataLoad();
    }

    void DataLoad()
    {
        cur_HP = saveManager.gameData.heroSaveData.cur_Hp;
        curDay = saveManager.gameData.mapData.curDay;
        curStage = saveManager.gameData.mapData.curStage;
        gold = saveManager.gameData.goodsSaveData.gold;
        eventEnd = saveManager.gameData.mapData.eventEnd;
        eventNode = (EventNode)map.curMapNode;

        if (eventEnd == false)
        {
            // heroData = (HeroData)heroInfo.castleData;
            isEventSet = saveManager.gameData.eventData.isEventSet;
            isAlreadySelect = saveManager.gameData.eventData.isAlreadySelect;
            ranPenalty = saveManager.gameData.eventData.ranPenalty;

            isNewSet = saveManager.gameData.villageData.isNewSet;
            rewardNum = saveManager.gameData.eventData.rewardNum;
            comBox = saveManager.gameData.eventData.comBox;

            for (int i = 0; i < 6; i++)
            {
                if (i < 3)
                {
                    optionNum[i] = saveManager.gameData.eventData.optionNum[i];
                    areaGolds[i] = saveManager.gameData.eventData.areaGold[i];
                    choiNum[i] = saveManager.gameData.eventData.choiNum[i];
                }
                if (i < 5) stepCheck[i] = saveManager.gameData.eventData.stepCheck[i];
                randRelic[i] = saveManager.gameData.villageData.randRelic[i];
                relicPrice[i] = saveManager.gameData.villageData.relicPrice[i];
                isSoldOut[i] = saveManager.gameData.villageData.isSoldOut[i];
            }
        }
    }

    public void SaveStepCheck(int i)
    {
        saveManager.gameData.eventData.stepCheck[i] = stepCheck[i];
    }

    public void SaveData()
    {
        // struct�� ����
        saveManager.gameData.heroSaveData.cur_Hp = cur_HP;
        saveManager.gameData.eventData.isEventSet = isEventSet;
        saveManager.gameData.mapData.curDay = curDay;
        saveManager.gameData.mapData.curStage = curStage;
        saveManager.gameData.eventData.isAlreadySelect = isAlreadySelect;
        saveManager.gameData.goodsSaveData.gold = curGold;
        saveManager.gameData.eventData.ranPenalty = ranPenalty;
        saveManager.gameData.mapData.eventEnd = eventEnd;

        saveManager.gameData.villageData.isNewSet = isNewSet;
        saveManager.gameData.goodsSaveData.gold = gold;
        saveManager.gameData.eventData.rewardNum = rewardNum;
        saveManager.gameData.eventData.comBox = comBox;

        for (int i = 0; i < 6; i++)
        {
            if (i < 3)
            {
                saveManager.gameData.eventData.optionNum[i] = optionNum[i];
                saveManager.gameData.eventData.areaGold[i] = areaGolds[i];
                saveManager.gameData.eventData.choiNum[i] = choiNum[i];
            }
            saveManager.gameData.villageData.randRelic[i] = randRelic[i];
            saveManager.gameData.villageData.relicPrice[i] = relicPrice[i];
            saveManager.gameData.villageData.isSoldOut[i] = isSoldOut[i];
        }

        saveManager.SaveGameData();
    }

    void ButtonOnOff()
    {
        if (isAlreadySelect == false)
        {
            for (int i = 0; i < 3; i++)
                Buttons[i].gameObject.SetActive(true);
            EndButton.gameObject.SetActive(false);
        }
        else
        {
            for (int i = 0; i < 3; i++)
                Buttons[i].gameObject.SetActive(false);
            EndButton.gameObject.SetActive(true);
        }
    }

    public void EndEvent()
    {
        saveManager.gameData.eventData = new EventData();
        saveManager.gameData.mapData.eventEnd = true;
        SceneManager.LoadScene("Map");
    }
}
