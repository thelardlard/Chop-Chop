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

    public Button repairButton;
    
    private RepairableObject currentTarget;
    [SerializeField]
    private PlayerInventory inventory;

    void Start()
    {
         
        // Ensure the button is assigned
        if (repairButton != null)
        {
            repairButton.onClick.AddListener(OnRepairClicked);
        }
    }

    public void Show(RepairableObject target)
    {
        currentTarget = target;
        RepairableData data = target.data;

        titleText.text = "Fix " + data.displayName;

        woodText.text = $"Wood: {inventory.GetCount(ResourceType.Wood)} / {data.woodCost}";
        fabricText.text = $"Fabric: {inventory.GetCount(ResourceType.Fabric)} / {data.fabricCost}";
        glassText.text = $"Glass: {inventory.GetCount(ResourceType.Glass)} / {data.glassCost}";

        bool canRepair = inventory.HasEnough(data);
        repairButton.interactable = canRepair;
        notEnoughText.gameObject.SetActive(!canRepair);

        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
        currentTarget = null;
    }

    public void OnRepairClicked()
    {
        if (currentTarget != null)
        {
            currentTarget.TryRepair(inventory);
            Hide();
        }
    }
}
