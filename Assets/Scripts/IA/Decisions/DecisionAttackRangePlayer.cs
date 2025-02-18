using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "IA/Decisions/DecisionAttackRangePlayer")]
public class DecisionAttackRangePlayer : IADecision
{
	public override bool Decide(IAController controller)
	{
		return InAttackRange(controller);
	}

    private bool InAttackRange(IAController controller)
    {
        if (controller.PlayerReference == null) return false;

        // Calculate the distance between the player and the enemy
        float distance = (controller.PlayerReference.position - controller.transform.position).sqrMagnitude; 
        if (distance < Mathf.Pow(controller.AttackRangeSelected, 2)) return true; // Está en rango de ataque y podemos transicionar al estado de ataque
        return false; // No está en rango de ataque y no podemos transicionar al estado de ataque
    }
}
