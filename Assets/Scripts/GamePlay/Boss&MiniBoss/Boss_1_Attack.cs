using System;
using System.Collections;
using UnityEngine;

public class Boss_1_Attack : MonoBehaviour
{
    [Header("========== Shoot Point 1 ==========")]
    [SerializeField] private Transform sp1_Parent; // Shoot Point 1 Parent
    [SerializeField] private Transform[] sp1s;     // Shoot Point 1 Children
	[SerializeField] private BossBulletId bulletId_1;
    [SerializeField] private float coolDown_1;
	[SerializeField] private int shootNumber_1;

    [Header("========== Shoot Point 2 ==========")]
    [SerializeField] private Transform sp2_Parent; // Shoot Point 2 Parent
	[SerializeField] private Transform[] sp2s;    // Shoot Point 2 Children
	[SerializeField] private BossBulletId bulletId_2;
	[SerializeField] private float coolDown_2;

	[Header("========== Shoot Point 3 ==========")]
    [SerializeField] private Transform sp3_Parent; // Shoot Point 3 Parent
	[SerializeField] private Transform[] sp3s;     // Shoot Point 3 Children
	[SerializeField] private EnemyBulletId bulletId_3;
	[SerializeField] private float coolDown_3;
	[SerializeField] private int shootNumber_3;

	[Header("========== Shoot Point 4 ==========")]
    [SerializeField] private Transform sp4_Parent; // Shoot Point 4 Parent
	[SerializeField] private Transform[] sp4s;     // Shoot Point 4 Children
	[SerializeField] private EnemyBulletId bulletId_4;
	[SerializeField] private float coolDown_4;
	[SerializeField] private int shootNumber_4;

	[Header("========== Shoot Point 5 ==========")]
    [SerializeField] private Transform sp5_Parent; // Shoot Point 5 Parent
	[SerializeField] private Transform[] sp5s;     // Shoot Point 5 Children
	[SerializeField] private EnemyBulletId bulletId_5;
	[SerializeField] private float coolDown_5;
	[SerializeField] private int shootNumber_5;

	private EnemyController controller;
	private Coroutine coroutine;

	[ContextMenu("Reload")]
	private void Reload()
	{
		GetShootPointChildren(sp1_Parent, out sp1s);
		GetShootPointChildren(sp2_Parent, out sp2s);
		GetShootPointChildren(sp3_Parent, out sp3s);
		GetShootPointChildren(sp4_Parent, out sp4s);
		GetShootPointChildren(sp5_Parent, out sp5s);
	}

	private void OnEnable()
	{
		controller = GetComponent<EnemyController>();

		StartCoroutine(Attack());
	}

	private IEnumerator Attack()
	{
		yield return new WaitUntil(() => controller.CanTakeDamage);

		while (true)
		{
			bool attackDone = false;

			StartCoroutine(Attack1(delegate {
				StartCoroutine(Attack2(delegate {
					StartCoroutine(Attack3(delegate {
						StartCoroutine(Attack4(delegate {
							StartCoroutine(Attack5(delegate {
								attackDone = true;
							}));
						}));
					}));
				}));
			}));

			yield return new WaitUntil(() => attackDone);
		}
	}

	private void GetShootPointChildren(Transform parent, out Transform[] children)
	{
		children = new Transform[parent.childCount];
		for (int i = 0; i < children.Length; i++)
		{
			children[i] = parent.GetChild(i);
		}
	}

	private IEnumerator Attack1(Action callBack)
	{
		for (int i = 0; i < shootNumber_1; i++)
		{
			for (int j = 0; j < sp1s.Length; j++)
			{
                GameObject bullet = PoolingManager.GetObject((int)bulletId_1, sp1s[j].position, sp1s[j].rotation);
				bullet.SetActive(true);
			}
			yield return new WaitForSeconds(coolDown_1);
		}

		callBack?.Invoke();
	}

	private IEnumerator Attack2(Action callBack)
	{
		for (int i = 0; i < sp2s.Length; i++)
		{
			GameObject laser = PoolingManager.GetObject((int)bulletId_2, sp2s[i].position, sp2s[i].rotation);
			LaserFollowBarrel follow = laser.AddComponent(typeof(LaserFollowBarrel)) as LaserFollowBarrel;
			follow.SetInfo(sp2s[i], coolDown_2);

			laser.SetActive(true);
		}

		yield return new WaitForSeconds(coolDown_2);

		callBack?.Invoke();
	}

	private IEnumerator Attack3(Action callBack)
	{
		for (int i = 0; i < shootNumber_3; i++)
		{
			for (int j = 0; j < sp3s.Length; j++)
			{
                GameObject bullet = PoolingManager.GetObject((int)bulletId_3, sp3s[j].position, sp3s[j].rotation);
				bullet.SetActive(true);
			}
			
			yield return new WaitForSeconds(coolDown_3);
		}

		callBack?.Invoke();
	}

	private IEnumerator Attack4(Action callBack)
	{
		for (int i = 0; i < shootNumber_4; i++)
		{
			for (int j = 0; j < sp4s.Length; j++)
			{
                GameObject bullet = PoolingManager.GetObject((int)bulletId_4, sp4s[j].position, sp4s[j].rotation);
				bullet.SetActive(true);
			}

			yield return new WaitForSeconds(coolDown_4);
		}

		callBack?.Invoke();
	}

	private IEnumerator Attack5(Action callBack)
	{
		for (int i = 0; i < shootNumber_5; i++)
		{

			yield return new WaitForSeconds(coolDown_5);
		}

		callBack?.Invoke();
	}
}
