using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class MiniBoss_1_Attack : MonoBehaviour
{
	[Header("========== Shoot Point 1 ==========")]
	[SerializeField] private Transform eye;
	[SerializeField] private Transform[] eyeChildren;
	[SerializeField] private EnemyBulletId bulletId_1;
	[SerializeField] private float coolDown_1;

	[Header("========== Shoot Point 2 ==========")]
	[SerializeField] private Transform[] shootPoints;
	[SerializeField] private EnemyBulletId bulletId_2;
	[SerializeField] private float coolDown_2;

	[Header("========== Shoot Point 3 ==========")]
	[SerializeField] private Transform shootPoint_Left_Parent;
	[SerializeField] private Transform[] shootPoint_Left;
	[SerializeField] private Transform shootPoint_Right_Parent;
	[SerializeField] private Transform[] shootPoint_Right;
	[SerializeField] private EnemyBulletId bulletId_3;
	[SerializeField] private float coolDown_3;

	[SerializeField] private float cdBetweenAttack;

	private EnemyController controller;
	private Coroutine coroutine;

	[ContextMenu("Reload")]
	private void Reload()
	{
		GetShootPoint(eye, out eyeChildren);
		GetShootPoint(shootPoint_Left_Parent, out shootPoint_Left);
		GetShootPoint(shootPoint_Right_Parent, out shootPoint_Right);
	}

	private IEnumerator Attack()
	{
		yield return new WaitUntil(() => controller.CanTakeDamage);

		while (true)
		{
			//yield return new WaitForSeconds(cdBetweenAttack);

			bool attackDone = false;
			StartCoroutine(Attack1(delegate//1
			{
				StartCoroutine(Attack2(delegate//2
				{
					StartCoroutine(Attack3(delegate//3
					{
						attackDone = true;
					}));
				}));
			}));
			yield return new WaitUntil(() => attackDone);

			//GetRandomAttackType();
		}
	}

	private void OnEnable()
	{
		controller = GetComponent<EnemyController>();

		StartCoroutine(Attack());
	}

	//private void GetRandomAttackType()
	//{
	//	if (coroutine != null)
	//	{
	//		StopCoroutine(coroutine);
	//		coroutine = null;
	//	}

	//	int randIndex = Random.Range(0, 3);

	//	switch (randIndex)
	//	{
	//		case 0:
	//			coroutine = StartCoroutine(Attack1());
	//			break;
	//		case 1:
	//			coroutine = StartCoroutine(Attack2());
	//			break;
	//		case 2:
	//			coroutine = StartCoroutine(Attack3());
	//			break;
	//	}
	//}

	private IEnumerator Attack3()
	{
		while (true)
		{
			for (int i = 0; i < shootPoint_Left.Length; i++)
			{
				Transform point = shootPoint_Left[i];
				GameObject bullet = PoolingManager.GetObject((int)bulletId_3, point.position, point.rotation);
				bullet.SetActive(true);
			}

			yield return new WaitForSeconds(coolDown_3);

			for (int i = 0; i < shootPoint_Right.Length; i++)
			{
				Transform point = shootPoint_Right[i];
				GameObject bullet = PoolingManager.GetObject((int)bulletId_3, point.position, point.rotation);
				bullet.SetActive(true);
			}

			yield return new WaitForSeconds(coolDown_3);
		}
	}

	private IEnumerator Attack3(System.Action callback)
	{
		for (int t = 0; t < 3; t++)
		{
			for (int i = 0; i < shootPoint_Left.Length; i++)
			{
				Transform point = shootPoint_Left[i];
				GameObject bullet = PoolingManager.GetObject((int)bulletId_3, point.position, point.rotation);
				bullet.SetActive(true);
			}

			yield return new WaitForSeconds(coolDown_3);

			for (int i = 0; i < shootPoint_Right.Length; i++)
			{
				Transform point = shootPoint_Right[i];
				GameObject bullet = PoolingManager.GetObject((int)bulletId_3, point.position, point.rotation);
				bullet.SetActive(true);
			}

			yield return new WaitForSeconds(coolDown_3);
		}

		callback?.Invoke();
	}

	private IEnumerator Attack2(System.Action callBack)
	{
		for (int j = 0; j < 3; j++)
		{
			for (int i = 0; i < shootPoints.Length; i++)
			{
				GameObject bullet = PoolingManager.GetObject((int)bulletId_2, shootPoints[i].position, shootPoints
[i].rotation);
				bullet.SetActive(true);

				yield return new WaitForSeconds(coolDown_2);
			}

			yield return new WaitForSeconds(coolDown_2);
		}

		callBack?.Invoke();
	}

	private IEnumerator Attack1(System.Action callBack)
	{
		for (int i = 0; i < 24; i++)
		{
			for (int j = 0; j < eyeChildren.Length; j++)
			{
				GameObject bullet = PoolingManager.GetObject((int)bulletId_1, eyeChildren[j].position, eyeChildren[j].rotation);
				bullet.SetActive(true);
			}

			yield return new WaitForSeconds(coolDown_1);
		}

		callBack?.Invoke();
	}

	private void GetShootPoint(Transform parent, out Transform[] array)
	{
		array = new Transform[parent.childCount];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = parent.GetChild(i);
		}
	}
}
