using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField]
    UIController _uiController;
    [SerializeField]
    int _countdownSec=3;
    [SerializeField]
    int _gameTimeSec = 10;

    int score = 0;
    int highScore = 0;

    private void Start() {
        if (_uiController) {
            _uiController.ShowMenuCanvas();
            _uiController.UpdateHighScore(highScore);
        }
    }

    public void NewGame() {
        score = 0;
        if (_uiController) {
            _uiController.UpdateInGameScore(score);
            _uiController.CountdownToStart(_countdownSec);
        }
        StartCoroutine(GameTimeCoroutine());
    }

    public void OnTap() {
        score++;
        if (_uiController)
            _uiController.UpdateInGameScore(score);
    }

    void GameOver() {
        if (score > highScore)
            highScore = score;
        if (_uiController) {
            _uiController.UpdateGameOverScore(score);
            _uiController.UpdateHighScore(highScore);
            _uiController.ShowGameOverCanvas();
        }
    }

    IEnumerator GameTimeCoroutine() {
        yield return new WaitForSeconds(_countdownSec);
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        float timeLeft = _gameTimeSec;
        while (timeLeft > 0) {
            if (_uiController)
                _uiController.UpdateGameTimeSlider(timeLeft / _gameTimeSec);
            yield return wait;
            timeLeft -= Time.deltaTime;
        }
        GameOver();
    }

}
