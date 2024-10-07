using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ReloadMapEditor : MonoBehaviour
{
    [ContextMenu("Delete _delete stuff")]
    public void Delete_Stuff()
    {
        var all = GetComponentsInChildren<Transform>();
        foreach (var item in all)
        {
            if (item.gameObject.name.Contains("_delete"))
            {
                Destroy(item.gameObject);
            }
        }
    }
}

