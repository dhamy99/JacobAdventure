using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public static UIManager Instance;
    
    [SerializeField] private Image lifeBar;

    private float currentLife;
    private float maxLife;

    private void Awake()
    {
        Instance = this;
    }

    private void UpdateUIPlayer()
    {
        lifeBar.fillAmount = Mathf.Lerp(lifeBar.fillAmount, currentLife / maxLife, Time.deltaTime * 5f);
    }

    public void UpdateLifeBar(float pCurrentLife, float pMaxLife)
    {
        currentLife = pCurrentLife;
        maxLife = pMaxLife;
        lifeBar.fillAmount = currentLife / maxLife;
    }
}
