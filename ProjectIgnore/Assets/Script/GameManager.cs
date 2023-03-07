using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject Eyes;

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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.tag == "Object")
            {
                Debug.Log("Object clicked");
            }
        }
    }

}
