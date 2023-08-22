using UnityEngine;

/// <summary>
/// Quan ly hien thi icon Selector
/// </summary>
public class ImageSelectManager : MonoBehaviour
{
	[SerializeField] private int oldId;

	private readonly string ICON_NAME = "Selector";

	private void OnEnable()
	{
		// Cap nhat lan dau
		oldId = GameDatas.PlaneUsedId;
		OnPlanedSelectedChanged(oldId);

		EventDispatcher.AddEvent(EventID.PlaneSelectedChanged, OnPlanedSelectedChanged);
	}

	private void OnDisable()
	{
		EventDispatcher.AddEvent(EventID.PlaneSelectedChanged, OnPlanedSelectedChanged);
	}

	private void OnPlanedSelectedChanged(object obj)
	{
		int newId = (int)obj;
		Transform oldIcon = transform.GetChild(oldId - 1).Find(ICON_NAME);
		oldIcon.gameObject.SetActive(false);

		Transform newIcon = transform.GetChild(newId - 1).Find(ICON_NAME);
		newIcon.gameObject.SetActive(true);

		oldId = newId;
	}
}
