using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    private float inputH;
    private float inputV;
    private Vector3 destinyPoint =  new Vector3();
    private Vector3 inputDirection;
    
    private float speed = 5.0f;
    private bool isMoving = false;

    private Vector3 interactionPoint;
    [Header ("InteractionParams")]
    [SerializeField] private float interactionRadius;
    [SerializeField] private LayerMask isCollisionable;

    private Collider2D collision;

    private bool isInteracting = false;

    public bool IsInteracting { get => isInteracting; set => isInteracting = value; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputControl();
        Move();
    }

    private void InputControl()
    {
        if (inputV == 0)
        {
            inputH = Input.GetAxisRaw("Horizontal");
        }
        if (inputH == 0)
        {
            inputV = Input.GetAxisRaw("Vertical");
        }
        inputDirection = new Vector3(inputH, inputV, 0);
    }

    private void Move()
    {    
        //We control the call to move if the player is not moving and there is a input
        if (!isInteracting && !isMoving && (inputH != 0 || inputV != 0))
        {
            destinyPoint = transform.position + inputDirection;
            interactionPoint = destinyPoint;
            collision = CheckInteraction();
            if(!collision)
            {
                StartCoroutine(MovePlayer());
            }
        }
    }

    IEnumerator MovePlayer()
    {
        isMoving = true;    
        while (transform.position != destinyPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinyPoint, speed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
        interactionPoint = transform.position + inputDirection;
    }

    private Collider2D CheckInteraction()
    {
        return Physics2D.OverlapCircle(interactionPoint, interactionRadius, isCollisionable);
    }
}
