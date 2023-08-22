using UnityEngine;

public enum MoveDirection
{
	None,
	Up,
	Down
}

public class BulletMovement : MonoBehaviour
{
	[SerializeField]
	private float speed;

	[SerializeField]
	private MoveDirection direction;

	[SerializeField]
	private Vector3 direct;

	[ContextMenu("Reload")]
	private void Reload()
	{
		switch (direction)
		{
			case MoveDirection.None:
				break;
			case MoveDirection.Up:
				direct = Vector3.up;
				break;
			case MoveDirection.Down:
				direct = Vector3.down;
				break;
		}
	}

	private void FixedUpdate()
	{
		transform.Translate(speed * Time.fixedDeltaTime * direct);
	}
}
