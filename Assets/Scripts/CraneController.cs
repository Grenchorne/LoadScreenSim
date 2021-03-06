﻿using UnityEngine;
using System.Collections;

public class CraneController : MonoBehaviour
{

	public Transform[] ropeTransforms;
	public Transform controlRope;

	private bool _inRange;
	public bool InRange
	{
		get
		{
			if(!magnetComponent.isAttached)
			{
				if(_inRange)
					MagnetState = Magnet_State.green;
				else
					MagnetState = Magnet_State.red;
			}
			return _inRange;
		}
		set
		{
			_inRange = value;
		}
	}

	public enum Magnet_State
	{
		green, yellow, red
	}

	private Magnet_State _magnetState;
	public Magnet_State MagnetState
	{
		get { return _magnetState; }
		set
		{
			greenLight.SetActive(false);
			yellowLight.SetActive(false);
			redLight.SetActive(false);

			switch(value)
			{
				case Magnet_State.green:
					greenLight.SetActive(true);
					break;
				case Magnet_State.yellow:
					yellowLight.SetActive(true);
					break;
				case Magnet_State.red:
					redLight.SetActive(true);
					break;
			}
			_magnetState = value;
		}
	}

	public GameObject greenLight;
	public GameObject yellowLight;
	public GameObject redLight;


	const float COOLDOWN_TIME = 0.5f;
	private float cooldownTime = 0;
	bool canAttach = true;

	Transform pulleyTransform;
	Transform prePulleyTransform;
	private MagnetComponent magnetComponent;

	public Transform extremeLeft;
	public Transform extremeRight;
	public Transform extremeUp;
	public Transform extremeDown;

	public float horizontalSpeed = .025f;
	public float verticalSpeed = .025f;

	void Start ()
	{
		pulleyTransform = GameObject.Find("Pulley").transform;
		prePulleyTransform = GameObject.Find("PrePulley").transform;
		magnetComponent = GameObject.FindObjectOfType<MagnetComponent>();
	}

	void FixedUpdate()
	{
		controlRope.position = new Vector2(pulleyTransform.transform.position.x, controlRope.position.y);
	}
	
	void Update()
	{
		pulleyTransform.Translate(Vector2.right * Input.GetAxis("Horizontal") * horizontalSpeed);
		prePulleyTransform.Translate(Vector2.up * Input.GetAxis("Vertical") * verticalSpeed);

		if(pulleyTransform.position.x < extremeLeft.position.x)
			pulleyTransform.position = extremeLeft.position;
		else if(pulleyTransform.position.x > extremeRight.position.x)
			pulleyTransform.position = extremeRight.position;

		if(prePulleyTransform.position.y < extremeDown.position.y)
			prePulleyTransform.position = extremeDown.position;
		else if(prePulleyTransform.position.y > extremeUp.position.y)
			prePulleyTransform.position = extremeUp.position;

		canAttach = cooldownTime < 0;
		if(!canAttach)
			cooldownTime -= Time.deltaTime;

		if((InRange || magnetComponent.isAttached) && canAttach)
		{
			if(Input.GetAxisRaw("Toggle Magnet") > 0)
			{
				magnetComponent.ToggleMagnet();
				cooldownTime = COOLDOWN_TIME;
			}
		}
	}

	void LateUpdate()
	{
		InRange = false;
	}
}
