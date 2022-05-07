using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(InventoryObject))]
public class InventoryObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        InventoryObject inventory = target as InventoryObject;
        if(GUILayout.Button("Clear container"))
        {
            for(int i = 0; i < inventory.InventorySlots.Count; i++)
            {
                inventory.InventorySlots[i].Clear();
            }
        }

        DrawDefaultInspector();
    }
}
