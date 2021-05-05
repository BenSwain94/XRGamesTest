using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelZone : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "Player")
		{
			//Double checking the player has the correct score the complete the level
			if (other.GetComponent<Collectables>().PlayerScore >= 5)
			{
				//Enable level complete text, should ideally be cached
				GameObject.Find("GUI").transform.GetChild(1).gameObject.SetActive(true);
				//disable player movement!
				other.GetComponent<PlayerMovementController>().enabled = false;
			}
		}
	}
}
