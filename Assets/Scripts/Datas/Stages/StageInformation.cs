using Assets.Scripts.Datas;
using UnityEngine;

public class StageInformation : MonoBehaviour
{
    [Header("Data For Begin")]
    [SerializeField] private int id;

    [Header("Data For Save")]
    public StageData stageData;

    public int Id { get => id; set => id = value; }

    
}