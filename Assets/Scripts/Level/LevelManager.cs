using UnityEngine;

public static class LevelManager
{
    private const string LEVEL_PREFIX_NAME = "Level";

    private static string _currentLevelName;
    private static Level _currentLevel;
    public static Level CurrentLevel => _currentLevel;

    public static bool LoadLevel()
    {
        if (DataManager.GameSettingsConfig.IsTestLevel)
        {
            ChangeLevel(DataManager.GameSettingsConfig.TestLevelName);
        }
        else
        {
            return ChangeToNextLevel();
        }

        return true;
    }
    
    private static void ChangeLevel(string levelName)
    {
        var levelData = DataManager.LoadLevelData(levelName);
        _currentLevel = LevelBuilder.BuildLevel(levelData);
    }

    private static bool ChangeToNextLevel()
    {
        var currentLevelIndex = PlayerPrefs.GetInt(SaveManager.SAVE_LEVEL_INDEX_KEY, 1);
        _currentLevelName = LEVEL_PREFIX_NAME + currentLevelIndex;
        
        Debug.Log("_currentLevelName = " + _currentLevelName);

        if (_currentLevelName == DataManager.GameSettingsConfig.SoonLevelName)
        {
            return false;
        }
        else
        {
            ChangeLevel(_currentLevelName);
            return true;
        }
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
