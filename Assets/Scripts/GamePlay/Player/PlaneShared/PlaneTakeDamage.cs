using System.Collections;
using UnityEngine;

public class PlaneTakeDamage : MonoBehaviour
{
	[SerializeField] private EffectId hitId;
	[SerializeField] private EffectId explosionId;

	private int life = 0;
	private bool canTakeDamage = true;

	private void OnEnable()
	{
		life = 3;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!canTakeDamage) return;

		if (collision.CompareTag("Enemy") || collision.CompareTag("EnemyBullet"))
		{
			if (life < 1)
			{
				canTakeDamage = false;
				ShowExplode();
				StartCoroutine(ShowDefeat());
				return;
			}

			life -= 1;
			ShowHit(collision);
			EventDispatcher.PostEvent(EventID.LifeChanged, 1);
			PoolingManager.PoolObject(collision.gameObject);
		}

		if (collision.CompareTag("Boss"))
		{
			canTakeDamage = false;
			StartCoroutine(ShowDefeat());
			return;
		}
	}

	private void ShowHit(Collider2D collision)
	{
		Vector2 pos = collision.ClosestPoint(collision.transform.position);
		GameObject hit = PoolingManager.GetObject((int)hitId, pos, Quaternion.identity);
		hit.SetActive(true);
	}

	private void ShowExplode()
	{
		GameObject explode = PoolingManager.GetObject((int)explosionId, transform.position, Quaternion.identity);
		explode.SetActive(true);
	}

	private IEnumerator ShowDefeat()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).gameObject.SetActive(false);
		}

		yield return new WaitForSeconds(2.5f);
		MainPanelManager.ShowDefeat();
	}
}
