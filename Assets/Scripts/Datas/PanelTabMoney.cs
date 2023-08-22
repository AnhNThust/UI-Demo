using UnityEngine;
using UnityEngine.UI;

public class PanelTabMoney : MonoBehaviour
{
	[Header("UI Element")]
	[SerializeField] private Text energyText;
	[SerializeField] private Text goldText;
	[SerializeField] private Text gemText;

	private void OnEnable()
	{
		//Cap nhat lan dau
		OnGoldChanged(GameDatas.Gold);
		OnGemChanged(GameDatas.Gem);

		//Dang ky event khi thong so gold, gem thay doi
		EventDispatcher.AddEvent(EventID.GoldChanged, OnGoldChanged);
		EventDispatcher.AddEvent(EventID.GemChanged, OnGemChanged);
	}

	private void OnDisable()
	{
		EventDispatcher.RemoveEvent(EventID.GoldChanged, OnGoldChanged);
	}

	// Ham thuc thi su kien thay doi gia tri cua gold
	private void OnGoldChanged(object obj)
	{
		int gold = (int)obj;
		goldText.text = gold.ToString();
	}

	// Ham thuc thi su kien thay doi gia tri cua gem
	private void OnGemChanged(object obj)
	{
		int gem = (int)obj;
		gemText.text = gem.ToString();
	}

	public void BuyGold(int amount)
	{
		GameDatas.Gold += amount;
	}

	public void BuyGem(int amount)
	{
		GameDatas.Gem += amount;
	}
}
