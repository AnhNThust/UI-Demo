using Lean.Touch;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartPanelManager : MonoBehaviour
{
	//[Header("Select Option")]
	//[SerializeField] private Transform optionParent;
	//[SerializeField] private Transform[] options;

	[Header("UI Element")]
	[SerializeField] private Transform backgroundStart;
	[SerializeField] private Transform backgroundDuoi;
	[SerializeField] private Transform mainPanel;
	[SerializeField] private Transform startText;
	[SerializeField] private Text mapText;
	[SerializeField] private Text mapLevelText;

	[Header("Player")]
	[SerializeField] private Transform player;

	private GameManager gameManager;

	//[ContextMenu("Reload")]
	//private void Reload()
	//{
	//	options = new Transform[optionParent.childCount];
	//	for (int i = 0; i < options.Length; i++)
	//	{
	//		options[i] = optionParent.GetChild(i);
	//	}
	//}

	private void OnEnable()
	{
		gameManager = FindFirstObjectByType<GameManager>();

		mapText.text = $"MAP {GameDatas.MapId}";
		mapLevelText.text= $"[{GameDatas.MapLevel}]";

		StartCoroutine(ShowStartText());
	}

	IEnumerator ShowStartText()
	{
		yield return new WaitUntil(() => gameManager.CanPlay);

		startText.gameObject.SetActive(true);
	}

	//public void ChooseOption()
	//{
	//	for (int i = 0; i < options.Length; i++)
	//	{
	//		if (options[i].name != EventSystem.current.currentSelectedGameObject.name)
	//		{
	//			options[i].Find("Icon").GetChild(0).gameObject.SetActive(true);
	//			options[i].Find("Icon").GetChild(1).gameObject.SetActive(false);
	//		}
	//		else
	//		{
	//			options[i].Find("Icon").GetChild(0).gameObject.SetActive(false);
	//			options[i].Find("Icon").GetChild(1).gameObject.SetActive(true);
	//		}
	//	}
	//}

	public void OnPlayClick()
	{
		if (gameManager == null || !gameManager.CanPlay) return;

		backgroundStart.gameObject.SetActive(false);
		backgroundDuoi.gameObject.SetActive(true);
		mainPanel.gameObject.SetActive(true);
		gameObject.SetActive(false);

		player.GetComponent<LeanDragTranslate>().enabled = true;
		player.GetComponent<PlaneAttackBase>().enabled = true;
		player.GetComponent<PlaneTakeDamage>().enabled = true;
	}
}
