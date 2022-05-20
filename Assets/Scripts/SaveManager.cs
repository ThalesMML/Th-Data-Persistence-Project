using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;    
    public string PlayerName;
    public int Score;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
