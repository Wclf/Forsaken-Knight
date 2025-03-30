using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Playables;

public class MoveState : IPlayerState
{
    public void EnterState(PlayerController player)
    {
        player.animator.SetBool("isWalking", true);

    }

    public void UpdateState(PlayerController player)
    {
        player.Move();
        CheckFlip(player);
        if (player.moveInput == Vector2.zero)
        {
            player.ChangeState(new IdleState());
        }

    }

    public void ExitState(PlayerController player)
    {

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
