using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int score;

    private AudioClip _shieldHitSound;
    private AudioClip _playerHurtSound;
    private bool _isGameOver = false;
    private CanvasUpdateScript _counterCanvas;
    [SerializeField] private GameObject _gameOverUI;
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        if (_counterCanvas == null)
        {
            _counterCanvas = GameObject.Find("Counter Canvas").GetComponent<CanvasUpdateScript>();
        }
        UpdateCanvas();
    }

    void Update()
    {
        if (_isGameOver && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }
    }

    public void IncreaseScore()
    {
        score++;
        UpdateCanvas();
    }

    public void DecreaseHealth()
    {
        health--;
        if (health <= 0)
        {
            health = 0;
            GameOver();
        }
        UpdateCanvas();
    }

    void UpdateCanvas()
    {
        _counterCanvas.SetHealth(health);
        _counterCanvas.SetScore(score);
    }

    void GameOver()
    {
        _isGameOver = true;
        Time.timeScale = 0.025f;
        _gameOverUI.SetActive(true);
    }
}
