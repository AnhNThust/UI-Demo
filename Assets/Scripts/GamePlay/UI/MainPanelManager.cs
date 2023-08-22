using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainPanelManager : MonoBehaviour
{
	[SerializeField] private Transform pBulletLevel;
	[SerializeField] private Transform[] imageBulletLevel;
	[SerializeField] private bool isPower = false;

	[Header("========== Wave ==========")]
	[SerializeField] private GameObject wave1;
	[SerializeField] private Text waveText;

	[Header("========== Panel ==========")]
	[SerializeField] private Transform victory;
	[SerializeField] private Transform defeat;
	[SerializeField] private Transform pause;

	[Header("========== UI Element ==========")]
	[SerializeField] private Text goldText;
	[SerializeField] private Text lifeText;

	private int bulletLevel = 0;
	private int gold = 0;
	private int life = 3;
	static MainPanelManager instance;

	[ContextMenu("Reload")]
	private void Reload()
	{
		imageBulletLevel = new Transform[pBulletLevel.childCount];
		for (int i = 0; i < imageBulletLevel.Length; i++)
		{
			imageBulletLevel[i] = pBulletLevel.GetChild(i);
		}
	}

	private IEnumerator CallWave1()
	{
		yield return new WaitForSeconds(3f);
		wave1.SetActive(true);
	}

	private void OnEnable()
	{
		if (instance != null) Debug.LogError("Only 1 MainPanelManager allows exists");
		instance = this;

		wave1 = FindFirstObjectByType<GameManager>().ListWave[0];
		lifeText.text = $"{life}";

		EventDispatcher.AddEvent(EventID.GoldChanged, OnGoldChanged);
		EventDispatcher.AddEvent(EventID.BulletLevelChanged, OnBulletLevelChanged);
		EventDispatcher.AddEvent(EventID.LifeChanged, OnLifeChanged);

		StartCoroutine(CallWave1());
	}
	private void OnDisable()
	{
		EventDispatcher.RemoveEvent(EventID.GoldChanged, OnGoldChanged);
		EventDispatcher.RemoveEvent(EventID.BulletLevelChanged, OnBulletLevelChanged);
		EventDispatcher.RemoveEvent(EventID.LifeChanged, OnLifeChanged);
	}

	private void OnLifeChanged(object obj)
	{
		life -= (int)obj;

		lifeText.text = $"{life}";
	}

	private void OnBulletLevelChanged(object obj)
	{
		if (bulletLevel >= imageBulletLevel.Length) return;

		Image image = imageBulletLevel[bulletLevel].GetComponent<Image>();
		image.color = Color.white;
		bulletLevel++;
	}

	private void OnGoldChanged(object obj)
	{
		//GameDatas.Gold += (int)obj; // Se cong khi hien thi victory panel hoac defeat panel
		gold += (int)obj;
		goldText.text = $"{gold}";
	}

	public void UpgradeBulletLevel()
	{
		if (bulletLevel >= imageBulletLevel.Length) return;

		Image iBL = imageBulletLevel[bulletLevel].GetComponent<Image>();
		iBL.color = Color.white;
		bulletLevel++;
		EventDispatcher.PostEvent(EventID.BulletLevelChanged, 1);
	}

	public void PlanePower()
	{
		if (isPower) return;

		isPower = true;
		EventDispatcher.PostEvent(EventID.PlanePowerUp, isPower);
		EventDispatcher.AddEvent(EventID.PlanePowerDown, (flag) => isPower = (bool)flag);
	}

	// ================= Private Method ====================
	private void ShowWaveTextBase(int waveNumber)
	{
		waveText.text = $"WAVE {waveNumber}";
		waveText.gameObject.SetActive(true);
	}

	private void EnableVictory()
	{
		Time.timeScale = 0;
		GameDatas.Gold += gold;
		victory.gameObject.SetActive(true);
	}

	private void EnableDefeat()
	{
		Time.timeScale = 0;
		defeat.gameObject.SetActive(true);
	}

	private void EnablePause()
	{
		Time.timeScale = 0;
		pause.gameObject.SetActive(true);
	}

	// ================= Public Method ====================
	public static void ShowWaveText(int waveNumber)
	{
		instance.ShowWaveTextBase(waveNumber);
	}

	public static void ShowVictory()
	{
		instance.EnableVictory();
	}

	public static void ShowDefeat()
	{
		instance.EnableDefeat();
	}

	public static void ShowPause()
	{
		instance.EnablePause();
	}
}
