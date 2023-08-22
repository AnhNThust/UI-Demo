using System.Collections;
using UnityEngine;

public class CheckHolder : MonoBehaviour
{
    [SerializeField] private Transform holder;
    [SerializeField] private Transform spawner;
    [SerializeField] private bool canCheck = false;
    [SerializeField] private bool isFinalWave = false;
    [SerializeField] private int waveNumber;
    [SerializeField] private Transform nextWave;

	public Transform NextWave { get => nextWave; set => nextWave = value; }
	public bool IsFinalWave { get => isFinalWave; set => isFinalWave = value; }

	private void OnEnable()
    {
        EventDispatcher.AddEvent(EventID.CheckHolderChanged, OnCheckHolderChanged);

        StartCoroutine(CheckedHolder());
    }

    private void OnCheckHolderChanged(object obj)
    {
        canCheck = (bool)obj;
    }

    IEnumerator CheckedHolder()
    {
        yield return new WaitForSeconds(2f);

		MainPanelManager.ShowWaveText(waveNumber);

		yield return new WaitUntil(() => canCheck);

        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (holder.childCount > 0) continue;

            if (!isFinalWave)
            {
                // Enable next wave
                nextWave.gameObject.SetActive(true);
                break;
            }

            // Call Panel Victory
            yield return new WaitForSeconds(2.5f);
            MainPanelManager.ShowVictory();
            
            if (GameDatas.MapId == GameDatas.LastMapIdOpen)
				GameDatas.LastMapIdOpen += 1;

			break;
		}

		gameObject.SetActive(false);
	}

    public void SetInfo(int waveNumber, bool isFinalWave)
    {
        this.waveNumber = waveNumber;
        this.isFinalWave = isFinalWave;
    }
}
