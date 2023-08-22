using UnityEngine;

public class PlaneModel : MonoBehaviour
{
	[SerializeField]
	private GameObject[] planeTextures;
	private readonly string resourcePathTexture = "PlaneTextures";

	[SerializeField]
	private GameObject[] planeTrails;
	private readonly string resourcePathTrail = "PlaneTrails";

	private void Start()
	{
		GetPlaneTexture();
		GetPlaneTrail();
	}

	private void GetPlaneTexture()
	{
		planeTextures = Resources.LoadAll<GameObject>(resourcePathTexture);
		for (int i = 0; i < planeTextures.Length; i++)
		{
			if (GameDatas.PlaneUsedId != i + 1) continue;
			GameObject obj = Instantiate(planeTextures[i]);
			obj.transform.SetParent(transform, false);
			break;
		}
	}

	private void GetPlaneTrail()
	{
		planeTrails = Resources.LoadAll<GameObject>(resourcePathTrail);
		for (int i = 0; i < planeTrails.Length; i++)
		{
			if (GameDatas.PlaneUsedId != i + 1) continue;
			GameObject obj = Instantiate(planeTrails[i]);
			obj.transform.SetParent(transform, false);
			break;
		}
	}
}
