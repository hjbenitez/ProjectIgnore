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
    [SerializeField] private List<ChangableObject> allObjects;

    private float targetDuration = 5;
    private float currentTimer = 0;
    private bool isEndedARound = false;

    private Blink blink;

    private void Awake()
    {
        instance = this;
        blink = Eyes.GetComponent<Blink>();
        item.gameObject.SetActive(false);
        
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
        this.selectedObject = selectedObject;
        selectedObject.SetIsSelected(true);
    }

    public void RemoveSelectedObject()
    {
        selectedObject.SetIsSelected(false);
        this.selectedObject = null; ;
    }

}
