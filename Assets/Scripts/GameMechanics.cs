using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMechanics : MonoBehaviour
{
    public int score;
    public GameObject scoreT;


    void Start()
    {
        score = ManagerScript.Instance.Score;

        scoreT.GetComponent<Text>().text = score.ToString();
    }

    void Update()
    {
        score = 5;
        scoreT.GetComponent<Text>().text = score.ToString();
    }
}
