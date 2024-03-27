using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

public class PrefabPreviewExporter : MonoBehaviour
{
    public string filepath;

    public List<Object> prefabsToExport;
}

[CustomEditor(typeof(PrefabPreviewExporter))]
public class PrefabPreviewExporterEditor : Editor
{
    private bool exporting = false;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Export prefab previews"))
        {
            PrefabPreviewExporter prefabPreview = (PrefabPreviewExporter)target;

            SavePrefabPreviewAsPNG(prefabPreview.prefabsToExport, prefabPreview.filepath);
        }

        if (GUILayout.Button("Stop export"))
        {
            StopExport();
        }
    }

    public void SavePrefabPreviewAsPNG(List<Object> prefabsToExport, string filepath)
    {
        if (filepath == "")
        {
            return;
        }

        if (prefabsToExport == null || prefabsToExport.Count == 0)
        {
            return;
        }

        exporting = true;

        foreach (Object prefabObject in prefabsToExport)
        {
            if (prefabObject == null)
                continue;

            Texture2D previewTexture = null;
            while (previewTexture == null && exporting)
            {
                previewTexture = AssetPreview.GetAssetPreview(prefabObject);
            }

            byte[] pngBytes = previewTexture.EncodeToPNG();
            File.WriteAllBytes(filepath + prefabObject.name + ".png", pngBytes);

            Debug.Log("Prefab preview saved as PNG: " + filepath + prefabObject.name + ".png");
        }

    }

    public void StopExport()
    {
        exporting = false;
    }
}