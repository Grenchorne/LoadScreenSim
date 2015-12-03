using UnityEngine;
using System.Collections;

public class TimedDestroyer : MonoBehaviour
{
	private void Start()
	{
		StartCoroutine(destroy());
		}
	
	IEnumerator destroy()
	{
		yield return new WaitForSeconds(3);
		GameObject.Destroy(gameObject);
	}
}