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
        SceneManager.LoadScene("MainTitle");//창 띄우고 거기서 버튼 클릭 시 이동하게 만들기
        GameManager.Instance.gameOverEvent.Invoke();
    }
}
