using System.Collections;
using UnityEngine;

namespace Locations.SecondCastle
{
    public class Barrel : MonoBehaviour
    {
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private float deactivatingDeathTime;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private GameObject damageZone;
        
        public void ActivateBarrel()
        {
            rb.isKinematic = false;
            StartCoroutine(DeactivateBarrelDamageCoroutine());
        } 
        
        private IEnumerator DeactivateBarrelDamageCoroutine()
        {
            yield return new WaitForSeconds(deactivatingDeathTime);
            
            damageZone.SetActive(false);
        }
    }
}
