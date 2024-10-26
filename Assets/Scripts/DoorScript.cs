using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public float openAmount = 0.0f; //0 is closed, 1 is fully open
    public float openSpeed = 3.0f;
    private Vector3 openDir;
    private float doorWidth;
    [SerializeField] private Transform door;
    private bool doorOpen = false;
    public bool doorLocked = true;

    void Start()
    {
        // the door is assumed to be child 0, aligned along the *local* x axis, and at position 0,0,0
        //door = transform.GetChild(0); //null pointer error amended to set in inspector
        openDir = Vector3.right; //direction of door movement

        BoxCollider boxCollider = door.GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            doorWidth = boxCollider.size.x *2; //find door width from box collider
        }
        else
        {
            Debug.LogError("Error: BoxCollider not found on the assigned door object.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (door != null)
            {
            if (doorOpen && openAmount < 1.0f)
            {
                //if player is standing on door trigger open door
                openAmount += Time.deltaTime * openSpeed;
                if (openAmount > 1.0f) openAmount = 1.0f; //clamp to 1.0 fully open
            }
            else if (!doorOpen && openAmount > 0.0f)
            {
                //close door if player is not on trigger
                openAmount -= Time.deltaTime * openSpeed;
                if (openAmount < 0.0f) openAmount = 0.0f; //clamp to 0.0 fully closed
            }
            door.localPosition = openDir * openAmount * doorWidth; //set door local position based on openAmount
        }
    }

    public void UnlockDoor()
    {
        doorLocked = false; //set door to unlocked when UnlockDoor method is called
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player") && doorLocked == false)
        { 
            doorOpen = true; 
        }
  
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Player"))
        { 
        doorOpen = false;
        }
    }


}
