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
	public float oriHorizontalBorderOffset;
	public float oriVerticalBorderOffset;

	Camera cam;
	Vector2 screenSize;
	Vector3 forward;
	Vector3 right;

	public float horizontalBorderOffset;
	public float verticalBorderOffset;

	float horizontalOffset;
	float verticalOffset;

	Vector3 oriPos;

	void Start ()
	{
		cam = GetComponent<Camera>();
		cam.orthographicSize = startingCamSize;
		screenSize = new Vector2 (Screen.width, Screen.height);
		forward = new Vector3 (transform.forward.x, 0, transform.forward.z).normalized;
		right = new Vector3 (transform.right.x, 0, transform.right.z).normalized;
		horizontalOffset = 0;
		verticalOffset = 0;
		oriPos = transform.position;
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
		BorderOffset ();
	}

	void BorderOffset ()
	{
		float difference = (cam.orthographicSize - startingCamSize) * 1.5f;
		horizontalBorderOffset = oriHorizontalBorderOffset - difference;
		verticalBorderOffset = oriVerticalBorderOffset - difference;
	}

	//handles camera movement
	void CameraMove () 
	{
		bool left = false;
		bool rright = false;
		bool down = false;
		bool up = false;

		if (horizontalOffset < -horizontalBorderOffset)
		{
			left = true;
			horizontalOffset = -horizontalBorderOffset;

		} else if (Input.GetKey (key: KeyCode.A) && horizontalOffset - cameraPanSpeed > -horizontalBorderOffset) 
		{ 
			transform.position += -right * cameraPanSpeed;
			horizontalOffset -= cameraPanSpeed;
		}
		if (horizontalOffset > horizontalBorderOffset)
		{
			rright = true;
			horizontalOffset = horizontalBorderOffset;

		} else if (Input.GetKey (key: KeyCode.D) && horizontalOffset + cameraPanSpeed < horizontalBorderOffset) 
		{ 
			transform.position += right * cameraPanSpeed;
			horizontalOffset += cameraPanSpeed;
		}
		if (verticalOffset < - verticalBorderOffset)
		{
			down = true;
			verticalOffset = -verticalBorderOffset;

		} else if (Input.GetKey (key: KeyCode.S) && verticalOffset - cameraPanSpeed > -verticalBorderOffset) 
		{ 
			transform.position += -forward * cameraPanSpeed;
			verticalOffset -= cameraPanSpeed;
		}
		if (verticalOffset > verticalBorderOffset)
		{
			up = true;
			verticalOffset = verticalBorderOffset;

		} else if (Input.GetKey (key: KeyCode.W) && verticalOffset + cameraPanSpeed < verticalBorderOffset) 
		{ 
			transform.position += forward * cameraPanSpeed;
			verticalOffset += cameraPanSpeed;
		}

		Vector3 newPos = oriPos;
		if (left || rright) newPos += new Vector3 (right.x * horizontalOffset, 0, right.z * horizontalOffset);
		if (down || up) newPos += new Vector3 (forward.x * verticalOffset, 0, forward.z * verticalOffset);

		if ((left || rright) && !(down || up)) newPos += new Vector3 (forward.x * verticalOffset, 0, forward.z * verticalOffset);
		if ((up || down) && !(left || rright)) newPos += new Vector3 (right.x * horizontalOffset, 0, right.z * horizontalOffset);

		if (left || rright || up || down) transform.position = newPos;
	}

	//handles camera zoom functionx
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