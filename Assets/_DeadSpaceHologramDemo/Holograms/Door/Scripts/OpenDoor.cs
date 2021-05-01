using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject doorTrigger;
    public GameObject doorHologram;
    public Transform leftDoor;
    public Transform rightDoor;
    public float moveSpeed = 1;
    public float sizeOfDoorInX = 1;
    public float amountOfDoorInWall = 0.9f;

    bool isInRange = false;

    private Vector3 leftDoorCloseTarget;
    private Vector3 rightDoorCloseTarget;
    private Vector3 leftDoorOpenTarget;
    private Vector3 rightDoorOpenTarget;
    private float startTime;
    private float totalDistanceToCover;
    private bool areDoorsOpen = false;
    private bool isDoorOpenButtonPressed = false;
    private bool isDoorCloseButtonPressed = false;

    void Start()
    {
        SetInitialReferences();
    }

    // Update is called once per frame
    void Update()
    {

        OpenDoors();
        CloseDoors();
    }


    void SetInitialReferences()
    {
        leftDoorCloseTarget = leftDoor.localPosition;
        rightDoorCloseTarget = rightDoor.localPosition;

        leftDoorOpenTarget = new Vector3(
            leftDoor.localPosition.x - (sizeOfDoorInX * amountOfDoorInWall),
            leftDoor.localPosition.y,
            leftDoor.localPosition.z);

        rightDoorOpenTarget = new Vector3(
            rightDoor.localPosition.x + (sizeOfDoorInX * amountOfDoorInWall),
            rightDoor.localPosition.y,
            rightDoor.localPosition.z);

        totalDistanceToCover = Vector3.Distance(leftDoorCloseTarget, leftDoorOpenTarget);
    }

    void OpenDoors()
    {
        if (areDoorsOpen || !isDoorOpenButtonPressed)
        {
            return;
        }

        float distanceCovered = (Time.time - startTime) * moveSpeed;
        float fractionOfJourney = distanceCovered / totalDistanceToCover;
        leftDoor.localPosition = Vector3.Lerp(leftDoor.localPosition, leftDoorOpenTarget, fractionOfJourney);
        rightDoor.localPosition = Vector3.Lerp(rightDoor.localPosition, rightDoorOpenTarget, fractionOfJourney);

        if (Mathf.Approximately(rightDoor.localPosition.x, rightDoorOpenTarget.x))
        {
            Debug.Log("Doors Opened");
            areDoorsOpen = true;
            isDoorOpenButtonPressed = false;
        }
    }

    void CloseDoors()
    {
        if (!areDoorsOpen || !isDoorCloseButtonPressed)
        {
            return;
        }

        float distanceCovered = (Time.time - startTime) * moveSpeed;
        float fractionOfJourney = distanceCovered / totalDistanceToCover;
        leftDoor.localPosition = Vector3.Lerp(leftDoor.localPosition, leftDoorCloseTarget, fractionOfJourney);
        rightDoor.localPosition = Vector3.Lerp(rightDoor.localPosition, rightDoorCloseTarget, fractionOfJourney);

        if (Mathf.Approximately(rightDoor.localPosition.x, rightDoorCloseTarget.x))
        {
            Debug.Log("Doors Closed");
            areDoorsOpen = false;
            isDoorCloseButtonPressed = false;
        }
    }

    public void ButtonTryToOpenDoors()
    {
        if (areDoorsOpen)
        {
            return;
        }

        startTime = Time.time;
        isDoorOpenButtonPressed = true;
    }

    public void ButtonTryToCloseDoors()
    {
        if (!areDoorsOpen)
        {
            return;
        }
        startTime = Time.time;
        isDoorCloseButtonPressed = true;
    }
}