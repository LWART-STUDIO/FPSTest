using System;
using System.Collections;
using UnityEngine;

namespace CodeBase
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private MeshRenderer _meshRenderer;
        private Vector3 _prevPosition;
        private Rigidbody _rigidbody;
        private bool _hit;

        private void Start()
        {
            _prevPosition = transform.position;
            StartCoroutine(CheckColision());
        }
        

        

        private IEnumerator CheckColision()
        {
            while (!_hit)
            {
                _prevPosition = transform.position;
                yield return null;
                RaycastHit[] hits =
                    Physics.RaycastAll(new Ray(_prevPosition, (transform.position - _prevPosition).normalized),
                        (transform.position - _prevPosition).magnitude);
                for (int i = 0; i < hits.Length; i++)
                {
                    _hit = true;
                    Destroy(_rigidbody);
                    transform.position = _prevPosition;//hits[i].point;
                    transform.forward=(transform.position - _prevPosition).normalized;
                    _particle.Play();
                    _meshRenderer.enabled = false;
                    if (hits[i].collider.TryGetComponent(out PlayerMover playerMover))
                    {
                        playerMover.GetComponent<CharacterController>().Move(-transform.forward*5f);
                    }

                    if (hits[i].collider.TryGetComponent(out Enemy enemy))
                    {
                        Debug.Log("!Попадание в агента");
                        enemy.DestroyEnemy();
                        Debug.Log("GetEnemy");
                    }
                    StartCoroutine(DestroyTimer());
                    break;
                }
            }
        }
        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
    }
}
