using UnityEngine;



public class Cloud : MonoBehaviour
{
	private float speed = 1;
	private float amplitude = 1;
	private float cycleTime = 1;

	private float degrees;
	private float positionY;

	public static GameObject CreateCloud(	Sprite sprite,
											Vector2 position,
											float speed = 1,
											float amplitude = 1,
											float cycleTime = 1,
											float size = 1)
	{
		GameObject cloudObject = new GameObject("Cloud");
		SpriteRenderer spriteRenderer = cloudObject.AddComponent<SpriteRenderer>();
		spriteRenderer.sprite = sprite;
		spriteRenderer.sortingOrder = -100;
		cloudObject.AddComponent<LateDestroyerComponent>();
		Cloud cloud = cloudObject.AddComponent<Cloud>();
		cloud.speed = speed;
		cloud.amplitude = amplitude;
		cloud.cycleTime = cycleTime;
		cloud.transform.localScale = new Vector3(size, size, size);
		cloud.transform.position = position;
		cloud.positionY = cloud.transform.position.y;
		return cloudObject;
	}

	private void Update()
	{
		float deltaTime = Time.deltaTime;

		float degreesPerSecond = 360 / cycleTime;
		degrees = Mathf.Repeat(degrees + (deltaTime * degreesPerSecond), 360);
		float radians = degrees * Mathf.Deg2Rad;

		float offsetY = amplitude * Mathf.Cos(radians);
		transform.position = new Vector2(transform.position.x + (speed * deltaTime), offsetY);
	}

}