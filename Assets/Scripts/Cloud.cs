using UnityEngine;



public class Cloud : MonoBehaviour
{
	private float speed = 1;
	private float amplitude = 1;
	private float cycleTime = 1;

	private float degrees;
	private Vector2 position;

	public static GameObject CreateCloud(	Sprite sprite,
											Vector2 position,
											float speed = 1,
											float amplitude = 1,
											float cycleTime = 1,
											float size = 1)
	{
		GameObject cloudObject = new GameObject("Cloud");
		cloudObject.AddComponent<SpriteRenderer>().sprite = sprite;
		cloudObject.AddComponent<LateDestroyerComponent>();
		Cloud cloud = cloudObject.AddComponent<Cloud>();
		cloud.speed = speed;
		cloud.amplitude = amplitude;
		cloud.cycleTime = cycleTime;
		cloud.transform.localScale = new Vector3(size, size, size);
		cloud.transform.position = position;
		return cloudObject;
	}

	private void Start()
	{
		position = transform.position;
	}

	private void Update()
	{
		float deltaTime = Time.deltaTime;

		float degreesPerSecond = 360 / cycleTime;
		degrees = Mathf.Repeat(degrees + (deltaTime * degreesPerSecond), 360);
		float radians = degrees * Mathf.Deg2Rad;

		//Vector2 offset = new Vector2(0,	amplitude * Mathf.Cos(radians));

		//transform.position = (position + offset);
		//transform.Translate(Vector2.right * speed * deltaTime);
		transform.Translate(new Vector2(speed * deltaTime, position.y + amplitude * Mathf.Cos(radians)));
	}

}
