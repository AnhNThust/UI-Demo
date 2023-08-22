using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchDragController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	[SerializeField]
	Canvas canvas;

	[SerializeField]
	Image imageJoystick;

	private Vector3 _inputVector;
	public Vector3 InputVector
	{
		get => _inputVector;
	}

	public Vector3 touchPosition { get; private set; }

	public enum TouchState
	{
		PointerDown,
		PointerDrag
	}

	public TouchState touchState { get; private set; }

	public void OnDrag(PointerEventData eventData)
	{
		if (Time.timeScale <= 0.0f)
		{
			return;
		}

		MoveKnob(eventData);
		touchState = TouchState.PointerDrag;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		MoveKnob(eventData);
		touchPosition = _inputVector;
		touchState = TouchState.PointerDown;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
	}

	/// <summary>
	/// Move joystick image
	/// </summary>
	/// <param name="e"></param>
	void MoveKnob(PointerEventData e)
	{
		RectTransform rectTransform = GetComponent<RectTransform>();

		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
			rectTransform,
			e.position,
			e.pressEventCamera,
			out Vector2 pos
			))
		{
			pos.x /= rectTransform.rect.width;
			pos.y /= rectTransform.rect.height;

			Vector3 knobPosition = new Vector3(pos.x, pos.y, 0);
			knobPosition = (knobPosition.magnitude > 1.0f) ? knobPosition.normalized : knobPosition;

			Vector3 joystickPosition = new Vector3(
				knobPosition.x * rectTransform.rect.width,
				knobPosition.y * rectTransform.rect.height,
				0);
			imageJoystick.rectTransform.anchoredPosition = joystickPosition;

			_inputVector = Camera.main.ScreenToWorldPoint(imageJoystick.transform.position);
			_inputVector.y = 0;
		}
	}
}
