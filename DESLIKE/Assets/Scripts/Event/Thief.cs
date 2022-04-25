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

    public void ThiefOption1()//ȭ�� ���� 1�� �Ҹ�
    {
        EndEvent();
        saveManager.gameData.goodsSaveData.gold -= 50 * level;
        if (saveManager.gameData.goodsSaveData.gold < 0)
        {
            saveManager.gameData.goodsSaveData.gold = 0;
        }
        //�� �Ҵ� ����Ʈ
    }

    public void ThiefOption2()//�� ���̱�
    {
        EndEvent();
        GameObject.Find("Hero").GetComponent<HeroInfo>().OnDamaged(null, 15 + level * 0.5f);//�ٲ����, gameData�� heroSaveData���� ����� ��
        // �� ���̴� ����Ʈ
    }

    public void ThiefOption3()//Ȯ���� ���ϱ� �� ���ҽ� 1 + 2
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
