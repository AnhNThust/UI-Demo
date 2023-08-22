using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
	[SerializeField] private float currentHp = 0;
	[SerializeField] private float totalHp = 25;

	[SerializeField] private Transform hpTransform;
	[SerializeField] private SpriteRenderer hpRender;

	[SerializeField] private EffectId hitId = EffectId.HIT;
	[SerializeField] private EffectId explodeId = EffectId.EXPLODE_1_MINI;

	[SerializeField] private EnemyController controller;

	private void OnEnable()
	{
		GetAllComponent();
		ResetHpBar();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("PlayerBullet"))
		{
			ShowHit(collision);
			PoolingManager.PoolObject(collision.gameObject);

			if (!controller.CanTakeDamage) return;

			TakeDamage(collision.GetComponent<BulletDamageSender>().Damage);
		}
	}

	public void TakeDamage(float damage)
	{
		if (!controller.CanTakeDamage) return;

		if (currentHp <= 0)
		{
			ShowExplode();
			GetRandomItem();
			PoolingManager.PoolObject(gameObject);
		}

		currentHp -= damage;
		float offset = currentHp / totalHp;
		hpTransform.gameObject.SetActive(true);
		hpRender.material.SetFloat("_Progress", offset);
	}

	private void ShowHit(Collider2D collision)
	{
		Vector2 pos = collision.ClosestPoint(collision.transform.position);
		GameObject hit = PoolingManager.GetObject((int)hitId, pos, Quaternion.identity);
		hit.SetActive(true);
	}

	private void ShowExplode()
	{
		GameObject explode = PoolingManager.GetObject((int)explodeId, transform.position, Quaternion.identity);
		explode.SetActive(true);
	}

	private void ResetHpBar()
	{
		currentHp = totalHp;
		hpTransform.gameObject.SetActive(false);
		hpRender.material.SetFloat("_Progress", 1);
	}

	private void GetAllComponent()
	{
		hpTransform = transform.Find("HpTexture");
		hpRender = hpTransform.Find("HpBar").GetComponent<SpriteRenderer>();
		controller = GetComponent<EnemyController>();
	}

	private void GetRandomItem()
	{
		int randIndex = Random.Range(0, 100);

		if (randIndex >= 50) return;

		GameObject item = (randIndex >= 0 && randIndex <= 8 && GameDatas.PlayerBulletLevel < 6)
												? PoolingManager.GetObject((int)ItemId.UPGRADE_ITEM, transform.position, Quaternion.identity)
			: (randIndex > 8 && randIndex <= 14) ? PoolingManager.GetObject((int)ItemId.POWERUP_ITEM, transform.position, Quaternion.identity)
			: (randIndex > 14 && randIndex <= 32) ? PoolingManager.GetObject((int)ItemId.GOLD_1, transform.position, Quaternion.identity)
			: PoolingManager.GetObject((int)ItemId.GOLD_2, transform.position, Quaternion.identity);


		item.SetActive(true);
	}
}
