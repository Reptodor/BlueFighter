using UnityEngine;

public class PlayerMovement
{
    private CharacterController _characterController;
    private float _movementSpeed;
    private float _rotateSpeed;

    public PlayerMovement(CharacterController characterController, float movementSpeed, float rotateSpeed)
    {
        _characterController = characterController;
        _movementSpeed = movementSpeed;
        _rotateSpeed = rotateSpeed;
    }

    public void MovePlayer(Vector3 moveDirection)
    {
        moveDirection = moveDirection * _movementSpeed;
        _characterController.Move(moveDirection * Time.deltaTime);
    }
}
