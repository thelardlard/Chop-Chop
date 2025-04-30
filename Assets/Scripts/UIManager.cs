using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI _interactionText;
    [SerializeField] private RhythmMinigame _rhythmMinigame;
    

    private void Awake()
    {
        Instance = this;
    }

    public void ShowInteraction(string message)
    {
        _interactionText.text = message;
        _interactionText.gameObject.SetActive(true);
    }

    public void ClearInteraction()
    {
        _interactionText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Starts the rhythm minigame. Returns the instance so the caller can close it later.
    /// </summary>
    public RhythmMinigame StartRhythmMinigame(Action<int> onComplete, int[] pattern = null)
    {
        _rhythmMinigame.gameObject.SetActive(true);            // Ensure the GameObject is enabled
        _rhythmMinigame.OnRhythmComplete = onComplete;         // Assign callback
        _rhythmMinigame.StartRhythm(pattern);                  // Begin the rhythm sequence
        return _rhythmMinigame;                                // Let caller hold a reference
    }


}
