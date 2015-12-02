using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudGenerator : MonoBehaviour
{
	public Sprite[] cloudSprites;

	public float size_min = 2;
	public float size_max = 3;
	public float speed = 0.75f;
	public float amplitude_min = 0.5f;
	public float amplitude_max = 1;
	public float cycleTime_min = 2.75f;
	public float cycleTime_max = 3.25f;

	public float frequency_max = 6;
	public float frequency_min = 1;

	public float particles_max = 10;
	public float spawnRadius = 3;


	private float spawnTimer;

	void Start()
	{
		resetTimer();
	}

	void resetTimer()
	{
		spawnTimer = Random.Range(frequency_min, frequency_max);
	}

	void Update()
	{
		float deltaTime = Time.deltaTime;
		if(spawnTimer > 0)
		{
			spawnTimer -= deltaTime;
			return;
		}
		if(GameObject.FindObjectsOfType<Cloud>().Length >= particles_max)
			return;
		spawnCloud();
	}

	private void spawnCloud()
	{
		Vector2 pos = transform.position;
		GameObject cloud = Cloud.CreateCloud(	cloudSprites[Random.Range(0, cloudSprites.Length)],
							new Vector2(pos.x, pos.y + Random.Range(spawnRadius * -1 * 0.5f, spawnRadius * 0.5f)),
							speed,
							Random.Range(amplitude_min, amplitude_max),
							Random.Range(cycleTime_min, cycleTime_max),
							Random.Range(size_min, size_max));

		cloud.transform.parent = transform;
		resetTimer();
	}
}
