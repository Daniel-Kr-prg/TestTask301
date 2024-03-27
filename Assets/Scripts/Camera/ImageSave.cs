using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ImageSave : MonoBehaviour
{
    public GameObject UIObject;

    public void SaveImageToGallery()
    { 
        try
        {
            CameraHandler handler = FindObjectOfType<CameraHandler>();

            if (handler == null)
            {
                return;
            }

            Camera cameraToCapture = handler.GetOrbitingCamera();

            RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);

            cameraToCapture.targetTexture = renderTexture;

            Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

            UIObject?.SetActive(false);

            cameraToCapture.Render();

            RenderTexture.active = renderTexture;
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            texture.Apply();

            UIObject?.SetActive(true);

            cameraToCapture.targetTexture = null;
            RenderTexture.active = null;
            Destroy(renderTexture);


            byte[] bytes = texture.EncodeToPNG();

            string filename = DateTime.Now.ToString("HHmmddMMyyyy");

            File.WriteAllBytes(filename + ".png", bytes);

            Debug.Log("Saved to: " + filename + ".png");
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            UIObject?.SetActive(true);
        }
    }
}
