using Core.Game;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Zenject;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Locations.EndLocation
{
    public class EndLocationTrigger : MonoBehaviour
    {
        private static readonly int Deconstruct = Animator.StringToHash("Deconstruct");
        
        [SerializeField] private LayerMask interactLayer;
        [SerializeField] private float animDuration;
        [SerializeField] private int loadSceneIndex;
        [SerializeField] private Image fader;
        [SerializeField] private Animator playerAnim;

        private GameStates _gameStates;
        
        [Inject]
        private void Construct(GameStates gameState) => _gameStates = gameState;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!LayerService.CheckLayersEquality(other.gameObject.layer, interactLayer))
                return;

            playerAnim.SetTrigger(Deconstruct);
            _gameStates.SetControlState(false);
            fader.DOFade(1f, animDuration)
                .onComplete += () => SceneManager.LoadScene(loadSceneIndex);
        }
    }
}
