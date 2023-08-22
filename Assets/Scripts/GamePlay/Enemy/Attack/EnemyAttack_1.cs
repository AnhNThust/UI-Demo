using System.Collections;
using UnityEngine;

public class EnemyAttack_1 : MonoBehaviour
{
	[Header("========== Info ==========")]
	[SerializeField] private Transform barrelParent;
	[SerializeField] private Transform[] barrels;
	[SerializeField] private float coolDown = 3f;

	private EnemyController controller;

	private void OnEnable()
	{
		barrelParent = transform.Find("Texture/BarrelPoint");
		barrels = new Transform[barrelParent.childCount];
		for (int i = 0; i < barrels.Length; i++)
		{
			barrels[i] = barrelParent.GetChild(i);
		}

		controller = GetComponent<EnemyController>();
	}

	private void Start()
	{
		StartCoroutine(Attack());
	}

	private IEnumerator Attack()
	{
		yield return new WaitUntil(() => controller.CanTakeDamage);

		while (true)
		{
			yield return new WaitForSeconds(coolDown);

			for (int i = 0; i < barrels.Length; i++)
			{
				Vector3 oldPos = barrels[i].position;
				Vector3 newPos = new Vector3(oldPos.x, oldPos.y, 0);
				GameObject bullet = PoolingManager.GetObject((int)EnemyBulletId.ENEMY_BULLET_1, newPos, barrels[i].rotation);

				if (bullet.GetComponent<ActionToMirror>() == null)
					_ = bullet.AddComponent(typeof(ActionToMirror)) as ActionToMirror;

				bullet.SetActive(true);
			}
		}
	}
}
