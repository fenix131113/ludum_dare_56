using Enemies.Data;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyConfig config;
        
        private Rigidbody2D _rb;
        private bool _rightMove;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        
        private void FixedUpdate()
        {
            var xMove = _rightMove ? config.speed * Time.deltaTime : -config.speed * Time.deltaTime;
            _rb.velocity = new Vector2(xMove * 500, 0);
        }

        public void RotateRight()
        {
            _rightMove = true;
        }

        public void RotateLeft()
        {
            _rightMove = false;
        }
    }
}
