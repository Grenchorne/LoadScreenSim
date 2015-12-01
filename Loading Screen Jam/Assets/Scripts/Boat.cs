using UnityEngine;
using System.Collections;

public class Boat : MonoBehaviour
{
	public float amplitude_vertical = 0.3f;
	public float amplitude_horizontal = 0.1f;
	public float cycleTime = 5;
	
	float degrees_y, degrees_x;

	private	Vector2 position;

	private void Start()
	{
		position = transform.position;
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

		Vector2 offset = new Vector2(	amplitude_horizontal * Mathf.Sin(radians_x),
										amplitude_vertical * Mathf.Sin(radians_y));
		transform.position = position + offset;
	}
}

