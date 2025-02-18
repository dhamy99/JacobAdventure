using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Actions/DeactivateWaypointMovement")]
public class DeactivateWaypointMovement : IAAction
{
    public override void Act(IAController controller)
	{
		if (controller.EnemyMovement == null) return;

        controller.EnemyMovement.enabled = false; // Deactivate the EnemyMovement script
	}
}
