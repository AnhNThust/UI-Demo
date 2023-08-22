using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAttack_2 : MonoBehaviour
{
	[Header("========== Info ==========")]
	[SerializeField] private EnemyBulletId bulletId;
	[SerializeField] private Transform barrel;
	[SerializeField] private float coolDown = 5;
	[SerializeField] private bool canAttack = false;

	private EnemyController controller;

	private void OnEnable()
	{
		bulletId = EnemyBulletId.ENEMY_BULLET_1;
		barrel = transform.Find("Texture/BarrelPoint_Straight");

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

			canAttack = Random.Range(0, 2) != 0;

			if (!canAttack) continue;

			GameObject bullet = PoolingManager.GetObject((int)bulletId, barrel.position, barrel.rotation);
			bullet.SetActive(true);

			yield return new WaitForSeconds(coolDown / 2);
		}
	}
}
