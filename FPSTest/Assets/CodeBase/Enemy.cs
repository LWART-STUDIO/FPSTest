
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private bool _canMoving;
        public bool CanMoving => _canMoving;
        [SerializeField] private float _moveSpeed;
        private NavMeshAgent _navMeshAgent;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private float _bulletForce;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            _navMeshAgent.speed = _moveSpeed;
        }

        public void Attack()
        {
            GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.AddForce(transform.forward*_bulletForce,ForceMode.Impulse);
        }

        public void DestroyEnemy()
        {
            Destroy(gameObject);
        }
    }
}
