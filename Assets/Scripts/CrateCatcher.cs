using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CrateCatcher : MonoBehaviour
{
	Timer timer;
	MagnetComponent magnet;
	CrateSpawner crateSpawner;

	public GameObject winScreen;

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
				crateSpawner.spawnCrate();
			}
		}
	}

	void Start()
	{
		timer = GameObject.FindObjectOfType<Timer>();
		magnet = GameObject.FindObjectOfType<MagnetComponent>();
		crateSpawner = GameObject.FindObjectOfType<CrateSpawner>();
	}

	void OnTriggerEnter2D(Collider2D collider2D)
	{
			SuccessValue++;
	}

	void OnTriggerExit2D(Collider2D collider2D)
	{
		SuccessValue--;
	}

	void Update()
	{
		//Check for win
		if(SuccessValue >= 5 && !magnet.isAttached)
		{
			winScreen.SetActive(true);
			timer.stop();
		}
	}
}