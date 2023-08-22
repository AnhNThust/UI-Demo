using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	// private readonly string stagePathPrefix = "Stage_";
	private readonly string wavePath = "Waves";
	private bool canPlay = false;
	private List<GameObject> listWave;

	// Test => OK
	private List<int> allWaves = new List<int>();

	public bool CanPlay { get => canPlay; set => canPlay = value; }
	public List<GameObject> ListWave { get => listWave; set => listWave = value; }

	private void OnEnable()
	{
		//wavePath = $"{wavePathPrefix}{GameDatas.MapId}";
		Time.timeScale = 1.0f;
		listWave = new List<GameObject>();

		// Test => OK
		allWaves = GameDatas.GetAllWaveOfMap(GameDatas.MapId);

		StartCoroutine(LoadAllResources());
	}

	IEnumerator LoadAllResources()
	{
		// yield return null;

		// Load resource for current map
		GameObject[] waves = Resources.LoadAll<GameObject>(wavePath);

		for (int i = 0; i < allWaves.Count; i++)
		{
			for (int j = 0; j < waves.Length; j++)
			{
				if (!$"Wave_{allWaves[i]}".Equals(waves[j].name)) continue;

				GameObject wave = Instantiate(waves[j]);
				wave.name = $"Wave_{allWaves[i]}";
				wave.transform.position = Vector3.zero;

				if (i == allWaves.Count - 1)
					wave.GetComponent<CheckHolder>().SetInfo(i + 1, true);
				else
					wave.GetComponent<CheckHolder>().SetInfo(i + 1, false);

				wave.SetActive(false);

				listWave.Add(wave);
				break;
			}
		}


		// Set NextWave for Wave
		for (int i = 0; i < listWave.Count; i++)
		{
			CheckHolder checkHolder = listWave[i].GetComponent<CheckHolder>();
			if (checkHolder.IsFinalWave) break;

			checkHolder.NextWave = listWave[i + 1].transform;
		}

		// Background

		// Wave

		yield return new WaitForSeconds(2f);

		canPlay = true;
	}
}
