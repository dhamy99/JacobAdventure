using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerLife : LifeSystem
{
    //public static Action DefeatedPlayerEvent;

    public bool Defeated { get; private set; }
    public bool CanBeHealed => Health < maxHealth; // Solo se puede curar si la vida actual es menor a la vida maxima

    private BoxCollider2D boxCollider2D;


    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    protected override void Start()
    {
        base.Start();
        UpdateLifeBar(Health, maxHealth);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))    // Herir al personaje
        {
            RecieveDamage(10);  // Prueba de daño
        }
        
        if (Input.GetKeyDown(KeyCode.Y))    // Curar al personaje
        {
            RestoreHealth(10);  // Prueba de curación
        }
    }

    public void RestoreHealth(float quantity)
    {   // Restaurar salud solo si el personaje puede ser curado y no ha sido derrotado
        if (Defeated)
        {
            return;
        }
        
        if (CanBeHealed)
        {
            Health += quantity;
            if (Health > maxHealth)
            {
                Health = maxHealth;
            }
            
            UpdateLifeBar(Health, maxHealth);
        }
    }


    protected override void PlayerDefeated()
    {
        boxCollider2D.enabled = false;
        Defeated = true;
        //DefeatedPlayerEvent?.Invoke();
    }

    public void RestorePlayer()
    {
        boxCollider2D.enabled = true;
        Defeated = false;
        Health = initialHealth;
        UpdateLifeBar(Health, initialHealth);
    }
    
    protected override void UpdateLifeBar(float vidaActual, float vidaMax)
    {
        //UIManager.Instance.ActualizarVidaPersonaje(vidaActual, vidaMax);
        Debug.Log("Vida actual: " + vidaActual + " Vida maxima: " + vidaMax);
    }

}
