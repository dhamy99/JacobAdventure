using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AttackTypes
{
    Melee,
    Embestida
}

public class IAController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private PlayerStats stats;


    [Header("States")]
    [SerializeField] private IAState initialState;
    [SerializeField] private IAState defaultState;


    [Header("Detections")]
    [SerializeField] private float detectionRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float embestidaRange;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float movementSpeed;


    [Header("Attack")]
    [SerializeField] private float damage;
    [SerializeField] private float attackRate; // Tiempo entre ataque y ataque
    [SerializeField] private AttackTypes attackType;
    private float nextAttackTime; // Tiempo para el siguiente ataque


    [Header("Debug")]
    [SerializeField] private bool showDetection;
    [SerializeField] private bool showAttackRange;
    [SerializeField] private bool showEmbestidaRange;

    private BoxCollider2D boxCollider2D;


    // Getters and Setters of Properties
    public Transform PlayerReference { get; set; }   
    public IAState currentState { get; set; }   // Para manejar la propiedad del estado actual de la IA
    public EnemyMovement EnemyMovement { get; set; }
    public float DetectionRange => detectionRange;
    public float MovementSpeed => movementSpeed;
    public float Damage => damage;
    public AttackTypes AttackType => attackType;
    public LayerMask PlayerLayer => playerLayer;
    public float AttackRangeSelected => attackType == AttackTypes.Embestida ? embestidaRange : attackRange; // Devuelve el rango según el tipo de ataque seleccionado
    

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        currentState = initialState;
        EnemyMovement = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        currentState.ExecuteState(this);
    }

    public void ChangeState(IAState newState)
    {
        if (newState != defaultState) 
        {
            currentState = newState;
        }
    }

    public void MeleeAtack(float damageQuantity)
    {
        if (PlayerReference != null)
        {
            ApplyDamageToPlayer(damageQuantity);
        }
    }

    public void EmbestidaAttack(float damageQuantity)
    {
        StartCoroutine(IEEmbestida(damageQuantity));
    }

    private IEnumerator IEEmbestida(float damageQuantity)
    {
        Vector3 playerPosition = PlayerReference.position;
        Vector3 initialPosition = transform.position;
        Vector3 directionToPlayer = (playerPosition - transform.position).normalized;
        Vector3 attackPosition = playerPosition - directionToPlayer * 0.5f; // Al atacar al player no nos colocamos encima de él

        boxCollider2D.enabled = false; // Desactivamos el collider para que no colisione con el player  

        float attackTransition = 0f;
        while (attackTransition <= 1f)
        {
            attackTransition += Time.deltaTime * movementSpeed;
            float interpolation = (-Mathf.Pow(attackTransition, 2) + attackTransition) * 4f; // Interpolación cuadrática -> Permite que el enemigo vaya a la posicion del player y regrese a la suya    
            transform.position = Vector3.Lerp(initialPosition, attackPosition, interpolation); // Actualizamos la posición del enemigo
            yield return null;
        }
        if (PlayerReference != null)
        {
            ApplyDamageToPlayer(damageQuantity);
        }
        boxCollider2D.enabled = true; // Activamos el collider para que colisione con el player
    }

    public void ApplyDamageToPlayer(float damageQuantity)
    {
        float damageToApply = 0;
        if (Random.value < stats.BlockProbability / 100) return; // No se le puede hacer daño al jugador
        // Si el jugador no bloquea el daño
        damageToApply = Mathf.Max(damageQuantity - stats.Defense, 1f); // Calculamos el daño a aplicar y nos aseguramos que no sea 0
        PlayerReference.GetComponent<PlayerLife>().RecieveDamage(damageToApply);
    }

    public bool PlayerInAttackRange(float range)
    {
        float distanceToPlayer = (PlayerReference.position - transform.position).sqrMagnitude; // Distancia al cuadrado para comparar dos posiciones
        if (distanceToPlayer < Mathf.Pow(range, 2)) return true; // Si la distancia al cuadrado es menor que el rango al cuadrado
        return false;
    }

    public bool CanAtack()
    {
        if (Time.time > nextAttackTime) return true; // Si el tiempo actual es mayor al tiempo del siguiente ataque podemos atacar
        return false;
    }

    public void UpdateTimeBetweenAttacks()
    {
        nextAttackTime = Time.time + attackRate;
    }

    private void OnDrawGizmos()
    {
        if (showDetection)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }
        if (showAttackRange)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
        if (showEmbestidaRange)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, embestidaRange);
        }
    }

}
