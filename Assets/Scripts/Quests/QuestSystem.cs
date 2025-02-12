using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    [SerializeField] private GameObject questPanel;
    [SerializeField] private TMP_Text questName;
    [SerializeField] private TMP_Text questDescription;

    [SerializeField] List<QuestSO> quests;

    private QuestSO currentQuest;

    public QuestSO CurrentQuest { get => currentQuest; }

    public void StartQuest(int questId)
    {
        currentQuest = quests.Where(x => x.QuestId == questId).FirstOrDefault();
        currentQuest.IsStarted = true;
        currentQuest.OnQuestCompleted += EndQuest;
        questName.text = currentQuest.Name;
        questDescription.text = currentQuest.Description;
        questPanel.SetActive(true);
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
        questPanel.SetActive(false);
    }

    // Solo para DEBUG
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentQuest.CompleteQuest();
        }
    }
}
