using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI bestScoreText;

    void OnEnable()
    {
        SetUI();
    }

    public void SetUI()
    {
        int currentScore = GameManager.Instance != null ? GameManager.Instance.CurrentScore : 0;
        int bestScore = PlayerPrefs.GetInt("BestScoreText", 0);
        
        if(currentScore > bestScore){ //최고점수 갱신
            bestScore = currentScore;
            PlayerPrefs.SetInt("BestScoreText", currentScore);
        }

        //점수 출력
        TextMeshProUGUI currentScoreText = GameObject.Find("CurrentScoreText")?.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI bestScoreText = GameObject.Find("BestScoreText")?.GetComponent<TextMeshProUGUI>();
        
        if (currentScoreText != null)
        {
            currentScoreText.text = currentScore.ToString();
        }
        else
        {
            Debug.LogWarning("CurrentScoreText 오브젝트를 찾을 수 없습니다.");
        }

        if (bestScoreText != null)
        {
            bestScoreText.text = bestScore.ToString();
        }
        else
        {
            Debug.LogWarning("BestScoreText 오브젝트를 찾을 수 없습니다.");
        }
    }
}
