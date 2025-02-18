using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWithQuest : NPC
{
    [SerializeField] private QuestSO quest;
    [SerializeField, TextArea(1, 5)] private string[] questPhrases;
    [SerializeField, TextArea(1, 5)] private string[] questStartedPhrases;
    [SerializeField, TextArea(1, 5)] private string[] questNotStartedPhrases;
    [SerializeField, TextArea(1, 5)] private string[] questAfter;
    [SerializeField, TextArea(1, 5)] private string[] questOther;

    private void Start()
    {
        currentPhrases = questPhrases;
        quest.OnQuestCompleted += EndQuest;
    }

    public override void Interact()
    {
        gameManager.NpcInteraction(true, this);

        if (gameManager.ExistsCurrentQuest() && !gameManager.IsMyquest(quest.QuestId) && !quest.IsCompleted)
            currentPhrases = questNotStartedPhrases;

        else if (!quest.IsCompleted && !gameManager.ExistsCurrentQuest() && !quest.IsStarted)
            currentPhrases = questPhrases;

        base.Interact();
    }

    protected override void EndDialog()
    {
        base.EndDialog();

        if (!quest.IsStarted && !gameManager.ExistsCurrentQuest())
        {
            gameManager.StartQuest(quest);
            currentPhrases = questStartedPhrases;
        }
    }

    private void EndQuest(QuestSO quest)
    {
        currentPhrases = phrases;
    }
}
