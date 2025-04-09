
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public PlayerInventory playerInventory;

    [System.Serializable]
    public class ResourceUIEntry
    {
        public ResourceType resourceType;
        public Image icon;
        public TextMeshProUGUI countText;
    }

    public List<ResourceUIEntry> entries;

    void Start()
    {
        UpdateAll();
    }

    public void UpdateAll()
    {
        foreach (var entry in entries)
        {
            int count = playerInventory.GetCount(entry.resourceType);
            entry.countText.text = count.ToString();
        }
    }

    public void OnResourceChanged()
    {
        UpdateAll();
    }
    void OnEnable()
    {
        PlayerInventory.OnInventoryChanged += UpdateAll;
    }

    void OnDisable()
    {
        PlayerInventory.OnInventoryChanged -= UpdateAll;
    }
}
