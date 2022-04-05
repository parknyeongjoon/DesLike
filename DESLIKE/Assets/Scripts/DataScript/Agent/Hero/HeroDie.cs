using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class HeroDie : MonoBehaviour
{
    public void GameOver()
    {
        Debug.Log("Game Over");
        //Destroy(gameObject);
        SaveManager.Instance.gameData.canContinue = false;
        SceneManager.LoadScene("MainTitle");//â ���� �ű⼭ ��ư Ŭ�� �� �̵��ϰ� �����
        GameManager.Instance.gameOverEvent.Invoke();
    }
}
