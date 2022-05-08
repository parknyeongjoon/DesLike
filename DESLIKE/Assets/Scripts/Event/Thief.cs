using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thief : MonoBehaviour
{
    SaveManager saveManager;
    int level;

    [SerializeField]
    Button option1, option2, option3, endBtn;

    void OnEnable()
    {
        saveManager = SaveManager.Instance;
        level = saveManager.map.level;

        Debug.Log("Thief");
    }

    public void ThiefOption1()//È­Æó ¶â±â±â 1ÀÏ ¼Ò¸ð
    {
        EndEvent();
        saveManager.gameData.goodsSaveData.gold -= 50 * level;
        if (saveManager.gameData.goodsSaveData.gold < 0)
        {
            saveManager.gameData.goodsSaveData.gold = 0;
        }
        //µ· ÀÒ´Â ÀÌÆåÆ®
    }

    public void ThiefOption2()//ÇÇ ±ïÀÌ±â
    {
        EndEvent();
        GameObject.Find("Hero").GetComponent<HeroInfo>().OnDamaged(null, 15 + level * 0.5f);//¹Ù²ã¾ßÇÔ, gameData¿¡ heroSaveData¿¡¼­ ±î¾ßÇÒ µí
        // ÇÇ ±ïÀÌ´Â ÀÌÆåÆ®
    }

    public void ThiefOption3()//È®·ü·Î ÇÇÇÏ±â ¸ø ÇÇÇÒ½Ã 1 + 2
    {
        EndEvent();
    }

    void EndEvent()
    {
        option1.interactable = false;
        option2.interactable = false;
        option3.interactable = false;
        endBtn.gameObject.SetActive(true);
    }
}
