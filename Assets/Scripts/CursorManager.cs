using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private bool _cursorLocked = true; // Initialize as locked

    void Start()
    {
        // Lock and hide the cursor initially
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Public method to toggle cursor visibility and lock state
    public void ToggleCursor()
    {
        if (_cursorLocked)
        {
            // Unlock and show the cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _cursorLocked = false;
        }
        else
        {
            // Lock and hide the cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _cursorLocked = true;
        }
    }
}