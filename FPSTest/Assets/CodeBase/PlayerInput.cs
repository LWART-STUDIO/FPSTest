using UnityEngine;
namespace CodeBase
{
    public class PlayerInput : MonoBehaviour
    {
        #region InmutEvents

        public delegate void PressedA(bool isPressed);
        public event PressedA OnPressedA;
        
        public delegate void PressedD(bool isPressed);
        public event PressedD OnPressedD;
        
        public delegate void PressedW(bool isPressed);
        public event PressedW OnPressedW;
        
        public delegate void PressedS(bool isPressed);
        public event PressedS OnPressedS;

        public delegate void MouseX(float mouseX);
        public event MouseX OnMouseX;
        
        public delegate void MouseY(float mouseY);
        public event MouseY OnMouseY;
        
        public delegate void MouseLeftDown(bool isPressed);
        public event MouseLeftDown OnMouseLeftDown;
        
        public delegate void MouseRight(bool isPressed);
        public event MouseRight OnMouseRight;
        

        #endregion
        
        void Update()
        {
            if (Input.GetKey(KeyCode.A)) 
                OnPressedA?.Invoke(isPressed: true);
            
            if (Input.GetKey(KeyCode.D))
                OnPressedD?.Invoke(isPressed: true);
            
            if (Input.GetKey(KeyCode.W)) 
                OnPressedW?.Invoke(isPressed: true);
            
            if (Input.GetKey(KeyCode.S))
                OnPressedS?.Invoke(isPressed: true);
            
            if (Input.GetAxis("Mouse X") != 0) 
                OnMouseX?.Invoke(mouseX: Input.GetAxis("Mouse X"));
            
            if (Input.GetAxis("Mouse Y") != 0) 
                OnMouseY?.Invoke( mouseY: Input.GetAxis("Mouse Y"));

            if (Input.GetMouseButtonDown(0)) 
                OnMouseLeftDown?.Invoke(isPressed: true);

            if (Input.GetMouseButton(1))
            {
                OnMouseRight?.Invoke(isPressed: true);
            }
            else
            {
                OnMouseRight?.Invoke(isPressed: false);
            }
                
        }
    }
}
