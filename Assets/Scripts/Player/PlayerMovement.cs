using UnityEngine;

namespace Player
{
    public class PlayerMovement
    {
        private readonly CharacterController _characterController;
        private readonly PlayerInputController _inputController;

        private readonly float _speed;

        public PlayerMovement(CharacterController characterController, PlayerInputController inputController, float speed)
        {
            _characterController = characterController;
            _inputController = inputController;
            _speed = speed;
        }

        public void Tick() => Move();

        private void Move()
        {
            var inputMovement = _inputController.Movement;
            var movement = new Vector3(inputMovement.x, 0, inputMovement.y);
            var motion = movement * Time.deltaTime * _speed;
            _characterController.Move(motion);
        }
    }
}