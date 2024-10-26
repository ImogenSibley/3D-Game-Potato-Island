using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //allows us to use UI functions in our code
using TMPro;

public class PlayerPrompt : MonoBehaviour
{
    public TextMeshProUGUI Prompt;
    public TextMeshProUGUI Collection;
    int Crystals = 0;
    int CrystalsMax = 3;

    private DoorScript doorScript;
    // Start is called before the first frame update

    void Start()
    {
        GameObject doorTrigger = GameObject.Find("DoorTrigger");
        doorScript = doorTrigger.GetComponent<DoorScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Lava")
        {
            Prompt.text = "Ouch!";
        }
        else if (other.gameObject.tag == "Crystal")
        {
            Crystals++;
            Collection.text = "Crystals: " + Crystals;
            Destroy(other.gameObject);

            if (Crystals == CrystalsMax)
            {
                Prompt.text = "Door is Unlocked!";
                doorScript.UnlockDoor();
                        
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Prompt.text = "";
    }

}
