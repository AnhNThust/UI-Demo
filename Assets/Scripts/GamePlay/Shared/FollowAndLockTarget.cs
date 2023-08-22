using System.Collections;
using UnityEngine;

public class FollowAndLockTarget : MonoBehaviour
{
	[SerializeField] private Transform origin;
	[SerializeField] private Transform target;
	[SerializeField] private LineRenderer laser;
	[SerializeField] private float timeLock;

	private float counter = 0;

	private IEnumerator Hidding()
	{
		yield return new WaitForSeconds(timeLock);

		PoolingManager.PoolObject(gameObject);
	}

	private void OnEnable()
	{
		laser = GetComponent<LineRenderer>();
		laser.positionCount = 2;

		StartCoroutine(Hidding());
	}

	private void Update()
	{
		counter += Time.deltaTime;
		laser.SetPosition(0, origin.position - laser.transform.position);

		if (counter >= timeLock)
		{
			counter = 0;
			return;
		}

		laser.SetPosition(1, target.position - laser.transform.position);
	}

	public void SetInfo(Transform origin, Transform target, float timeLock)
	{
		this.origin = origin;
		this.target = target;
		this.timeLock = timeLock;
	}
}
