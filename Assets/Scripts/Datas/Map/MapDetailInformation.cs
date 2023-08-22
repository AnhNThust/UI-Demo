using Assets.Scripts.Enums;
using UnityEngine;

public class MapDetailInformation : MonoBehaviour
{
    [SerializeField] private MapLevel level;
    [SerializeField] private bool unlocked;

	[SerializeField] private Animator anim;
	[SerializeField] private Transform mission;

    public MapLevel Level { get => level; set => level = value; }
    public bool Unlocked { get => unlocked; set => unlocked = value; }
    public Animator Anim { get => anim; set => anim = value; }
    public Transform Mission { get => mission; set => mission = value; }

    public void DisplayMapDetail()
	{
		anim.enabled = unlocked;
		mission.gameObject.SetActive(unlocked);
	}
}
