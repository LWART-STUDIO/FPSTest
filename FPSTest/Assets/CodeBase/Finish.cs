using System;
using UnityEngine;

namespace CodeBase
{
    public class Finish : MonoBehaviour
    {
        [SerializeField] private UIDisplay _uiDisplay;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if(other.TryGetComponent(out PlayerMover playerMover))
                {
                    playerMover.enabled = false;
                }
                Debug.Log("!Победа");
                _uiDisplay.WinDisplay();
            }
        }
    }
}
