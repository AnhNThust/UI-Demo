using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HorizontalDragArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
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

	public bool IsStop { get; private set; }

	public void OnDrag(PointerEventData eventData)
	{
		RectTransform rectTransform = GetComponent<RectTransform>();
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
			rectTransform,
			eventData.position,
			eventData.pressEventCamera,
			out Vector2 pos))
		{
			IsStop = false;
			pos.x /= rectTransform.rect.width;

			Vector3 nobPosition = new Vector3(pos.x, 0, 0);
			nobPosition = (nobPosition.magnitude > 1.0f) ? nobPosition.normalized : nobPosition;

			Vector3 joystickPosition = new Vector3(nobPosition.x * rectTransform.rect.width, 0, 0);
			imageJoystick.rectTransform.anchoredPosition = joystickPosition;

			_inputVector = Camera.main.ScreenToWorldPoint(imageJoystick.transform.position);
			_inputVector.y = 0;
			_inputVector.z = 0;
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		OnDrag(eventData);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		IsStop = true;
	}
}
