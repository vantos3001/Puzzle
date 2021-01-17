using UnityEngine.SceneManagement;

namespace Game.Managers
{
    public class CustomSceneManager : SceneManager
    {
        public static void LoadGameplayScene(bool isUseFade)
        {
            if (isUseFade)
            {
                UIManager.UiFade.OnFadeIn += OnFadeIn;
                UIManager.UiFade.StartFadeIn();
            }
            else
            {
                LoadGameplayScene();
            }
        }
        
        private static void LoadGameplayScene()
        {
            LoadScene("SampleScene");
        }

        private static void OnFadeIn()
        {
            UIManager.UiFade.OnFadeIn -= OnFadeIn;
            LoadGameplayScene();
        }
    }
}