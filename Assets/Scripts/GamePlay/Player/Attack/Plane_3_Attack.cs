using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Plane_3_Attack : MonoBehaviour
{
	[SerializeField]
	private PlayerBulletId bulletId;

	[SerializeField]
	private int level = 0;

	[SerializeField]
	private int maxLevel = 6;

	[SerializeField]
	private float coolDown;

	[SerializeField]
	private float distance;

	[SerializeField]
	private bool canAttack = false;

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

	private IEnumerator Attack()
	{
		yield return new WaitUntil(() => canAttack);

		while (true)
		{
			yield return new WaitForSeconds(coolDown);

			Transform child = GetRandomChild();
			GameObject bullet = PoolingManager.GetObject((int)bulletId, child.position, child.rotation);
			bullet.SetActive(true);
		}
	}

	private Transform GetRandomChild()
	{
		int index = Random.Range(0, transform.childCount);

		return transform.GetChild(index);
	}

	private IEnumerator CoolDownPlanePower(float tempCoolDown)
	{
		yield return new WaitForSeconds(5);

		canAttack = false;
		isPower = false;

		level = tempLevel;
		coolDown = tempCoolDown;
		bulletId = PlayerBulletId.PLAYER_BULLET_3;

		ResetBarrel();
		CreateBarrel();

		EventDispatcher.PostEvent(EventID.PlanePowerDown, isPower);
	}

	private void OnPlanePowerUp(object obj)
	{
		isPower = (bool)obj;
		canAttack = false;

		tempLevel = level;
		float tempCd = coolDown;
		bulletId = PlayerBulletId.PLAYER_BULLET_3_SPECIAL;

		level = maxLevel + 4;
		coolDown = 0.02f;

		ResetBarrel();
		CreateBarrel();

		StartCoroutine(CoolDownPlanePower(tempCd));
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

	private void CreateBarrel()
	{
		float distanceOfParent = level * distance / 2;

		for (int i = 0; i <= level; i++)
		{
			GameObject barrel = new GameObject($"Barrel_{i + 1}");
			barrel.transform.SetPositionAndRotation(new Vector3(i * distance, 0, 0), Quaternion.identity);
			barrel.transform.SetParent(transform, false);
		}

		transform.localPosition = new Vector3(-distanceOfParent, 0, 0);

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
