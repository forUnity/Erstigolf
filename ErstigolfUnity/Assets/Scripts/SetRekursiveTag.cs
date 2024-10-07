using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRekursiveTag : MonoBehaviour
{
    public string Tag = "Road";
    [ContextMenu("Set Tag In CHildren")]
    public void SetTagInChildren()
    {
        var children = GetComponentsInChildren<Transform>();
        foreach (Transform item in children)
        {
            item.gameObject.tag = Tag;
        }
    }
}
