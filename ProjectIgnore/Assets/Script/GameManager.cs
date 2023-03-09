using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Animations;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private ChangableObject[] selectedObjects;
    [SerializeField] private GameObject Eyes;
    [SerializeField] private List<ChangableObject> allObjects;
    [SerializeField] private SpriteRenderer currentBackground;
    [SerializeField] private List<Sprite> backgrounds;
    [SerializeField] private Animator hands;
    [SerializeField] private Vector3[] handsPositions;

    [SerializeField] private float targetDuration = 2;
    private float currentTimer = 0;
    private bool isEndedARound = false;

    private int objectIndex = 0;
    private Blink blink;

    private void Awake()
    {
        instance = this;
        blink = Eyes.GetComponent<Blink>();
        selectedObjects = new ChangableObject[3];

        GameObject[] allObjectsGO = GameObject.FindGameObjectsWithTag("Object");

        foreach(GameObject go in allObjectsGO)
        {
            allObjects.Add(go.GetComponent<ChangableObject>());
        }
    }

    private void UpdateAllObjects()
    {
        foreach (ChangableObject obj in allObjects)
        {
            obj.UpdatePhrase();
        }
    }

    private void DeselectAllObjects()
    {
        for (int i = 0; i < selectedObjects.Length; i++)
        {
            if (selectedObjects[i] != null)
            {
                selectedObjects[i].SetIsSelected(false);
                selectedObjects[i].gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                selectedObjects[i] = null;
            }
        }
        objectIndex = 0;
    }

    private void Update()
    {
        

        if (GetEyesIsClosed())
        {
            hands.speed = 1;
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

                    //Change background
                    int backgroundIndex = allObjects[2].GetIndex();
                    currentBackground.sprite = backgrounds[backgroundIndex];

                    //Set hands center
                    int handsIndex = allObjects[7].GetIndex();
                    hands.gameObject.transform.localPosition = handsPositions[handsIndex];

                    SoundManager.instance.Play("CompleteSound");

                    //Deselects the user selections after succdssful blink
                    DeselectAllObjects();
                }
            }
        }
        else
        {
            hands.speed = 0;
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

    public void ResetGame()
    {
        foreach (ChangableObject obj in allObjects)
        {
            obj.ResetPhrase();
        }

        //Change background
        int backgroundIndex = allObjects[2].GetIndex();
        currentBackground.sprite = backgrounds[backgroundIndex];

        DeselectAllObjects();

    }

}
