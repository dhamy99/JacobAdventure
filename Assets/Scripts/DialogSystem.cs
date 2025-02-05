using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] private GameObject dialogFrame;
    [SerializeField] private TMP_Text dialogText;

    public void ChangeFrameStatus(bool status)
        => dialogFrame.SetActive(status);

    public void SetFrameText(string text)
        => dialogText.text = text;
}
