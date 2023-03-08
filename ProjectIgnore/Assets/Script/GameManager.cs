using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private List<SpriteRenderer> items;
    [SerializeField] private ChangableObject[] selectedObjects;
    [SerializeField] private GameObject Eyes;
    private int objectIndex = -1;
    private Blink blink;

    private void Awake()
    {
        instance = this;
        blink = Eyes.GetComponent<Blink>();
        selectedObjects = new ChangableObject[3];
    }

    private void Update()
    {
        objectIndex = Mathf.Clamp(objectIndex, 0, 3);
    }

    public bool GetEyesIsClosed()
    {
        return blink.closed;
    }

    public void SetSelectedObject(ChangableObject selectedObject)
    {
        this.selectedObjects[objectIndex] = selectedObject;
        selectedObject.SetIsSelected(true);
        selectedObject.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        IncrementObject();
    }

    public void RemoveSelectedObject()
    {
        selectedObjects[objectIndex-1].SetIsSelected(false);
        selectedObjects[objectIndex - 1].gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        this.selectedObjects[objectIndex-1] = null;

        DecrementObject();
    }

    public void IncrementObject()
    {
        objectIndex++;
    }

    public void DecrementObject()
    {
        objectIndex--;
    }

}
