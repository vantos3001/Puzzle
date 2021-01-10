using UnityEngine;

public static class LevelManager
{
    private const string LEVEL_PREFIX_NAME = "Level";

    private static string _currentLevelName;
    private static Level _currentLevel;
    public static Level CurrentLevel => _currentLevel;
    
    public static void ChangeLevel(string levelName)
    {
        var levelData = DataManager.LoadLevelData(levelName);
        _currentLevel = LevelBuilder.BuildLevel(levelData);
    }

    public static void ChangeToNextLevel()
    {
        var currentLevelIndex = PlayerPrefs.GetInt(SaveManager.SAVE_LEVEL_INDEX_KEY, 1);
        _currentLevelName = LEVEL_PREFIX_NAME + currentLevelIndex;
        
        ChangeLevel(_currentLevelName);
    }

    public static void FinishLevel()
    {
        var levelIndex = PlayerPrefs.GetInt(SaveManager.SAVE_LEVEL_INDEX_KEY, 1);
        SaveManager.SaveLevelIndex(levelIndex + 1);
    }

    private static void RestartLevel()
    {
        ChangeLevel(_currentLevelName);
    }
}
