using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpecialEnemyMoveToGroupSpawner : MonoBehaviour
{
	[Header("========== Info ==========")]
	[SerializeField] private EnemyId enemyId;
	[SerializeField] private int numberEnemy;
	[SerializeField] private float coolDown;
	[SerializeField] private SpecialEnemyAttackId attackId;

	[Header("========== Path Start ==========")]
	[SerializeField] private DOTweenPath startPath;
	[SerializeField] private float startTime;

	[Header("========== Groups ==========")]
	[SerializeField] private Transform group;
	[SerializeField] private List<Transform> destinations;

	[SerializeField] private Transform holder;

	[ContextMenu("Reload")]
	private void Reload()
	{
		destinations = new List<Transform>();
		for (int i = 0; i < group.childCount; i++)
		{
			destinations.Add(group.GetChild(i));
		}
	}

	IEnumerator Spawning()
	{
		yield return new WaitForSeconds(3f);

		for (int i = 0; i < numberEnemy; i++)
		{
			GameObject enemy = PoolingManager.GetObject((int)enemyId, transform.position, Quaternion.identity);
			SetAttackType(enemy);
			EnemyMoveToGroup move = enemy.AddComponent(typeof(EnemyMoveToGroup)) as EnemyMoveToGroup;

			GetRandomDesination(out Vector3 destination);
			move.SetInfo(startPath, startTime, destination);

			enemy.SetActive(true);
			enemy.transform.SetParent(holder, false);

			yield return new WaitForSeconds(coolDown);
		}

		EventDispatcher.PostEvent(EventID.CheckHolderChanged, true);
	}

	private void OnEnable()
	{
		StartCoroutine(Spawning());
	}

	private void GetRandomDesination(out Vector3 dest)
	{
		int randIndex = Random.Range(0, destinations.Count);
		dest = destinations[randIndex].position;

		destinations.RemoveAt(randIndex);
	}

	private void SetAttackType(GameObject obj)
	{
		bool canAttack = Random.Range(0, 2) != 0;

		if (!canAttack) return;

		switch (attackId)
		{
			case SpecialEnemyAttackId.Attack_1:
				_ = obj.AddComponent(typeof(EnemyAttack_1)) as EnemyAttack_1;
				break;
			case SpecialEnemyAttackId.Attack_2:
				_ = obj.AddComponent(typeof(EnemyAttack_2)) as EnemyAttack_2;
				break;
			case SpecialEnemyAttackId.Attack_3:
				EnemyAttack_3 attack = obj.AddComponent(typeof(EnemyAttack_3)) as EnemyAttack_3;
				Transform player = GameObject.Find("PLAYER").transform;
				attack.SetInfo(player);
				break;
			case SpecialEnemyAttackId.Attack_4:
				_ = obj.AddComponent(typeof(EnemyAttack_4)) as EnemyAttack_4;
				break;
			case SpecialEnemyAttackId.Attack_5:
				_ = obj.AddComponent(typeof(EnemyAttack_5)) as EnemyAttack_5;
				break;
			case SpecialEnemyAttackId.Attack_6:
				_ = obj.AddComponent(typeof(EnemyAttack_1)) as EnemyAttack_1;
				break;
			case SpecialEnemyAttackId.Attack_7:
				_ = obj.AddComponent(typeof(EnemyAttack_1)) as EnemyAttack_1;
				break;
			case SpecialEnemyAttackId.Attack_8:
				_ = obj.AddComponent(typeof(EnemyAttack_1)) as EnemyAttack_1;
				break;
			case SpecialEnemyAttackId.Attack_9:
				_ = obj.AddComponent(typeof(EnemyAttack_1)) as EnemyAttack_1;
				break;
			case SpecialEnemyAttackId.Attack_10:
				_ = obj.AddComponent(typeof(EnemyAttack_1)) as EnemyAttack_1;
				break;
			case SpecialEnemyAttackId.None:
			default:
				break;
		}
	}
}
