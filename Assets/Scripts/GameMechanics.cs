using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMechanics : MonoBehaviour
{
    private int score;
    public GameObject scoreT;
    public GameObject gameUI;
    public GameObject startUI;
    public GameObject endUI;
    public GameObject scoreE;
    public GameObject coin;
    public List<GameObject> coinIns;
    private int randX, randY;
    public static bool coinDestroyed;
    private bool startCountdown;
    public GameObject time;
    public static float timeFloat = 5f;
    private int timeInt;
    public static bool gameEnded;
    public Transform player;
    public AudioSource audioSource;
    public AudioSource audioSourceEnd;
    public AudioSource audioSourceCD;

    void Start()
    {
        timeInt = Mathf.RoundToInt(timeFloat);
        time.GetComponent<Text>().text = timeInt.ToString();

        endUI.SetActive(false);
        gameUI.SetActive(false);
        startUI.SetActive(true);
        gameEnded = true;
    }

    void Update()
    {
        score = ManagerScript.Instance.Score;
        scoreT.GetComponent<Text>().text = score.ToString();        
        scoreE.GetComponent<Text>().text = score.ToString();
        time.GetComponent<Text>().text = timeInt.ToString();

        if(coinDestroyed){
            coinIns.Clear();
            CoinSpawn();
            coinDestroyed = false;
        }
    
        if(startCountdown){
            Countdown();
        }

    }

    public void Replay(){
        audioSource.Play();
        gameEnded = false;
        endUI.SetActive(false);
        gameUI.SetActive(true);
        startUI.SetActive(false);
        coinDestroyed = true;
        timeFloat = 5f;
        player.transform.position = new Vector3(0, 0, 0);
        ManagerScript.Instance.Score = 0;
        score = ManagerScript.Instance.Score;
        scoreT.GetComponent<Text>().text = score.ToString();   
    }

    void CoinSpawn(){
        randX = Random.Range (-3750, 2885);
        randY = Random.Range (-3540, 3220);
        coinIns.Add(Instantiate (coin, new Vector3(randX, randY, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("Coins").transform) as GameObject);
        startCountdown = true;
    }

    void Countdown()
    {
        if(timeFloat > 0.0f){
            timeFloat -= Time.deltaTime * 1f;
            timeInt = Mathf.RoundToInt(timeFloat);
            if(!audioSourceCD.isPlaying && timeInt < 5)
                audioSourceCD.Play();
        } else {
            if(coinIns.Count > 0){
                Destroy(coinIns[coinIns.Count-1]);
                coinIns.Clear();
            }
            audioSourceCD.Stop();
            audioSourceEnd.Play();
            endUI.SetActive(true);
            gameUI.SetActive(false);
            startUI.SetActive(false);
            gameEnded = true;
            startCountdown = false;
        }
    }
}
