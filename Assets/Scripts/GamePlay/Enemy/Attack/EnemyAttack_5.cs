using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack_5 : MonoBehaviour
{
	[Header("========== Info ==========")]
	[SerializeField] private EffectId preEnemyLaser;
	[SerializeField] private EnemyBulletId bulletId;
	[SerializeField] private readonly float coolDown = 4f;

	private EnemyController controller;
	private readonly float timeLock = 2f;

	private void OnEnable()
	{
		preEnemyLaser = EffectId.PRE_ENEMY_LASER;
		bulletId = EnemyBulletId.ENEMY_LASER_1;

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
			Transform player = GameObject.Find("PLAYER").transform;

			GameObject alert = PoolingManager.GetObject((int)preEnemyLaser, transform.position, Quaternion.identity);
			alert.GetComponent<FollowAndLockTarget>().SetInfo(transform, player, timeLock);
			alert.SetActive(true);

			yield return new WaitForSeconds(timeLock);

			Vector3 targetPos = player.position;

			yield return new WaitForSeconds(0.2f);

			GameObject laser = PoolingManager.GetObject((int)bulletId, transform.position, Quaternion.identity);
			SetLaser(laser.transform, targetPos);
			_ = laser.AddComponent(typeof(HideLaser)) as HideLaser;
			laser.SetActive(true);

			yield return new WaitForSeconds(coolDown);
		}
	}

	private void SetLaser(Transform laser, Vector3 targetPos)
	{
		LineRenderer line = laser.GetComponent<LineRenderer>();
		EdgeCollider2D collider = laser.GetComponent<EdgeCollider2D>();
		line.positionCount = 2;

		line.SetPositions(new Vector3[]
		{
			transform.position,
			targetPos
		});

		collider.edgeRadius = 0.06f;
		collider.SetPoints(new List<Vector2>()
		{
			transform.position - laser.position,
			targetPos - laser.position
		});
	}
}
