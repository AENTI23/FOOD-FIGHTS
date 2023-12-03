using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyfallingricecontroller : MonoBehaviour
{
    [SerializeField]
    float Jumpforce = 0;

    [SerializeField]
    float Flightforce = 0;

    [SerializeField]
    float Rotationforce = 0;

    [SerializeField]
    GameObject explode;



    // Start is called before the first frame update
    void Start()
    {
       

            Rigidbody2D RB = GetComponent<Rigidbody2D>();

            Vector2 jumper = new Vector2(-Flightforce, Jumpforce);
            RB.AddForce(jumper);
            RB.AddTorque(Rotationforce * Time.deltaTime);
           
    
    }

    void OnTriggerEnter2D(Collider2D Other)
    {
        if(Other.gameObject.tag == "Player" || Other.gameObject.tag == "Risboll_Tag" || Other.gameObject.tag == "swiftrisbolltag")
        {
          Destroy(this.gameObject);
          
        }
        if(Other.gameObject.tag == "Explode")
        {
            Instantiate(explode, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
           // Debug.Log("E_FALLRISBOLL: Destroyed by explode");
        }
       
    }


    // Update is called once per frame
    void Update()
    {

        if(transform.position.y < -5.5)
      {
        Destroy(this.gameObject);

      }
    }
}
