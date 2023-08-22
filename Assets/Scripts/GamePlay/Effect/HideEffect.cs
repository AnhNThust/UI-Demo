using System.Collections;
using UnityEngine;

public class HideEffect : MonoBehaviour
{
    [SerializeField] private float timeHide;

	private void OnEnable()
	{
		StartCoroutine(Hidding());
	}

	IEnumerator Hidding()
	{
		yield return new WaitForSeconds(timeHide);

		PoolingManager.PoolObject(gameObject);
	}
}
