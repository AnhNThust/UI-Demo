using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EnemyChangeAndLoopSpawner : MonoBehaviour
{
	[Header("========== Info ==========")]
	[SerializeField] private EnemyId enemyId;
	[SerializeField] private int numberSpawn;
	[SerializeField] private float coolDown;

	[Header("========== Start ==========")]
	[SerializeField] private DOTweenPath startPath;
	[SerializeField] private float startTime;

	[Header("========== Loop ==========")]
	[SerializeField] private DOTweenPath loopPath;
	[SerializeField] private float loopTime;

	[SerializeField] private Transform holder;

	IEnumerator Spawning()
	{
		yield return new WaitForSeconds(3f);

		for (int i = 0; i < numberSpawn; i++)
		{
			GameObject enemy = PoolingManager.GetObject((int)enemyId, transform.position, Quaternion.identity);
			EnemyMoveChangeAndLoop mov = enemy.AddComponent(typeof(EnemyMoveChangeAndLoop)) as EnemyMoveChangeAndLoop;
			mov.SetInfo(startPath, startTime, loopPath, loopTime);
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
