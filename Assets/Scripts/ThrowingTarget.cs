using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThrowingTarget : MonoBehaviour
{
    [SerializeField] GameObject pickupStar, spawnPos;
	[SerializeField] TextMeshProUGUI text;
	[SerializeField] Door doorToActivate;

	int score = 0;

	/// <summary>
	/// Handles when and if the target board recieves collision from a throwable object
	/// </summary>

	private void OnTriggerEnter(Collider other)
	{
		//If target boards centre trigger is hit by a throwable object, spawn in the pickup
		if (other.GetComponent<Throwable>() != null)
		{
			score++;
			text.text = "Score: " + score.ToString();
			if (score == 3)
			{
				Instantiate(pickupStar, spawnPos.transform.position, Quaternion.identity);
				doorToActivate.OpenDoor();
			}
		}
	}
}
