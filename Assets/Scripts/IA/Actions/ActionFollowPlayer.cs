using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Actions/ActionFollowPlayer")]
public class ActionFollowPlayer : IAAction
{
	public override void Act(IAController controller)
	{
		FollowPlayer(controller);
	}

    private void FollowPlayer(IAController controller)
    {
        if (controller.PlayerReference == null)
        {
            return;
        }
        
        // Obtener dirección hacia el jugador
        Vector3 playerDirection = controller.PlayerReference.position - controller.transform.position;
        Vector3 playerDirectionNormalized = playerDirection.normalized;
        float distance = playerDirection.magnitude; // Distancia hasta el jugador

        if (distance >= 1.3f) // Así evitamos que se ponga encima del jugador cuando colisione
        {
            controller.transform.Translate(playerDirectionNormalized * controller.MovementSpeed * Time.deltaTime);
        }
    
    }
}
