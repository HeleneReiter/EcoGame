using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;

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
            print("Mouse button pressed");

            // Get the mouse position
            mousePos = Input.mousePosition;

            // pos von screen space zu world space umwandeln
            mousePosWorld = mainCamera.ScreenToWorldPoint(mousePos);

            // umwandeln von Vector3 zu Vector2
            mousePosWorld2D = new Vector2(mousePosWorld.x, mousePosWorld.y);

            // Raycast 2D --> hit abspeichern
            hit = Physics2D.Raycast(mousePosWorld2D, Vector2.zero);


            if (!dialogueRunner.IsDialogueRunning)
            {
                if (hit.collider != null)
                {
                    print("Hit: " + hit.collider.gameObject.name + hit.collider.gameObject.tag);
                    if (hit.collider.gameObject.tag == "Door")
                    {
                        // Tür erkannt
                        print("Door hit");

                        // nächste Scene laden

                        SceneChange();
                        StartConversation();


                        currentStartNode = nextStartNode;
                        currentScene = nextScene;
                        currentText = nextText;
                  
                        

                        

                    }
                }
                else
                {
                    print("No hit");
                }

            }
        }
    }
}
