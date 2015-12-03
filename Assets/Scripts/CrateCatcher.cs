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

	private Image[] indicators;
	public Image indicator0;
	public Image indicator1;
	public Image indicator2;
	public Image indicator3;
	public Image indicator4;

	private Color opaqueColor;
	private Color transparentColor;

	private int _successfulDrops;
	public int SuccessfulDrops
	{
		get
		{
			return _successfulDrops;
		}
		set
		{
			if(value < _successfulDrops)
				indicators[_successfulDrops - 1].color = transparentColor;

			else if(value > _successfulDrops)
				indicators[value - 1].color = opaqueColor;

			_successfulDrops = value;
		}
	}

	void Start()
	{
		timer = GameObject.FindObjectOfType<Timer>();
		magnet = GameObject.FindObjectOfType<MagnetComponent>();
		crateSpawner = GameObject.FindObjectOfType<CrateSpawner>();
		indicators = new Image[]
		{
			indicator0,
			indicator1,
			indicator2,
			indicator3,
			indicator4
		};
		opaqueColor = new Color(1, 1, 1, 1);
		transparentColor = new Color(1, 1, 1, 0.3f);
		foreach(Image image in indicators)
			image.color = transparentColor;
	}

	void OnTriggerEnter2D(Collider2D collider2D)
	{
			SuccessfulDrops++;
	}

	void OnTriggerExit2D(Collider2D collider2D)
	{
		SuccessfulDrops--;
	}

	void Update()
	{
		if(SuccessfulDrops >= 5 && !magnet.isAttached)
		{
			winScreen.SetActive(true);
			timer.stop();
		}
	}
}