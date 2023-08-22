using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TabCampaignDetailManager : MonoBehaviour
{
    [Header("Self")]
    [SerializeField] private Transform detailFooter;

    [Header("Campaign Tab")]
    [SerializeField] private Transform campaignTab;
    [SerializeField] private Transform campaignFooter;

    [Header("UI Element")]
    [SerializeField] private Text titleText;
    [SerializeField] private MapDetailInformation[] mapDetails;

    [Header("Scene")]
    [SerializeField] private string nameScenePlay;

    private void OnEnable()
	{
        titleText.text = $"MAP {GameDatas.MapId}";

		for (int i = 0; i < mapDetails.Length; i++)
		{
            mapDetails[i].DisplayMapDetail();
		}
	}

    public void ReturnCampaignTab()
    {
        // Show Campaign Tab
        campaignTab.gameObject.SetActive(true);
        campaignFooter.gameObject.SetActive(true);

        // Hide Campaign Detail Tab
        gameObject.SetActive(false);
        detailFooter.gameObject.SetActive(false);
    }

    public void ShowMission(Transform mission)
	{
        mission.gameObject.SetActive(true);
	}

    public void Play()
    {
        SceneManager.LoadSceneAsync(nameScenePlay);
    }
}
