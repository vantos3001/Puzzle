using UnityEditor;
using UnityEngine;

namespace FullMetalMonsters.Meta
{
	public static class EditorHelper
	{
		[MenuItem("Puzzle/Tools/GenerateTestConfig")]
		public static void GenerateTestConfig()
		{
			var levelData = LevelGenerator.CreateTestLevelData();
			SaveManager.SaveLevel(levelData, "testFile");
		}
		
		[MenuItem("Puzzle/Save/DeleteAll")]
		public static void DeleteAll()
		{
			PlayerPrefs.DeleteAll();
		}
	}
}