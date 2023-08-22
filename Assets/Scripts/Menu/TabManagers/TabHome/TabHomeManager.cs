using UnityEngine;

public class TabHomeManager : MonoBehaviour
{
	[Header("Self")]
	[SerializeField] private Transform homeFooter;

	[Header("Campaign")]
	[SerializeField] private Transform campaignTab;
	[SerializeField] private Transform campaignFooter;

	[Header("Planes UI")]
	[SerializeField] private Transform planesUITab;
	[SerializeField] private Transform planesUIFooter;

	[Header("Coming Soon")]
	[SerializeField] private Transform comingSoonPanel;

	[Header("Data Campain")]
	[SerializeField] private int currentStage;

	private void OnEnable()
	{
		currentStage = GameDatas.CurrentStage;

		if (IsOpenCampaign)
		{
			ShowCampaignTab();
			IsOpenCampaign = false;
		}
	}

	public void ShowCampaignTab()
	{
		// Show everything of campaign tab
		campaignTab.gameObject.SetActive(true);
		campaignFooter.gameObject.SetActive(true);

		// Hide everything of home tab
		gameObject.SetActive(false);
		homeFooter.gameObject.SetActive(false);
	}

	public void ShowComingSoonPanel()
	{
		comingSoonPanel.gameObject.SetActive(true);
	}

	public void ShowPlanesUITab()
	{
		planesUITab.gameObject.SetActive(true);
		planesUIFooter.gameObject.SetActive(true);

		gameObject.SetActive(false);
		homeFooter.gameObject.SetActive(false);
	}

	// ================ Static ===================

	public static bool IsOpenCampaign = false;
}
