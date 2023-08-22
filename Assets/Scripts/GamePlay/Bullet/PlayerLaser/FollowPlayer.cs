using System;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
	public Transform Target { get; set; }

	private void OnEnable()
	{
		EventDispatcher.AddEvent(EventID.BulletLevelChanged, OnBulletLevelChanged);
		EventDispatcher.AddEvent(EventID.PlanePowerUp, OnBulletLevelChanged);
		EventDispatcher.AddEvent(EventID.PlanePowerDown, OnPlanePowerDown);
	}

	private void OnDisable()
	{
		EventDispatcher.RemoveEvent(EventID.BulletLevelChanged, OnBulletLevelChanged);
		EventDispatcher.RemoveEvent(EventID.PlanePowerUp, OnBulletLevelChanged);
		EventDispatcher.RemoveEvent(EventID.PlanePowerDown, OnPlanePowerDown);
	}

	private void OnPlanePowerDown(object obj)
	{
		PoolingManager.PoolObject(gameObject);
	}

	private void OnBulletLevelChanged(object obj)
	{
		if (transform.GetComponent<ObjectPool>().GetID() == (int)PlayerBulletId.PLAYER_BULLET_2_SPECIAL) return;

		PoolingManager.PoolObject(gameObject);
	}

	private void FixedUpdate()
	{
		if (Target == null) return;

		transform.position = Target.position;
	}
}
