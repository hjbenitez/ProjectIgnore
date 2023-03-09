using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

[RequireComponent(typeof(SpriteRenderer))]
public class ChangableObject : MonoBehaviour
{
    [SerializeField] List<ObjectInfo> objectInfos;
    private int currentIndex = 0;
    private SpriteRenderer render;
    private bool isSelected = false;
    private Animator ac;

    [SerializeField] private bool canDisappear = true;
    private int disappearRequiredCount = 3;
    private int disappearCounter = 0;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        ac = GetComponent<Animator>();
    }

    void Start()
    {
        render.sprite = objectInfos[currentIndex].image;
    }

    public void UpdatePhrase()
    {
        if (currentIndex == objectInfos.Count - 1 && !isSelected && canDisappear) // in last phrase and not selected
        {
            disappearCounter++;
            if (disappearCounter >= disappearRequiredCount)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            disappearCounter = 0;
            currentIndex = isSelected ? currentIndex - 1 : currentIndex + 1;
            currentIndex = Mathf.Clamp(currentIndex, 0, objectInfos.Count - 1);
            Debug.Log("currentIndex = " + currentIndex);
            ac.SetInteger("index", currentIndex);
            render.sprite = objectInfos[currentIndex].image;
        }
    }

    public void SetIsSelected(bool isSelected)
    {
        this.isSelected = isSelected;
    }

    public bool GetIsSelected()
    {
        return isSelected;
    }

    public int GetIndex()
    {
        return currentIndex;
    }
}
