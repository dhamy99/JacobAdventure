using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest/QuestRecollect")]
public class RecollectQuest : QuestSO
{
    [SerializeField] private string itemCollectedTag;
    [SerializeField] private int requiredCollected;

    private int currentCollected = 0;

    private void OnEnable()
    {
        currentCollected = 0;
    }

    public void UpdateQuest(Item item)
    {
        if(item.CompareTag(itemCollectedTag))
        {
            currentCollected++;
            CheckIfCompleted();
        }
    }

    public void CheckIfCompleted()
    {
        if (currentCollected == requiredCollected)
            CompleteQuest();
    }
}
