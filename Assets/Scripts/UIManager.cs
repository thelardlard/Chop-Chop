using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI _interactionText;
    

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

    
}
