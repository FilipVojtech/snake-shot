using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreensController : MonoBehaviour
{
    private bool _isPaused, _isOver;
    private int _playerLives;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject life1, life2, life3, life4;
    [SerializeField] private GameObject status, pauseScreen, gameOverScreen, statsScreen;
    private Player _player;

    private void Start()
    {
        _player = player.GetComponent<Player>();
        _isPaused = false;
        _isOver = false;
    }

    private void Update()
    {
        LivesController();
        PauseScreenController();
        GameOverScreenController();
        statsScreen.SetActive(_isOver || _isPaused);
    }

    private void LivesController()
    {
        _playerLives = _player.GetLives();
        switch (_playerLives)
        {
            case 3:
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(true);
                life4.SetActive(false);
                break;
            }
            case 2:
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(false);
                life4.SetActive(false);
                break;
            }
            case 1:
            {
                life1.SetActive(true);
                life2.SetActive(false);
                life3.SetActive(false);
                life4.SetActive(false);
                break;
            }
            case 0:
            {
                life1.SetActive(false);
                life2.SetActive(false);
                life3.SetActive(false);
                life4.SetActive(false);
                break;
            }
            default:
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(true);
                life4.SetActive(true);
                break;
            }
        }

        if (_playerLives < 1)
        {
            _isOver = true;
        }
    }

    private void PauseScreenController()
    {
        pauseScreen.SetActive(_isPaused);
        Time.timeScale = _isPaused ? 0f : 1f;

        if (Input.GetKeyDown(KeyCode.Escape) && !_isOver)
        {
            _isPaused = !_isPaused;
        }
    }

    private void GameOverScreenController()
    {
        gameOverScreen.SetActive(_isOver);
        status.SetActive(!_isOver);
        if (_isOver)
        {
            Time.timeScale = 0;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ResumeGame()
    {
        _isPaused = false;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void SetGameOver()
    {
        _isOver = true;
    }

    public bool GetIsPaused()
    {
        return _isPaused;
    }
}