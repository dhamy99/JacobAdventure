using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest/QuestTalk")]
public class TalkQuest : QuestSO
{
    [SerializeField] private int npcId;
    private QuestType type = QuestType.TalkToNpc;

    public int NpcId { get => npcId; }

    public void CheckIfCompleted(int id)
    {
        if (NpcId == id)
            CompleteQuest();
    }
}
