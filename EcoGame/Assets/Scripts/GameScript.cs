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

    private DialogueRunner dialogueRunner;




    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
    }

    // Update is called once per frame
    void Update()
    {
        Hitdetect();

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
                        SceneManager.LoadScene("Level01");
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
