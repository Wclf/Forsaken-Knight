using NUnit.Framework.Interfaces;
using UnityEngine;

public class JumpState : IPlayerState
{
    public void EnterState(PlayerController player)
    {
        player.playerRb.linearVelocity = new Vector2(player.playerRb.linearVelocity.x, player.jumpForce);
        player.animator.SetTrigger("isJumping");
        player.animator.SetBool("IsJumping",true);  
    }

    public void UpdateState(PlayerController player)
    {
        if (player.playerRb.linearVelocity.y <= 0 && player.IsGrounded())
        {
            if(player.speedAdd == player.speed)
            {
                player.ChangeState(new IdleState());

            }
            else if(player.speedAdd == player.speedRun)
            {
                player.ChangeState(new SprintState());

            }
        }

    }

    public void ExitState(PlayerController player)
    {
        player.animator.SetBool("IsJumping", false);

    }
}
