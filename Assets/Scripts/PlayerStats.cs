using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Stats")]
public class PlayerStats : ScriptableObject
{
    [Header("Stats")]
    public float Damage = 5f;
    public float Defense = 2f;

    [Range(0f, 100f)] public float CriticalProbability;
    [Range(0f, 100f)] public float BlockProbability;


    public void AñadirBonudPorAtributoFuerza()
    {
        Damage += 2f;
        Defense += 1f;
        BlockProbability += 0.03f;
    }

    public void AñadirBonusPorAtributoInteligencia()
    {
        Damage += 3f;
        CriticalProbability += 0.2f;
    }
    
    
    public void ResetearValores()
    {
        Damage = 5f;
        Defense = 2f;
    }

}
