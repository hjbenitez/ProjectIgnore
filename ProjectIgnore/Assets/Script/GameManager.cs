using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject Eyes;
    [SerializeField] private ChangableObject selectedObject;
    private Blink blink;

    private void Awake()
    {
        instance = this;
        blink = Eyes.GetComponent<Blink>();
    }

    public bool GetEyesIsClosed()
    {
        return blink.closed;
    }

    public void SetSelectedObject(ChangableObject selectedObject)
    {
        this.selectedObject = selectedObject;
        selectedObject.SetIsSelected(true);
    }

    public void RemoveSelectedObject()
    {
        selectedObject.SetIsSelected(false);
        this.selectedObject = null; ;
    }

}
