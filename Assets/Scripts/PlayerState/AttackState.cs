using UnityEngine;

public class AttackState : IPlayerState
{
    private int attackIndex;

    public AttackState(int index)
    {
        attackIndex = index;
    }
    public void EnterState(PlayerController player)
    {
        player.animator.SetBool("isAttack", true);
        player.animator.SetTrigger("Attack" + attackIndex);
    }
    public void UpdateState(PlayerController player)
    {
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            player.OnAttackEnd();
        }
    }
    public void ExitState(PlayerController player)
    {

    }
}
