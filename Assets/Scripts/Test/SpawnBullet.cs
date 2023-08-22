using Assets.Scripts.Enums;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnBullet : MonoBehaviour
{
	[SerializeField]
	private float coolDown;
	public float CoolDown { get => coolDown; set => coolDown = value; }

	[SerializeField]
	private ObjectID bulletNormalId;

	[SerializeField]
	private ObjectID bulletSpecialId;

	private void Start()
	{

		StartCoroutine(Attack());

	}

	private IEnumerator Attack()
	{
		while (true)
		{
			yield return new WaitForSeconds(coolDown);

			GameObject bullet = PoolingManager.GetObject((int)bulletNormalId, transform.position, Quaternion.identity);
			bullet.SetActive(true);
		}
	}
}
