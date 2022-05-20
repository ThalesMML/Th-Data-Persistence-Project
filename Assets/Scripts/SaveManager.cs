using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
        
    public TMP_Text playerName;
    private TMP_Text HighestName;
    private TMP_Text HighestScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //public string PlayerName()
    //{
    //    string playerName = SaveManager.Instance.playerName.text;
    //    return playerName;
    //}

    //class SaveData
    //{
    //    public string playerName;
    //    public string HighestName;
    //    public int HighestScore;
    //}
}
