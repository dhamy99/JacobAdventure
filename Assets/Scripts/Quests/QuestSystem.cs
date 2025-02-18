using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Objects/QuestSystem")]
public class QuestSystem : ScriptableObject
{
    [NonSerialized] private QuestSO currentQuest;

    public QuestSO CurrentQuest { get => currentQuest; }

    public event Action<QuestSO> OnQuestStarted;
    public event Action OnQuestCompleted;

    public void StartQuest(QuestSO quest)
    {
        currentQuest = quest;
        currentQuest.IsStarted = true;
        currentQuest.OnQuestCompleted += EndQuest;
        OnQuestStarted?.Invoke(currentQuest);
    }

    public void CheckQuestUpdates(IInteractable interactable)
    {
        if (!currentQuest) return;

        if (currentQuest.Type.Equals(QuestType.TalkToNpc))
        {
            var myQuest = (TalkQuest)currentQuest;

            if (interactable.GameObject.TryGetComponent<NPC>(out NPC npc))
            {
                myQuest.CheckIfCompleted(npc.Id);
            }
        }
    }

    public void EndQuest(QuestSO quest)
    {
        currentQuest = null;
        quest.OnQuestCompleted -= EndQuest;
        OnQuestCompleted?.Invoke();
    }
}
