using System;
using System.Collections;
using UnityEngine;

public class LaserFollowBarrel : MonoBehaviour
{
	[SerializeField] private Transform attacher;
	[SerializeField] private float coolDown;

	public void SetInfo(Transform attacher, float coolDown)
	{
		this.attacher = attacher;
		this.coolDown = coolDown;
	}

	private void OnEnable()
	{
		StartCoroutine(Following());
		StartCoroutine(HideLaser());
	}

	private void OnDisable()
	{
		StopAllCoroutines();
		Destroy(GetComponent<LaserFollowBarrel>());
	}

	private IEnumerator Following()
	{
		while (true)
		{
			transform.SetPositionAndRotation(attacher.position, attacher.rotation);

			yield return new WaitForSeconds(0.02f);
		}
	}

	private IEnumerator HideLaser()
	{
		while (true)
		{
			yield return new WaitForSeconds(coolDown);

			PoolingManager.PoolObject(gameObject);
		}
	}
}
