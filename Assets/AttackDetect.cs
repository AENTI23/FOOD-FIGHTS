using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyattackdetect : MonoBehaviour
{
    public float detectpoint = 0f;
    public bool detectbool = false;

   [SerializeField]
   float boolstoptimer = 0;


    void OnTriggerEnter2D(Collider2D Dodgeother)
    {
        if (Dodgeother.gameObject.tag == "Risboll_Tag")
        {
            detectpoint += 1f;
            //Debug.Log(detectpoint);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (detectpoint > 0)
        {
            detectbool = true;
            boolstoptimer += Time.deltaTime;
        }

        if (boolstoptimer > 0.3f)
        {
            detectbool = false;
            boolstoptimer = 0;
            detectpoint = 0;
        }
    }
}
