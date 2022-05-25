
using UnityEngine;

namespace CodeBase
{
    public class RayCastTarget : MonoBehaviour
    {
       [SerializeField] private Renderer _renderer;
    

        private void OnMouseEnter()
        {
            _renderer.material.color=Color.yellow;
        }

        private void OnMouseExit()
        {
            _renderer.material.color=Color.red;
        }
    }
}
