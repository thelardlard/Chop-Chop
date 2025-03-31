using UnityEngine;
using UnityEditor;

public class TreeConverter : MonoBehaviour
{
   /* public GameObject treePrefab; // Assign your tree prefab with colliders
    public Terrain terrain;

    [ContextMenu("Convert Trees to GameObjects")]
    void ConvertTerrainTrees()
    {
        if (!terrain || !treePrefab)
        {
            Debug.LogError("Assign the Terrain and Tree Prefab before converting.");
            return;
        }

        TreeInstance[] trees = terrain.terrainData.treeInstances;
        TerrainData terrainData = terrain.terrainData;

        GameObject treeParent = new GameObject("Converted Trees"); // Group trees under a parent

        foreach (TreeInstance tree in trees)
        {
            // Convert tree position from terrain space to world space
            Vector3 worldPos = Vector3.Scale(tree.position, terrainData.size) + terrain.transform.position;

            // Instantiate the prefab with correct position
            GameObject newTree = PrefabUtility.InstantiatePrefab(treePrefab) as GameObject;
            newTree.transform.position = worldPos;
            newTree.transform.SetParent(treeParent.transform); // Keep things organized
        }

        // Clear terrain trees ONLY in the editor (not in Play Mode)
        if (!Application.isPlaying)
        {
            terrain.terrainData.treeInstances = new TreeInstance[0];
        }

        Debug.Log("Converted trees to GameObjects. You can now delete this script.");
    }*/
}
