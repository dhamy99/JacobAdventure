using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Actions/ActivateWaypointMovement")]
public class ActivateWaypointMovement : IAAction
{
	public override void Act(IAController controller)
	{
		if (controller.EnemyMovement == null) return;

        controller.EnemyMovement.enabled = true; // Enable the EnemyMovement script
	}
    
}
