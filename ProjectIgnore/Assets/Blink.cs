using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{

     public GameObject upperLid;
     public GameObject lowerLid;

     //Upper lid open and close positions
     Vector2 upperClose;
     Vector2 upperOpen;

     //Lower lid open and close positions
     Vector2 lowerClose;
     Vector2 lowerOpen;

     //variables that control eye speed
     float timer;
     public float eyeSpeed;

     bool closing; //controls the eye opening/closing animation
     public  bool closed; //used to check if eyes are closed to things can happen

     // Start is called before the first frame update
     void Start()
     {
          //Initialization
          upperOpen = new Vector3(0, 10, 2);
          upperClose = new Vector3(0, 5, 2);

          lowerOpen = new Vector3(0, -10, 2);
          lowerClose = new Vector3(0, -5, 2);

          timer = 0;
     }

     // Update is called once per frame
     void Update()
     {
          timer = Mathf.Clamp(timer, 0f, 1f); //clamps the time between 0 and 1 (needed for lerping)

          //moves the eye lids to the open and close positions
          upperLid.transform.position = Vector3.Lerp(upperOpen, upperClose, timer);
          lowerLid.transform.position = Vector3.Lerp(lowerOpen, lowerClose, timer);

          //Player is  closing eyes
          if (Input.GetKeyDown(KeyCode.Space))
          {
               closing = true;
          }

          //Player is opening eyes
          else if (Input.GetKeyUp(KeyCode.Space))
          {
               closing = false;
          }
          
          //Eye closing animation
          if (closing)
          {
               timer += Time.deltaTime * eyeSpeed;

               //checks to see if the eyes are completely closed
               if (timer >= 1)
               {
                    closed = true;
               }
          }

          //Eye opening animation
          if (!closing)
          {
               timer -= Time.deltaTime * eyeSpeed;

               //checks to see if the eyes are still open (even squinting)
               if (timer < 1)
               {
                    closed = false;
               }
          }       
     }
}
