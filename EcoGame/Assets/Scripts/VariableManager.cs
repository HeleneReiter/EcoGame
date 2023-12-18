using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;

public class VariableManager : MonoBehaviour
{

    private VariableStorageBehaviour variableStorage;
    
    public bool thirdDayreached;
    public TextMeshProUGUI counterText;
    public GameObject counterObject;

    private bool endingReached;

    private float ecoPoints;
    private bool lamp_hit = false;
    private bool gotUp = false; // player got up from bed
    private bool awake = false; // player is awake


    public GameObject lightBackground;
    public GameObject darkBackground;

    public GameObject player;
    public GameObject playerSleeping;
    public GameObject playerAwake;

    private bool inBedroom = false;
    private bool inBathroom = false;
    private bool inKitchen = false;
    private bool inOffice = false;
    private bool inBar = false;





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

        //Ending
        endingReached = false; 

        
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$thirdDayreached", out thirdDayreached);
        variableStorage.TryGetValue("$EcoPoints", out ecoPoints);
        variableStorage.TryGetValue("$lamp_hit", out lamp_hit);
        variableStorage.TryGetValue("$gotUp", out gotUp);
        variableStorage.TryGetValue("$awake", out awake);
        variableStorage.TryGetValue("$endingReached", out endingReached);

        variableStorage.TryGetValue("$inBedroom", out inBedroom);
        variableStorage.TryGetValue("$inBathroom", out inBathroom);
        variableStorage.TryGetValue("$inKitchen", out inKitchen);
        variableStorage.TryGetValue("$inOffice", out inOffice);
        variableStorage.TryGetValue("$inBar", out inBar);

        thirdDay();
        GotUp();
        Ending();

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

    public void Ending()
    {
        if (endingReached == true)
        {
            SceneManager.LoadScene("End");
        }
    }

   
}
