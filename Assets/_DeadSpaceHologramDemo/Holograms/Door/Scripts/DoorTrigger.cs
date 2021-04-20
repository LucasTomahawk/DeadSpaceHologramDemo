using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public OpenDoor Door;
    bool isInRange = false;


    private void OnTriggerStay(Collider other)
    {
        isInRange = true;
        Debug.Log("In Trigger");
    }

    private void OnTriggerExit(Collider other)
    {
        isInRange = false;
        Debug.Log("NOT in Trigger");
        Door.ButtonTryToCloseDoors(); // Close Door on Trigger Exit
    }

    private void Update()
    {
        if (isInRange = true & Input.GetKeyDown(KeyCode.E)) // Open Door Press
        {
            Door.ButtonTryToOpenDoors();
        }
    }
}
