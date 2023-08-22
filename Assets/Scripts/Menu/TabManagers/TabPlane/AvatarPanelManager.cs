using UnityEngine;

public class AvatarPanelManager : MonoBehaviour
{
	[SerializeField] private int oldId;

	private void OnEnable()
	{
		oldId = GameDatas.PlaneUsedId;

		// Cap nhat ban dau
		OnPlaneSelectedChanged(GameDatas.PlaneSelectId);

		EventDispatcher.AddEvent(EventID.PlaneSelectedChanged, OnPlaneSelectedChanged);
	}

	private void OnDisable()
	{
		EventDispatcher.RemoveEvent(EventID.PlaneSelectedChanged, OnPlaneSelectedChanged);
	}

	private void OnPlaneSelectedChanged(object obj)
	{
		int id = (int)obj;
		transform.GetChild(oldId - 1).gameObject.SetActive(false);
		transform.GetChild(id - 1).gameObject.SetActive(true);

		oldId = id;
	}
}
