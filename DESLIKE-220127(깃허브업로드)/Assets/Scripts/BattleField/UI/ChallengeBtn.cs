using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeBtn : MonoBehaviour
{
    SaveManager saveManager;

    PortsOption challengeOption;

    string prefabPath;

    Dictionary<string, GameObject> soldierPrefabs = new Dictionary<string, GameObject>();
    [SerializeField]
    GameObject challengeObject;

    void Start()
    {
        saveManager = SaveManager.Instance;
        challengeOption = ((BattleNode)saveManager.gameData.map.curMapNode).challengeOption;

        soldierPrefabs.Clear();
        prefabPath = "SoldierPrefabs/Enemy/";
        for (int i = 0; i < challengeOption.soldierOption.Count; i++)
        {
            soldierPrefabs.Add(challengeOption.soldierOption[i].soldierData.code, Resources.Load<GameObject>(prefabPath + challengeOption.soldierOption[i].soldierData.soldier_name));
        }
    }

    public void DoChallenge()
    {
        transform.GetComponent<Button>().interactable = false;
        ((BattleNode)saveManager.gameData.map.curMapNode).isChallenge = true;
        StartCoroutine(SpawnChallenge());
    }

    IEnumerator SpawnChallenge()
    {
        GameObject createSoldier;
        SoldierData soldierData;

        for (int i = 0; i < challengeOption.soldierOption.Count; i++)
        {
            soldierData = challengeOption.soldierOption[i].soldierData;
            for (int j = 0; j < challengeOption.soldierOption[i].portNum[0]; j++)
            {
                createSoldier = Instantiate(soldierPrefabs[soldierData.code], challengeObject.transform);
                if (soldierData.mutant)
                {
                    Instantiate(soldierData.mutant, createSoldier.transform);
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    }
}
