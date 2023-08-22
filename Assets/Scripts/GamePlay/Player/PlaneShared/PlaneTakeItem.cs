using UnityEngine;

public class PlaneTakeItem : MonoBehaviour
{
	private bool isPower = false;
	private int level = 0;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Item")) return;

		ObjectPool poolInfo = collision.GetComponent<ObjectPool>();

		if (poolInfo.GetID() == (int)ItemId.UPGRADE_ITEM)
		{
			GameDatas.PlayerBulletLevel = ++level;
			EventDispatcher.PostEvent(EventID.BulletLevelChanged, 1);
		}

		if (poolInfo.GetID() == (int)ItemId.POWERUP_ITEM)
		{
			if (!isPower)
			{
				isPower = true;
				EventDispatcher.PostEvent(EventID.PlanePowerUp, isPower);
				EventDispatcher.AddEvent(EventID.PlanePowerDown, OnPlanePowerDownChanged);
			}
		}

		if (poolInfo.GetID() == (int)ItemId.GOLD_1)
		{
			EventDispatcher.PostEvent(EventID.GoldChanged, 1);
		}

		if (poolInfo.GetID() == (int)ItemId.GOLD_2)
		{
			EventDispatcher.PostEvent(EventID.GoldChanged, 10);
		}

		PoolingManager.PoolObject(collision.gameObject);
	}

	private void OnDisable()
	{
		EventDispatcher.RemoveEvent(EventID.PlanePowerDown, OnPlanePowerDownChanged);
	}

	private void OnPlanePowerDownChanged(object obj)
	{
		isPower = (bool)obj;
	}
}
