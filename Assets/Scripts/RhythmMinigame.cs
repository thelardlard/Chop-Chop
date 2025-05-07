using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class RhythmMinigame : MonoBehaviour
{
    [Header("References")]
    public RectTransform tracker;               // Tracker image (off-center sprite)
    public List<RectTransform> sweetSpots;      // Sweet spot images (off-center sprites)

    [Header("Timing Settings")]
    public float rotationDuration = 1.8f;       // Time for one full rotation (seconds)
    public int rotations = 3;                   // Number of rotations per session
    public float previewDelay = 0.5f;           // Delay before spinning starts (seconds)
    public float hitRange = 10f;                // Degrees tolerance for hit

    // Callback: passes total successful hits
    public Action<int> OnRhythmComplete;

    // Internal state
    private float _rotationSpeed;
    private float _totalTime;
    private float _elapsedTime;
    private float _currentAngle;
    private bool _active;
    private int _successCount;
    private Tree _targetTree;

    // Allowed angles (0° at top, then clockwise every 40°)
    private readonly List<float> _allowedAngles = new() { 0f, 40f, 80f, 120f, 160f, 200f, 240f, 280f, 320f };
    private int[] _currentPattern = new int[] { 0, 3, 6 };  // Default: 0°, 120°, 240°

    // Audio Clips
    public AudioClip successSound;
    public AudioClip failSound;

    /// <summary>
    /// Starts the rhythm sequence using the given pattern indices (into _allowedAngles).
    /// </summary>
    public void StartRhythm(int[] pattern = null)
    {
        _currentPattern = pattern ?? _currentPattern;
        _rotationSpeed = 360f / rotationDuration;
        _totalTime = rotationDuration * rotations;
        StartCoroutine(RhythmSequence());
    }

    

    public void SetTargetTree(Tree tree)
    {
        _targetTree = tree;
    }

    private IEnumerator RhythmSequence()
    {
        // Reset state
        _elapsedTime = 0f;
        _successCount = 0;
        _currentAngle = 0f;
        _active = false;

        // Place sweet spots by rotating their RectTransforms
        PlaceSweetSpots(_currentPattern);

        // Animate preview (e.g., scale/pulse), hook in VFX/SFX here
        foreach (var spot in sweetSpots)
        {
            // Example: pulse scale
            spot.localScale = Vector3.one * 1.2f;
            // TODO: Play preview VFX/SFX
        }

        // Preview delay
        yield return new WaitForSeconds(previewDelay);

        // Activate rotation and input
        _active = true;
    }

    private void Update()
    {
        if (!_active) return;

        // Advance time and angle
        _elapsedTime += Time.deltaTime;
        _currentAngle = (_currentAngle + _rotationSpeed * Time.deltaTime) % 360f;

        // Rotate tracker around pivot
        tracker.localRotation = Quaternion.Euler(0f, 0f, -_currentAngle);

        // Input check
        if (Input.GetButtonDown("Fire1"))//this shouldn't be called directly - FIX
        {
            if (IsInSweetSpot(_currentAngle))
            {
                _successCount++;
                AudioManager.Instance.PlayUIClip(successSound);
                if (_targetTree != null)
                    _targetTree.PlaySmokeRing(0.5f); // Bigger ring on success
                // TODO: Play hit VFX/SFX
            }
            else
            {
                AudioManager.Instance.PlayUIClip(failSound);
                if (_targetTree != null)
                    _targetTree.PlaySmokeRing(0.25f); // Smaller ring on miss
                // TODO: Play miss VFX/SFX
            }
        }

        // End condition
        if (_elapsedTime >= _totalTime)
        {
            if (_targetTree != null)
            {                
                _targetTree.PlaySmokeLength(); 
            }
            _active = false;            
            OnRhythmComplete?.Invoke(_successCount);
        }
    }

    /// <summary>
    /// Rotates each sweet spot image to its designated angle around the pivot.
    /// Assumes each sweetSpot RectTransform is placed at the circle center.
    /// </summary>
    private void PlaceSweetSpots(int[] pattern)
    {
        for (int i = 0; i < sweetSpots.Count; i++)
        {
            if (i < pattern.Length)
            {
                float angle = _allowedAngles[pattern[i]];
                sweetSpots[i].localRotation = Quaternion.Euler(0f, 0f, -angle);
                sweetSpots[i].gameObject.SetActive(true);
            }
            else
            {
                sweetSpots[i].gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Determines if the current angle falls within any sweet spot.
    /// </summary>
    private bool IsInSweetSpot(float angle)
    {
        foreach (var spot in sweetSpots)
        {
            // Spot rotation Z is -allowedAngle
            float spotAngle = -spot.localRotation.eulerAngles.z;
            float delta = Mathf.DeltaAngle(angle, spotAngle);
            if (Mathf.Abs(delta) <= hitRange)
                return true;
        }
        return false;
    }
    public void CloseMinigame()
    {
        gameObject.SetActive(false);
        OnRhythmComplete = null; // Optional: clear callback to prevent leaks
    }
}

