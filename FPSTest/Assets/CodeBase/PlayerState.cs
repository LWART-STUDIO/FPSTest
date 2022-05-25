using System;
using UnityEngine;

namespace CodeBase
{
    [RequireComponent(typeof(PlayerMover))]
    public class PlayerState : MonoBehaviour
    {
        [SerializeField] private UIDisplay _uiDisplay;
        private PlayerMover _playerMover;
        private bool _faild;

        private void Awake()
        {
            _faild = false;
            Debug.Log("!Старт уровня");
            Time.timeScale = 1;
            _playerMover = GetComponent<PlayerMover>();
        }

        private void Update()
        {
            if (transform.position.y < -3&&!_faild)
            {
                _playerMover.enabled = false;
                Debug.Log("!Фейл");
                _uiDisplay.DeadDisplay();
                _faild = true;
            }
        }
    }
}
