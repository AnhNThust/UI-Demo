using UnityEngine;

public class ChangeLength : MonoBehaviour
{
	[SerializeField]
	private LayerMask layerMask;

	private LineRenderer line;
	private RaycastHit2D hit;
	private BulletDamageSender damageSender;

	private void OnEnable()
	{
		line = GetComponent<LineRenderer>();
		damageSender = GetComponent<BulletDamageSender>();
	}

	private void Update()
	{
		hit = Physics2D.Raycast(transform.position, transform.up, 12f, layerMask); // layerMask la Enemy
		if (hit.collider == null)
		{
			line.SetPosition(1, new Vector3(0, 12, 0));
		}
		else
		{
			Vector3 target = hit.collider.transform.position;
			Vector2 pos = hit.collider.ClosestPoint(target);
			line.SetPosition(1, new Vector3(0, pos.y - transform.position.y, 0));

			EnemyTakeDamage etd = hit.collider.transform.GetComponent<EnemyTakeDamage>();
			etd.TakeDamage(damageSender.Damage);
		}
	}
}
