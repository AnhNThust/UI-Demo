using DG.Tweening;
using DG.Tweening.Core;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveToGroup : MonoBehaviour
{
	[SerializeField] private DOTweenPath path;
	[SerializeField] private float timeMove;
	[SerializeField] private Vector3 destination;

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
		Destroy(GetComponent<EnemyMoveToGroup>());
	}

	public void SetInfo(DOTweenPath path, float timeMove, Vector3 destination)
	{
		this.path = path;
		this.timeMove = timeMove;
		this.destination = destination;
	}

	private void Moving()
	{
		List<Vector3> points = new List<Vector3>(path.wps);
		points.Add(destination);

		TweenerCore = transform.DOPath(points.ToArray(), timeMove, PathType.CatmullRom, PathMode.TopDown2D)
				.SetEase(Ease.Linear)
				.OnComplete(() => controller.CanTakeDamage = true);
	}
}
