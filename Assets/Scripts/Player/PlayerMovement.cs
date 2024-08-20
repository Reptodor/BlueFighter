using UnityEngine;

public class PlayerMovement
{
    private Animator _animator;
    private CharacterController _characterController;
    private float _movementSpeed;
    private float _rotateSpeed;

    public PlayerMovement(Animator animator,CharacterController characterController, float movementSpeed, float rotateSpeed)
    {
        _animator = animator;
        _characterController = characterController;
        _movementSpeed = movementSpeed;
        _rotateSpeed = rotateSpeed;
    }

    public void MovePlayer(Vector3 moveDirection)
    {
        if(moveDirection.magnitude == 0)
        {
            _animator.SetBool("IsMoving", false);
        }
        else
        {
            _animator.SetBool("IsMoving", true);
        }
        moveDirection = moveDirection * _movementSpeed;
        _characterController.Move(moveDirection * Time.deltaTime);
    }
}
