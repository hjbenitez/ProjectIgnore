using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject InstructionScreen;
    // Update is called once per frame
    void Update()
    {
        if (InstructionScreen.activeSelf)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) && GameManager.instance.GetObjectIndex() < 3)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.tag == "Object" && !hit.transform.gameObject.GetComponent<ChangableObject>().GetIsSelected())
            {
                GameManager.instance.SetSelectedObject(hit.transform.gameObject.GetComponent<ChangableObject>());
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape) && GameManager.instance.GetObjectIndex() != 0)
        {
            GameManager.instance.RemoveSelectedObject();
        }
    }
}
