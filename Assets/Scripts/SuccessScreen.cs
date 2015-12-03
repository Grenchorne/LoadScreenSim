using UnityEngine;
using System.Collections;

public class SuccessScreen : MonoBehaviour
{
	private void Update()
	{
		if(Input.GetKey(KeyCode.Q))
		{
			Application.Quit();
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;			
#endif
		}

		if(Input.GetKey(KeyCode.L))
		{
			Application.LoadLevel(0);
		}
	}
}