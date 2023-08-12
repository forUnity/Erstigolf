using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

public class ForestPlacer : MonoBehaviour
{
    [SerializeField] Transform holder;
    [SerializeField] Vector2Int size;
    [SerializeField] float angle;
    [SerializeField] float gridSize;
    [SerializeField] float minGridDistance;
    [SerializeField] int maxCount;
    [SerializeField] Prop[] props;
    
    Mesh mesh => Resources.GetBuiltinResource<Mesh>("Cube.fbx");

    public void Delete(){
        if (holder == null){
            Debug.LogWarning("no prop holder specified");
            return;
        }
        for (int i = holder.childCount - 1; i >= 0; i--){
            DestroyImmediate(holder.GetChild(i).gameObject);
        }
        
        #if UNITY_EDITOR
        var p = PrefabStageUtility.GetCurrentPrefabStage();
        if (p){
            EditorSceneManager.MarkSceneDirty(PrefabStageUtility.GetCurrentPrefabStage().scene);
        }
        else {
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
        
        #endif
    }

    public void PlaceProps(){
        Delete();
        List<Vector2Int> availablePositions = new List<Vector2Int>();
        for (int x = -size.x; x <= size.x; x++){
            for (int y = -size.y; y <= size.y; y++){
                availablePositions.Add(new Vector2Int(x,y));
            }
        }

        for (int i = 0; i < maxCount; i++){
            GameObject prefab = GetPrefab();
            int posIndex = Random.Range(0, availablePositions.Count);
            Vector2Int point = availablePositions[posIndex];
            // remove all points in range
            int gridDistanceFloor = Mathf.FloorToInt(minGridDistance);
            for (int dx = -gridDistanceFloor; dx <= gridDistanceFloor; dx++)
            for (int dy = -gridDistanceFloor; dy <= gridDistanceFloor; dy++){
                if (new Vector3(dx, 0, dy).sqrMagnitude <= minGridDistance * minGridDistance){
                    availablePositions.Remove(new Vector2Int(dx, dy) + point);
                }
            }
            Instantiate(prefab, GetWorldPosition(point), GetRotation(), holder);
        }
    }

    private Quaternion GetRotation(){
        return Quaternion.Euler(0, Random.Range(0f, 360f), 0);
    }

    private GameObject GetPrefab(){
        int sum = 0;
        for (int i = 0; i < props.Length; i++){
            sum += props[i].relativeAmount;
        }

        int index = Random.Range(0, sum);
        int k = -1;
        while (index >= 0) index -= props[++k].relativeAmount;
        return props[k].prefab;
    }

    private Vector3 GetWorldPosition(Vector2Int point){
        Vector3 pos = new Vector3(point.x, 0f, point.y);
        pos *= gridSize;
        pos = Quaternion.AngleAxis(angle, Vector3.up) * pos;
        pos += transform.position;
        return pos;
    }

    private void OnDrawGizmosSelected()
    {
        if (transform.childCount <= 0)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawMesh(mesh, transform.position, Quaternion.Euler(0, angle, 0), 2 * new Vector3(size.x * gridSize, 1f, size.y * gridSize));
        }
    }
}

[System.Serializable]
public struct Prop{
    public GameObject prefab;
    public int relativeAmount;
}
