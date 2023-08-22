using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupSpawner : MonoBehaviour
{
    [Header("========== Info ==========")]
    [SerializeField] private EnemyId enemyId;
    [SerializeField] private int numberEnemy;
    [SerializeField] private float coolDown;

    [Header("========== Path ==========")]
    [SerializeField] private DOTweenPath path;
    [SerializeField] private float timeMove;

    [Header("========== Group ==========")]
    [SerializeField] private Transform group;
    [SerializeField] private List<Transform> destinations;

    [Header("========== Status ==========")]
    [SerializeField] private bool randomSlot = true;

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
			EnemyMoveToGroup move = enemy.AddComponent(typeof(EnemyMoveToGroup)) as EnemyMoveToGroup;

			GetRandomDesination(out Vector3 destination);
			move.SetInfo(path, timeMove, destination);

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
        int id = 0;
        if (randomSlot)
        {
			id = Random.Range(0, destinations.Count);
        }

		dest = destinations[id].position;
		destinations.RemoveAt(id);
    }
}
