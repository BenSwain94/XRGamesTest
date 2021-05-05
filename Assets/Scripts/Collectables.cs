using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectables : MonoBehaviour
{
	private int playerScore = 0;

	public int PlayerScore
	{
		get
		{
			return playerScore;
		}
	}

	Text scoreText;

	private void Start()
	{
		scoreText = GameObject.Find("GUI").transform.GetChild(0).GetComponent<Text>();
	}

	private void OnTriggerEnter(Collider other)
	{
		//Check if object colliding with is a pickup item
		if (other.GetComponent<Pickup>() != null)
		{
			CollectItem(other.GetComponent<Pickup>().GetPickedUp());
			other.GetComponent<AudioSource>().Play();
		}
	}

	void CollectItem(int scoreToIncrement)
	{
		playerScore += scoreToIncrement;
		scoreText.text = "SCORE: " + playerScore.ToString();
	}
}
