    using UnityEngine;
    using Utils;

    namespace Enemies
{
    public class EnemyRotateChecker : MonoBehaviour
    {
        [SerializeField] private bool rotateRight;
        [SerializeField] private Enemy enemy;
        [SerializeField] private LayerMask collisionMask;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!LayerService.CheckLayersEquality(collision.gameObject.layer, collisionMask)) return;

            enemy.SetMoveRotate(rotateRight);
        }
    }
}
