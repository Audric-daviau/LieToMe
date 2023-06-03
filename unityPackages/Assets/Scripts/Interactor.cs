using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    public int interactDistance = 2;
    public LayerMask interactableLayerMask = 8;
    public Image interactImage;
    public Sprite defaultIcon;
    public Vector2 defaultIconSize;
    public Sprite defaulInteractIcon;
    public Vector2 defaultInteractIconSize;
    Interactable interactable;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactDistance, interactableLayerMask))
        {
            if (hit.collider.GetComponent<Interactable>() != false)
            {
                if (interactable == null || interactable.ID != hit.collider.GetComponent<Interactable>().ID)
                {
                    interactable = hit.collider.GetComponent<Interactable>();
                    if(interactable.iconSize == Vector2.zero)
                    {
                        interactImage.rectTransform.sizeDelta = defaultInteractIconSize;
                    }
                    else
                    {
                        interactImage.rectTransform.sizeDelta = interactable.iconSize;
                    }
                }
                if (interactable.interactionIcon != null)
                {
                    interactImage.sprite = interactable.interactionIcon;
                }
                else
                {
                    interactImage.sprite = defaulInteractIcon;
                    interactImage.rectTransform.sizeDelta = defaultInteractIconSize;
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    interactable.onInteract.Invoke();
                }
            }
        }
        else
        {
            if(interactImage.sprite!= defaultIcon)
            {
                interactImage.sprite = defaultIcon;
                interactImage.rectTransform.sizeDelta = defaultIconSize;
            }
        }
    }
}
