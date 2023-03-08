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
    [SerializeField] private ChangableObject selectedObject;
    [SerializeField] private List<ChangableObject> allObjects;

    private float targetDuration = 5;
    private float currentTimer = 0;
    private bool isEndedARound = false;

    private int objectIndex = -1;
    private Blink blink;

    private void Awake()
    {
        instance = this;
        blink = Eyes.GetComponent<Blink>();
        selectedObjects = new ChangableObject[3];
    }

    private void UpdateAllObjects()
    {
        foreach (ChangableObject obj in allObjects)
        {
            obj.UpdatePhrase();
        }
    }

    private void Update()
    {
        objectIndex = Mathf.Clamp(objectIndex, 0, 3);
        if (GetEyesIsClosed())
        {
            //Debug.Log("Eye cloesed");
            if (!isEndedARound)
            {
                currentTimer += Time.deltaTime;
                if (currentTimer >= targetDuration)
                {
                    Debug.Log("Update Object");
                    UpdateAllObjects();
                    isEndedARound = true;
                    currentTimer = 0;
                }
            }
        }
        else
        {
            isEndedARound = false;
            currentTimer = 0;
        }

        if (selectedObject != null)
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
