using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class HeroDie : MonoBehaviour
{
    public void GameOver()
    {
        GetComponent<HeroInfo>().skeletonAnimation.state.SetAnimation(0, "H_23101_Die", false);
        gameObject.layer = 7;
        Debug.Log("Game Over");
        SaveManager.Instance.gameData.canContinue = false;
        AkSoundEngine.PostEvent("H_23101_Die", gameObject);
        Invoke("GamePause", 2.0f);
        //SceneManager.LoadScene("MainTitle");//창 띄우고 거기서 버튼 클릭 시 이동하게 만들기
        GameManager.Instance.gameOverEvent.Invoke();
    }

    void GamePause()
    {
        GameManager.Instance.GamePause(false);
    }
}
