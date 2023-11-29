using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyMovementControler : MonoBehaviour
{
    public bool patrolswitch = false; // Bool för att byta mellan riktning i patrol rörelsen (ifall den är FALSE röra sig till höger, ifall TRUE rör sig till vänster)

    [SerializeField]
    float normalmovespeed = 0f; // Hastigheten av den vanliga rörelsen.

    // Variabler och bools för rörelser reaktioner
TrackingScript detectTwoScript; // kod för att få tag på close attack detect boolen

TrackingEnemy2 detectGroundScript; //kod för att få tag på ground react boolen

public bool RandomNumberLock = true; // Bool för att låsa reaktions nummer randomiseringen så de inte sker flera rörelse reaktioner.

public bool Lockgrounddetect = false; // bool för att låsa ground detect så den triggar jump för mycket

    [SerializeField] // Float som får en randomiserad siffra som avgör vilken reaktion som ska se.
    float ReactNumber = 0; 
    [SerializeField] // Float som fungerar som en timer på hur länge en reaktion ska ske, en rörelse sker under en viss tid.
    float ReactionTimer = 0;



 public bool dodgeswitch = false; // Bool som agerar som en switch för DODGE react så att de sker en gång och inte flera gånger
     public bool dodgeprio = false; // Bool så att dodge får prioritet och kan röra sig utan att krocka med patrol rörelse// ifall den är FALSE så rör sig patrol som vanligt
     public bool dodgestop = false; //Bool så att dodge har en barrier (ifall bool FALSE så kan dodge fortsätta röra sig och ifall den är TRUE så stoppar dodge)

     [SerializeField]
     Rigidbody2D body;

     [SerializeField]
     float JumpForce = 0f;

     [SerializeField]
     float DodgeForce = 0f;

     [SerializeField]
     Transform groundCheck;

     [SerializeField]
     LayerMask groundLayer;

     [SerializeField]
    float groundRadius = 0.1f; //Bestämmer storleken på jumpswitch bool boxen

    [SerializeField]
    float groundWidth = 0f; //Bestämmer storleken på jumpswitch bool boxen

     [SerializeField]
     float temporarypoint = 0;

     Animator animcontrol;



    // Start is called before the first frame update
    void Start()
    {
        animcontrol = GetComponent<Animator>();
        // Få tag på script komponenten för att kunna upptäcka nära attacker.
        detectTwoScript = GameObject.FindWithTag("Attack_Detect_Two").GetComponent<TrackingScript>(); 
        detectGroundScript = GameObject.FindWithTag("Attack_Detect_Three").GetComponent<TrackingEnemy2>();
    }

    // Update is called once per frame

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 size = new Vector3(groundWidth, groundRadius);
        Gizmos.DrawWireCube(groundCheck.position, size);
    }

    private Vector3 GroundCheckSize() => new Vector3(groundWidth, groundRadius);
    void Update()
    {
        // Rörelse variabeler och vectorer
    Vector2 normalmovement = new Vector2(normalmovespeed, 0);
    Vector3 size = GroundCheckSize();
    bool allowjump = Physics2D.OverlapBox(groundCheck.position, size, 0, groundLayer);
if (allowjump == true)
{
    animcontrol.SetBool("Walking", true);
}
else
{
   animcontrol.SetBool("Walking", false);
}
    //Patrol rörelse KOD (Rörelse fram och tillbaka)
    if (dodgeprio == false && patrolswitch == false)//Rörelse till höger
    {
        transform.Translate(normalmovement * normalmovespeed * Time.deltaTime);
    }
    else if (dodgeprio == false && patrolswitch == true)// Rörelse till vänster
    {
        transform.Translate(-normalmovement * normalmovespeed * Time.deltaTime);
    }
    // Kod så patrol byter riktning när fienden kommer till punkterna//
        if (transform.position.x > 14f)
        {
            patrolswitch = true;
            animcontrol.SetFloat("SideSwitchFloat", -1);

        }
        if (transform.position.x < 1f)
        {
            patrolswitch = false;
            animcontrol.SetFloat("SideSwitchFloat", 1);
        }


    // Float som innehåller de möjliga siffrorna för reaktioner. (randomizern)
     float numberrandom = Random.Range(1, 2);

     //KOD SOM BESTÄMMER VAD FÖR TYP AV REAKTION SOM SKA HÄNDA
    if (detectTwoScript.closedetectbool == true && RandomNumberLock == true )
    {
        ReactNumber += numberrandom;
        RandomNumberLock = false;
    }
    if (detectTwoScript.closedetectbool == false)
    {
        RandomNumberLock = true;
    }

if(ReactNumber == 1)
{
 Vector2 DodgeV = Vector2.right * DodgeForce;
 body.AddForce(DodgeV);
 dodgeprio = true;
 //print("move force" + body.velocity.x);
 if(body.velocity.x > 5 && patrolswitch == false)//Right dodge
{
    ReactNumber = 0;
    dodgeprio = false;
}
if(body.velocity.x > 12.5f && patrolswitch == true) // Left dodge
{
    ReactNumber = 0;
    dodgeprio = false;
}
}




    /* REACT 1 (Dodge) KOD

      bool dodgeactive = false;

     bool keepleft = false;

     bool keepright = false;

        float DodgeSpeed = 3.5f;

        Vector2 DodgeMove = new Vector2(DodgeSpeed, 0);       

       if (ReactNumber == 1f)
       {
        dodgeprio = true;
        dodgeactive = true;

       
        if (dodgestop == false && transform.position.x > 6 && keepright == false) //Dodgerörelsen // ifall den är på höger sida rör den sig mot mitten
        {
        transform.Translate(-DodgeMove * DodgeSpeed * Time.deltaTime); 
        keepleft = true;
        }
        if (dodgestop == false && transform.position.x < 7 && keepleft == false) //Dodgerörelsen // ifall den är på vänster sida rör den sig mot kanten
        {
        transform.Translate(DodgeMove * DodgeSpeed * Time.deltaTime); 
        keepright = true;
        }
        if (transform.position.x > 14f)//Stoppar dodge rörelsen ifall den rör barrieren.
        {
           dodgestop = true;
        }
       }
       if (dodgeactive == true)
       {
        ReactionTimer += Time.deltaTime;
        if (ReactionTimer > 0.5f)
        {
            ReactNumber = 0;
            ReactionTimer = 0;
           dodgeprio = false;
           dodgestop = false;
           dodgeactive = false;
           keepleft = false;
           keepright = false;
        }
       }
*/

       

        //REACT 2 (Hopp)



    

        if(detectGroundScript.grounddetectbool == true && Lockgrounddetect == false && allowjump == true)
        {
            Lockgrounddetect = true;
            temporarypoint += 1f;
        }
        if (temporarypoint == 1f)
        {
            temporarypoint = 0;
            Vector2 JumpV = Vector2.up * JumpForce;

            body.AddForce(JumpV);
        }
     if (Lockgrounddetect == true)
     {
            ReactionTimer += Time.deltaTime;
            if (ReactionTimer > 0.5)
            {
                ReactionTimer = 0;
                Lockgrounddetect = false;
                
            }

     }
        
    }
}
