using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPanel : MonoBehaviour
{
    SaveManager saveManager;

    [SerializeField]
    TMP_Text money_txt, ally_produce_time, enemy_produce_time, ally_Round_txt, enemy_Round_txt;
    [SerializeField]
    PortDatas ally_ports, enemy_ports;
    GoodsCollection goodsCollection;

    void Awake()
    {
        saveManager = SaveManager.Instance;
        ally_ports = saveManager.gameData.allyPortDatas;
        goodsCollection = saveManager.gameData.goodsCollection;
    }

    void Start()
    {
        money_txt.text = goodsCollection.food.ToString();
        SetRoundText();
        StartCoroutine(SetTimeText());
    }

    public void SetMoneyText()
    {
        money_txt.text = goodsCollection.food.ToString();
    }

    public void SetRoundText()
    {
        if(ally_ports.round >= ally_ports.max_round)
        {
            ally_Round_txt.text = "Fever";
        }
        else
        {
            ally_Round_txt.text = ally_ports.round.ToString() + "/" + ally_ports.max_round.ToString();
        }
        if(enemy_ports.round >= enemy_ports.max_round)
        {
            enemy_Round_txt.text = "Fever";
        }
        else
        {
            enemy_Round_txt.text = enemy_ports.round.ToString() + "/" + enemy_ports.max_round.ToString();
        }
    }

    //좀 건드려보기
    public IEnumerator SetTimeText()
    {
        while (true)
        {
            ally_produce_time.text = ((int)ally_ports.cur_producetime).ToString();
            enemy_produce_time.text = ((int)enemy_ports.cur_producetime).ToString();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
