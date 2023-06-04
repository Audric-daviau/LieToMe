using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspect : MonoBehaviour
{
    [SerializeField]
    GameObject firstPersonController;
    [SerializeField]
    GameObject firstPersonCamera;
    [SerializeField]
    GameObject interactionObject;
    [SerializeField]
    GameObject mainCanvas;
    [SerializeField]
    GameObject inspectionCanvas;

    public GameObject inspectObject = null;
    private bool inpect = true;

    public void OnInspect(GameObject objectToShow)
    {
        if (inpect)
        {
            mainCanvas.SetActive(false);
            inspectionCanvas.SetActive(true);
            firstPersonCamera.GetComponent<FirstPersonLook>().setPauseCamera(true);
            firstPersonController.GetComponent<FirstPersonMovement>().setPauseMovement(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            inspectObject = Instantiate(objectToShow, gameObject.transform);
            inspectObject.transform.Translate(-objectToShow.transform.position + interactionObject.transform.position);
            inspectObject.layer = interactionObject.layer;
        }
        inpect = false;
    }

    public void ExitOnInspect()
    {
        mainCanvas.SetActive(true);
        inspectionCanvas.SetActive(false);
        firstPersonCamera.GetComponent<FirstPersonLook>().setPauseCamera(false);
        firstPersonController.GetComponent<FirstPersonMovement>().setPauseMovement(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (inspectObject != null)
        {
            Destroy(inspectObject);
        }
        inpect = true;
    }
}
