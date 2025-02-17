using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IATransition
{
    // Class to control the transitions between the states of the IA
    public IADecision Decision;
    public IAState TrueState;
    public IAState FalseState; //Referencia al valor que devuelve decision
}
