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

    public void ThiefOption1()//È­Æó ¶â±â±â
    {
        EndEvent();
        goodsCollection.food -= 50 * level;
        if(goodsCollection.food < 0)
        {
            goodsCollection.food = 0;
        }
        //µ· ÀÒ´Â ÀÌÆåÆ®
        Debug.Log("Thief 1");
    }

    public void ThiefOption2()//ÇÇ ±ïÀÌ±â
    {
        EndEvent();
        GameObject.Find("Hero").GetComponent<HeroInfo>().OnDamaged(15 + level * 0.5f);
        // ÇÇ ±ïÀÌ´Â ÀÌÆåÆ®
        Debug.Log("Thief 2");
    }

    public void ThiefOption3()//È®·ü·Î ÇÇÇÏ±â ¸ø ÇÇÇÒ½Ã 1 + 2
    {
        EndEvent();
        int randomInt = Random.Range(0, 100);
        if (randomInt >= 50)
        {
            //µµ¸Á°¡´Â ÀÌÆåÆ®
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
