using System.Collections;
using UnityEngine;

public class Plane_2_Attack : MonoBehaviour
{
	[SerializeField]
	private PlayerBulletId bulletId;

	[SerializeField]
	private int level = 0;

	[SerializeField]
	private readonly int maxLevel = 6;

	[SerializeField]
	private float coolDown;

	[SerializeField]
	private float distance;

	[SerializeField]
	private bool canAttack = false;

	[SerializeField]
	private bool isPower = false;

	private int numberBarrel = 1;
	private int tempNumberBarrel = 1;

	private IEnumerator Attack()
	{
		yield return new WaitUntil(() => canAttack);

		yield return new WaitForSeconds(coolDown);

		for (int i = 0; i < numberBarrel; i++)
		{
			Transform child = transform.GetChild(i);
			GameObject laser = PoolingManager.GetObject((int)bulletId, child.position, Quaternion.identity);
			laser.GetComponent<FollowPlayer>().Target = child;
			laser.SetActive(true);
		}
	}

	private IEnumerator CoolDownPlanePower()
	{
		yield return new WaitForSeconds(5);

		canAttack = false;
		isPower = false;

		numberBarrel = tempNumberBarrel;
		bulletId = PlayerBulletId.PLAYER_BULLET_2;

		ResetBarrel();
		CreateBarrel();

		StartCoroutine(Attack());
		EventDispatcher.PostEvent(EventID.PlanePowerDown, isPower);
	}

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

	private void OnPlanePowerUp(object obj)
	{
		isPower = (bool)obj;
		canAttack = false;

		tempNumberBarrel = numberBarrel;
		numberBarrel = 1;
		bulletId = PlayerBulletId.PLAYER_BULLET_2_SPECIAL;

		ResetBarrel();
		CreateBarrel();

		StartCoroutine(Attack());
		StartCoroutine(CoolDownPlanePower());
	}

	private void OnBulletLevelChanged(object obj)
	{
		if (level >= maxLevel) return;

		level += (int)obj;

		if (level % 2 == 0)
		{
			tempNumberBarrel++;
		}

		if (isPower) return;

		numberBarrel = tempNumberBarrel;

		canAttack = false;
		ResetBarrel();
		CreateBarrel();

		StartCoroutine(Attack());
	}

	private void CreateBarrel()
	{
		float distanceForParent = (numberBarrel - 1) * distance / 2;

		for (int i = 0; i < numberBarrel; i++)
		{
			GameObject barrel = new GameObject($"Barrel_{i + 1}");
			barrel.transform.localPosition = new Vector3(i * distance, 0, 0);
			barrel.transform.localRotation = Quaternion.identity;
			barrel.transform.SetParent(transform, false);
		}

		transform.localPosition = new Vector3(-distanceForParent, 0, 0);

		canAttack = true;
	}

	private void ResetBarrel()
	{
		if (transform.childCount < 1) return;

		for (int i = 0; i < transform.childCount; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}

		transform.localPosition = new Vector3(0, 0, 0);
	}
}
