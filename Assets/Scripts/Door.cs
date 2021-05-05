using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool doorOpen = false;

    /// <summary>
    /// Simply rotate the door 90 degrees when activated from any other script
    /// </summary>
    public void OpenDoor()
    {
        if (!doorOpen)
        {
            transform.Rotate(0, 90, 0, Space.World);
            doorOpen = true;
        }
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.name == "Player")
        {
            if (other.GetComponent<Collectables>().PlayerScore >= 5)
            {
                OpenDoor();
            }
        }
	}
}
