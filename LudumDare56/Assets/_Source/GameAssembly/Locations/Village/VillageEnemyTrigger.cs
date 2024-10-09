using Enemies;
using UnityEngine;
using Utils;

namespace Locations.Village
{
    public class VillageEnemyTrigger : MonoBehaviour
    {
        [SerializeField] private LayerMask interactLayer;
        [SerializeField] private Enemy enemy;
        [SerializeField] private bool isRight = true;

        private bool _isActivated;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!LayerService.CheckLayersEquality(other.gameObject.layer, interactLayer) || _isActivated)
                return;
            
            SendEnemy();
            _isActivated = true;
        }

        private void SendEnemy()
        {
            enemy.gameObject.SetActive(true);
            enemy.StartRunManually(isRight);
        }
    }
}
