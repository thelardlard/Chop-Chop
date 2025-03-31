using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    private int _logCount;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddLog(int amount)
    {
        _logCount += amount;
        UIManager.Instance.UpdateLogCount(_logCount);
    }
}
