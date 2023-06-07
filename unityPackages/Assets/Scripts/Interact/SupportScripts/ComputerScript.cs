using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerScript : MonoBehaviour
{
    [SerializeField]
    GameObject screen;
    [SerializeField]
    GameObject spotLight;
    [SerializeField]
    Sprite image;

    private bool isGlowingRed = true;

    public void ChangeColor()
    {
        Material mat = screen.GetComponent<Renderer>().material;
        if (isGlowingRed)
        {
            mat.SetColor("_EmissionColor", new Color(0.072f, 0.226f, 0.05f, 1f));
            spotLight.GetComponent<Light>().color = Color.green;
            mat.SetTexture("_MainTex", image.texture);
            mat.color = Color.white;
        }
        else
        {
            mat.SetColor("_EmissionColor", new Color(0.25f, 0f, 0f, 1f));
            spotLight.GetComponent<Light>().color = Color.red;
            mat.SetTexture("_MainTex", null);
            mat.color = new Color(0.125f, 0.118f, 0.118f, 1f);
        }
        isGlowingRed = !isGlowingRed;
    }
}
