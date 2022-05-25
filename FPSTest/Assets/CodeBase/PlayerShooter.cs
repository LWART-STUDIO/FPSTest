using System;
using UnityEngine;

namespace CodeBase
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerShooter : MonoBehaviour
    {
        private PlayerInput _playerInput;
        [SerializeField] private Camera _cameraToZoom;
        [SerializeField] private Gun _gun;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            
        }

        private void OnEnable()
        {
            _playerInput.OnMouseLeftDown += Shoot;
            _playerInput.OnMouseRight += Zoom;
        }
        
        private void OnDisable()
        {
            _playerInput.OnMouseLeftDown -= Shoot;
            _playerInput.OnMouseRight -= Zoom;
        }
        private void Zoom(bool isPressed)
        {
            if (isPressed)
            {
                _cameraToZoom.fieldOfView = 20f;
            }
            else
            {
                _cameraToZoom.fieldOfView = 60f;
            }
        }

        private void Shoot(bool iPressed)
        {
            Debug.Log("!Выстрел");
            _gun.Shoot();
        }
    }
}
