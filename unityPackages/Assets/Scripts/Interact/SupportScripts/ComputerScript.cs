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
    [SerializeField]
    GameObject emotionScreen;
    [SerializeField]
    GameObject firstPersonController;
    [SerializeField]
    GameObject firstPersonCamera;
    [SerializeField]
    GameObject mainCanvas;
    [SerializeField]
    GameObject canvas;

    public string emotion;

    public bool isRunning = false;

    public void ActivateEmotion()
    {
        gameObject.GetComponent<Socket>().enabled = true;
        emotionScreen.SetActive(true);
        screen.SetActive(false);
        spotLight.GetComponent<Light>().color = Color.white;
        isRunning = true;
        ActivateFirstPersonMovement(false);
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void ValidateEmotion()
    {
        emotionScreen.SetActive(false);
        screen.SetActive(true);
        Material mat = screen.GetComponent<Renderer>().material;
        mat.SetColor("_EmissionColor", new Color(0.072f, 0.226f, 0.05f, 1f));
        spotLight.GetComponent<Light>().color = Color.green;
        mat.SetTexture("_MainTex", image.texture);
        mat.color = Color.white;
        isRunning = false;
        ActivateFirstPersonMovement(true);
        Destroy(gameObject.GetComponent<Interactable>());
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.GetComponent<Socket>().enabled = false;
    }

    public void ExitScreen()
    {
        emotionScreen.SetActive(false);
        screen.SetActive(true);
        Material mat = screen.GetComponent<Renderer>().material;
        mat.SetColor("_EmissionColor", new Color(0.25f, 0f, 0f, 1f));
        spotLight.GetComponent<Light>().color = Color.red;
        mat.SetTexture("_MainTex", null);
        mat.color = new Color(0.125f, 0.118f, 0.118f, 1f);
        isRunning = false;
        ActivateFirstPersonMovement(true);
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.GetComponent<Socket>().enabled = false;
    }

    private void ActivateFirstPersonMovement(bool value)
    {
        mainCanvas.SetActive(value);
        canvas.SetActive(!value);
        firstPersonCamera.GetComponent<FirstPersonLook>().enabled = value;
        firstPersonController.GetComponent<FirstPersonMovement>().enabled = value;
        firstPersonController.GetComponent<Jump>().enabled = value;
        firstPersonController.GetComponent<Crouch>().enabled = value;
        Cursor.visible = !value;
    }
}
