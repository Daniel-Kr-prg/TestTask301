using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PrefabPreviewExporter : MonoBehaviour
{
    public string filepath;

    public List<GameObject> prefabsToExport;
}

[CustomEditor(typeof(PrefabPreviewExporter))]
public class PrefabPreviewExporterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Export prefab previews"))
        {
            PrefabPreviewExporter prefabPreview = (PrefabPreviewExporter)target;

            SavePrefabPreviewAsPNG(prefabPreview.prefabsToExport, prefabPreview.filepath);
        }
    }

    public void SavePrefabPreviewAsPNG(List<GameObject> prefabsToExport, string filepath)
    {
        if (filepath == "")
        {
            return;
        }

        if (prefabsToExport == null || prefabsToExport.Count == 0)
        {
            return;
        }

        foreach (GameObject prefabObject in prefabsToExport)
        {
            if (prefabObject == null)
                continue;

            Texture2D previewTexture = AssetPreview.GetAssetPreview(prefabObject);

            if (previewTexture == null)
            {
                Debug.LogWarning("Failed to generate preview for the prefab.");
                return;
            }

            byte[] pngBytes = previewTexture.EncodeToPNG();
            File.WriteAllBytes(filepath + prefabObject.name + ".png", pngBytes);

            Debug.Log("Prefab preview saved as PNG: " + filepath);
        }

    }
}