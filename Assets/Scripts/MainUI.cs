using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using TMPro;

public class MainUI : MonoBehaviour
{
    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
        SaveManager.Instance.Awake();
    }
}
