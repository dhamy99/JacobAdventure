using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyLifeBar : MonoBehaviour
{
    [SerializeField] private Image lifeBar;

    private float currentLife;
    private float maxLife;

    private void Update()
    {
        lifeBar.fillAmount = Mathf.Lerp(lifeBar.fillAmount, currentLife / maxLife, Time.deltaTime * 5f);
    }

    public void ModifyHealth(float pCurrentLife, float pMaxLife)
    {
        currentLife = pCurrentLife;
        maxLife = pMaxLife;
    }
}
