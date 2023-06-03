using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    [SerializeField]
    GameObject inspection;

    public float scale = 1f;
    Vector3 lastPos, currPos;
    float rotationSpeed = -0.2f;
    GameObject inspectObject;

    void Start()
    {
        lastPos = Input.mousePosition;
        inspectObject = inspection.GetComponent<Inspect>().inspectObject;
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            inspectObject = inspection.GetComponent<Inspect>().inspectObject;
        }
        if (Input.GetMouseButton(0) && inspectObject != null)
        {
            currPos = Input.mousePosition;
            Vector3 offset = currPos - lastPos;
            inspectObject.transform.RotateAround(inspectObject.transform.position, Vector3.up, offset.x * rotationSpeed);
            inspectObject.transform.RotateAround(transform.position, Vector3.left, offset.y * rotationSpeed);
        }
        lastPos = Input.mousePosition;
    }
}
