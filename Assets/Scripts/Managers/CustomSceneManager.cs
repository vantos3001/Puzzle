using UnityEngine.SceneManagement;

namespace Game.Managers
{
    public class CustomSceneManager : SceneManager
    {
        public static void LoadGameplayScene()
        {
            LoadScene("SampleScene");
        }
    }
}