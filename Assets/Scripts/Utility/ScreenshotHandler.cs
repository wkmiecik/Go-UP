using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour
{
    //private static ScreenshotHandler instance;

    //private Camera cam;
    //private bool takeScreenshot;

    //private void Awake()
    //{
    //    instance = this;
    //    cam = gameObject.GetComponent<Camera>();
    //}

    //private void OnPostRender()
    //{
    //    if(takeScreenshot)
    //    {
    //        takeScreenshot = false;
    //        RenderTexture renderTexture = cam.targetTexture;

    //        Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
    //        Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
    //        renderResult.ReadPixels(rect, 0, 0);

    //        byte[] byteArray = renderResult.EncodeToPNG();
    //        System.IO.File.WriteAllBytes(Application.dataPath + "/Screen1.png", byteArray);
    //        Debug.Log("Screenshot saved");

    //        RenderTexture.ReleaseTemporary(renderTexture);
    //        cam.targetTexture = null;
    //    }
    //}

    //private void TakeScreenshot(int width, int height)
    //{
    //    cam.targetTexture = RenderTexture.GetTemporary(width, height, 16);
    //    takeScreenshot = true;
    //}

    //public static void TakeScreenshot_Static(int width, int height)
    //{
    //    instance.TakeScreenshot(width, height);
    //}
}
