using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class BallZone : MonoBehaviour
{
	public TextMeshProUGUI tm;

	[SerializeField] int ballsHerded;
	[SerializeField] int ballsNeeded;

	Material mat;

	[SerializeField] Color fail;
	[SerializeField] Color success;

	[SerializeField] Door doorToOpen;

	private void Start()
	{
		mat = GetComponent<MeshRenderer>().material;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<MovableObject>() != null)
		{
			//Check if movable object is the correct type
			if (other.GetComponent<MovableObject>().objId == 0)
				UpdateHerd(1);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.GetComponent<MovableObject>() != null)
		{
			//Check if movable object is the correct type
			if (other.GetComponent<MovableObject>().objId == 0)
				UpdateHerd(-1);
		}
	}

	void UpdateHerd(int balls)
	{
		ballsHerded += balls;
		tm.text = "Balls: " + ballsHerded;

		if (ballsNeeded == ballsHerded)
		{
			mat.color = success;
			doorToOpen.DoorOpen = true;
		}
		else
			mat.color = fail;
	}
}
