using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EnemyOnlyLoopSpawner : MonoBehaviour
{
    [Header("========== Info ==========")]
    [SerializeField] private EnemyId enemyId;
    [SerializeField] private int numberEnemy;
    [SerializeField] private float coolDown;

    [Header("========== Loop ==========")]
    [SerializeField] private DOTweenPath path;
    [SerializeField] private float timeMove;

    [Header("========== Item ==========")]
    [SerializeField] private Transform[] items;
    [SerializeField] private int numberItem;
    [SerializeField] private int[] listIndexHaveItem;

    [SerializeField] private Transform holder;

    IEnumerator Spawning()
    {
        yield return new WaitForSeconds(3f);

        for (int i = 0; i < numberEnemy; i++)
        {
			GameObject enemy = PoolingManager.GetObject((int)enemyId, transform.position, Quaternion.identity);
			EnemyOnlyMove move = enemy.AddComponent(typeof(EnemyOnlyMove)) as EnemyOnlyMove;
            move.SetInfo(path, timeMove);
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
