using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyattackdetect : MonoBehaviour
{
    public float detectpoint = 0f;

    public int Swiftpoint = 0;
    public bool detectbool = false;

    public bool transformbool = false;

    public bool Swiftbool = false;

   [SerializeField]
   float boolstoptimer = 0;

   [SerializeField]
   float SwiftBoolStopTimer = 0;


    void OnTriggerEnter2D(Collider2D Dodgeother)
    {
        if (Dodgeother.gameObject.tag == "Risboll_Tag")
        {
            detectpoint += 1f;
            //Debug.Log(detectpoint);
        }
        if(Dodgeother.gameObject.tag == "swiftrisbolltag")
        {
            Swiftpoint += 1;
            print("SwiftDetected");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (detectpoint > 0)
        {
            detectbool = true;
            boolstoptimer += Time.deltaTime;
            transformbool = true;

        }

        if (boolstoptimer > 0.3f)
        {
            detectbool = false;
            boolstoptimer = 0;
            detectpoint = 0;
            transformbool = false;
        }

        if(Swiftpoint == 1)
        {
            Swiftbool = true;
            SwiftBoolStopTimer += Time.deltaTime;
        }
        if (SwiftBoolStopTimer > 0.5f)
        {
            Swiftbool = false;
            SwiftBoolStopTimer = 0;
            Swiftpoint = 0;
        }
    }
}
