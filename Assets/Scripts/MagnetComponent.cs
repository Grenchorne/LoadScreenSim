using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class MagnetComponent : MonoBehaviour {

	private List<Transform> cratesWithinRange;
	private CraneController craneController;

	private Transform attachedCrateTransform;
	public bool isAttached;

	public void OnTriggerStay2D(Collider2D collider2d)
	{
		if(collider2d.name != "MagnetTrigger")	return;
		craneController.InRange = true;
	}

	public void OnTriggerEnter2D(Collider2D collider2d)
	{
		cratesWithinRange.Add(collider2d.GetComponentInParent<Transform>());
	}

	public void OnTriggerExit2D(Collider2D collider2d)
	{
		cratesWithinRange.Remove(collider2d.GetComponentInParent<Transform>());
	}

	public void ToggleMagnet()
	{
		if(isAttached)
		{
			if(attachedCrateTransform)
			{
				Debug.Log("DETATCH!");
				attachedCrateTransform.GetComponent<HingeJoint2D>().enabled = false;
				isAttached = false;
			}
			return;
		}

		for(int i = 0; i < 100; i++)
		{
			HingeJoint2D joint = cratesWithinRange[i].GetComponentInParent<HingeJoint2D>();
			if(!joint) continue;
			joint.enabled = true;
			attachedCrateTransform = joint.transform;
			isAttached = true;
			return;
		}

		Debug.Log("What happened here?\nIterated through all of the crates and couldn't attach.");
	}

	void Start()
	{
		craneController = GameObject.FindObjectOfType<CraneController>();
		cratesWithinRange = new List<Transform>();
	}
}