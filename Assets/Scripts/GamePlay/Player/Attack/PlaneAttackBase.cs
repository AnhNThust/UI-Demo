using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Note: p - Parent, c - Child
/// Ex: pBarrel, cBarrel
/// </summary>
public class PlaneAttackBase : MonoBehaviour
{
	[SerializeField]
	private GameObject[] planeAttacks;
	private readonly string resourcePathAttack = "PlaneAttacks";

	private void OnEnable()
	{
		StartCoroutine(StartAttack());
	}

	private IEnumerator StartAttack()
	{
		yield return new WaitForSeconds(3f);

		GetPlaneAttack();
	}

	/// <summary>
	/// Goi Prefab Plane_X_Attack thong qua Id cua May Bay duoc truyen vao tu menu
	/// </summary>
	private void GetPlaneAttack()
	{
		planeAttacks = Resources.LoadAll<GameObject>(resourcePathAttack);
		for (int i = 0; i < planeAttacks.Length; i++)
		{
			if (GameDatas.PlaneUsedId != i + 1) continue;
			GameObject obj = Instantiate(planeAttacks[i]);
			obj.transform.SetParent(transform, false);
			break;
		}
	}
}
