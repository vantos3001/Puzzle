using UnityEditor;
using UnityEngine;

namespace FullMetalMonsters.Tools
{
    public class GenerateConfigWindow : EditorWindow
    {

        private string _configName;

        private int _x;
        private int _y;
        
        private int _isViewPrefabFound = 0;

        private LevelData _levelData;


        [MenuItem("Puzzle/Tools/GenerateTemplateConfigWindow")]
        public static void Open()
        {
            var w = GetWindow<GenerateConfigWindow>();
            w.minSize = new Vector2(500, 100);
            w.maxSize = w.minSize;
            w.Show();
        }

        private void OnEnable()
        {
            name = "GenerateConfigWindow";
            _levelData = LevelGenerator.CreateTestLevelData();
        }

        private void OnGUI()
        {
            if (_levelData == null)
            {
                GUILayout.Label("Not found LevelData. Try reopen window");
                return;
            }

            CreateLevelInputView();
        }
        
        private void CreateLevelInputView()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.ExpandWidth(false);
            _x = EditorGUILayout.IntField("Horizontal Length", _x);
            _y = EditorGUILayout.IntField("Vertical Length", _y);

            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.Space();
            
            EditorGUILayout.BeginHorizontal();
            _configName = EditorGUILayout.TextField("Config Name", _configName);

            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.Space();
            
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Create Config"))
            {
                TryCreateConfig();
            }
            
            EditorGUILayout.EndHorizontal();
        }

        private void TryCreateConfig()
        {
            if (string.IsNullOrEmpty(_configName))
            {
                Debug.LogError("ConfigName is empty");
            }
            else
            {
                var levelData = LevelGenerator.CreateTempLevelData(_x,  _y);
                SaveManager.SaveLevel(levelData, _configName);
            }
        }
    }
}