using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadData : MonoBehaviour
{
	public string nextScene;

	private void Start()
	{
		StartCoroutine(LoadedData());

		SceneManager.LoadSceneAsync(nextScene);
	}

	IEnumerator LoadedData()
	{
		// TODO: Logic Load Resources

		yield return new WaitForSeconds(4);
	}
}
