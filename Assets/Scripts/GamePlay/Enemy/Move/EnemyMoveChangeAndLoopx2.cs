using DG.Tweening.Core;
using DG.Tweening;
using UnityEngine;

public class EnemyMoveChangeAndLoopx2 : MonoBehaviour
{
	[Header("========== Start Path ==========")]
	[SerializeField] private DOTweenPath startPath;
	[SerializeField] private float startMoveTime;

	[Header("========== Loop Path 1 ==========")]
	[SerializeField] private DOTweenPath loopPath_1;
	[SerializeField] private float loopMoveTime_1;

	[Header("========== Loop Path 2 ==========")]
	[SerializeField] private DOTweenPath loopPath_2;
	[SerializeField] private float loopMoveTime_2;

	public TweenerCore<Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> TweenerCore { get; set; }
	private EnemyController controller;

	private void OnEnable()
	{
		controller = GetComponent<EnemyController>();
		Moving();
	}

	private void OnDisable()
	{
		TweenerCore.Kill();
		Destroy(GetComponent<EnemyMoveChangeAndLoop>());
	}

	public void SetInfo(DOTweenPath startPath, float startMoveTime, DOTweenPath loopPath_1, float loopMoveTime_1, DOTweenPath loopPath_2, float loopMoveTime_2)
	{
		this.startPath = startPath;
		this.startMoveTime = startMoveTime;
		this.loopPath_1 = loopPath_1;
		this.loopMoveTime_1 = loopMoveTime_1;
		this.loopPath_2 = loopPath_2;
		this.loopMoveTime_2 = loopMoveTime_2;
	}

	private void Moving()
	{
		var points = startPath.wps;

		TweenerCore = transform.DOPath(points.ToArray(), startMoveTime, PathType.CatmullRom, PathMode.TopDown2D)
				.SetEase(Ease.Linear)
				.OnComplete(OnComplete1);

	}

	private void OnComplete1()
	{
		TweenerCore.Kill();

		// Get way point 0
		Vector3 wps_0 = loopPath_1.transform.position;

		// Get all way point but not have way point 0
		var points = loopPath_1.wps;

		// Add way point 0 to all way point
		points.Add(wps_0);

		controller.CanTakeDamage = true;

		TweenerCore = transform.DOPath(points.ToArray(), loopMoveTime_1, PathType.CatmullRom, PathMode.TopDown2D)
			.SetOptions(true)
			.SetLoops(2, LoopType.Restart)
			.SetEase(Ease.Linear)
			.OnComplete(OnComplete2);
	}

	private void OnComplete2()
	{
		TweenerCore.Kill();

		// Get way point 0
		Vector3 wps_0 = loopPath_2.transform.position;

		// Get all way point but not have way point 0
		var points = loopPath_2.wps;

		// Add way point 0 to all way point
		points.Add(wps_0);

		controller.CanTakeDamage = true;

		TweenerCore = transform.DOPath(points.ToArray(), loopMoveTime_2, PathType.CatmullRom, PathMode.TopDown2D)
			.SetOptions(true)
			.SetLoops(2, LoopType.Restart)
			.SetEase(Ease.Linear)
			.OnComplete(OnComplete1);
	}
}
