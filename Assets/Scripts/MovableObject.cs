using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
	[SerializeField] private int objectId;
	
	public int objId
	{
		get
		{
			return objectId;
		}
	}
}
