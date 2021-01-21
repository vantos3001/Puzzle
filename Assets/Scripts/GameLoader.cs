using Game.Managers;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    private void Awake()
    {
        TutorialManager.Init();
        
        UIManager.Init();
        
        CraftManager.Init();

        HintManager.Init();

        CustomSceneManager.LoadGameplayScene(false);
    }
}
