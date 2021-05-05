using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class SwitchRoom : MonoBehaviour
{
	[SerializeField] Switch[] switches;

	//Order that the switches need to be activated in
	int[] switchOrder = new int[] {0,1,2,3,4};
	//Orde the player has flicked the switches in
	int[] inputOrder = new int[] {5,5,5,5,5};

	[SerializeField] Door doorToOpen;

	private void Start()
	{
		ShuffleArray();
	}

	/// <summary>
	/// shuffles the arrays so the switch order is different every time
	/// </summary>
	void ShuffleArray()
	{
		for (int i = 0; i < switchOrder.Length; i++)
		{
			int random = Random.Range(0, switchOrder.Length);

			int tempInt = switchOrder[random];
			Switch tempSwitch = switches[random];

			switchOrder[random] = switchOrder[i];
			switches[random] = switches[i];

			switchOrder[i] = tempInt;
			switches[i] = tempSwitch;
		}
	}

	/// <summary>
	/// Once a switch is flicked this is called and the input order is calculated
	/// </summary>
	/// <param name="flicked"></param>
	public void SwitchFlicked(Switch flicked)
	{
		int switchId = 0;

		//Get the id of the switch being flicked - Could just hard code these in on the switches, but wanted to randomize it 
		//and keep it all within this script
		for (int i = 0; i < switches.Length; i++)
		{
			if (switches[i] == flicked)
			{
				switchId = i;
			}
		}


		//Check if the flicked switch is the first in the array
		if (switchId == 0)
		{
			print("first");
			inputOrder[switchId] = 0;
		}
		//if the inputOrder[switchId - 1] != 5 (it's default value out of the array length) it means the the flicked switch is the next switch in the list 
		else if (inputOrder[switchId - 1] != 5)
		{
			print("second " + (inputOrder[switchId- 1]));
			inputOrder[switchId] = switchId;
		}
		//If the next inputOrder == 5 then it means the flicked switch is not the next one in the list
		else if (inputOrder[switchId - 1] == 5)
		{
			print("resetting");

			inputOrder = new int[] { 5, 5, 5, 5, 5 };

			for (int i = 0; i < switches.Length; i++)
			{
				switches[i].ResetSwitch();
			}
			return;
		}

		//Check the inputOrder array correctly equals 0,1,2,3,4 and if so open the door
		if (CompletionCheck())
		{
			doorToOpen.OpenDoor();

			for (int i = 0; i < switches.Length; i++)
			{
				switches[i].enabled = false;
			}
		}

	}

	bool CompletionCheck()
	{
		int[] compare = new int[] { 0, 1, 2, 3, 4 };

		for (int i = 0; i < inputOrder.Length; i++)
		{
			if (inputOrder[i] != compare[i])
				return false;
		}

		return true;
	}
}
