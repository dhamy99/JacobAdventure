using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    [SerializeField] protected float initialHealth;
    [SerializeField] protected float maxHealth;

    public float Health { get; protected set; }
    
    protected virtual void Start()
    {
        Health = initialHealth;
    }

    public void RecieveDamage(float damageToApply)
    {
        if (damageToApply <= 0)
        {
            return;
        }

        if (Health > 0f)
        {
            Health -= damageToApply;
            UpdateLifeBar(Health, maxHealth);
            if (Health <= 0f)
            {
                Health = 0f;
                UpdateLifeBar(Health, maxHealth);
                PlayerDefeated();
            }
        }
    }

    protected virtual void UpdateLifeBar(float currentLife, float maxLife)
    {
        
    }

    protected virtual void PlayerDefeated()
    {
        
    }
}
