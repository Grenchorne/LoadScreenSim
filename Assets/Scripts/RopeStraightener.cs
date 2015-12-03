using UnityEngine;
using System.Collections;

public class RopeStraightener : MonoBehaviour
{

	private bool isUp = false;
	private CraneController craneController;

	private void Start()
	{
		if(name == "Up")	isUp = true;
		else				isUp = false;
		craneController = GameObject.FindObjectOfType<CraneController>();
	}

	private void OnTriggerEnter2D(Collider2D collider2d)
	{
		Rigidbody2D body = collider2d.GetComponent<Rigidbody2D>();
		if(!body)	return;

		body.fixedAngle = isUp;
		craneController.controlRope = body.transform;
	}
}