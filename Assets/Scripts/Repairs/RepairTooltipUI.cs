using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RepairTooltipUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI fabricText;
    public TextMeshProUGUI glassText;
    public TextMeshProUGUI notEnoughText;
    public TextMeshProUGUI pressEText;

    private bool canRepair = false;

    private RepairableObject currentTarget;
    [SerializeField]
    private PlayerInventory inventory;


        public void Show(RepairableObject target)
    {
        currentTarget = target;
        RepairableData data = target.data;

        titleText.text = "Repair " + data.displayName;

        woodText.text = $"Wood: {inventory.GetCount(ResourceType.Wood)} / {data.woodCost}";
        fabricText.text = $"Fabric: {inventory.GetCount(ResourceType.Fabric)} / {data.fabricCost}";
        glassText.text = $"Glass: {inventory.GetCount(ResourceType.Glass)} / {data.glassCost}";

        canRepair = inventory.HasEnough(data);
        notEnoughText.gameObject.SetActive(!canRepair);
        pressEText.gameObject.SetActive(canRepair);

        panel.SetActive(true);
    }
       

    public void Hide()
    {
        panel.SetActive(false);
        currentTarget = null;
        canRepair = false;
    }    
}
