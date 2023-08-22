using System.Collections;
using UnityEngine;

public class HideObject : MonoBehaviour
{
    [SerializeField] private float timeHide;

	private void OnEnable()
	{
		StartCoroutine(Hidding());
	}

	IEnumerator Hidding()
	{
		yield return new WaitForSeconds(timeHide);

		gameObject.SetActive(false);
	}
}
