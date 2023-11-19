using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class VariableManager : MonoBehaviour
{

    private VariableStorageBehaviour variableStorage;
    
    public bool thirdDayreached;
    public TextMeshProUGUI counterText;
    public GameObject counterObject;

    private float ecoPoints;
    private bool lamp_hit = false;
    private bool gotUp = false; // player got up from bed
    private bool awake = false; // player is awake


    public GameObject lightBackground;
    public GameObject darkBackground;

    public GameObject player;
    public GameObject playerSleeping;
    public GameObject playerAwake;


    // Start is called before the first frame update
    void Start()
    {
        variableStorage = FindObjectOfType<VariableStorageBehaviour>(); 

        // Bedroom
        lightBackground.SetActive(false);
        darkBackground.SetActive(true); 
        lamp_hit = false; 
        gotUp = false;
        awake = false;


        // Player
        player.SetActive(false);
        playerAwake.SetActive(false);
        playerSleeping.SetActive(true);

        // Counter
        counterObject.SetActive(false);   
        thirdDayreached = false; 
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$thirdDayreached", out thirdDayreached);
        variableStorage.TryGetValue("$EcoPoints", out ecoPoints);
        variableStorage.TryGetValue("$lamp_hit", out lamp_hit);
        variableStorage.TryGetValue("$gotUp", out gotUp);
        variableStorage.TryGetValue("$awake", out awake);
        thirdDay();
        GotUp();
    }

    public void thirdDay()
    {
        if(thirdDayreached == true){
            print("thirdDayreached");
            counterObject.SetActive(true);
            counterText.text = ecoPoints.ToString();
        }
    }

    public void GotUp()
    {

        if (awake == true)
        {
            player.SetActive(false);
            playerAwake.SetActive(true);
            playerSleeping.SetActive(false);
            lightBackground.SetActive(true);
            darkBackground.SetActive(false);
        }
        else if (gotUp == true)
        {
            player.SetActive(true);
            playerAwake.SetActive(false);
            playerSleeping.SetActive(false);
        }
        else
        {
            player.SetActive(false);
            playerAwake.SetActive(false);
            playerSleeping.SetActive(true);
            lightBackground.SetActive(false);
            darkBackground.SetActive(true);
        }
    }
}
