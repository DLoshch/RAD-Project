using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Serializable]
    public class GameData
    {
        public bool isPlayedIntro = false;
        public bool isMusicOn = true;

        public int playerHealth = 100;
        public int coins = 10; // Задайте начальные значения
        public float playTime = 0f;

        public float posX, posY, posZ;

        public List<string> inventoryItems = new List<string>();
    }

    [Header("Текущие данные игры")]
    public GameData currentGameData = new GameData();

    public void Start()
    {
        // Загружаем данные из файла
        GameData loadedData = LoadGame();

        // Проверяем: если файл существовал, переносим данные
        if (loadedData != null)
        {
            currentGameData = loadedData;
        }
    }

    // Вызывайте ЭТОТ метод с кнопки сохранения (UIButton)!
    public void SaveCurrentGame()
    {
        if (currentGameData == null) return;

        string path = Path.Combine(Application.persistentDataPath, "game_save.json");
        string json = JsonUtility.ToJson(currentGameData, true);

        File.WriteAllText(path, json);
        Debug.Log($"[GameManager] Данные успешно сохранены в: {path}\nМонеты: {currentGameData.coins}, HP: {currentGameData.playerHealth}");
    }

    public GameData LoadGame()
    {
        string path = Path.Combine(Application.persistentDataPath, "game_save.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<GameData>(json);
        }

        // Если файла нет — возвращаем null, чтобы не затирать дефолтные значения из кода/инспектора
        return null;
    }
}