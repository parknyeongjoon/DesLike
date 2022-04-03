using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampFire : MonoBehaviour
{
    HeroInfo heroInfo;
    [SerializeField]
    Button option1, option2, option3, endBtn;
    
    public void CampFireOption1()//���� 20���� ȸ��, 3�� �Ҹ�
    {
        heroInfo = GameObject.Find("Hero").GetComponent<HeroInfo>();
        heroInfo.OnHealed(heroInfo.castleData.hp * 0.2f);
        EndEvent();
    }

    public void CampFireOption2()//���� 10���� ȸ��, 2�� �Ҹ�
    {
        heroInfo = GameObject.Find("Hero").GetComponent<HeroInfo>();
        heroInfo.OnHealed(heroInfo.castleData.hp * 0.1f);
        EndEvent();
    }

    public void CampFireOption3()//�ѱ��, 1�� �Ҹ�
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
