using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private SpriteRenderer item;
    [SerializeField] private GameObject Eyes;
    [SerializeField] private ChangableObject selectedObject;
    private Blink blink;

    private void Awake()
    {
        instance = this;
        blink = Eyes.GetComponent<Blink>();
        item.gameObject.SetActive(false);

    }

    private void Update()
    {
        if(selectedObject != null)
        {
            item.gameObject.SetActive(true);
            item.sprite = selectedObject.GetComponent<SpriteRenderer>().sprite;
        }

        else
        {
            item.sprite = null;
            item.gameObject.SetActive(false);
        }
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
