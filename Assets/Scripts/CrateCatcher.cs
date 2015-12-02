using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CrateCatcher : MonoBehaviour
{
	MagnetComponent magnet;
	public Slider loadingSlider;
	public Slider failSlider;

	private int _successValue;

	public int SuccessValue
	{
		get { return _successValue; }
		set
		{
			//Remove
			if(value < SuccessValue)
			{
				if(loadingSlider.value > 0)
					loadingSlider.value--;
				else
					failSlider.value++;
			}

			//Add
			else
			{
				if(failSlider.value > 0)
					failSlider.value--;
				else
					loadingSlider.value++;
			}
			_successValue = value;
			if(_successValue <= -3)
			{
				Debug.Log("Load Failed. Try again?");
			}
			if(_successValue >= 5 && !magnet.isAttached)
			{
				Debug.Log("You Win!");
			}

		}
	}

	void Start()
	{
		magnet = GameObject.FindObjectOfType<MagnetComponent>();
	}

	void OnTriggerEnter2D(Collider2D collider2D)
	{
			SuccessValue++;
	}

	void OnTriggerExit2D(Collider2D collider2D)
	{
		SuccessValue--;
	}	
}