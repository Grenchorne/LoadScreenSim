using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TimeSpan = System.TimeSpan;

public class Timer : MonoBehaviour
{
	public Text timerText;
	float time = 0;
	bool stopped = false;

	private void Update()
	{
		if(stopped)
			return;

		time += Time.deltaTime;

		TimeSpan timeSpan = TimeSpan.FromSeconds(time);
		timerText.text =  string.Format("{1:D2}m:{2:D2}s:{3:D3}ms", 
                timeSpan.Hours, 
                timeSpan.Minutes, 
                timeSpan.Seconds, 
                timeSpan.Milliseconds);
	}

	public void stop()
	{
		stopped = true;
	}
}