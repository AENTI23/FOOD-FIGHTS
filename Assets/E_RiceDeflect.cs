using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ricegraincontroller : MonoBehaviour
{
[SerializeField]
GameObject explode;
    bool killbool = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D Other)
    {
        if(Other.gameObject.tag == "swiftrisbolltag" || Other.gameObject.tag == "Player" || Other.gameObject.tag == "Risboll_Tag")
        {
            killbool = true;
            Destroy(this.gameObject);
           // print("I SHOULD DIE NOW");
        }
        if(Other.gameObject.tag == "Explode")
        {
            Instantiate(explode, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
          // Debug.Log("DEFLECT: Destroyed by explode");
        }
    }
    // Update is called once per frame
    void Update()
    {
        float grainspeed = 3.5f;

        Vector2 grainmove = new Vector2(grainspeed, 1);
        transform.Translate(-grainmove * grainspeed * Time.deltaTime);

         if(transform.position.x < -15 || killbool == true)
        {
            Destroy(this.gameObject);
        }

    }
}
