using UnityEngine;

namespace CodeBase
{
    public class UIDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject _winDisplay;
        [SerializeField] private GameObject _deadDisplay;
        public void DeadDisplay()
        {
            Time.timeScale = 0;
            _deadDisplay.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            
        }

        public void WinDisplay()
        {
            Time.timeScale = 0;
            _winDisplay.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            
        }
    }
}
