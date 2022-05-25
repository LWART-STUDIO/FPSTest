using System;
using UnityEngine;

namespace CodeBase
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMover : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private CharacterController _characterController;
        private Vector3 _pressVector;
        private Vector2 _rotate;
        private Vector2 _playerRotate;
        
        

        [SerializeField] private float _speed = 2f;
        [SerializeField] private float _sensitivity = 1f;
        [SerializeField] private bool _lockCursor = true;
        [SerializeField] private Transform _mainCamera;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _characterController = GetComponent<CharacterController>();
        }

        private void OnEnable()
        {
            _playerInput.OnPressedA += MoveLeft;
            _playerInput.OnPressedD += MoveRight;
            _playerInput.OnPressedW += MoveForward;
            _playerInput.OnPressedS += MoveBack;
            _playerInput.OnMouseX += RotateX;
            _playerInput.OnMouseY += RotateY;
        }

        


        private void OnDisable()
        {
            _playerInput.OnPressedA -= MoveLeft;
            _playerInput.OnPressedD -= MoveRight;
            _playerInput.OnPressedW -= MoveForward;
            _playerInput.OnPressedS -= MoveBack;
            _playerInput.OnMouseX -= RotateX;
            _playerInput.OnMouseY -= RotateY;
        }

        private void Update()
        {
            if (_lockCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            Vector3 movementVector=Vector3.zero;
            if (_pressVector != Vector3.zero)
            {
                movementVector = _pressVector;
                _pressVector = Vector3.zero;
            }
                
           // movementVector.y = 0;
            movementVector = transform.right * movementVector.x + transform.forward * movementVector.z;
            movementVector.Normalize();
            movementVector += Physics.gravity;
            _characterController.Move(_speed * movementVector*Time.deltaTime);
            _mainCamera.localRotation = Quaternion.Euler(-_rotate.y,0,0);
            transform.Rotate(Vector3.up*_playerRotate.x);
           _playerRotate.x = 0;

        }

        private void MoveBack(bool isPressed)
        {
            if(isPressed)
                 _pressVector += Vector3.back;
            

        }

        private void MoveForward(bool isPressed)
        {
            if (isPressed)
                _pressVector += Vector3.forward;
            
        }

        private void MoveRight(bool isPressed)
        {
            if (isPressed)
                _pressVector += Vector3.right;
            
        }

        private void MoveLeft(bool isPressed)
        {
            if (isPressed)
                _pressVector += Vector3.left;
            
        }
        private void RotateX(float mouseX)
        {
            _rotate.x += mouseX * _sensitivity;
            _playerRotate.x = mouseX * _sensitivity;
        }
        private void RotateY(float mouseY)
        {
            _rotate.y += mouseY * _sensitivity;
        }
    }
}
