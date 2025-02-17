using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : WaypointMovement
{
    [SerializeField] private MovementDirection movementDirection;

    //private readonly int caminarAbajo = Animator.StringToHash("CaminarAbajo");
    
    // protected override void RotateCharacter()
    // {
    //     if (movementDirection != MovementDirection.Horizontal)
    //     {
    //         return;
    //     }

    //     if (DestinationPoint.x > lastPosition.x)
    //     {
    //         transform.localScale = new Vector3(1, 1, 1);
    //     }
    //     else
    //     {
    //         transform.localScale = new Vector3(-1, 1, 1);
    //     }
    // }

    // protected override void RotateCharacterVertical()
    // {
    //     if (movementDirection != MovementDirection.Vertical) return;

    //     if (DestinationPoint.y > lastPosition.y)
    //     {
    //         _animator.SetBool(caminarAbajo, false);
    //     }
    //     else
    //     {
    //         _animator.SetBool(caminarAbajo, true);
    //     }
    // }
}
