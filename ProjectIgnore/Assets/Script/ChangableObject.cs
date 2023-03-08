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
    private float targetDuration = 0;
    private float currentTimer = 0;
    private bool isSelected = false;
    private Animator ac;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        ac = GetComponent<Animator>();
    }

    void Start()
    {
        render.sprite = objectInfos[currentIndex].image;
        targetDuration = objectInfos[currentIndex].duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentIndex >= objectInfos.Count)
        {
            return; // All Changed
        }
        bool shouldUpdate = GameManager.instance.GetEyesIsClosed(); // change to eye blink variable
        if (shouldUpdate && isSelected)
        {
            currentTimer += Time.deltaTime;
            if (currentTimer > targetDuration)
            {
                currentIndex++;
                ac.SetInteger("index", currentIndex);

                if (currentIndex < objectInfos.Count)
                {
                    render.sprite = objectInfos[currentIndex].image;
                    targetDuration = objectInfos[currentIndex].duration;
                    currentTimer = 0;
                }
            }
        }
    }

    public void SetIsSelected(bool isSelected)
    {
        this.isSelected = isSelected;
    }
}
