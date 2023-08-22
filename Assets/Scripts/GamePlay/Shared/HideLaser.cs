using System.Collections;
using UnityEngine;

public class HideLaser : MonoBehaviour
{
	private readonly float timeHide = 0.2f;

	private void OnEnable()
	{
		StartCoroutine(Hidding());
	}

	private void OnDisable()
	{
		Destroy(GetComponent<HideLaser>());
	}

	private IEnumerator Hidding()
	{
		yield return new WaitForSeconds(timeHide);

		PoolingManager.PoolObject(gameObject);
	}
}
