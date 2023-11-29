using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ricecorncontroller : MonoBehaviour
{
    // Start is called before the first frame update
[SerializeField]
GameObject explode;
    bool killbool = false;
    
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D Other)
    {
        if(Other.gameObject.tag == "swiftrisbolltag" || Other.gameObject.tag == "Player" || Other.gameObject.tag == "Risboll_Tag")
        {
            killbool = true;
        }
        if(Other.gameObject.tag == "Explode")
        {
            Instantiate(explode, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            //Debug.Log("RISBOLT: Destroyed by explode");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float cornspeed = 3.5f;

        Vector2 grainmove = new Vector2(cornspeed, 0);
        transform.Translate(grainmove * cornspeed * Time.deltaTime);



        if(transform.position.x < -15 || killbool == true)
        {
            Destroy(this.gameObject);
        }
    }
}
