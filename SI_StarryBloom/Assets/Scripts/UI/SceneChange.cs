using UnityEngine.SceneManagement;
using NaughtyAttributes;
using UnityEngine;

namespace Menu
{
    public class SceneChange : MonoBehaviour
    {
        [Scene]
        public string sceneName;

        [ContextMenu("ChangeToScene")]
        public void ChangeToSceneIn(float delay)
        {
            Invoke("LoadSceneMode", delay);
        }
        public void ChangeToScene()
        {
            SceneManager.LoadScene(sceneName);
        }
        public void ChangeToScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }
        public void ChangeToScene(int scene)
        {
            SceneManager.LoadScene(scene);
        }
        private void LoadSceneMode()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
