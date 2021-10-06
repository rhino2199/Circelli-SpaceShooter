/****
 * Created by: Ryan Circelli
 * Date Created: Sept 29, 2021
 * 
 * Last Edited By: Ryan Circelli
 * Last Updated Oct 2,2021
 * 
 * Description:Game manager to control global game behaviours
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Variables
    #region GameManager Sigleton
    static GameManager gm;
    public static GameManager GM
    {
        get { return gm; }
    }

    void CheckGameManagerIsInScene()
    {
        if (gm == null)
        {
            gm = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion
        // UI Elements
    public static int Score;
    public string ScorePrefix = string.Empty;
    public string ScorePostfix = string.Empty;
    public TMP_Text ScoreText = null;
    public GameObject GameOverText = null;
    public GameObject LevelCompleteText = null;
    public GameObject[] Lives = new GameObject[3];

    public GameObject Player;
    public int currentLevel = 1;
    //Score to get at the current level - 1
    public int[] LevelScoreArray = new int[2];
    private Health PlayerHealth;
    //Time with end game UI before next level loads
    public float LoadTime = 3;
    //True if the game ended in a game over and scene1 is loaded
    private bool GO = false;


    private void Awake()
    {
        CheckGameManagerIsInScene();
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth = Player.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        //Set Score Text
        if (ScoreText != null)
        {
            ScoreText.text = ScorePrefix + Score.ToString() + ScorePostfix + LevelScoreArray[currentLevel - 1].ToString();
        }

        //Check if Level is complete
        if (Score >= LevelScoreArray[currentLevel - 1])
        {
            LevelComplete();
        }


        //Display current player lives
        if(PlayerHealth.HealthPoints < 300)
        {
            gm.Lives[0].SetActive(false);
        }
        if (PlayerHealth.HealthPoints < 200)
        {
            gm.Lives[1].SetActive(false);
        }
        if (PlayerHealth.HealthPoints < 100)
        {
            gm.Lives[2].SetActive(false);
        }

    }

    //Called when the player completes a level
    //Makes player invinicible while new level is loaded
    public void LevelComplete()
    {
        PlayerHealth.HealthPoints = 100000;
        gm.LevelCompleteText.gameObject.SetActive(true);
        Invoke("NewScene", LoadTime);
    }

    //Called when the player loses
    public static void GameOver()
    {
        if(gm.GameOverText != null)
        {
            gm.GameOverText.gameObject.SetActive(true);
        }
        gm.GO = true;
        gm.Invoke("NewScene", gm.LoadTime);
    }

    private void OnLevelWasLoaded(int level)
    {
        Score = 0;
        /*ScoreText = GameObject.Find("Canvas/ScoreText").GetComponent<TMP_Text>();
        GameOverText = GameObject.Find("Canvas/GameOverText");
        LevelCompleteText = GameObject.Find("Canvas/LevelCompleteText");
        Lives[0] = GameObject.Find("Canvas/Life1");
        Lives[0].SetActive(true);
        Lives[1] = GameObject.Find("Canvas/Life2");
        Lives[1].SetActive(true);
        Lives[2] = GameObject.Find("Canvas/Life3");
        Lives[2].SetActive(true);
        Player = GameObject.FindGameObjectWithTag("Player");
        */
    }

    //Plays an object explosion
    public static void ObjectDestroyed()
    {
        gm.GetComponent<AudioSource>().Play();
    }

    //Loads new scene, scene1 if game resulted in gmae over
    public void NewScene()
    {
        if (GO)
        {
            SceneManager.LoadScene("Scene1");
            currentLevel = 1;
        }
        else
        {   currentLevel += 1;
            SceneManager.LoadScene("Scene2");
            
        }
    }
}
