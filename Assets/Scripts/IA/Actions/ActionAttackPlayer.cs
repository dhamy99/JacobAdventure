using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "IA/Actions/ActionAttackPlayer")]
public class ActionAttackPlayer : IAAction
{
    public override void Act(IAController controller)
    {
        AttackPlayer(controller);
    }

    private void AttackPlayer(IAController controller)
    {
        if (controller.PlayerReference == null) return;

        if (controller.CanAtack() == false) return;

        if (controller.PlayerInAttackRange(controller.AttackRangeSelected)) 
        {
            // Atack player
            if (controller.AttackType == AttackTypes.Embestida)
            {
                controller.EmbestidaAttack(controller.Damage);
            }
            else
            {
                controller.MeleeAtack(controller.Damage);

            }
            controller.UpdateTimeBetweenAttacks();
        }

    }
    
}
