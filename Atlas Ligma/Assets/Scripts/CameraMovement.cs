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
		if (ManaSystem.gameStateS == GameStates.started || ManaSystem.gameStateS == GameStates.afterWin)
		{
			CamFunctions();
		}
	}

	void CamFunctions () 
	{
		CameraZoom ();
		CameraMove ();
	}

	//handles camera movement
	void CameraMove () 
	{
		if (Input.GetKey (key: KeyCode.A) && horizontalOffset > -horizontalBorderOffset) 
		{ 
			transform.position += -right * cameraPanSpeed;
			horizontalOffset -= cameraPanSpeed;
		}
		if (Input.GetKey (key: KeyCode.D) && horizontalOffset < horizontalBorderOffset) 
		{ 
			transform.position += right * cameraPanSpeed;
			horizontalOffset += cameraPanSpeed;
		}
		if (Input.GetKey (key: KeyCode.S) && verticalOffset > -verticalBorderOffset) 
		{ 
			transform.position += -forward * cameraPanSpeed;
			verticalOffset -= cameraPanSpeed;
		}
		if (Input.GetKey (key: KeyCode.W) && verticalOffset < verticalBorderOffset) 
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