using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rullet : MonoBehaviour
{
    [SerializeField]
    Button rollBtn, endBtn;
    [SerializeField]
    Text rollBtn_Text, EndButton;
    [SerializeField]
    RectTransform rulletTransform;

    GoodsCollection goodsCollection;
    Map map;

    int eventCount = 8;
    bool isRoll;

    void OnEnable()
    {
        isRoll = false;
        goodsCollection = SaveManager.Instance.gameData.goodsCollection;
        map = SaveManager.Instance.gameData.map;
        EndButton.text = "¸ÊÀ¸·Î\n(ÃÑ 1ÀÏ ¼Ò¸ð)";
        Debug.Log("Rullet");
    }

    public void RollBtn()//µ¹¸®°Å³ª ¸ØÃß±â
    {
        isRoll = !isRoll;
        if (isRoll)
        {
            rollBtn_Text.text = "¸ØÃã";
            StartCoroutine(RollRullet());
        }
        else
        {
            rollBtn.interactable = false;
        }
    }

    IEnumerator RollRullet()//·ê·¿ µ¹¸®´Â ÇÔ¼ö
    {
        float rotateAmount = 0;
        float rotateSpeed = 700;

        SaveManager saveManager = SaveManager.Instance;
        saveManager.gameData.map.curDay += 1;

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
        // endBtn.gameObject.SetActive(true);
        EndButton.text = "¸ÊÀ¸·Î";
    }

    void GiveReward(float rotateAmount)
    {
        rotateAmount = rotateAmount % 360;
        rotateAmount = rotateAmount / (360 / eventCount);
        
        switch ((int)rotateAmount)
        {
            case 0:
                Debug.Log("0, °ñµå+");
                goodsCollection.gold += 30 * map.level;
                break;
            case 1:
                Debug.Log("1, À¯¹É");

                break;
            case 2:
                Debug.Log("2, ²Î");
                break;
            case 3:
                Debug.Log("3, °ñµå-");
                goodsCollection.gold -= 20 * map.level;
                break;
            case 4:
                Debug.Log("4, °ñµå++");
                goodsCollection.gold += 50 * map.level;
                break;
            case 5:
                Debug.Log("5, ²Î");
                break;
            case 6:
                Debug.Log("6, ¸¶¼®");
                goodsCollection.magicalStone += (int)(1 + map.level * 0.5f);
                break;
            case 7:
                Debug.Log("7, ¹öÇÁ");
                break;
        }
    }

    public void EndEvent()
    {
        rollBtn.interactable = false;
        // endBtn.gameObject.SetActive(true);
        SaveManager saveManager = SaveManager.Instance;
        saveManager.gameData.map.curDay += 1;

    }
}
