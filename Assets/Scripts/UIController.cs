using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField]
    Transform _menuCanvas;
    [SerializeField]
    Transform _gameCanvas;
    [SerializeField]
    Transform _gameOverCanvas;
    [SerializeField]
    Transform _countdownCanvas;
    [SerializeField]
    Text _countdownText;
    [SerializeField]
    Text _inGamesScore;
    [SerializeField]
    Text _menuHighScore;
    [SerializeField]
    Slider _gameTimeSlider;
    [SerializeField]
    Text _gameOverScore;
    [SerializeField]
    Text _gameOverHighScore;

    public void ShowMenuCanvas() {
        _menuCanvas.gameObject.SetActive(true);
        _gameCanvas.gameObject.SetActive(false);
        _gameOverCanvas.gameObject.SetActive(false);
        _countdownCanvas.gameObject.SetActive(false);
    }

    public void ShowGameOverCanvas() {
        _gameOverCanvas.gameObject.SetActive(true);
        _menuCanvas.gameObject.SetActive(false);
        _gameCanvas.gameObject.SetActive(false);
    }

    public void CountdownToStart(int seconds) {
        _menuCanvas.gameObject.SetActive(false);
        _gameOverCanvas.gameObject.SetActive(false);
        _countdownCanvas.gameObject.SetActive(true);
        StartCoroutine(CountdownCoroutine(seconds));
    }

    public void UpdateInGameScore(int score) {
        _inGamesScore.text = score.ToString();
    }

    public void UpdateGameOverScore(int score) {
        _gameOverScore.text = score.ToString();
    }

    public void UpdateHighScore(int highScore) {
        _gameOverHighScore.text = _menuHighScore.text = highScore.ToString();
    }

    public void UpdateGameTimeSlider(float percentage) {
        _gameTimeSlider.value = percentage;
    }

    IEnumerator CountdownCoroutine(int seconds) {
        WaitForSeconds wait = new WaitForSeconds(1f);
        int timer = 0;
        while (timer < seconds) {
            _countdownText.text = (seconds - timer).ToString();
            for(int i=0; i<3; i++)
                yield return AddDotToCountdown();
            yield return new WaitForSeconds(0.25f);
            timer++;
        }
        _countdownText.text = "GO!";
        yield return new WaitForSeconds(0.3f);
        ShowGameCanvas();
    }

    IEnumerator AddDotToCountdown() {
        WaitForSeconds waitOneFourth = new WaitForSeconds(0.25f); 
        yield return waitOneFourth;
        _countdownText.text += ".";
    }

    void ShowGameCanvas() {
        _gameCanvas.gameObject.SetActive(true);
        _countdownCanvas.gameObject.SetActive(false);
    }
}
