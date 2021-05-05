using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_BallsReset : MonoBehaviour
{
	[SerializeField] Transform[] spawnPositions;


	/// <summary>
	/// Simply resets the throwable object positions once they land in the zone
	/// </summary>

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Throwable>())
		{
			other.transform.position = spawnPositions[Random.Range(0, 2)].position;
		}
	}
}
