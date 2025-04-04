using UnityEngine;

public class PlayerDebugUI : MonoBehaviour
{
    [SerializeField] private PlayerStateManager _stateManager;

    private void OnGUI()
    {
        if (_stateManager == null) return;

        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        style.normal.textColor = Color.white;

        string stateName = _stateManager.CurrentStateName;
        GUI.Label(new Rect(10, 10, 300, 40), $"State: {stateName}", style);
    }
}
