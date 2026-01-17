using System;
using UnityEngine;
using System.IO;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public string playerName;

    public string bestPlayer;
    public int bestScore;

    public TextMeshProUGUI bestScoreText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadScorer();
    }

    [Serializable]
    public class SaveData
    {
        public string name;
        public int score;
    }

    public void SaveScorer()
    {
        SaveData data = new SaveData();
        data.name = bestPlayer;
        data.score = bestScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScorer()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayer = data.name;
            bestScore = data.score;
        }

        bestScoreText.text = $"Best score: {bestPlayer} - {bestScore}";
    }
}
