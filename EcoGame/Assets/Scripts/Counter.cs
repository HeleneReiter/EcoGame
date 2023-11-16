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


    // Start is called before the first frame update
    void Start()
    {

        variableStorage = FindObjectOfType<VariableStorageBehaviour>(); //noch zu int umschreiben
        counterObject.SetActive(false);   
        thirdDayreached = false;   
    }

    // Update is called once per frame
    void Update()
    {

        variableStorage.TryGetValue("$thirdDayreached", out thirdDayreached);
        variableStorage.TryGetValue("$EcoPoints", out ecoPoints);
        thirdDay();
    }

    public void thirdDay()
    {
        if(thirdDayreached == true){
            print("thirdDayreached");
            counterObject.SetActive(true);
            counterText.text = ecoPoints.ToString();
        }
    }
}
