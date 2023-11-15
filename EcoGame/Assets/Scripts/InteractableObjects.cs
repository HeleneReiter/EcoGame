using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObjects : MonoBehaviour
{

    public TextMeshProUGUI textDisplay;
    private Vector3 mousePos;
    private Vector3 mousePosWorld;
    private Vector2 mousePosWorld2D; // x und y wert von mousePosWorld
    public Camera mainCamera;
    RaycastHit2D hit;

    void Start()
    {
        textDisplay.text = gameObject.name; //obejektname wird in textfeld angezeigt
        textDisplay.enabled = false; //textfeld wird nicht angezeigt
    }

    void Update()
    {

        mousePos = Input.mousePosition;
        mousePosWorld = mainCamera.ScreenToWorldPoint(mousePos);
        mousePosWorld2D = new Vector2(mousePosWorld.x, mousePosWorld.y);
        hit = Physics2D.Raycast(mousePosWorld2D, Vector2.zero);

        if (hit.collider != null)
        {
            OnMouseOver();

        }
        else if (hit.collider == null)
        {
            OnMouseExit();
        }

    }

    void OnMouseOver()
    {
        Debug.Log("Mouse is over GameObject.");
        textDisplay.enabled = true;
    }

    void OnMouseExit()
    {
        textDisplay.enabled = false;
    }
}
