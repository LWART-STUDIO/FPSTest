using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase
{
    public class SceneRelouder : MonoBehaviour
    {
        public void Reloud()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
    }
}
