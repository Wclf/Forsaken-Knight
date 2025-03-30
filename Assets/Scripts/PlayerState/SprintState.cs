using UnityEditor.Build;
using UnityEngine;

public class SprintState : IPlayerState
{
    public void EnterState(PlayerController player)
    {
        player.animator.SetBool("isRunning", true);
    }
    public void UpdateState(PlayerController player)
    {
        player.Move();
        CheckFlip(player);

        if (player.moveInput == Vector2.zero && player.speedAdd == player.speed)
        {
            player.ChangeState(new IdleState());
        }


    }
    public void ExitState(PlayerController player)
    {
        player.animator.SetBool("isRunning", false);
    }

    private void CheckFlip(PlayerController player)
    {
        if (player.moveInput.x > 0 && !player.facingRight)
        {
            player.Flip();
            player.facingRight = true;
        }
        else if (player.moveInput.x < 0 && player.facingRight)
        {
            player.Flip();
            player.facingRight = false;
        }
    }
}
