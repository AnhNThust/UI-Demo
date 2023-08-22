using System.Collections;
using UnityEngine;

public class Plane_1_Attack : MonoBehaviour
{
	[SerializeField]
	private PlayerBulletId bulletId;

	[SerializeField, Tooltip("Level of Bullet")]
	private int level = 0;

	[SerializeField, Tooltip("Max Level of Bullet")]
	private int maxLevel = 6;

	[SerializeField, Tooltip("Degree between of shooting line")]
	private float degree;

	[SerializeField]
	private bool canAttack = false;

	[SerializeField, Tooltip("Time delay shooting")]
	private float coolDown;

	[SerializeField]
	private bool isPower = false;

	private int tempLevel = 0;

	private void OnEnable()
	{
		ResetBarrel();
		CreateBarrel();

		EventDispatcher.AddEvent(EventID.BulletLevelChanged, OnBulletLevelChanged);
		EventDispatcher.AddEvent(EventID.PlanePowerUp, OnPlanePowerUp);
		StartCoroutine(Attack());
	}

	private void OnDisable()
	{
		EventDispatcher.RemoveEvent(EventID.BulletLevelChanged, OnBulletLevelChanged);
		EventDispatcher.RemoveEvent(EventID.PlanePowerUp, OnPlanePowerUp);
	}

	private IEnumerator CoolDownPowerPlane()
	{
		yield return new WaitForSeconds(5);

		canAttack = false;
		isPower = false;

		level = tempLevel;
		bulletId = PlayerBulletId.PLAYER_BULLET_1;

		ResetBarrel();
		CreateBarrel();

		EventDispatcher.PostEvent(EventID.PlanePowerDown, isPower);
	}

	private void OnPlanePowerUp(object obj)
	{
		isPower = (bool)obj;
		canAttack = false;

		tempLevel = level;
		level = maxLevel + 4;
		bulletId = PlayerBulletId.PLAYER_BULLET_1_SPECIAL;

		ResetBarrel();
		CreateBarrel();

		StartCoroutine(CoolDownPowerPlane());
	}

	private void OnBulletLevelChanged(object obj)
	{
		if (tempLevel >= maxLevel) return;

		tempLevel += (int)obj;

		if (isPower) return;

		level = tempLevel;
		canAttack = false;

		ResetBarrel();
		CreateBarrel();
	}

	private IEnumerator Attack()
	{
		yield return new WaitUntil(() => canAttack == true);

		while (true)
		{
			yield return new WaitForSeconds(coolDown);

			for (int i = 0; i <= level; i++)
			{
				Transform child = transform.GetChild(i);
				GameObject bullet = PoolingManager.GetObject((int)bulletId, child.position, child.rotation);
				bullet.SetActive(true);
			}
		}
	}

	private void CreateBarrel()
	{
		float degreeForParent = level * degree / 2;

		for (int i = 0; i <= level; i++)
		{
			GameObject barrel = new GameObject($"Barrel_{i + 1}");
			barrel.transform.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(0, 0, -i * degree));
			barrel.transform.SetParent(transform, false);
		}

		transform.rotation = Quaternion.Euler(0, 0, degreeForParent);

		canAttack = true;
	}

	private void ResetBarrel()
	{
		if (transform.childCount <= 0) return;

		for (int i = 0; i < transform.childCount; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}

		transform.rotation = Quaternion.identity;
	}
}
