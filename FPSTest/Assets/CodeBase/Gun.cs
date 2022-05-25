using UnityEngine;

namespace CodeBase
{
    public class Gun : MonoBehaviour
    {

        [SerializeField] private Transform _shootPoint;
        [SerializeField] private GameObject _bulletPrefub;
        [SerializeField] private float _forse = 100;
        [SerializeField] private Camera _camera;
        public void Shoot()
        {
            Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit))
                targetPoint = hit.point;
            else
                targetPoint = ray.GetPoint(75);
            Vector3 direction = targetPoint - _shootPoint.position;

            Debug.Log("Shoot");
            GameObject bullet = Instantiate(_bulletPrefub, _shootPoint.position, Quaternion.identity);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.AddForce(direction.normalized * _forse, ForceMode.Impulse);
            
        }
    }
}
