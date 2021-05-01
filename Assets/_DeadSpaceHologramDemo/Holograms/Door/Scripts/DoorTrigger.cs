using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public OpenDoor Door;
    public DoorHologram DoorHologram;
    public bool isInRange = false;


    public void OnTriggerEnter(Collider other)
    {
        isInRange = true;
        StartCoroutine(DoorHologram.ExpandHolo());
        Debug.Log("In Trigger");
    }



    public void OnTriggerExit(Collider other)
    {
        isInRange = false;
        Door.ButtonTryToCloseDoors(); // Close Door on Trigger Exit
        StartCoroutine(DoorHologram.CollapseHolo());
        Debug.Log("NOT in Trigger");
    }

    private void Update()
    {
        if (isInRange == true & Input.GetKeyDown(KeyCode.E)) // Open Door Press
        {
            if (isInRange == false)
            {
                return;
            }
            Door.ButtonTryToOpenDoors();
            StartCoroutine(DoorHologram.RemoveHolo());
        }
    }
}
