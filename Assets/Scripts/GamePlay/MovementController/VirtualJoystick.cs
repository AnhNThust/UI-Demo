using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	enum JoystickType
	{
		Fixed,
		Floating
	}

	[SerializeField]
	Canvas canvas;

	[SerializeField]
	JoystickType joystickType = JoystickType.Fixed;

	[SerializeField]
	Image imageBg;

	[SerializeField]
	Image imageJoystick;

	private Vector3 _inputVector;
	public Vector3 InputVector
	{
		get => this._inputVector;
	}

	void MoveJoystickToCurrentTouchPosition()
	{
		Vector2 pos;
		RectTransformUtility
			.ScreenPointToLocalPointInRectangle(this.canvas.transform as RectTransform, Input.mousePosition, this.canvas.worldCamera, out pos);
		this.imageBg.rectTransform.position =
			this.canvas.transform.TransformPoint(pos);
	}

	public void OnDrag(PointerEventData eventData)
	{
		Vector2 pos;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
			this.imageBg.rectTransform,
			eventData.position,
			eventData.pressEventCamera,
			out pos))
		{
			pos.x /= this.imageBg.rectTransform.sizeDelta.x;
			pos.y /= this.imageBg.rectTransform.sizeDelta.y;

			this._inputVector = new Vector3(pos.x * 2, pos.y * 2, 0);
			this._inputVector = (this._inputVector.magnitude > 1.0f) ?
				this._inputVector.normalized : this._inputVector;

			Vector3 joystickPosition = new Vector3(
				this._inputVector.x * (this.imageBg.rectTransform.sizeDelta.x * 0.4f),
				this._inputVector.y * (this.imageBg.rectTransform.sizeDelta.y * 0.4f),
				0);
			this.imageJoystick.rectTransform.anchoredPosition = joystickPosition; ;
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (this.joystickType == JoystickType.Floating)
		{
			MoveJoystickToCurrentTouchPosition();
		}

		OnDrag(eventData);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		this._inputVector = Vector3.zero;
		this.imageJoystick.rectTransform.anchoredPosition =
			Vector3.zero;
	}
}
