using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EnemyChangeAndLoopx2Spawner : MonoBehaviour
{
	[Header("========== Info ==========")]
	[SerializeField] private EnemyId enemyId;
	[SerializeField] private int numberSpawn;
	[SerializeField] private float coolDown;

	[Header("========== Start ==========")]
	[SerializeField] private DOTweenPath startPath;
	[SerializeField] private float startTime;

	[Header("========== Loop 1 ==========")]
	[SerializeField] private DOTweenPath loopPath_1;
	[SerializeField] private float loopTime_1;

	[Header("========== Loop 1 ==========")]
	[SerializeField] private DOTweenPath loopPath_2;
	[SerializeField] private float loopTime_2;

	[SerializeField] private Transform holder;

	IEnumerator Spawning()
	{
		yield return new WaitForSeconds(3f);

		for (int i = 0; i < numberSpawn; i++)
		{
			GameObject enemy = PoolingManager.GetObject((int)enemyId, transform.position, Quaternion.identity);
			EnemyMoveChangeAndLoopx2 mov = enemy.AddComponent(typeof(EnemyMoveChangeAndLoopx2)) as EnemyMoveChangeAndLoopx2;
			mov.SetInfo(startPath, startTime, loopPath_1, loopTime_1, loopPath_2, loopTime_2);
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
}
