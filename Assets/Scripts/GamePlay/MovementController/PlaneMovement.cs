using System;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
	// Toc do di chuyen cua may bay bang joystick
	[SerializeField]
	float joystickMovementSpeed;

	// Toc do di chuyen cua may bay bang ban phim
	[SerializeField]
	float keyboardMovementSpeed;

	// Khoang cach lan cham dau tien
	Vector3 firstTouchDistance;

	private void Update()
	{
		Movement();
	}

	private void Movement()
	{
		switch (PlaneControllerSetting.Instance.controllerType)
		{
			case ControllerType.Keyboard:
				KeyboardMovement();
				break;
			case ControllerType.VirtualJoystick:
				JoystickMovement();
				break;
			case ControllerType.VirtualJoystickHorizontal:
				HorizontalJoystickMovement();
				break;
			case ControllerType.TouchAndDrag:
				TouchAndDragMovement();
				break;
		}

		ClampBoundary();
	}

	void JoystickMovement()
	{
		Vector3 velocity = PlaneControllerSetting.Instance.joystick.InputVector * joystickMovementSpeed;
		transform.position += Time.deltaTime * velocity;

		if (!PlaneControllerSetting.Instance.lookAtByDirection) return;

		if (velocity.magnitude <= 0) return;

		Quaternion to = Quaternion.LookRotation(velocity.normalized);
		transform.localRotation = Quaternion.Slerp(transform.localRotation, to, Time.deltaTime * PlaneControllerSetting.Instance.turnSpeed);
	}

	void HorizontalJoystickMovement()
	{
		Vector3 targetPosition = PlaneControllerSetting.Instance.horizontalJoystick.InputVector;
		Vector3 myPosition = transform.position;

		targetPosition.y = myPosition.y;
		targetPosition.z = myPosition.z;

		if (PlaneControllerSetting.Instance.allowTeleport)
		{
			myPosition.x = targetPosition.x;
			transform.position = myPosition;
		}
		else
		{
			Vector3 velocity = Vector3.zero;

			if ((targetPosition - myPosition).magnitude < 0.1f)
			{
				velocity = Vector3.zero;
			}
			else
			{
				velocity = (targetPosition - myPosition).normalized * joystickMovementSpeed;
			}

			if (PlaneControllerSetting.Instance.horizontalJoystick.IsStop)
			{
				velocity = Vector3.zero;
			}

			transform.position += Time.deltaTime * velocity;
		}
	}

	void TouchAndDragMovement()
	{
		if (PlaneControllerSetting.Instance.touchDragController.touchState == TouchDragController.TouchState.PointerDown)
		{
			Vector3 touchPosition = PlaneControllerSetting.Instance.touchDragController.touchPosition;
			Vector3 myPosition = transform.position;
			myPosition.y = 0;

			firstTouchDistance = myPosition - touchPosition;
		}
		else if (PlaneControllerSetting.Instance.touchDragController.touchState == TouchDragController.TouchState.PointerDrag)
		{
			Vector3 input = PlaneControllerSetting.Instance.touchDragController.InputVector;
			Vector3 myPosition = transform.position;
			Vector3 newPosition = input + firstTouchDistance;

			if (PlaneControllerSetting.Instance.onlyXMovement)
			{
				newPosition.x = myPosition.x;
			}
			transform.position = newPosition;
		}
	}

	void KeyboardMovement()
	{
		// Make the speed same for each direction straight and diagonal
		Vector3 input = Vector3.ClampMagnitude(PlaneControllerSetting.Instance.keyboardController.InputVector, 1.0f);
		Vector3 velocity = input * keyboardMovementSpeed;
		transform.position += Time.deltaTime * velocity;
	}

	void ClampBoundary()
	{
		Vector3 pos = transform.position;
		pos.x = Mathf.Clamp(pos.x, -0.5f, 5.0f);
		pos.y = Mathf.Clamp(pos.y, -0.5f, 5.0f);
		transform.position = pos;
	}
}
