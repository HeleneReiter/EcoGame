using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class Counter : MonoBehaviour
{

    private VariableStorageBehaviour variableStorage;
    
    public bool thirdDayreached;
    public TextMeshProUGUI counterText;
    public GameObject counterObject;

    private float ecoPoints;
    private bool lamp_hit = false;
    public GameObject lightBackground;
    public GameObject darkBackground;


    // Start is called before the first frame update
    void Start()
    {
        variableStorage = FindObjectOfType<VariableStorageBehaviour>(); 
        lightBackground.SetActive(false);
        darkBackground.SetActive(true);
        counterObject.SetActive(false);   
        thirdDayreached = false;  
        lamp_hit = false; 
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$thirdDayreached", out thirdDayreached);
        variableStorage.TryGetValue("$EcoPoints", out ecoPoints);
        variableStorage.TryGetValue("$lamp_hit", out lamp_hit);
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
        if (lamp_hit == true)
        {
            lightBackground.SetActive(true);
            darkBackground.SetActive(false);
        }

    }
}
