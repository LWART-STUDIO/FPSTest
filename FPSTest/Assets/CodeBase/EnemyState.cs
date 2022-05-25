
using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace CodeBase
{
    [RequireComponent(typeof(NavMeshAgent),typeof(Enemy))]
    public class EnemyState : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Transform _player;

        [SerializeField] private Vector3 _walkPoint;
        private bool _walkPointSet;
        [SerializeField] private float _walkPointRange;
        [SerializeField] private LayerMask _playerLayerMask;
        [SerializeField] private LayerMask _groundLayerMask;

        [SerializeField] private float _timeBetweenAtack;
        private bool _alreadyAttacked;

        [SerializeField] private float _atackRange;
        private bool _playerInAttackRange;
        private Enemy _enemy;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _player = FindObjectOfType<PlayerMover>().transform;
            _enemy = GetComponent<Enemy>();
        }

        private void Update()
        {
            _playerInAttackRange = Physics.CheckSphere(transform.position, _atackRange, _playerLayerMask);

            if (!_playerInAttackRange)
                Patrol();
            else
                AttackPlayer();
        }

        private void Patrol()
        {
            if (!_walkPointSet&&_enemy.CanMoving) GetWalkPoint();
            if (_walkPointSet&&_enemy.CanMoving)
                _agent.SetDestination(_walkPoint);

            Vector3 distnceToWalkPoint = transform.position - _walkPoint;
            if (distnceToWalkPoint.magnitude < 1)
            {
                _walkPointSet = false;
            }
        }

        private void GetWalkPoint()
        {
            float randomX = Random.Range(-_walkPointRange, _walkPointRange);
            _walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z);
            if (Physics.Raycast(_walkPoint, -transform.up, 2f, _groundLayerMask))
            {
                _walkPointSet = true;
            }
        }

        private void AttackPlayer()
        {
            transform.LookAt(_player);
            if (!_alreadyAttacked)
            {
                _enemy.Attack();
                Debug.Log("Atack");
                _alreadyAttacked = true;
                Invoke(nameof(ResetAtack),_timeBetweenAtack);
            }
            
            
        }

        private void ResetAtack()
        {
            _alreadyAttacked = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color=Color.red;
            Gizmos.DrawWireSphere(transform.position,_atackRange);
        }
    }
}
