using System.Collections;
using UnityEngine;
using Utils;

namespace Locations.SecondCastle
{
    public class DeathAltar : MonoBehaviour
    {
        [SerializeField] private GameObject defaultAltar;
        [SerializeField] private GameObject deathAltar;
        [SerializeField] private float toDeathAnimTime;
        [SerializeField] private float toDefaultAnimTime;
        [SerializeField] private CameraShaker cameraShaker;

        private void Start() => StartCoroutine(StartDeathCoroutine());

        // ReSharper disable once FunctionRecursiveOnAllPaths
        private IEnumerator StartDefaultCoroutine()
        {
            defaultAltar.SetActive(true);
            deathAltar.SetActive(false);
            cameraShaker.StopShake();
            
            yield return new WaitForSeconds(toDeathAnimTime);
            
            StartCoroutine(StartDeathCoroutine());
        }
        
        private IEnumerator StartDeathCoroutine()
        {
            defaultAltar.SetActive(false);
            deathAltar.SetActive(true);
            cameraShaker.StartShake();
            
            yield return new WaitForSeconds(toDefaultAnimTime);
            
            StartCoroutine(StartDefaultCoroutine());
        }
    }
}