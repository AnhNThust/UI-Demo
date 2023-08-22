using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabPlanesManager : MonoBehaviour
{
	[Header("Self")]
	[SerializeField] private Transform selfFooter;

	//[SerializeField] private Transform parentOption;
	//[SerializeField] private Transform[] options;
	[SerializeField] private Transform useButton;

	//[Header("Panel Show Avatar")]
	//[SerializeField] private Transform parentAvatar;
	//[SerializeField] private Transform[] avatars;

	//[Header("Panel Show Plane Animation")]
	//[SerializeField] private Transform parentPlaneUI;
	//[SerializeField] private Transform[] planeUIs;

	[Header("Upgrade Panel")]
	[SerializeField] private Transform upgradePanel;

	[SerializeField] private Text priceUpgradeGoldText;
	[SerializeField] private Text priceUpgradeGemText;

	[Header("Unlock Panel")]
	[SerializeField] private Transform unlockPanel;

	[SerializeField] private Text priceUnlockText;
	[SerializeField] private Transform commonHeader;

	[Header("Main Menu")]
	[SerializeField] private Transform homeTab;

	[SerializeField] private Transform homeFooter;

	[Header("Coming Soon")]
	[SerializeField] private Transform comingSoonPanel;

	[Header("Not Enough Gem")]
	[SerializeField] private Transform notEnoughPanel;

	[Header("Test")]
	[SerializeField] private Transform oldPlaneSelect;

	[ContextMenu("Reload")]
	private void Reload()
	{
		//options = new Transform[parentOption.childCount];
		//for (int i = 0; i < options.Length; i++)
		//{
		//	options[i] = parentOption.GetChild(i);
		//}

		//avatars = new Transform[parentAvatar.childCount];
		//for (int i = 0; i < avatars.Length; i++)
		//{
		//	avatars[i] = parentAvatar.GetChild(i);
		//}

		//planeUIs = new Transform[parentPlaneUI.childCount];
		//for (int i = 0; i < planeUIs.Length; i++)
		//{
		//	planeUIs[i] = parentPlaneUI.GetChild(i);
		//}
	}

	[SerializeField] private PlaneInformation[] allCells = null;

	private void OnEnable()
	{
		//Display first time
		for (int i = 0; i < allCells.Length; i++)
		{
			//PlaneDatas data = GameDatas.GetPlaneData(allCells[i].Id);
			allCells[i].DisplayPlane(GameDatas.GetPlaneData(allCells[i]));
			allCells[i].SetStateSelectImage(false);
			allCells[i].SetStateInUseImage(allCells[i].planeData.id == GameDatas.PlaneUsedId);
		}

		//Select plane 1 with id = 0
		OnClickCell(allCells[0]);
	}

	[SerializeField] private PlaneInformation currentCell;

	/// <summary>
	/// Ham click vao mot may bay
	/// </summary>
	/// <param name="button">Tham chieu den class PlaneInformation trong button</param>
	public void OnClickCell(PlaneInformation button)
	{
		if (currentCell != null) currentCell.SetStateSelectImage(false);

		currentCell = button;
		currentCell.SetStateSelectImage(true);
		DisplayCurrentPlane(currentCell.planeData);
	}

	/// <summary>
	/// Ham mo khoa may bay
	/// </summary>
	public void OnClickUnlock()
	{
		int price = currentCell.planeData.priceUnlock;
		if (price > GameDatas.Gem)
		{
			notEnoughPanel.gameObject.SetActive(true);
			return;
		}

		// Thay doi trang thai cua may bay
		currentCell.planeData.unlocked = true;
		currentCell.UnlockPlane();

		// Cap nhat thong tin hien tai
		DisplayCurrentPlane(currentCell.planeData);
		GameDatas.Gem -= price;

		// Luu du lieu
		GameDatas.SavePlaneData(currentCell.planeData);
	}

	/// <summary>
	/// Phuong thuc nang cap may bay bang Gem
	/// </summary>
	public void OnClickUpgradeByGem()
	{
		int price = currentCell.planeData.priceUpgradeGem;
		if (price > GameDatas.Gem)
		{
			notEnoughPanel.gameObject.SetActive(true);
			return;
		}

		// Cap nhat trang thai cua may bay
		currentCell.planeData.level++;
		currentCell.UpdateLevel();

		// Cap nhat thong tin hien tai
		GameDatas.Gem -= price;
		DisplayCurrentPlane(currentCell.planeData);

		// Luu du lieu
		GameDatas.SavePlaneData(currentCell.planeData);
	}

	/// <summary>
	/// Phuong thuc nang cap may bay bang Gold
	/// </summary>
	public void OnClickUpgradeByGold()
	{
		int price = currentCell.planeData.priceUpgradeGold;
		if (price > GameDatas.Gold)
		{
			notEnoughPanel.gameObject.SetActive(true);
			return;
		}

		// Cap nhat trang thai cua may bay
		currentCell.planeData.level++;
		currentCell.UpdateLevel();

		// Cap nhat thong tin hien tai
		GameDatas.Gold -= price;
		DisplayCurrentPlane(currentCell.planeData);

		// Luu du lieu
		GameDatas.SavePlaneData(currentCell.planeData);
	}

	//Display information
	[SerializeField] private Text levelText;

	[SerializeField] private Text nameText;
	[SerializeField] private Transform parentAvatars;
	[SerializeField] private Transform parentPlaneDemo;

	/// <summary>
	/// Phuong thuc hien thi thong tin cua tung may bay khi click vao tung may bay do
	/// </summary>
	/// <param name="data"></param>
	public void DisplayCurrentPlane(PlaneData data)
	{
		for (int i = 0; i < parentAvatars.childCount; i++)
		{
			parentAvatars.GetChild(i).gameObject.SetActive(i == data.id - 1);
			parentPlaneDemo.GetChild(i).gameObject.SetActive(i == data.id - 1);
		}
		nameText.text = data.GetName();
		levelText.text = $"LV.{data.level}/30";

		if (data.unlocked)
		{
			upgradePanel.gameObject.SetActive(true);
			priceUpgradeGoldText.text = data.priceUpgradeGold.ToString();
			priceUpgradeGemText.text = data.priceUpgradeGem.ToString();

			unlockPanel.gameObject.SetActive(false);
			useButton.gameObject.SetActive(true);
		}
		else
		{
			upgradePanel.gameObject.SetActive(false);
			unlockPanel.gameObject.SetActive(true);
			priceUnlockText.text = data.priceUnlock.ToString();

			useButton.gameObject.SetActive(false);
		}
	}

	/// <summary>
	/// Su kien click button Use
	/// </summary>
	public void UsePlane()
	{
		//GameDatas.PlaneUsedId = GameDatas.PlaneSelectId;

		GameDatas.PlaneUsedId = currentCell.planeData.id;
		for (int i = 0; i < allCells.Length; i++)
		{
			allCells[i].SetStateInUseImage(allCells[i].Id == currentCell.planeData.id);
		}
	}

	/// <summary>
	/// Su kien click chon may bay
	/// </summary>
	public void SelectPlane()
	{
		// Get Button Clicked
		GameObject planeSelect = EventSystem.current.currentSelectedGameObject;
		PlaneInformation planeInfo = planeSelect.GetComponent<PlaneInformation>();
		GameDatas.PlaneSelectId = planeInfo.Id;

		if (!planeInfo.Unlocked)
		{
			unlockPanel.gameObject.SetActive(true);
			upgradePanel.gameObject.SetActive(false);

			useButton.gameObject.SetActive(false);
		}
		else
		{
			unlockPanel.gameObject.SetActive(false);
			upgradePanel.gameObject.SetActive(true);

			useButton.gameObject.SetActive(true);
		}
	}

	/// <summary>
	/// Phuong thuc quay lai Menu chinh
	/// </summary>
	public void ReturnMainMenu()
	{
		homeTab.gameObject.SetActive(true);
		homeFooter.gameObject.SetActive(true);

		gameObject.SetActive(false);
		selfFooter.gameObject.SetActive(false);
	}

	/// <summary>
	/// Phuong thuc hien thi bang Coming Soon
	/// </summary>
	public void ShowComingSoonPanel()
	{
		comingSoonPanel.gameObject.SetActive(true);
	}
}