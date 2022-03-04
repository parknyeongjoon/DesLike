using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thief : MonoBehaviour
{
    SaveManager saveManager;

    GoodsCollection goodsCollection;
    int level;

    [SerializeField]
    Button option1, option2, option3, endBtn;

    void OnEnable()
    {
        saveManager = SaveManager.Instance;
        level = saveManager.gameData.map.level;
        goodsCollection = saveManager.gameData.goodsCollection;

        Debug.Log("Thief");
    }

    public void ThiefOption1()//ȭ�� ����
    {
        EndEvent();
        goodsCollection.food -= 50 * level;
        if(goodsCollection.food < 0)
        {
            goodsCollection.food = 0;
        }
        //�� �Ҵ� ����Ʈ
        Debug.Log("Thief 1");
    }

    public void ThiefOption2()//�� ���̱�
    {
        EndEvent();
        GameObject.Find("Hero").GetComponent<HeroInfo>().OnDamaged(15 + level * 0.5f);
        // �� ���̴� ����Ʈ
        Debug.Log("Thief 2");
    }

    public void ThiefOption3()//Ȯ���� ���ϱ� �� ���ҽ� 1 + 2
    {
        EndEvent();
        int randomInt = Random.Range(0, 100);
        if (randomInt >= 50)
        {
            //�������� ����Ʈ
        }
        else
        {
            ThiefOption1();
            ThiefOption2();
        }
        Debug.Log("Thief 3");
    }

    void EndEvent()
    {
        option1.interactable = false;
        option2.interactable = false;
        option3.interactable = false;
        endBtn.gameObject.SetActive(true);
    }
}
