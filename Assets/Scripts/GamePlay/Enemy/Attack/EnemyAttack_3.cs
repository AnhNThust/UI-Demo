using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAttack_3 : MonoBehaviour
{
    [Header("========== Info ==========")]
    [SerializeField] private EnemyBulletId bulletId;
    [SerializeField] private Transform barrel;
    [SerializeField] private float coolDown = 5f;
    [SerializeField] private bool canAttack = false;

    private EnemyController controller;
	private Transform player;

	private void OnEnable()
	{
        bulletId = EnemyBulletId.ENEMY_MISSLE_1;
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

			canAttack = Random.Range(0, 2) == 1;
			if (!canAttack) continue;

			yield return new WaitForSeconds(0.2f);

			for (int i = 0; i < 2; i++)
			{
				GameObject bullet = PoolingManager.GetObject((int)bulletId, barrel.position, barrel.rotation);
				bullet.GetComponent<HomingMissle>().SetInfo(player);
				bullet.SetActive(true);

				yield return new WaitForSeconds(0.2f);
			}
		}
	}

	public void SetInfo(Transform player)
	{
		this.player = player;
	}
}
