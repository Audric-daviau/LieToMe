using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent onInteract;
    public Sprite interactionIcon;
    public Vector2 iconSize;
    public int ID;

    // Start is called before the first frame update
    void Start()
    {
        ID = Random.Range(0, 999999);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
