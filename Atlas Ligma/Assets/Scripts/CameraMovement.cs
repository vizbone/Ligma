using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public float maxCamSize;
	public float minCamSize;
	public float startingCamSize;
	public float scrollSpeed;

	public float cameraPanSpeed;
	public float horizontalBorderOffset;
	public float verticalBorderOffset;

	Camera cam;
	Vector2 screenSize;
	Vector3 forward;
	Vector3 right;
	float horizontalOffset;
	float verticalOffset;

	void Start ()
	{
		cam = GetComponent<Camera>();
		cam.orthographicSize = startingCamSize;
		screenSize = new Vector2 (Screen.width, Screen.height);
		forward = new Vector3 (transform.forward.x, 0, transform.forward.z).normalized;
		right = new Vector3 (transform.right.x, 0, transform.right.z).normalized;
		horizontalOffset = 0;
		verticalOffset = 0;
	}

	void Update ()
	{
		CamFunctions ();
	}

	void CamFunctions () 
	{
		CameraZoom ();
		CameraMove ();
	}

	//handles camera movement
	void CameraMove () 
	{
		if (Input.mousePosition.x <= 10 && horizontalOffset > -horizontalBorderOffset) 
		{ 
			transform.position += -right * cameraPanSpeed;
			horizontalOffset -= cameraPanSpeed;
		}
		if (Input.mousePosition.x >= screenSize.x - 10 && horizontalOffset < horizontalBorderOffset) 
		{ 
			transform.position += right * cameraPanSpeed;
			horizontalOffset += cameraPanSpeed;
		}
		if (Input.mousePosition.y <= 10 && verticalOffset > -verticalBorderOffset) 
		{ 
			transform.position += -forward * cameraPanSpeed;
			verticalOffset -= cameraPanSpeed;
		}
		if (Input.mousePosition.y >= screenSize.y - 10 && verticalOffset < verticalBorderOffset) 
		{ 
			transform.position += forward * cameraPanSpeed;
			verticalOffset += cameraPanSpeed;
		}
	}

	//handles camera zoom function
	void CameraZoom () 
	{
		float size = cam.orthographicSize;
		if (Input.GetAxisRaw ("Mouse ScrollWheel") > 0 && size > minCamSize) { size -= scrollSpeed; } 
		else if (Input.GetAxisRaw ("Mouse ScrollWheel") < 0 && size < maxCamSize) { size += scrollSpeed; }
		if (size > maxCamSize) { size = maxCamSize; }
		if (size < minCamSize) { size = minCamSize; }
		cam.orthographicSize = size;
	}
}