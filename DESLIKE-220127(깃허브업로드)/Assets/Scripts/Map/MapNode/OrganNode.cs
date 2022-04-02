using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "OrganNode", menuName = "ScriptableObject/MapNodeT/OrganNode")]
public class OrganNode : MapNode
{
    public void Play_OrganNode()
    {
        SceneManager.LoadScene("Organ");
    }
}