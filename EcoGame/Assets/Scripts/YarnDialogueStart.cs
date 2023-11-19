using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

// https://docs.yarnspinner.dev/unity-tutorial-projects/example-project-3



public class YarnDialogueStart : MonoBehaviour
{

    private DialogueRunner dialogueRunner;
    private bool isCurrentConversation;
    private bool interactable;
    public string conversationStartNode;


    public void Start()
    {
        interactable = true;
        dialogueRunner = FindObjectOfType<DialogueRunner>();
    }


    private void StartConversation()
    {
        isCurrentConversation = true;
        dialogueRunner.StartDialogue(conversationStartNode);
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);
        interactable = false;
    }

    public void OnMouseDown()
    {
        if (interactable && !dialogueRunner.IsDialogueRunning)
        {
            // then run this character's conversation
            StartConversation();
        }
    }

    private void EndConversation()
    {
        if (isCurrentConversation)
        {
            isCurrentConversation = false;
        }

    }


    // make character not able to be clicked on
    [YarnCommand("disable")]
    public void DisableConversation()
    {
        interactable = false;
    }

}
