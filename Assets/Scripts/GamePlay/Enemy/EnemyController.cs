using UnityEngine;

public class EnemyController : MonoBehaviour
{
	[SerializeField] private bool canTakeDamage = false;

	public bool CanTakeDamage { get => canTakeDamage; set => canTakeDamage = value; }

	private void OnDisable()
	{
		canTakeDamage = false;
	}
}
