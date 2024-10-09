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
        [SerializeField] private AudioSource barrelSource;
        
        public void ActivateBarrel()
        {
            rb.isKinematic = false;
            barrelSource.Play();
            StartCoroutine(DeactivateBarrelDamageCoroutine());
        } 
        
        private IEnumerator DeactivateBarrelDamageCoroutine()
        {
            yield return new WaitForSeconds(deactivatingDeathTime);
            
            barrelSource.Stop();
            damageZone.SetActive(false);
        }
    }
}
