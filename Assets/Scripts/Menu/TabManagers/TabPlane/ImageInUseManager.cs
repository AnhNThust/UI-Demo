using System;
using UnityEngine;

public class ImageInUseManager : MonoBehaviour
{
    [SerializeField] private int oldId;

    private readonly string IMAGE_NAME = "BgInUse";

	private void OnEnable()
	{
		// Cap nhat lan dau
		oldId = GameDatas.PlaneUsedId;
		OnPlaneUsedIdChanged(oldId);

		// Dang ky su kien lang nghe su thay doi Id su dung
		EventDispatcher.AddEvent(EventID.PlaneUsedIdChanged, OnPlaneUsedIdChanged);
	}

	private void OnDisable()
	{
		EventDispatcher.RemoveEvent(EventID.PlaneUsedIdChanged, OnPlaneUsedIdChanged);	
	}

	private void OnPlaneUsedIdChanged(object obj)
	{
		int newId = (int)obj;

		Transform oldImage = transform.GetChild(oldId - 1).Find(IMAGE_NAME);
		oldImage.gameObject.SetActive(false);

		Transform newImage = transform.GetChild(newId - 1).Find(IMAGE_NAME);
		newImage.gameObject.SetActive(true);
	}
}
