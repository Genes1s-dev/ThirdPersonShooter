using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject playerWinUI;
    [SerializeField] private GameObject playerLoseUI;
    [SerializeField] TextMeshProUGUI winCountsText;
    [SerializeField] TextMeshProUGUI loseCountsText;
    [SerializeField] List<Enemy> enemyList;
    private bool isPaused;
    private int winCount = 0;
    private int loseCount = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(this.gameObject);
        } else {
            Instance = this;
        }
    }

    private void Start()
    {
        LoadWinLoseCount();
        UpdateWinCounts();
        UpdateLoseCounts();
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void TogglePauseGame()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f;
        } else {
            Time.timeScale = 1f;
        }
        mainMenuUI.SetActive(isPaused);  
    }

    public void NewGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void QuitGame() => Application.Quit();


    public void EnemyDefeated(Enemy enemy)
    {
        enemyList.Remove(enemy);
        if (enemyList.Count == 0)
        {
            winCount++;
            UpdateWinCounts();
            playerWinUI.SetActive(true);
        }
    }

    public void HandlePlayerDeath()
    {
        loseCount++;
        UpdateLoseCounts();
        playerLoseUI.SetActive(true);
    }

    private void UpdateWinCounts()
    {
        winCountsText.text = winCount.ToString();
        PlayerPrefs.SetInt("WinCount", winCount);
        PlayerPrefs.Save();
    }

    private void UpdateLoseCounts()
    {
        loseCountsText.text = loseCount.ToString();
        PlayerPrefs.SetInt("LoseCount", loseCount);
        PlayerPrefs.Save();
    }

    private void LoadWinLoseCount()
    {
        winCount = PlayerPrefs.GetInt("WinCount");
        loseCount = PlayerPrefs.GetInt("LoseCount");
    }




}
