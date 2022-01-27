using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampFire : MonoBehaviour
{
    HeroInfo heroInfo;
    [SerializeField]
    Button option1, option2, endBtn;

    public void CampFireOption1()//���� 20���� ȸ��
    {
        heroInfo = GameObject.Find("Hero").GetComponent<HeroInfo>();
        heroInfo.OnHealed(heroInfo.castleData.hp * 0.2f);
        EndEvent();
    }

    public void CampFireOption2()//�ѱ��
    {
        EndEvent();
    }

    void EndEvent()
    {
        option1.interactable = false;
        option2.interactable = false;
        endBtn.gameObject.SetActive(true);
    }
}
