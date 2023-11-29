using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
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
     float DodgeStartPos = 0f;

     [SerializeField]
     float DodgeStopFinal = 0f;

     bool StartPosGotten = false;

     bool touchedwall = false;

     [SerializeField]
     float DodgeStopLimit = 0;

     [SerializeField]
     float CrouchDodgeForce = 0f;

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

     


void OnTriggerEnter2D(Collider2D Other)
{
    if(Other.gameObject.tag == "Walltag" && StartPosGotten == true)
    {
        touchedwall = true;

    }

}
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
  animcontrol.SetBool("Jumping", false);
  animcontrol.SetBool("Walking", true);
}
else
{
   animcontrol.SetBool("Jumping", true);
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
    float DodgeSave1 = 0f;
    float dodgecurrentpos;
 dodgeprio = true;
 Vector2 DodgeV = Vector2.right * DodgeForce;
 if(StartPosGotten == false)
 {
    DodgeStartPos = body.transform.position.x;
    DodgeSave1 += DodgeStartPos;
    DodgeSave1 += DodgeStopLimit;
     DodgeStopFinal += DodgeSave1;
    StartPosGotten = true;
    print("Start position" + DodgeStartPos);
    print("Stop Position" + DodgeStopFinal);
 }
 body.AddForce(DodgeV);
 dodgecurrentpos = body.transform.position.x;
if (StartPosGotten == true)
{
 print("Current pos" + transform.position.x);
 if(dodgecurrentpos > DodgeStopFinal && patrolswitch == false || touchedwall == true)//Dodge to the left
{
    dodgeprio = false;
    DodgeStartPos = 0;
    body.constraints = RigidbodyConstraints2D.FreezePositionX;
}
if(dodgecurrentpos > DodgeStopFinal && patrolswitch == true || touchedwall == true) // Dodge to the right 
{
    dodgeprio = false;
    DodgeStartPos = 0;
    body.constraints = RigidbodyConstraints2D.FreezePositionX;
}
}
if(dodgeprio == false)
{
    body.constraints = RigidbodyConstraints2D.None;
    StartPosGotten = false;
    touchedwall = false;
    dodgecurrentpos = 0;
    DodgeStopFinal = 0;
    DodgeSave1 = 0;
    ReactNumber = 0;
}
}


//Crouch Dodge
if(ReactNumber == 2)
{
    animcontrol.SetBool("Crouch", true);
    dodgeprio = true;
    Vector2 CrouchDodgeL = Vector2.left * CrouchDodgeForce;
    Vector2 CrouchDodgeR = Vector2.right * CrouchDodgeForce;

    if(patrolswitch == false)
    {
     body.AddForce(CrouchDodgeR);
     if(body.velocity.x > 7.5f)
     {
        dodgeprio = false;
        ReactNumber = 0;
        animcontrol.SetBool("Crouch", false);
     }
    }
    if(patrolswitch == true)
    {
        print("Moving left");
     body.AddForce(CrouchDodgeL);
     if(body.velocity.x > 7.5f)
     {
        dodgeprio = false;
        ReactNumber = 0;
        animcontrol.SetBool("Crouch", false);
     }

    }

}

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
