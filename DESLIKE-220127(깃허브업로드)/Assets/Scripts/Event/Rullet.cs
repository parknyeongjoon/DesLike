using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Rullet : MonoBehaviour
{
    [SerializeField]
    Button rollBtn, endBtn, backBtn;
    [SerializeField]
    TMP_Text rollBtn_Text, EndButton;
    [SerializeField]
    RectTransform rulletTransform;

    GoodsCollection goodsCollection;
    Map map;
    SaveManager saveManager;

    int eventCount = 8;
    bool isRoll;
    
    void OnEnable()
    {
        SaveManager saveManager = SaveManager.Instance;
        isRoll = false;
        backBtn.gameObject.SetActive(false);
        goodsCollection = saveManager.gameData.goodsCollection;
        map = saveManager.gameData.map;
    }

    public void RollBtn()//�����ų� ���߱�
    {
        isRoll = !isRoll;
        if (isRoll)
        {
            rollBtn_Text.text = "����";
            StartCoroutine(RollRullet());
            backBtn.gameObject.SetActive(true);
        }
        else
        {
            rollBtn.interactable = false;
        }
    }

    IEnumerator RollRullet()//�귿 ������ �Լ�
    {
        float rotateAmount = 0;
        float rotateSpeed = 700;

        while (isRoll)
        {
            float rotateAngle = rotateSpeed * Time.deltaTime;
            rotateAmount += rotateAngle;
            rulletTransform.Rotate(new Vector3(0, 0, rotateAngle));
            yield return null;
        }
        while(rotateSpeed > 10)
        {
            float rotateAngle = rotateSpeed * Time.deltaTime;
            rotateAmount += rotateAngle;
            rulletTransform.Rotate(new Vector3(0, 0, rotateAngle));
            rotateSpeed *= 0.99f;
            yield return null;
        }
        GiveReward(rotateAmount);
    }

    void GiveReward(float rotateAmount)
    {
        rotateAmount = rotateAmount % 360;
        rotateAmount = rotateAmount / (360 / eventCount);
        
        switch ((int)rotateAmount)
        {
            case 0:
                Debug.Log("0, ���+");
                goodsCollection.gold += 30 * map.level;
                break;
            case 1:
                Debug.Log("1, ����");

                break;
            case 2:
                Debug.Log("2, ��");
                break;
            case 3:
                Debug.Log("3, ���-");
                goodsCollection.gold -= 20 * map.level;
                break;
            case 4:
                Debug.Log("4, ���++");
                goodsCollection.gold += 50 * map.level;
                break;
            case 5:
                Debug.Log("5, ��");
                break;
            case 6:
                Debug.Log("6, ����");
                goodsCollection.magicalStone += (int)(1 + map.level * 0.5f);
                break;
            case 7:
                Debug.Log("7, ����");
                break;
        }
    }

    public void EndEvent()
    {
        rollBtn.interactable = false;
    }
}
