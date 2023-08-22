using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

public class EnemyOnlyMove : MonoBehaviour
{
	[SerializeField] private DOTweenPath path;
	[SerializeField] private float timeMove;

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
		Destroy(GetComponent<EnemyOnlyMove>());
	}

	public void SetInfo(DOTweenPath path, float timeMove)
	{
		this.path = path;
		this.timeMove = timeMove;
	}

	private void Moving()
	{
		var points = path.wps;

		TweenerCore = transform.DOPath(points.ToArray(), timeMove, PathType.CatmullRom, PathMode.TopDown2D)
				.SetEase(Ease.Linear)
				.SetLoops(-1, LoopType.Restart);
		controller.CanTakeDamage = true;
	}
}
