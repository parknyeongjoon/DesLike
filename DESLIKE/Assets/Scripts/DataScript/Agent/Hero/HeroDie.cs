using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class HeroDie : MonoBehaviour
{
    public void GameOver()
    {
        gameObject.layer = 7;
        Debug.Log("Game Over");
        //Destroy(gameObject);
        SaveManager.Instance.gameData.canContinue = false;
        AkSoundEngine.PostEvent("H_23101_Die", gameObject);
        Invoke("GamePause", 2.0f);
        //SceneManager.LoadScene("MainTitle");//â ���� �ű⼭ ��ư Ŭ�� �� �̵��ϰ� �����
        GameManager.Instance.gameOverEvent.Invoke();
    }

    void GamePause()
    {
        GameManager.Instance.GamePause(false);
    }
}
