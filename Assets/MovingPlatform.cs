using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] float moveSpeed;

    bool point1reached = false;

    // Update is called once per frame
    void Update()
    {
		if (!point1reached)
		{
			transform.position = Vector3.MoveTowards(transform.position, points[0].position, Time.deltaTime * moveSpeed);
			if (transform.position == points[0].position)
				point1reached = true;
		}

        if (point1reached)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[1].position, Time.deltaTime * moveSpeed);
            if (transform.position == points[1].position)
                point1reached = false;
        }
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			print("collided");
			other.transform.parent = transform;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
			other.transform.parent = null;
	}
}
