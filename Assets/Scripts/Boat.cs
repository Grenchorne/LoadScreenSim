using UnityEngine;
using System.Collections;

public class Boat : MonoBehaviour
{

	public float amplitude_vertical = 0.3f;
	public float amplitude_horizontal = 0.1f;
	public float amplitude_zRotation = 0.1f;

	public float cycleTime = 5;

	float degrees;

	private	Vector2 position;
	private Quaternion rotation;

	private void Start()
	{
		position = transform.position;
		rotation = transform.rotation;
	}
	
	private void Update()
	{
		float deltaTime = Time.deltaTime;
		
		float degreesPerSecond = 360 / cycleTime;
		degrees = Mathf.Repeat(degrees + (deltaTime * degreesPerSecond), 360);
		float radians = degrees * Mathf.Deg2Rad;

		Vector2 offset = new Vector2(amplitude_horizontal * Mathf.Sin(radians),
										amplitude_vertical * Mathf.Cos(radians));
		Quaternion rotationOffset = new Quaternion(rotation.x, rotation.y, amplitude_zRotation * Mathf.Sin(radians), rotation.w);

		transform.position = position + offset;
		transform.rotation = rotation * rotationOffset;
	}
}

