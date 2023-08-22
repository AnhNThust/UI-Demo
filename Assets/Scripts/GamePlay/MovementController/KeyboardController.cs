using UnityEngine;

public class KeyboardController : MonoBehaviour
{
	private Vector3 _inputVector;
	public Vector3 InputVector { get => _inputVector; }

	private void Update()
	{
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		_inputVector = new Vector3(h, v, 0);
	}
}
