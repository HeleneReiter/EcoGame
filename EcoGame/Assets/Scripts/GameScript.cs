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

    public GameObject player;

    RaycastHit2D hit;

    public GameObject lightBackground;
    public GameObject darkBackground;
    public DialogueRunner dialogueRunner;

    private InMemoryVariableStorage variableStorage;

    
    


    // Start is called before the first frame update
    void Start()
    {
        variableStorage = FindObjectOfType<InMemoryVariableStorage>();
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        

    }

    // Update is called once per frame
    void Update()
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

            if (hit.collider != null)
            {
                print("Hit: " + hit.collider.gameObject.name + hit.collider.gameObject.tag);

                if (hit.collider.gameObject.tag == "Door")
                {
                    // Tür erkannt
                    print("Door hit");

                    // nächste Scene laden
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

                }

                if (hit.collider.gameObject.name == "lamp")
                {
                    // Lampe erkannt
                    print("Lamp hit");

                    // Lampe einschalten
                    lightBackground.SetActive(true);
                    darkBackground.SetActive(false);

                    LampHit(false);
                    dialogueRunner.StartDialogue("lampOn");
                    // https://docs.yarnspinner.dev/unity-tutorial-projects/example-project-3

                }
            }
            else
            {
                print("No hit");
            }

        }
        
    }

    public void LampHit(bool light_hit)
    {
        bool temp;
        variableStorage.TryGetValue("$light_hit", out temp);
        temp = true;
        variableStorage.SetValue("$light_hit", temp);
        
    }


}
