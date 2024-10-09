using System;
using System.Collections;
using DG.Tweening;
using Player;
using UnityEngine;
using Utils;
using Zenject;

namespace Locations.SecondCastle
{
    public class BoxDrop : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D boxRb;
        [SerializeField] private Collider2D boxCollider;
        [SerializeField] private GameObject[] fireflies;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private GameObject interactKeyHelp;

        private PlayerInputListener _inputListener;
        private bool _isUsed;

        [Inject]
        private void Construct(PlayerInputListener inputListener) => _inputListener = inputListener;

        private void StartInteract()
        {
            if(_isUsed)
                return;
            
            interactKeyHelp.SetActive(false);
            _isUsed = true;
            StartCoroutine(InteractCoroutine());
        }

        private IEnumerator InteractCoroutine()
        {
            foreach (var firefly in fireflies)
            {
                yield return new WaitForSeconds(0.05f);
                firefly.SetActive(true);
            }
            
            yield return new WaitForSeconds(0.2f);
            
            foreach (var firefly in fireflies)
                firefly.transform.DOScale(firefly.transform.localScale * 1.5f, 1.5f);
            
            yield return new WaitForSeconds(1.75f);
            
            boxRb.isKinematic = false;
            boxCollider.enabled = true;
            boxRb.AddForce(Vector2.right * 450f, ForceMode2D.Impulse);
            
            foreach (var firefly in fireflies)
                firefly.SetActive(false);

            yield return new WaitForSeconds(.8f);

            boxRb.drag = 25;
        }

        private void Bind() => _inputListener.OnInteractiveKeyDown += StartInteract;

        private void Expose() => _inputListener.OnInteractiveKeyDown -= StartInteract;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(!LayerService.CheckLayersEquality(collision.gameObject.layer, layerMask))
                return;
            
            if(!_isUsed)
                interactKeyHelp.SetActive(true);
            
            Bind();
        }
        
        private void OnTriggerExit2D(Collider2D collision)
        {
            if(!LayerService.CheckLayersEquality(collision.gameObject.layer, layerMask))
                return;
            
            interactKeyHelp.SetActive(false);
            
            Expose();
        }

        private void OnApplicationQuit() => Expose();
    }
}
