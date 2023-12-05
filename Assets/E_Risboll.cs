using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

public class Enemyrisboll : MonoBehaviour
{
  [SerializeField]
  GameObject explode;
    [SerializeField]
    float Jumpforce = 0;

    [SerializeField]
    float Flightforce = 0;

    public bool killbool = false;


    // Start is called before the first frame update
    void Start()
    {
            Rigidbody2D RB = GetComponent<Rigidbody2D>();
            Vector2 jumper = new Vector2(-Flightforce, Jumpforce);
            RB.AddForce(jumper);
    }
    void OnTriggerEnter2D(Collider2D Other)
    {
        if(Other.gameObject.tag == "Risboll_Tag" || Other.gameObject.tag == "Player" || Other.gameObject.tag == "swiftrisbolltag")
        {
        killbool = true;
        }
        if(Other.gameObject.tag == "Explode")
        {
            Instantiate(explode, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            //Debug.Log("E_RISBOLL: Destroyed by explode");
        }

    }


    // Update is called once per frame
    void Update()
    {

        if(transform.position.y < -5.5 || killbool == true)
        {
          Destroy(this.gameObject);
        }
    }
}
