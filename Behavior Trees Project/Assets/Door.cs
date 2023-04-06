using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = true;
    public bool isLocked = false;

    Vector3 closedRotation = new Vector3(0, 0, 0);
    Vector3 openRotation = new Vector3(0, -135, 0);

    // Start is called before the first frame update
    void Start()
    {
        if (isOpen)
        {
            transform.eulerAngles = openRotation;
        }
        else
        {
            transform.eulerAngles = closedRotation;
        }
    }


    public bool Open()
    {
        if (!isOpen && !isLocked)
        {
            isOpen = true;
            transform.eulerAngles = openRotation;

            Debug.Log("door is now open");
            return true;
        }

        Debug.Log("door was either locked or already open");
        return false;
    }

    public bool Close()
    {
        if (isOpen)
        {
            isOpen = false;
            transform.eulerAngles = closedRotation;

            Debug.Log("door is now closed");
        }
        return true;
    }

    public void ToggleisOpen()
    {
        isOpen = !isOpen;

        if (isOpen)
            transform.eulerAngles = openRotation;
        else
            transform.eulerAngles = closedRotation;

    }
    public void ToggleisLocked()
    {
        isLocked = !isLocked;
    }
}
