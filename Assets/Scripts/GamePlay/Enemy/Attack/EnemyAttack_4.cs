using System;
using System.Collections;
using UnityEngine;

public class EnemyAttack_4 : MonoBehaviour
{
	[Header("========== Info ==========")]
	[SerializeField] private EnemyBulletId bulletId;
	[SerializeField] private Transform barrelParent;
	[SerializeField] private float coolDown = 4f;
	[SerializeField] private int numberBarrel = 10;
	
	private EnemyController controller;

	private void OnEnable()
	{
		bulletId = EnemyBulletId.ENEMY_BULLET_1;
		barrelParent = transform.Find("Texture/BarrelPoint_Circle");

		controller = GetComponent<EnemyController>();
	}

	private void Start()
	{
		CreateBarrel();

		StartCoroutine(Attack());
	}

	private IEnumerator Attack()
	{
		yield return new WaitUntil(() => controller.CanTakeDamage);

		while (true)
		{
			for (int j = 0; j < 3; j++)
			{
				for (int i = 0; i < numberBarrel; i++)
				{
					Transform barrel = barrelParent.GetChild(i);
					GameObject bullet = PoolingManager.GetObject((int)bulletId, barrel.position, barrel.rotation);
					bullet.SetActive(true);
				}

				yield return new WaitForSeconds(0.2f);
			}

			yield return new WaitForSeconds(coolDown);
		}
	}

	private void CreateBarrel()
	{
		float angle = 360 / numberBarrel;

		for (int i = 0; i < numberBarrel; i++)
		{
			GameObject barrel = new GameObject($"Barrel_{i + 1}");
			barrel.transform.SetParent(barrelParent);
			barrel.transform.SetPositionAndRotation(barrelParent.position, Quaternion.Euler(new Vector3(0, 0, i * angle)));
		}
	}
}
