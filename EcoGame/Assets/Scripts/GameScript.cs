using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;
using UnityEngine.Audio;

public class GameScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Vector3 mousePosWorld;
    private Vector2 mousePosWorld2D; // x und y wert von mousePosWorld
    public Camera mainCamera;

    RaycastHit2D hit;

    private DialogueRunner dialogueRunner;
    public string[] startnodes;
    private string currentStartNode;
    private string nextStartNode;
    private bool isCurrentConversation;

    public GameObject[] scenes;
    public GameObject[] texts;

    private GameObject currentText;
    private GameObject nextText;

    private GameObject currentScene;
    private GameObject nextScene;

    public AudioSource audioSource;
    public AudioClip DoorSound;
    public AudioClip SinkSound;
    public AudioClip CoffeeSound;
    public AudioClip DeskSound;




    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();

        currentScene = scenes[0];
        int currentIndex = System.Array.IndexOf(scenes, currentScene);
        nextScene = scenes[(currentIndex + 1) % scenes.Length];

        currentText = texts[0];
        int currentIndexText = System.Array.IndexOf(texts, currentText);
        nextText = texts[(currentIndexText + 1) % texts.Length];

        currentScene.SetActive(true);
        nextScene.SetActive(false);

        currentText.SetActive(true);
        nextText.SetActive(false);

        currentStartNode = startnodes[0];
        int currentIndexStartNode = System.Array.IndexOf(startnodes, currentStartNode);
        nextStartNode = startnodes[(currentIndexStartNode + 1) % startnodes.Length];
    }

    // Update is called once per frame
    void Update()
    {
        Hitdetect();

        int currentIndex = System.Array.IndexOf(scenes, currentScene);
        int nextIndex = (currentIndex + 1) % scenes.Length;

        nextScene = scenes[nextIndex];
        nextText = texts[nextIndex];
        nextStartNode = startnodes[nextIndex];
    }

     private void StartConversation()
    {
        isCurrentConversation = true;
        dialogueRunner.StartDialogue(currentStartNode);
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);
    }
     private void EndConversation()
    {
        if (isCurrentConversation)
        {
            isCurrentConversation = false;
        }

    }

    void SceneChange()
    {
       
        currentScene.SetActive(false);
        nextScene.SetActive(true);

        currentText.SetActive(false);
        nextText.SetActive(true);
        
    }

    void Hitdetect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("Mausklick erkannt");

            // Mausposition abrufen
            mousePos = Input.mousePosition;

            // Position von Bildschirmraum in Weltraum umwandeln
            mousePosWorld = mainCamera.ScreenToWorldPoint(mousePos);

            // Umwandlung von Vector3 in Vector2
            mousePosWorld2D = new Vector2(mousePosWorld.x, mousePosWorld.y);

            // Raycast 2D ausführen und Treffer speichern
            hit = Physics2D.Raycast(mousePosWorld2D, Vector2.zero);

            // Überprüfen, ob der Raycast etwas getroffen hat
            if (hit.collider != null)
            {
                print("Getroffen: " + hit.collider.gameObject.name + " " + hit.collider.gameObject.tag);

                // Weiter mit den Überprüfungen nur, wenn kein Dialog läuft
                if (!dialogueRunner.IsDialogueRunning)
                {
                    string tag = hit.collider.gameObject.tag;

                    if (tag == "Door")
                    {
                        // Logik für Tür
                        SceneChange();
                        StartConversation();
                        audioSource.PlayOneShot(DoorSound);
                    }
                    else if (tag == "Sink")
                    {
                        // Logik für Waschbecken
                        audioSource.PlayOneShot(SinkSound);
                    }
                    else if (tag == "Coffee")
                    {
                        // Logik für Kaffeemaschine
                        audioSource.PlayOneShot(CoffeeSound);
                    }
                    else if (tag == "Desk")
                    {
                        // Logik für Schreibtisch
                        audioSource.PlayOneShot(DeskSound);
                    }
                    // Weitere Bedingungen nach Bedarf hinzufügen
                }
            }
        }
    }
}
