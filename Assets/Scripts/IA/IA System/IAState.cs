using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/IAState")]
public class IAState : ScriptableObject
{
    public IAAction[] Actions;
    public IATransition[] Transitions;


    public void ExecuteState(IAController controller)
    {
        ExecuteActions(controller);
        ExecuteTransitions(controller);
    }

    private void ExecuteActions(IAController controller)
    {
        if (Actions == null || Actions.Length <= 0) return;
        
        for (int i = 0; i < Actions.Length; i++)
        {
            Actions[i].Act(controller);
        }
    }

    private void ExecuteTransitions(IAController controller)
    {
        if (Transitions == null || Transitions.Length <= 0) return;
        
        else {
            for (int i = 0; i < Transitions.Length; i++)
            {
                // Obtener el valor de la decisión de cada transición
                bool decisionValue = Transitions[i].Decision.Decide(controller);
                
                if (decisionValue)
                {
                    controller.ChangeState(Transitions[i].TrueState);
                }
                else
                {
                    controller.ChangeState(Transitions[i].FalseState);
                }
            }
        }
    }
}
