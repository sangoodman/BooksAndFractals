using UnityEngine;
using UnityEngine.SceneManagement;

namespace BooksAndFractals
{
    public static class SceneManagement
    {
        public static void HardLoad(int buildIndex)
        {
            SceneManager.LoadScene(buildIndex);
        }
        public static void HardLoad(string sceneName)
        {
            SceneManager.LoadScene(GetBuildIndex(sceneName));
        }
        public static void SafeLoad(int buildIndex)
        {
            var menu = ReferenceSearcher.GetMenuScript();
            switch (buildIndex) 
            {
                case 0:
                    HardLoad(0);
                    break;
                case 1:
                    menu.ReturnToMainMenuConfirmed();
                    break;
                case 2:
                    menu.LoadChapter0NowFromStart();
                    break;
                case 3:
                    menu.LoadChapter1NowFromStart();
                    break;
                case 4:
                    menu.LoadChapter2NowFromStart();
                    break;
                case 5:
                    menu.LoadChapter3NowFromStart();
                    break;
                case 6:
                    menu.LoadChapter4NowFromStart();
                    break;
                default:
                    Debug.LogWarning("No build index!");
                    break;
            }
        }

        static int GetBuildIndex(string sceneName)
        {
            string scenePath = "";

            // Iterate through build settings to find the scene path
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string path = SceneUtility.GetScenePathByBuildIndex(i);
                if (path.Contains(sceneName))  // Scene names are usually at the end of the path
                {
                    scenePath = path;
                    break;
                }
            }

            // Return build index if found
            return string.IsNullOrEmpty(scenePath) ? -1 : SceneUtility.GetBuildIndexByScenePath(scenePath);
        }
    }
}
