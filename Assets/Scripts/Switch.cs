using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
	public GameObject switchMesh;

	bool activated = false;

	[SerializeField] Door doorToActivate;
	[SerializeField] SwitchRoom room;

	/// <summary>
	/// Simple switch object, when the player interacts with the switch, change it's rotation 
	/// and activate the door, or a multitude of other scripts, etc
	/// </summary>
	public void FlipSwitch()
	{
		//Used only for the switchroom puzzle
		if (room != null && !activated)
		{
			switchMesh.transform.Rotate(new Vector3(75, 0, 0), Space.Self);
			activated = true;
			room.SwitchFlicked(this);
			return;
		}
		

		//General switch activation
		if (!activated)
		{
			switchMesh.transform.Rotate(new Vector3(75,0,0), Space.Self);

			if (doorToActivate != null)
			{
				doorToActivate.OpenDoor();
			}

			activated = true;
		}
	}

	public void ResetSwitch()
	{
		if (activated)
		{
			switchMesh.transform.Rotate(new Vector3(-75, 0, 0), Space.Self);
			activated = false;
		}
	}
}
