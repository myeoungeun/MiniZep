using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Image scoreUI;
    public ScoreUI scoreUIManager;
    public Button exitButton;
    
    // Start is called before the first frame update
    void Start()
    {
        if(scoreUI == null){
            Debug.LogError("scoreUI is null");
        }

        if(scoreText == null){
            Debug.LogError("score text is null");
        }
        if (exitButton == null) {
            Debug.LogError("exitButton is null!");
        }
        exitButton.onClick.AddListener(OnClickExitButton);
        scoreUI.gameObject.SetActive(false); //오브젝트를 끔
    }

    public void SetRestart(){
        scoreUI.gameObject.SetActive(true); //오브젝트를 켬
        scoreUIManager.SetUI(); //점수 업데이트
    }

    public void UpdateScore(int score){ //현재점수 보여줌
        scoreText.text = score.ToString();
    }

    public void OnClickExitButton()
    {
        SceneManager.LoadScene("MainScene");
    }
}
