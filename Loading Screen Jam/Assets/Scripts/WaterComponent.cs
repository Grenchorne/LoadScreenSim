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

	float degrees_x, degrees_y;

	Vector2 fore_position;
	Vector2 mid_position;
	Vector2 back_position;

	bool fore_start, mid_start, back_start;

	IEnumerator waterDelay()
	{
		if(!fore_start)
		{
			fore_start = true;
			yield return new WaitForSeconds(0.1f);
		}
		if(!mid_start)
		{
			mid_start = true;
			yield return new WaitForSeconds(0.1f);
		}
		if(!back_start)
		{
			back_start = true;
			yield return new WaitForSeconds(0.1f);
		}
		yield break;
	}
	#endregion

	private void Start()
	{
		fore_position = foreground.transform.position;
		mid_position = midground.transform.position;
		back_position = background.transform.position;

		StartCoroutine(waterDelay());
	}


	private void Update()
	{
		float deltaTime = Time.deltaTime;

		float degreesPerSecond_y = 360  / cycleTime;
		float degreesPerSecond_x = degreesPerSecond_y * 2;

		degrees_x = Mathf.Repeat(degrees_x + (deltaTime * degreesPerSecond_x), 360);
		degrees_y = Mathf.Repeat(degrees_y + (deltaTime * degreesPerSecond_y), 360);
		float radians_x = degrees_x * Mathf.Deg2Rad;
		float radians_y = degrees_y * Mathf.Deg2Rad;

		Vector2 offset = new Vector2(amplitude_horizontal * Mathf.Sin(radians_x),
										amplitude_vertical * Mathf.Sin(radians_y));

		if(fore_start)
			foreground.position = fore_position + offset * 1.125f;
		if(mid_start)
			midground.position = mid_position + offset;
		if(back_start)
			background.position = back_position + offset * 1.25f;
	}

	private void OnTriggerEnter2D(Collider2D collider2d)
	{
		if(collider2d.name == "Magnet")	return;
		Rigidbody2D body = collider2d.GetComponent<Rigidbody2D>();
		if(!body)	body = collider2d.GetComponentInChildren<Rigidbody2D>();
		if(!body)	return;

		body.drag = drag;
		body.fixedAngle = true;
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