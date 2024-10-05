using Fireflies;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerInputListener : ITickable
    {
        private readonly FirefliesMovement _firefliesMovement;
        private readonly FirefliesContainer _firefliesContainer;
        
        [Inject]
        private PlayerInputListener(FirefliesMovement firefliesMovement, FirefliesContainer firefliesContainer)
        {
            _firefliesMovement = firefliesMovement;
            _firefliesContainer = firefliesContainer;
        }
        
        public void Tick()
        {
            ReadMovementInput();
            ReadInvisibleAbilityInput();
        }

        private void ReadInvisibleAbilityInput()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;
            
            if(_firefliesContainer.InvisibleModule.IsInvisible)
                _firefliesContainer.MakeVisible();
            else
                _firefliesContainer.MakeInvisible();
        }

        private void ReadMovementInput()
        {
            var inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            
            _firefliesMovement.Move(inputVector.normalized);
        }
    }
}