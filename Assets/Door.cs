using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool doorOpen = false;

    public bool DoorOpen
    {
        get
        {
            return doorOpen;
        }
        set
        {
            doorOpen = value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (doorOpen)
        {
            transform.Rotate(0, 90, 0, Space.World);
            doorOpen = true;
            this.enabled = false;
        }
    }
}
