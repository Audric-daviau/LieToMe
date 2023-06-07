using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    int currentCamIndex = 0;
    WebCamTexture text;

    public RawImage display;

    public void Start()
    {
        if (WebCamTexture.devices.Length > 0)
        {
            currentCamIndex += 1;
            currentCamIndex %= WebCamTexture.devices.Length;
        }

        WebCamDevice device = WebCamTexture.devices[currentCamIndex];
        text = new WebCamTexture(device.name);
        display.texture = text;

        text.Play();
    }

}
