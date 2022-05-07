using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class AreaEvent : EventBasic
{
    void OnEnable()
    {
        SetOption();
        isEventSet = true;
        eventEnd = false;
        SaveData();
    }
  
    void SetOption()
    {
        if (isAlreadySelect == false)   // ¿ÃπÃ º±≈√µ«¡ˆ æ æ“¿ª ∂ß∏∏ ºº∆√
        {
            for(int i = 0; i<3; i++)
                Buttons[i].gameObject.SetActive(true);
            EndButton.gameObject.SetActive(false);

            EventOption1();
            EventOption2();
            EventOption3();
        }
        else
        {
            for (int i = 0; i < 3; i++)
                Buttons[i].gameObject.SetActive(false);
            EndButton.gameObject.SetActive(true);
        }
    }

    void EventOption1()    // 1π¯ ∫∏±‚
    {
        if (isEventSet == false)
        {
            areaGolds[0] = 40 + Random.Range(0, 20);
            OptionText[0].text = "1¿œ º“∏, ¡¯øµ»≠∆Û" + areaGold + "»πµÊ";
        }
        else
        {
            // ±‚¡∏ µ•¿Ã≈Õ æ≤±‚
            OptionText[0].text = "1¿œ º“∏, ¡¯øµ»≠∆Û" + areaGold + "»πµÊ";
        }
    }

    void EventOption2()    // 2π¯ ∫∏±‚
    {
        if (isEventSet == false)
        {
            areaGolds[1] = 70 + Random.Range(0, 30);
            OptionText[1].text = "2¿œ º“∏, ¡¯øµ»≠∆Û" + areaGold + "»πµÊ";
        }
        else
        {
            OptionText[1].text = "2¿œ º“∏, ¡¯øµ»≠∆Û" + areaGold + "»πµÊ";
        }
    }

    void EventOption3()    // 3π¯ ∫∏±‚
    {
        if (isEventSet == false)
        {
            areaGolds[2] = 100 + Random.Range(0, 40);
            OptionText[2].text = "3¿œ º“∏, ¡¯øµ»≠∆Û" + areaGold + "»πµÊ";
        }
        else
        {
            OptionText[2].text = "3¿œ º“∏, ¡¯øµ»≠∆Û" + areaGold + "»πµÊ";
        }
    }

    void ButtonsOff()
    {
        for (int i = 0; i < 3; i++)
            Buttons[i].gameObject.SetActive(false);
        EndButton.gameObject.SetActive(true);
    }

    // πˆ∆∞øÎ «‘ºˆ
    public void Button1()
    {
        curDay += 1;
        areaGold += areaGolds[0];
        isAlreadySelect = true;
        SaveData();
        ButtonsOff();
    }

    public void Button2()
    {
        curDay += 2;
        areaGold += areaGolds[1];
        isAlreadySelect = true;
        SaveData();
        ButtonsOff();
    }

    public void Button3()
    {
        curDay += 3;
        areaGold += areaGolds[2];
        isAlreadySelect = true;
        SaveData();
        ButtonsOff();
    }
}
