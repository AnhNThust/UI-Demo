using UnityEngine;

public class PanelPlaneInTabHome : MonoBehaviour
{
	[Header("UI Element")]
	public Transform parentUIPlane;
	public Transform confirmQuitPanel;

	private const string resourcePaths = "PlayerForUI";
	private CanvasRenderer[] planeUIs;

	private void OnEnable()
	{
		ResetPlaneUIParent();
		LoadPrefab();

		// Cap nhat lan dau
		OnPlaneUsedIdChanged(GameDatas.PlaneUsedId);
	}

	private void Update()
	{
		if (Input.GetKey("escape"))
		{
			confirmQuitPanel.gameObject.SetActive(true);
		}
	}

	private void OnPlaneUsedIdChanged(int usedId)
	{
		ShowPlaneUI(usedId);
	}

	private void ShowPlaneUI(int planeId)
	{
		CanvasRenderer plane = Instantiate(planeUIs[planeId - 1]);
		plane.transform.SetParent(parentUIPlane);
		plane.GetComponent<RectTransform>().localPosition = Vector3.zero;
		plane.GetComponent<RectTransform>().localScale = new Vector3(0.85f, 0.85f, 0.85f);
	}

	private void LoadPrefab()
	{
		planeUIs = new CanvasRenderer[100];
		planeUIs = Resources.LoadAll<CanvasRenderer>(resourcePaths);
	}

	private void ResetPlaneUIParent()
	{
		for (int i = 0; i < parentUIPlane.childCount; i++)
		{
			DestroyImmediate(parentUIPlane.GetChild(i).gameObject);
		}
	}
}
