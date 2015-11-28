using UnityEngine;
using System.Collections;

public class CraneController : MonoBehaviour
{
	public bool canDeploy;

	Transform pulleyTransform;
	Transform magnetTransform;

	public Transform extremeLeft;
	public Transform extremeRight;

	public float horizontalSpeed = .025f;
	public float verticalSpeed = .1f;

	void Start ()
	{
		pulleyTransform = GameObject.Find("Pulley").transform;
		magnetTransform = GameObject.Find("Magnet").transform;
	}
	
	void Update()
	{
		pulleyTransform.Translate(Vector2.right * Input.GetAxis("Horizontal") * horizontalSpeed);
		magnetTransform.Translate(Vector2.up * Input.GetAxis("Vertical") * verticalSpeed);

		if(pulleyTransform.position.x < extremeLeft.position.x)
			pulleyTransform.position = extremeLeft.position;
		else if(pulleyTransform.position.x > extremeRight.position.x)
			pulleyTransform.position = extremeRight.position;

		if(Input.GetAxisRaw("Toggle Magnet") > 0)
		{

		}
	}
}
