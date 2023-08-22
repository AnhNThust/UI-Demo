using System;
using System.Collections;
using UnityEngine;

public class HomingMissle : MonoBehaviour
{
	[Header("========== Info ==========")]
    [SerializeField] private Transform target;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float angleChangeSpeed;
    [SerializeField] private float moveSpeed;

	[Header("========== Hidden ==========")]
	[SerializeField] private float timeHide = 3;
	[SerializeField] private EffectId explodeId;

	private void OnEnable()
	{
		rb = GetComponent<Rigidbody2D>();
		//target = GameObject.Find("PLAYER").transform;

        StartCoroutine(Moving());
		StartCoroutine(Hiding());
	}

	private IEnumerator Hiding()
	{
		yield return new WaitForSeconds(timeHide);

		GameObject explode = PoolingManager.GetObject((int)explodeId, transform.position, Quaternion.identity);
		explode.SetActive(true);

		PoolingManager.PoolObject(gameObject);
	}

	private IEnumerator Moving()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.02f);

			Vector2 direction = (Vector2) target.position - rb.position;
			direction.Normalize();
			float rotateAmount = Vector3.Cross(direction, transform.up).z;
			rb.angularVelocity = - angleChangeSpeed * rotateAmount;
			rb.velocity = transform.up * moveSpeed;
		}
	}

	public void SetInfo(Transform target)
	{
		this.target = target;
	}
}
