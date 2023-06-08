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
    private Transform customTransform;

    public void OnInspect(GameObject objectToShow)
    {
        if (inpect)
        {
            ActivateFirstPersonMovement(false);
            Cursor.lockState = CursorLockMode.Confined;
            inspectObject = Instantiate(objectToShow, gameObject.transform);
            if (inspectObject.GetComponent<CustomTransform>() != null)
            {
                CustomTransform customTransform = inspectObject.GetComponent<CustomTransform>();
                inspectObject.transform.position = customTransform.position;
                inspectObject.transform.eulerAngles = customTransform.rotation;
                inspectObject.transform.localScale *= customTransform.scale;
            }
            else
            {
                inspectObject.transform.position = interactionObject.transform.position;
            }
            foreach (Transform child in inspectObject.transform)
            {
                child.gameObject.layer = interactionObject.layer;
            }
            inspectObject.layer = interactionObject.layer;
        }
        inpect = false;
    }

    public void ExitOnInspect()
    {
        ActivateFirstPersonMovement(true);
        Cursor.lockState = CursorLockMode.Locked;

        if (inspectObject != null)
        {
            Destroy(inspectObject);
        }
        inpect = true;
    }

    private void ActivateFirstPersonMovement(bool value)
    {
        mainCanvas.SetActive(value);
        inspectionCanvas.SetActive(!value);
        firstPersonCamera.GetComponent<FirstPersonLook>().enabled = value;
        firstPersonCamera.GetComponent<Zoom>().enabled = value;
        firstPersonController.GetComponent<FirstPersonMovement>().enabled = value;
        firstPersonController.GetComponent<Jump>().enabled = value;
        firstPersonController.GetComponent<Crouch>().enabled = value;
        Cursor.visible = !value;
    }
}
