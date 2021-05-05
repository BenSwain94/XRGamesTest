using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
	bool inHand = false;
	Rigidbody rb;
	Collider coll;

	[SerializeField] float throwPower;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		coll = GetComponent<Collider>();
	}


	/// <summary>
	/// When the ball is activated, if it's currently not in hand, then place it in the players,
	/// if it is in hand already, then apply force in the players forward direction
	/// </summary>
	public void Activate(Transform pickupPos)
	{
		//Throw object
		if (inHand)
		{
			rb.isKinematic = false;
			transform.parent = null;
			rb.AddForce(pickupPos.transform.forward * throwPower, ForceMode.Impulse);
			coll.isTrigger = false;
			inHand = false;
		}
		//Pickup if not currently holding
		else
		{
			coll.isTrigger = true;
			rb.isKinematic = true;
			transform.parent = pickupPos;
			transform.position = pickupPos.position;
			inHand = true;
		}
	}
}
