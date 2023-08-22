using UnityEngine;

public class Despawn : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		PoolingManager.PoolObject(collision.gameObject);
	}
}
