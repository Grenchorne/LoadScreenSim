using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class WaterComponent: MonoBehaviour
{
	private const float drag = 100;
	public ParticleSystem splash;

	private void OnTriggerEnter2D(Collider2D collider2d)
	{
		if(collider2d.name == "Magnet")	return;
		Rigidbody2D body = collider2d.GetComponent<Rigidbody2D>();
		if(!body)	body = collider2d.GetComponentInChildren<Rigidbody2D>();
		if(!body)	return;

		body.drag = drag;
		body.gameObject.AddComponent<LateDestroyerComponent>();

		foreach(Transform child in body.transform)
			GameObject.Destroy(child.gameObject);
	}

	private void Reset()
	{
		BoxCollider2D boxCollider2d = GetComponent<BoxCollider2D>();
		boxCollider2d.isTrigger = true;
	}
}