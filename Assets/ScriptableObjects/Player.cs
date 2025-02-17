using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManager;

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

    private Animator anim;

    [SerializeField] private GameObject pauseMenu;

    private bool isPaused = false;

    public bool IsInteracting { get => isInteracting; set => isInteracting = value; }

    void Start()
    {
        transform.position = gameManager.NewPosition;
        anim = GetComponent<Animator>();

        anim.SetFloat("inputH", gameManager.NewOrientation.x);
        anim.SetFloat("inputV", gameManager.NewOrientation.y);
    }

    // Update is called once per frame
    void Update()
    {
        InputControl();
        Move();
        Pause();
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractionTrigger();
        }
    }

    private void InteractionTrigger()
    {
        collision = CheckInteraction();
        if (collision)
        {
            if (collision.TryGetComponent(out IInteractable interactable))
            {
                if (interactable.transform.TryGetComponent(out Weapon weapon))
                {
                    AudioManager.instance.PlaySFX("Thorn");
                    gameManager.NewItem(weapon.ScriptableObjectData);
                }

                interactable.Interact();
            }
        }
    }

    private void Move()
    {    
        //We control the call to move if the player is not moving and there is a input
        if (!isInteracting && !isMoving && (inputH != 0 || inputV != 0))
        {
            anim.SetBool("isMoving", true);
            anim.SetFloat("inputH", inputH);
            anim.SetFloat("inputV", inputV);
            destinyPoint = transform.position + inputDirection;
            interactionPoint = destinyPoint;
            collision = CheckInteraction();
            if(!collision)
            {
                StartCoroutine(MovePlayer());
            }
        }
        else if(inputH == 0 && inputV == 0)
        {
            anim.SetBool("isMoving", false);
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

    //Pause Control

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.P) && isPaused == false)
        {
            pauseMenu.GetComponent<PauseMenu>().Pause();
            pauseMenu.SetActive(true);
            isPaused = true;
        }

        if (Time.timeScale == 1)
        {
            isPaused = false;
        }
    }
}
