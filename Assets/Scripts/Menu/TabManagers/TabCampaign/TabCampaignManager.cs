using UnityEngine;
using UnityEngine.EventSystems;

public class TabCampaignManager : MonoBehaviour
{
    [Header("Self")]
    [SerializeField] private Transform selfFooter;

    [Header("Main Menu")]
    [SerializeField] private Transform homeTab;
    [SerializeField] private Transform homeFooter;

    [Header("Detail")]
    [SerializeField] private Transform detailTab;
    [SerializeField] private Transform detailFooter;

    [Header("Status")]
    [SerializeField] private Transform mapParent;
    [SerializeField] private MapInformation[] maps;

    [Header("Data")]
    [SerializeField] private int stageId;

	[ContextMenu("Reload")]
    private void Reload()
    {
        maps = new MapInformation[mapParent.childCount];
        for (int i = 0; i < maps.Length; i++)
        {
            maps[i] = mapParent.GetChild(i).GetComponentInChildren<MapInformation>();
        }
    }

	private void OnEnable()
	{
        stageId = GameDatas.CurrentStage;

		for (int i = 0; i < maps.Length; i++)
		{
            //maps[i].DisplayMap(GameDatas.GetMapData(maps[i]));
            maps[i].ShowMapStatus();
		}
	}

	public void ReturnHomeTab()
    {
        // Show everything of home tab
        homeTab.gameObject.SetActive(true);
        homeFooter.gameObject.SetActive(true);

        // Hide everything of campaign tab
        transform.gameObject.SetActive(false);
        selfFooter.gameObject.SetActive(false);
    }

    public void ShowDetailTab() //MapInformation mapInfo
	{
		//if (!mapInfo.mapData.unlocked) return;

		MapInformation info = EventSystem.current.currentSelectedGameObject.GetComponent<MapInformation>();
        if (!info.Unlocked) return;

        // Set Map Id
        GameDatas.MapId = info.Id;

        // Show detail tab
        detailTab.gameObject.SetActive(true);
        detailFooter.gameObject.SetActive(true);

        // Hide current tab
        gameObject.SetActive(false);
        selfFooter.gameObject.SetActive(false);
    }
}
