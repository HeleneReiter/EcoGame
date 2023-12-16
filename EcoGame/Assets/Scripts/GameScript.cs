using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;

public class GameScript : MonoBehaviour
{
    public Vector3 mousePos;
    public Vector3 mousePosWorld;
    public Vector2 mousePosWorld2D; // x und y wert von mousePosWorld
    public Camera mainCamera;

    RaycastHit2D hit;

    private DialogueRunner dialogueRunner;

    public GameObject[] scenes;
    public GameObject[] texts;

    public GameObject currentText;
    public GameObject nextText;

    public GameObject currentScene;
    public GameObject nextScene;


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

    }

    // Update is called once per frame
    void Update()
    {
        Hitdetect();
        

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
                        currentScene = nextScene;
                        int currentIndex = System.Array.IndexOf(scenes, currentScene);
                        nextScene = scenes[(currentIndex + 1) % scenes.Length];

                        currentText = nextText;
                        int currentIndexText = System.Array.IndexOf(texts, currentText);
                        nextText = texts[(currentIndexText + 1) % texts.Length];

                        

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
