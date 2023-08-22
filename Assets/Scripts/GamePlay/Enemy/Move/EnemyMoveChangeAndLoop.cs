using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

public class EnemyMoveChangeAndLoop : MonoBehaviour
{
	[SerializeField]
	private DOTweenPath startPath;

	[SerializeField]
	private float startMoveTime;

	[SerializeField]
	private DOTweenPath loopPath;

	[SerializeField]
	private float loopMoveTime;

	public DOTweenPath StartPath { get => startPath; set => startPath = value; }
	public float StartMoveTime { get => startMoveTime; set => startMoveTime = value; }
	public DOTweenPath LoopPath { get => loopPath; set => loopPath = value; }
	public float LoopMoveTime { get => loopMoveTime; set => loopMoveTime = value; }

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

	public void SetInfo(DOTweenPath startPath, float startMoveTime, DOTweenPath loopPath, float loopMoveTime)
	{
		this.startPath = startPath;
		this.startMoveTime = startMoveTime;
		this.loopPath = loopPath;
		this.loopMoveTime = loopMoveTime;
	}

	private void Moving()
	{
		var points = startPath.wps;

		TweenerCore = transform.DOPath(points.ToArray(), startMoveTime, PathType.CatmullRom, PathMode.TopDown2D)
				.SetEase(Ease.Linear)
				.OnComplete(OnComplete);

	}

	private void OnComplete()
	{
		var points = loopPath.wps;

		controller.CanTakeDamage = true;

		TweenerCore = transform.DOPath(points.ToArray(), loopMoveTime, PathType.CatmullRom, PathMode.TopDown2D)
			.SetOptions(true)
			.SetLoops(-1, LoopType.Restart)
			.SetEase(Ease.Linear);
	}
}
