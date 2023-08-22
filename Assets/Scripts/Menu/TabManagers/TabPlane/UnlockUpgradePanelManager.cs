using UnityEngine;

public class UnlockUpgradePanelManager : MonoBehaviour
{
	[SerializeField] private int oldId;

	private void OnEnable()
	{
		EventDispatcher.AddEvent(EventID.PlaneSelectedChanged, OnPlaneSelectedChanged);
	}

	private void OnDisable()
	{
		EventDispatcher.RemoveEvent(EventID.PlaneSelectedChanged, OnPlaneSelectedChanged);
	}

	private void OnPlaneSelectedChanged(object obj)
	{
		int newId = (int)obj;


	}
}
