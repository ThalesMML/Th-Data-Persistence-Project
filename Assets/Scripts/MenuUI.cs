using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUI : MonoBehaviour        
{
    public TMP_Text highestScoreText;
    [SerializeField] TMP_InputField playerNameInput;

    private int HighestScore;
    private string HighestPlayer;

    void Awake()
    {
        LoadHighest();
    }
    void Start()
    {
        SeeHighScore();
    }
    public void SeeHighScore()
    {
        if (HighestPlayer == null)
        {
            HighestPlayer = "Empty";
        }else
        {
            highestScoreText.text = $"Highest Score : {HighestPlayer} : {HighestScore}";
        }        
    }
    public void LoadHighest()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HighestScore = data.HighestScore;
            HighestPlayer = data.HighestName;
        }
    }
    [System.Serializable]
    class SaveData
    {
        public string HighestName;
        public int HighestScore;
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
