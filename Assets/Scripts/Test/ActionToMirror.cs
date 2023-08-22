using System.Collections;
using UnityEngine;

public class ActionToMirror : MonoBehaviour
{
	private int count;

	private void OnEnable()
	{
		count = 0;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Mirror")) return;

		Vector2 outVec = Vector2.Reflect(transform.up, collision.transform.right);

		if (count > 2)
			PoolingManager.PoolObject(gameObject);

		transform.up = outVec;
		count++;
	}
}
