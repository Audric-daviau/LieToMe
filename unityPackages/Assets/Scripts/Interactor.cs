using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    public int interactDistance = 2;
    public LayerMask interactableLayerMask = 8;
    UnityEvent onInteract;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactDistance, interactableLayerMask))
        {
            if (hit.collider.GetComponent<Interactable>() != false)
            {
                onInteract = hit.collider.GetComponent<Interactable>().onInteract;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    onInteract.Invoke();
                }
            }
        }
    }
}
