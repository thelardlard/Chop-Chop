
using UnityEngine;

[CreateAssetMenu(menuName = "Repairable/Repair Item")]
public class RepairableData : ScriptableObject
{
    public string displayName;
    public Sprite icon;

    [Header("Resource Costs")]
    public int woodCost;
    public int fabricCost;
    public int glassCost;

    [Header("Repair Info")]
    public float repairTime = 2f;
    public GameObject replacementPrefab;
    public AudioClip repairSound;

    [Header("Progression")]
    public string unlocksFeature;
    public RepairableData[] requiredBeforeThis;
}
