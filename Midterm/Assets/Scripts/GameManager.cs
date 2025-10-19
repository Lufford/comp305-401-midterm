using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoad;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.name);
        if (scene.name.Equals("TitleScreen") || scene.name.Equals("GameOverScreen"))
        {
            gameOver = true;
        }
        else
        {
            gameOver = false;

            StartCoroutine(FindScoreBoard());
        }
        level = scene.buildIndex;
    }

    //Scoreboard
    private GameObject scoreboard;
    [SerializeField] private int score = 0;
    private Dictionary<int, int> levelScore = new Dictionary<int, int>();
    private bool gameOver = false;
    private int level = 0;


    private void Start()
    {
        Debug.Log("Level"+level);
    }

    public void SetGameOver()
    {
        levelScore[level] = score;
        gameOver = true;
        SceneManager.LoadScene("GameOverScreen");
        StartCoroutine(FindScoreBoard());
    }

    public void StartGame()
    {
        gameOver = false;
        score = 0;
        StartCoroutine(FindScoreBoard());
    }

    public void updateScore(int score)
    {
        this.score += score;

        if (scoreboard != null)
        {
            scoreboard.GetComponent<TMP_Text>().text = "Scrap: " + this.score.ToString();
        }
    }

    //allows you to change the level
    public IEnumerator LevelChange()
    {
        Debug.Log("Level Changed");
        levelScore[level] = score; 

        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(5f);
        Time.timeScale = 1f;

        level++;
        score = 0;

        SceneManager.LoadScene(level);
        StartCoroutine(FindScoreBoard());
    }

    private IEnumerator FindScoreBoard()
    {
        yield return new WaitForSeconds(0.1f);
        scoreboard = GameObject.FindGameObjectWithTag("Scoreboard");
        if (scoreboard == null)
        {
            yield break;
        }

        if (gameOver)
        {
            string result = "Final Scores:\n";
            for (int i = 1; i < 3; i++)
            {
                if (levelScore.ContainsKey(i))
                    result += $"Level {i}: {levelScore[i]}\n";
            }
            scoreboard.GetComponent<TMP_Text>().text = result;
        }
    }
}