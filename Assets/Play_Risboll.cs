using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Realrisboll : MonoBehaviour
{

  public bool damagecheck = false;

    [SerializeField]
    float Jumpforce = 0;

    [SerializeField]
    float Flightforce = 0;

    [SerializeField]
    GameObject slash;

    [SerializeField]
    GameObject Explode;

    Transform MeTransform;


    


    // Start is called before the first frame update
    void Start()
    {
        MeTransform = GetComponent<Transform>();

       
            Rigidbody2D RB = GetComponent<Rigidbody2D>();

            Vector2 jumper = new Vector2(Flightforce, Jumpforce);
            RB.AddForce(jumper);
           
    }

    void OnTriggerEnter2D(Collider2D Other)
    {
      
        if(Other.gameObject.tag == "E_Attack_Tag")
        {
          Instantiate(Explode, transform.position, Quaternion.identity);
          Destroy(this.gameObject);
          //print("player: I SHOULD DIE NOW");
          
        

        }
        if(Other.gameObject.tag == "Evilricetag")
        {
        //  Destroy(this.gameObject);
           Vector3 sizechang = new Vector3(0.001f, 0,0.001f);
                     MeTransform.transform.localScale = sizechang;
        }


// Side och Top tags för fiende, kan användas för att lägga till splash/damage effekt på just de ställen som blivit skadade
// Just nu onödigt och gör dubbel skada (kan lösas med en bool när fiende tar skada)
// Koden gör inget eftersom "Top" och "Side" är avkryssade och inaktiva just nu.
       if(Other.gameObject.tag == "Sidetag")
        {
          Debug.Log("SIDETAGGING!");
        }
        if(Other.gameObject.tag == "Toptag")
       {
          Debug.Log("TOPTAGGING!");
       }
    }
    // Update is called once per frame
    void Update()
    {
      if(damagecheck == true)
      {
        damagecheck = false;
        Destroy(this.gameObject);
        Debug.Log("RISBOLL: Killed by damagecheck");
      }
        if(transform.position.y < -50)
        {
        Destroy(this.gameObject);
        }

      if(transform.position.y < -5.5)
      {
        Destroy(this.gameObject);

      }
    }
}
