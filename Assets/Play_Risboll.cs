using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Realrisboll : MonoBehaviour
{

  public bool damagecheck = false;

  EnemyMovementControler EMOVESCRIPT;

    [SerializeField]
    float Jumpforce = 0;

    [SerializeField]
    float Flightforce = 0;

    [SerializeField]
    GameObject slash;

    [SerializeField]
    GameObject Explode;

    Transform MeTransform;

    public bool Debugbool = false;


    


    // Start is called before the first frame update
    void Start()
    {
      
        MeTransform = GetComponent<Transform>();

        EMOVESCRIPT = GameObject.FindWithTag("Evilricetag").GetComponent<EnemyMovementControler>();
            Rigidbody2D RB = GetComponent<Rigidbody2D>();
        if(Debugbool == false)
        {
            Vector2 jumper = new Vector2(Flightforce, Jumpforce);
            RB.AddForce(jumper);
        }
        if(Debugbool == true)
        {

           RB.constraints = RigidbodyConstraints2D.FreezePositionY;
           RB.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void OnTriggerEnter2D(Collider2D Other)
    {
      
        if(Other.gameObject.tag == "E_Attack_Tag" && Debugbool == false)
        {
          Instantiate(Explode, transform.position, Quaternion.identity);
          Destroy(this.gameObject);
          //print("player: I SHOULD DIE NOW");
          EMOVESCRIPT.shutdown = true;
          EMOVESCRIPT.allowshutdown = true;
        

        }
        if(Other.gameObject.tag == "Evilricetag" && Debugbool == false|| Other.gameObject.tag == "E_Attack_Tag" && Debugbool == false)
        {
         //  Vector3 sizechang = new Vector3(0.001f, 0,0.001f);
          //           MeTransform.transform.localScale = sizechang;
          EMOVESCRIPT.shutdown = true;
          EMOVESCRIPT.allowshutdown = true;
        Destroy(this.gameObject);
          
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

      if(damagecheck == true && Debugbool == false)
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
        EMOVESCRIPT.shutdown = true;
        EMOVESCRIPT.allowshutdown = true;
      }
      if(Debugbool == true)
        {
          Rigidbody2D RB = GetComponent<Rigidbody2D>();
           RB.constraints = RigidbodyConstraints2D.FreezePositionY;
           RB.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
