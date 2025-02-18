using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private QuestSystem _questSystem;
    [SerializeField] private GameObject questPanel;
    [SerializeField] private TMP_Text questName;
    [SerializeField] private TMP_Text questDescription;

    private void Start()
    {
        _questSystem.OnQuestStarted += ShowQuest;
        _questSystem.OnQuestCompleted += HideQuest;

        if (_questSystem.CurrentQuest != null)
            ShowQuest(_questSystem.CurrentQuest);
    }

    private void ShowQuest(QuestSO quest)
    {
        questName.text = quest.Name;
        questDescription.text = quest.Description;
        questPanel.SetActive(true);
    }

    private void HideQuest()
    {
        questPanel.SetActive(false);
    }

    private void OnDisable()
    {
        _questSystem.OnQuestStarted -= ShowQuest;
        _questSystem.OnQuestCompleted -= HideQuest;
    }
}
