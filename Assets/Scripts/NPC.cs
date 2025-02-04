using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField, TextArea(1, 5)] private string[] phrases;
    [SerializeField] private float timeBetweenLetters;
    [SerializeField] private GameObject dialogFrame;
    [SerializeField] private TMP_Text dialogText;

    private bool isTalking = false;
    private int currentPhraseIndex = -1;

    public void Interact()
    {
        dialogFrame.SetActive(true);

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
        dialogFrame.SetActive(false);
    }

    private IEnumerator WritePhrase()
    {
        isTalking = true;
        dialogText.text = "";

        char[] phraseCharacters = phrases[currentPhraseIndex].ToCharArray();

        foreach (char c in phraseCharacters)
        {
            dialogText.text += c;
            yield return new WaitForSeconds(timeBetweenLetters);
        }

        isTalking = false;
    }

    private void CompletePhrase()
    {
        StopAllCoroutines();

        dialogText.text = phrases[currentPhraseIndex];
        isTalking = false;
    }

    // SOLO PARA DEBUG: En el proyecto final será el player el que active interactuar
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Interact();
        }
    }
}
