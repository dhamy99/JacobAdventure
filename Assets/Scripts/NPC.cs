using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour, Interactable
{
    [SerializeField] private GameManagerSO gameManager;
    [SerializeField, TextArea(1, 5)] private string[] phrases;
    [SerializeField] private float timeBetweenLetters;

    private bool isTalking = false;
    private int currentPhraseIndex = -1;

    public void Interact()
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

    private void NextPhrase()
    {
        currentPhraseIndex++;

        if (currentPhraseIndex >= phrases.Length)
        {
            EndDialog();
            return;
        }

        StartCoroutine(WritePhrase());
    }

    private void EndDialog()
    {
        isTalking = false;
        currentPhraseIndex = -1;
        gameManager.NpcInteraction(false);
        gameManager.ChangePlayerStatus(true);
    }

    private IEnumerator WritePhrase()
    {
        isTalking = true;
        gameManager.NpcTalk("");

        char[] phraseCharacters = phrases[currentPhraseIndex].ToCharArray();

        var currentPhrase = "";

        foreach (char c in phraseCharacters)
        {
            currentPhrase += c;
            gameManager.NpcTalk(currentPhrase);
            yield return new WaitForSeconds(timeBetweenLetters);
        }

        isTalking = false;
    }

    private void CompletePhrase()
    {
        StopAllCoroutines();

        gameManager.NpcTalk(phrases[currentPhraseIndex]);
        isTalking = false;
    }

    // SOLO PARA DEBUG: En el proyecto final será el player el que active interactuar
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.I))
    //    {
    //        Interact();
    //    }
    //}
}
