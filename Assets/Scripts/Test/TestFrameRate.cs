using UnityEngine;

public class TestFrameRate : MonoBehaviour
{
	private void Start()
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
	}
}
