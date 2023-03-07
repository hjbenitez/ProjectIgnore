using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool objectSelected;
    // Start is called before the first frame update
    void Start()
    {
        objectSelected = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && !objectSelected)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.tag == "Object")
            {
                GameManager.instance.SetSelectedObject(hit.transform.gameObject.GetComponent<ChangableObject>());
                objectSelected = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.RemoveSelectedObject();
            objectSelected = false;
        }
    }
}
