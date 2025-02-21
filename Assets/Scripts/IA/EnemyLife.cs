using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLife : LifeSystem
{
    [SerializeField] private EnemyLifeBar enemyLifeBarPrefab;
    [SerializeField] private Transform lifeBarPosition;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private EnemyLifeBar enemyLifeBarInstance;
    private IAController controller;

    private EnemyMovement enemyMovement;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        controller = GetComponent<IAController>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    protected override void Start()
    {
        base.Start();
        CreateLifeBar();
    }

    private void CreateLifeBar()
    {
        enemyLifeBarInstance = Instantiate(enemyLifeBarPrefab, lifeBarPosition); 
        UpdateLifeBar(Health, maxHealth);
        //enemyLifeBarInstance.transform.SetParent(lifeBarPosition);
    }

    public override void UpdateLifeBar(float currentLife, float maxLife)
    {
        enemyLifeBarInstance.ModifyHealth(currentLife, maxLife);
    }

    protected override void PlayerDefeated()
    {
        DestroyEnemy();
    }

    private void DestroyEnemy()
    {
        enemyLifeBarInstance.gameObject.SetActive(false);
        controller.enabled = false;
        spriteRenderer.enabled = false;
        boxCollider2D.enabled = false;
        enemyMovement.enabled = false;
    }
}
