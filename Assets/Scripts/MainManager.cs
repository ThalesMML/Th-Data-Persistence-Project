using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text currentScoreText;
    public Text highestScoreText;

    private string currentPlayerName;
    private static int HighestScore;
    private static string HighestPlayer;

    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    private void Awake()
    {
        LoadHighest();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentPlayerName = SaveManager.Instance.PlayerName;
        AddPoint(0);

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
        SetBest();
    }

    private void Update()
    {        
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {                
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {                
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        SaveManager.Instance.Score = m_Points;
        currentScoreText.text = $"Score : {currentPlayerName} : {m_Points}";
    }
    public void GameOver()
    {
        m_GameOver = true;
        CheckBest();
        GameOverText.SetActive(true);
    }

    private void CheckBest()
    {
        int currentScore = SaveManager.Instance.Score;
        if (currentScore > HighestScore)
        {
            HighestScore = currentScore;
            HighestPlayer = SaveManager.Instance.PlayerName;
                        
            SaveHighest(HighestScore, HighestPlayer);
        }
    }
    private void SetBest()
    {
        highestScoreText.text = $"Highest Score : {HighestPlayer} : {HighestScore}";
    }
    public void SaveHighest(int bestScore, string bestName)
    {
        SaveData data = new SaveData();

        data.HighestScore = bestScore;
        data.HighestName = bestName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
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
    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
        SaveManager.Instance.Awake();
    }
}