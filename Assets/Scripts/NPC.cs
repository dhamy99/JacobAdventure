using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private int id;
    [SerializeField] protected GameManagerSO gameManager;
    [SerializeField, TextArea(1, 5)] protected string[] phrases;
    [SerializeField] protected float timeBetweenLetters;

    protected bool isTalking = false;
    protected int currentPhraseIndex = -1;
    protected string[] currentPhrases;

    public int Id { get => id; }

    public GameObject GameObject => gameObject;

    private void Start()
    {
        currentPhrases = phrases;
    }

    public virtual void Interact()
    {
        gameManager.ChangePlayerStatus(false);
        gameManager.NpcInteraction(true);

        if (!isTalking)
        {
            NextPhrase();
            return;
        }

        CompletePhrase();
    }

    protected void NextPhrase()
    {
        currentPhraseIndex++;

        if (currentPhraseIndex >= currentPhrases.Length)
        {
            EndDialog();
            return;
        }

        StartCoroutine(WritePhrase());
    }

    protected virtual void EndDialog()
    {
        isTalking = false;
        currentPhraseIndex = -1;
        gameManager.NpcInteraction(false);
        gameManager.ChangePlayerStatus(true);

        gameManager.EndInteraction(this);
    }

    protected IEnumerator WritePhrase()
    {
        isTalking = true;
        gameManager.NpcTalk("");

        char[] phraseCharacters = currentPhrases[currentPhraseIndex].ToCharArray();

        var currentPhrase = "";

        foreach (char c in phraseCharacters)
        {
            currentPhrase += c;
            gameManager.NpcTalk(currentPhrase);
            yield return new WaitForSeconds(timeBetweenLetters);
        }

        isTalking = false;
    }

    protected void CompletePhrase()
    {
        StopAllCoroutines();

        gameManager.NpcTalk(currentPhrases[currentPhraseIndex]);
        isTalking = false;
    }
}
