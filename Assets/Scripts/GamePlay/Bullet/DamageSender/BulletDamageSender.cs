using UnityEngine;

public class BulletDamageSender : MonoBehaviour
{
    [SerializeField] private float damage;

	public float Damage { get => damage; set => damage = value; }
}
