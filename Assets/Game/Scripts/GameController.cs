using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    private bool isGamerRunning;
    private int score;
    private int currentLevelIndex;

    public ObstacleGenerator generator;
    public GameConfiguration config;
    public TextMeshProUGUI scoreLabel;
    public GameUI gameStartUI;
    public GameUI gameOverUI;
    public Player player;
    public LevelConfiguration[] levels;

    void Start()
    {
        isGamerRunning = false;

        gameStartUI.Show();

        config.speed = 0f;

    }

    private void Update()
    {
        scoreLabel.text = score.ToString("00000000.##");

        if (!isGamerRunning) return;
        score++;

        CheckLevelUpdate();
    }

    private void CheckLevelUpdate()
    {
        if (currentLevelIndex >= levels.Length - 1) return;
        if (score < levels[currentLevelIndex + 1].minScore) return;
        currentLevelIndex++;

        print(currentLevelIndex);

        SetCurrentLevelConfiguration();
    }

    private void SetCurrentLevelConfiguration()
    {

        LevelConfiguration level = levels[currentLevelIndex];
        config.speed = level.speed;
        config.minRangeObstacleGenerator = level.minRangeObstacleGenerator;
        config.maxRangeObstacleGenerator = level.maxRangeObstacleGenerator;
    }

    public void GameStart()
    {
        currentLevelIndex = 0;
        SetCurrentLevelConfiguration();

        isGamerRunning = true;

        generator.GenerateObstacles();

        score = 0;

        gameStartUI.Hide();

        player.SetActive();
    }

    public void GameOver()
    {
        print("O Jogo Acabou!!!");

        isGamerRunning = false;

        config.speed = 0f;
        generator.StopGenerator();

        gameOverUI.Show();
    }

    public void RestartGame()
    {
        gameOverUI.Hide();
        generator.ResetGenerator();
        GameStart();
    }
}
