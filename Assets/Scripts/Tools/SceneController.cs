using UnityEngine;
using UnityEngine.SceneManagement;

namespace Meta.Tools
{
    public class SceneController : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}