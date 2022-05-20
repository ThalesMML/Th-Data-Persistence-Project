using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUI : MonoBehaviour
{
    public TMP_Text highestScoreText;
    public TMP_Text playerNameInput;
    void Start()
    {
        string HighestPlayer = SaveManager.Instance.PlayerName;
        int HighestScore = SaveManager.Instance.Score;
        highestScoreText.text = $"Highest Score : {HighestPlayer} : {HighestScore}";
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void SetPlayerName()
    {
        SaveManager.Instance.PlayerName = playerNameInput.text;
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
