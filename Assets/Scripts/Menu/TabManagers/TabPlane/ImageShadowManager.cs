using UnityEngine;

public class ImageShadowManager : MonoBehaviour
{
    [SerializeField] private int oldId;

    private readonly string IMAGE_NAME = "BongMo";

	private void OnEnable()
	{
		OnNumberPlaneUnlockedChanged(GameDatas.NumberPlaneUnlocked);

		EventDispatcher.AddEvent(EventID.NumberPlaneUnlockedChanged, OnNumberPlaneUnlockedChanged);
	}

	private void OnDisable()
	{
		EventDispatcher.RemoveEvent(EventID.NumberPlaneUnlockedChanged, OnNumberPlaneUnlockedChanged);
	}

	private void OnNumberPlaneUnlockedChanged(object obj)
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform imageShadow = transform.GetChild(i).Find(IMAGE_NAME);
			PlaneInformation planeInfo = transform.GetChild(i).GetComponent<PlaneInformation>();
			if (!planeInfo.Unlocked)
				imageShadow.gameObject.SetActive(true);
			else
				imageShadow.gameObject.SetActive(false);
		}
	}
}
