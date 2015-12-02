using UnityEngine;
using System.Collections;

public class CrateSpawner : MonoBehaviour
{
	public GameObject objectToSpawn;

	public void spawnCrate()
	{
		GameObject spawnedCrate = GameObject.Instantiate(objectToSpawn);
		spawnedCrate.transform.position = transform.position;
		spawnedCrate.transform.rotation = transform.rotation;

		spawnedCrate.GetComponent<HingeJoint2D>().connectedBody = GameObject.FindObjectOfType<MagnetComponent>().GetComponent<Rigidbody2D>();
	}
}