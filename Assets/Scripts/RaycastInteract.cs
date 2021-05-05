using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastInteract : MonoBehaviour
{
	RaycastHit hit;

	[SerializeField] Transform rayPos;
	[SerializeField] Transform pickedUpPos;
	[SerializeField] LayerMask interactMask;
	[SerializeField] Camera cam;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			CastRay();
		}

		Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f, 0));
		Debug.DrawRay(ray.origin, ray.direction * 50, Color.red);
	}

	/// <summary>
	/// Simple raycast whenever the player presses E, Raycasts from the cameras centre, forward
	/// </summary>
	void CastRay()
	{
		Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f, 0));

		if (Physics.Raycast(ray, out hit, 7.5f, interactMask))
		{
			if (hit.transform.GetComponent<Switch>() != null)
			{
				hit.transform.GetComponent<Switch>().FlipSwitch();
			}
			else if (hit.transform.GetComponent<Throwable>() != null)
			{
				hit.transform.GetComponent<Throwable>().Activate(pickedUpPos);
			}
		}
	}
}
