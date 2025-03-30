using UnityEngine;

public class IdleState : IPlayerState
{
    public void EnterState(PlayerController player)
    {
        player.animator.SetBool("isAttack", false);
        player.animator.SetBool("isWalking",false);
        player.animator.SetBool("isRunning", false);
    }

    public void UpdateState(PlayerController player) 
    { 
        if(player.moveInput != Vector2.zero)
        {
            player.ChangeState(new MoveState());
        }
    }

    public void ExitState(PlayerController player)
    {

    }
}
