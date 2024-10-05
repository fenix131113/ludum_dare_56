using UnityEngine;
using Zenject;

namespace PlayerMovement
{
    public class PlayerInputListener : ITickable
    {
        private readonly FirefliesMovement _firefliesMovement;
        
        [Inject]
        private PlayerInputListener(FirefliesMovement firefliesMovement)
        {
            _firefliesMovement = firefliesMovement;
        }
        
        public void Tick()
        {
            ReadMovementInput();
        }

        private void ReadMovementInput()
        {
            var inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            
            _firefliesMovement.Move(inputVector.normalized);
        }
    }
}