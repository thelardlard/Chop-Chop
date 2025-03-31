using UnityEngine;

public class Tree : MonoBehaviour
{
    public int _maxHits = 5;
    private int _currentHits = 0;
    public GameObject _logPrefab;

    public void ChopTree()
    {
        _currentHits++;

        if (_currentHits >= _maxHits)
        {
            FallTree();
        }
    }

    void FallTree()
    {
        UIManager.Instance.ClearInteraction();
        Vector3 spawnPosition = transform.position + new Vector3 (0,2,0);
        Instantiate(_logPrefab, spawnPosition , Quaternion.identity);
        Destroy(gameObject);

    }
}
