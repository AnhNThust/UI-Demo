using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanelManager : MonoBehaviour
{
	[SerializeField] private int menuSceneIndex;
	[SerializeField] private int gamePlaySceneIndex;

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

	public void Continue()
	{
		gameObject.SetActive(false);
		Time.timeScale = 1.0f;
	}
}
