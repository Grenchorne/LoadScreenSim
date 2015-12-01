using UnityEngine;
using System.Collections;

/// <summary>
/// A component that destroys the GameObject once it is offscreen.
/// </summary>
public class LateDestroyerComponent : MonoBehaviour
{
	new Renderer renderer;
	private void Start()
	{
		renderer = GetComponent<Renderer>();
		if(!renderer)
			renderer = GetComponentInChildren<Renderer>();
	}
	
	private void Update()
	{
		if(!renderer.isVisible)
			GameObject.Destroy(gameObject);
	}
}