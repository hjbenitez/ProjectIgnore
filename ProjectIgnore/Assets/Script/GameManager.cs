using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private ChangableObject[] selectedObjects;
    [SerializeField] private GameObject Eyes;
    [SerializeField] private List<ChangableObject> allObjects;

    private float targetDuration = 5;
    private float currentTimer = 0;
    private bool isEndedARound = false;

    private int objectIndex = 0;
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
                    objectIndex = 0;
                    SoundManager.instance.Play("CompleteSound");
                    for(int i = 0; i < selectedObjects.Length; i++)
                    {
                        if(selectedObjects[i] != null )
                        {
                            selectedObjects[i].SetIsSelected(false);
                            selectedObjects[i].gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                            selectedObjects[i] = null;
                        }
                    }
                }
            }
        }
        else
        {
            isEndedARound = false;
            currentTimer = 0;
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
        objectIndex = Mathf.Clamp(objectIndex, 0, 3);
    }

    public void DecrementObject()
    {
        objectIndex--;
        objectIndex = Mathf.Clamp(objectIndex, 0, 3);
    }

    public int GetObjectIndex()
    {
        return objectIndex;
    }

}
