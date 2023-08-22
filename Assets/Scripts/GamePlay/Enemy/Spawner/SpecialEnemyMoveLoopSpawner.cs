using DG.Tweening;
using System.Collections;
using UnityEngine;

public class SpecialEnemyMoveLoopSpawner : MonoBehaviour
{
	[Header("========== Info ==========")]
	[SerializeField] private EnemyId enemyId;
	[SerializeField] private int numberSpawn;
	[SerializeField] private float coolDown;
	[SerializeField] private SpecialEnemyAttackId attackId;

	[Header("========== Path Start ==========")]
	[SerializeField] private DOTweenPath startPath;
	[SerializeField] private float startTime;

	[Header("========== Path Loop ==========")]
	[SerializeField] private DOTweenPath loopPath;
	[SerializeField] private float loopTime;

	[SerializeField] private Transform holder;

	IEnumerator Spawning()
	{
		yield return new WaitForSeconds(3f);

		for (int i = 0; i < numberSpawn; i++)
		{
			GameObject enemy = PoolingManager.GetObject((int)enemyId, transform.position, Quaternion.identity);

			EnemyMoveChangeAndLoop move = enemy.AddComponent(typeof(EnemyMoveChangeAndLoop)) as EnemyMoveChangeAndLoop;
			move.SetInfo(startPath, startTime, loopPath, loopTime);

			SetAttackType(enemy);

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

	private void SetAttackType(GameObject obj)
	{
		switch (attackId)
		{
			case SpecialEnemyAttackId.Attack_1:
				_ = obj.AddComponent(typeof(EnemyAttack_1)) as EnemyAttack_1;
				break;
			case SpecialEnemyAttackId.Attack_2:
				_ = obj.AddComponent(typeof(EnemyAttack_2)) as EnemyAttack_2;
				break;
			case SpecialEnemyAttackId.Attack_3:
				_ = obj.AddComponent(typeof(EnemyAttack_1)) as EnemyAttack_1;
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
