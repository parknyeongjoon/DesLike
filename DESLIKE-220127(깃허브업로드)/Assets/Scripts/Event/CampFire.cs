using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampFire : MonoBehaviour
{
    HeroInfo heroInfo;
    [SerializeField]
    Button option1, option2, option3, endBtn;

    public void CampFireOption1()//피의 20프로 회복, 3일 소모
    {
        heroInfo = GameObject.Find("Hero").GetComponent<HeroInfo>();
        heroInfo.OnHealed(heroInfo.castleData.hp * 0.2f);
        SaveManager saveManager = SaveManager.Instance;
        saveManager.gameData.map.curDay += 3;
        EndEvent();
    }

    public void CampFireOption2()//피의 10프로 회복, 2일 소모
    {
        heroInfo = GameObject.Find("Hero").GetComponent<HeroInfo>();
        heroInfo.OnHealed(heroInfo.castleData.hp * 0.1f);
        SaveManager saveManager = SaveManager.Instance;
        saveManager.gameData.map.curDay += 2;
        EndEvent();
    }

    public void CampFireOption3()//넘기기, 1일 소모
    {
        SaveManager saveManager = SaveManager.Instance;
        saveManager.gameData.map.curDay += 1;
        EndEvent();
    }

    void EndEvent()
    {
        option1.interactable = false;
        option2.interactable = false;
        endBtn.gameObject.SetActive(true);
    }
}
