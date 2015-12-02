using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class WaterComponent: MonoBehaviour
{
	private const float drag = 100;
	public ParticleSystem splash;


	#region Props
	public Transform foreground;
	public Transform midground;
	public Transform background;

	public float amplitude_vertical = 0.125f;
	public float amplitude_horizontal = 0f;
	public float cycleTime = 5;

	float degrees;

	Vector2 fore_position;
	Vector2 mid_position;
	Vector2 back_position;

	bool fore_start, mid_start, back_start;
	float startDelay = 0.5f;

	IEnumerator waterDelay()
	{
		if(!fore_start)
		{
			fore_start = true;
			yield return new WaitForSeconds(startDelay);
		}
		if(!mid_start)
		{
			mid_start = true;
			yield return new WaitForSeconds(startDelay);
		}
		if(!back_start)
		{
			back_start = true;
			yield return new WaitForSeconds(startDelay);
		}
		yield break;
	}
	#endregion

	private void Start()
	{
		fore_position = foreground.transform.position;
		mid_position = midground.transform.position;
		back_position = background.transform.position;

		fore_start = false;
		mid_start = false;
		back_start = false;
		StartCoroutine(waterDelay());
	}

	private void Update()
	{
		float deltaTime = Time.deltaTime;

		float degreesPerSecond = 360 / cycleTime;
		degrees = Mathf.Repeat(degrees + (deltaTime * degreesPerSecond), 360);
		float radians = degrees * Mathf.Deg2Rad;

		Vector2 offset = new Vector2(amplitude_horizontal * Mathf.Sin(radians),
										amplitude_vertical * Mathf.Cos(radians));

		if(fore_start)
			foreground.position = fore_position + offset * 1.125f;
		if(mid_start)
			midground.position = mid_position + offset;
		if(back_start)
			background.position = back_position + offset * 1.25f;
	}

	private void OnTriggerEnter2D(Collider2D collider2d)
	{
		Rigidbody2D body = collider2d.GetComponent<Rigidbody2D>();
		if(!body)	body = collider2d.GetComponentInChildren<Rigidbody2D>();
		if(!body)	return;

		body.drag = drag;
		body.fixedAngle = true;
		body.gameObject.AddComponent<LateDestroyerComponent>();

		BoxCollider2D boxCollider2d = body.GetComponent<BoxCollider2D>();
		if(boxCollider2d)
			Component.Destroy(boxCollider2d);

		GameObject.FindObjectOfType<CrateCatcher>().SuccessValue--;

		foreach(Transform child in body.transform)
			GameObject.Destroy(child.gameObject);
	}

	private void Reset()
	{
		BoxCollider2D boxCollider2d = GetComponent<BoxCollider2D>();
		boxCollider2d.isTrigger = true;
	}
}