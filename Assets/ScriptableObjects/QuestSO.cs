using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class QuestSO : ScriptableObject
{
    [SerializeField] private int questId;
    [SerializeField] private QuestType type;
    [SerializeField] private new string name;
    [SerializeField, TextArea] private string description;
    
    [NonSerialized] private bool isCompleted = false;
    [NonSerialized] private bool isStarted = false;

    public string Name { get => name; }
    public string Description { get => description; }
    public QuestType Type { get => type; }
    public int QuestId { get => questId; }
    public bool IsStarted { get => isStarted; set => isStarted = value; }
    public bool IsCompleted { get => isCompleted; }

    public delegate void QuestCompleted(QuestSO quest);
    public event QuestCompleted OnQuestCompleted;

    public void CompleteQuest()
    {
        if (!isCompleted)
        {
            isCompleted = true;
            OnQuestCompleted?.Invoke(this);
        }
    }
}
