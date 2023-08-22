using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefeatPanelManager : MonoBehaviour
{
	[Header("========== Scene ==========")]
	[SerializeField] private int menuSceneIndex;
	[SerializeField] private int gamePlaySceneIndex;

	[Header("========== UI Element ==========")]
	[SerializeField] private Text energyText;
	[SerializeField] private Text goldText;
	[SerializeField] private Text gemText;

	private void OnEnable()
	{
		energyText.text = $"XXX/XXX";
		goldText.text = $"{GameDatas.Gold}";
		gemText.text = $"{GameDatas.Gem}";
	}

	public void BackToCampaignTab()
	{
		Time.timeScale = 1.0f;
		SceneManager.LoadSceneAsync(menuSceneIndex);
		TabHomeManager.IsOpenCampaign = true;
	}

	public void Replay()
	{
		SceneManager.LoadSceneAsync(gamePlaySceneIndex);
		Time.timeScale = 1.0f;
	}
}
