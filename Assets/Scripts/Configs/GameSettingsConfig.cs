using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameSettingsConfig", menuName = "Configs/GameSettingsConfig", order = 51)]
public class GameSettingsConfig : ScriptableObject
{
    public string SoonLevelName;
    
    [Header("Test")]
    public string TestLevelName;
    public bool IsTestLevel;
}
