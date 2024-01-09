using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMovementControler : MonoBehaviour
{

    [SerializeField]
    GameObject gun;

    [SerializeField]
    GameObject ricedeflect;

    public bool shootokay = true;
    public bool patrolswitch = false; // Bool för att byta mellan riktning i patrol rörelsen (ifall den är FALSE röra sig till höger, ifall TRUE rör sig till vänster)

    [SerializeField]
    float normalmovespeed = 0f; // Hastigheten av den vanliga rörelsen.

    // Variabler och bools för rörelser reaktioner
TrackingScript detectTwoScript; // kod för att få tag på close attack detect boolen

enemyattackdetect detectscript; // kod för att få tag på transform boolen

public bool RandomNumberLock = true; // Bool för att låsa reaktions nummer randomiseringen så de inte sker flera rörelse reaktioner.

public bool Lockgrounddetect = false; // bool för att låsa ground detect så den triggar jump för mycket

    [SerializeField] // Float som får en randomiserad siffra som avgör vilken reaktion som ska se.
    float ReactNumber = 0; 
    [SerializeField] // Float som fungerar som en timer på hur länge en reaktion ska ske, en rörelse sker under en viss tid.
    float ReactionTimer = 0;

    [SerializeField]
    float jumptimer = 0;

    
     public bool dodgeprio = false; // Bool så att dodge får prioritet och kan röra sig utan att krocka med patrol rörelse// ifall den är FALSE så rör sig patrol som vanligt

     bool DodgeReactPrio = false; // Bool som finns så att dodgereaktionen kan få prioritering över crouch reaktionen

     [SerializeField]
     Rigidbody2D body;

     [SerializeField]
     float JumpForce = 0f;

     [SerializeField]
     float DodgeForce = 0f;

     [SerializeField]
     bool crouch = false;

     [SerializeField]
     float DodgeStartPos = 0f;

     [SerializeField]
     float DodgeStopFinal = 0f;

     bool StartPosGotten = false;

    public bool touchedwall = false;

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



     [SerializeField]
     GameObject MePrefab;

     Transform RisBollPrefab;

     Transform SwiftBoltPrefab;
     
[SerializeField]
float temptimer;

[SerializeField]
public bool ActivateTracking = false;

public bool ActivateSwiftTracking = false;


public bool activatecrouch = false;
    public bool activatedodge = false;

    public bool ActivateJump = false;


    public bool dodgemethodprio = false;

    public bool LockJumpForce = false;

    public bool LockJumpForce2 = false;

    bool allowjump;

    public bool LockWalk = false;

    public Vector2 JumpV;

    public float jumpforcetimer = 0;

  private Vector3 GroundCheckSize() => new Vector3(groundWidth, groundRadius);



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
        JumpV = Vector2.up * JumpForce;
        animcontrol = GetComponent<Animator>();
        // Få tag på script komponenten för att kunna upptäcka nära attacker.
        detectTwoScript = GameObject.FindWithTag("Attack_Detect_Two").GetComponent<TrackingScript>(); 
        detectscript = GameObject.FindWithTag("Attack_Detect_One").GetComponent<enemyattackdetect>(); 
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 size = new Vector3(groundWidth, groundRadius);
        Gizmos.DrawWireCube(groundCheck.position, size);
    }

   public void CrouchMethod()

    {
        GameObject aRisboll = GameObject.FindWithTag("Risboll_Tag");
    if (aRisboll == null) 
    {
     animcontrol.SetBool("Crouch", false);
     LockWalk = false;
     ReactionTimer = 0;
     activatecrouch= false;
     shootokay = true;
    crouch = false;
    ActivateTracking = false;
    return;
    }

     if(DodgeReactPrio == false)
     {
        if(aRisboll.activeInHierarchy)
     {
         RisBollPrefab = GameObject.FindWithTag("Risboll_Tag").GetComponent<Transform>();
     }
float yattack = RisBollPrefab.transform.position.y;
float xattack = RisBollPrefab.transform.position.x;
float yenemy = MePrefab.transform.position.y;
float xenemy = MePrefab.transform.position.x;
Vector2 YattackPos = new Vector2(yattack, yattack);
Vector2 YenemyPos = new Vector2(yenemy, yenemy);
Vector2 XattackPos = new Vector2(xattack, xattack);
Vector2 XenemyPos = new Vector2(xenemy, xenemy);

float XDistance = Vector2.Distance(XenemyPos, XattackPos);
float YDistance = Vector2.Distance(YenemyPos, YattackPos);

if (YDistance < 5 && XDistance < 3 && DodgeReactPrio == false)
{
 activatecrouch = true;
}
     

if(activatecrouch == true && DodgeReactPrio == false)
{
    crouch = true;
    animcontrol.SetBool("Crouch", true);
    LockWalk = true;
  ReactionTimer += Time.deltaTime;
  if(transform.position.x > 11.2f && shootokay == true)
  {
  shootokay = false;
  Instantiate(ricedeflect, gun.transform.position, Quaternion.identity);
  //print("CORNERED SHOOT SHOOT");
  }
}
 if (ReactionTimer > 0.4 || DodgeReactPrio == true)
 {
    animcontrol.SetBool("Crouch", false);
     LockWalk = false;
     ReactionTimer = 0;
     activatecrouch= false;
     shootokay = true;
    crouch = false;
    ActivateTracking = false;
     }
}
    }

public void JumpMethod()
{
       //REACT 2 (Hopp)
    GameObject swift = GameObject.FindWithTag("swiftrisbolltag");
    if (swift == null && ActivateSwiftTracking == true) 
    {
         DodgeReactPrio = false;
        ActivateSwiftTracking = false;
        temporarypoint = 0;
        LockJumpForce = false;
    return;
    }

    if(swift.activeInHierarchy)
    {
 SwiftBoltPrefab = GameObject.FindWithTag("swiftrisbolltag").GetComponent<Transform>();
    }
    float ySwift = SwiftBoltPrefab.transform.position.y;
    float xSwift = SwiftBoltPrefab.transform.position.x;
   float yenemy = MePrefab.transform.position.y;
   float xenemy = MePrefab.transform.position.x;

      Vector2 YSwiftPos = new Vector2(ySwift, ySwift);
      Vector2 YenemyPos = new Vector2(yenemy, yenemy);
      Vector2 XSwiftPos = new Vector2(xSwift, xSwift);
      Vector2 XenemyPos = new Vector2(xenemy, xenemy);

    float XDistanceSwift = Vector2.Distance(XenemyPos, XSwiftPos);
    float YDistanceSwift = Vector2.Distance(YenemyPos, YSwiftPos);


       if(YDistanceSwift < 3.3f && XDistanceSwift < 7.5f)
       {
        ActivateJump = true;
       }
        if(ActivateJump == true && allowjump == true)
        {
           
            if(ActivateTracking == true)
            {
                DodgeReactPrio = true;
            }
            temporarypoint += 1f;
        }
        if (temporarypoint == 1f)
        {
             if(patrolswitch == false)
            {
                patrolswitch = true;
                animcontrol.SetFloat("SideSwitchFloat", -1);
            }
            if(allowjump == true && LockJumpForce == false)
            {
           // body.AddForce(JumpV);
           BodyForce();
            LockJumpForce = true;
            }
            ActivateJump = false;
            temporarypoint = 2;
        }
        if(temporarypoint == 2)
        {
            DodgeReactPrio = false;
            ActivateSwiftTracking = false;
            temporarypoint = 0;
            LockJumpForce = false;
        }
       
        
}

public void BodyForce() // Jumpforcen behövde egen metod för att undvika super hopp bug
{
   
   if(LockJumpForce2 == false)
   {
    body.AddForce(JumpV);
    print("Jumpcounting!");
    LockJumpForce2 = true;
    jumpforcetimer = 0;
return;
   }

    return;
}

    void Update()
    {  

         jumpforcetimer += Time.deltaTime; // tillhör bodyforce metoden
        if(jumpforcetimer > 0.14f)
        {
            LockJumpForce2 = false;
        }// timer och sånt för att hjälpa låsa jump reaktionens force.

if (detectscript.Swiftbool == true)
{
    ActivateSwiftTracking = true;
}

if(ActivateSwiftTracking == true)
{
    JumpMethod();
}


        if(detectscript.transformbool == true)
        {
            ActivateTracking = true;
        }
        if(ActivateTracking == true)
        {
        CrouchMethod();
        }



        // Rörelse variabeler och vectorer
    Vector2 normalmovement = new Vector2(normalmovespeed, 0);
    Vector3 size = GroundCheckSize();
    allowjump = allowjump = Physics2D.OverlapBox(groundCheck.position, size, 0, groundLayer);
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
    if (dodgeprio == false && patrolswitch == false && LockWalk == false)//Rörelse till höger
    {
        transform.Translate(normalmovement * normalmovespeed * Time.deltaTime);
    }
    else if (dodgeprio == false && patrolswitch == true && LockWalk == false)// Rörelse till vänster
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





    if (detectTwoScript.closedetectbool == true && RandomNumberLock == true && transform.position.x < 10.5f )
    {

        if(transform.position.x < 10.5f)
        {
        activatedodge = true;
        }
        if(transform.position.x > 10.5f)
        {
         Instantiate(ricedeflect, gun.transform.position, Quaternion.identity);
        }
        RandomNumberLock = false;
    }

    if (detectTwoScript.closedetectbool == false)
    {
        RandomNumberLock = true;
    }
    if(activatedodge == false)
    {
        animcontrol.SetBool("Dodge", false);
    }

if(touchedwall == true)// Stänger av dodge reaktionen ifall fienden nuddar vägg
{
    dodgeprio = false;
}
if(activatedodge == true)
{
Vector2 DodgeV = Vector2.right * DodgeForce;
float DodgeSave1 = 0f;
float dodgecurrentpos;
 dodgeprio = true;
 DodgeReactPrio = true;
if(StartPosGotten == false) //delen som hämtar position där dodge startade och kombinerar det med hur många meter dodge ska röra sig (Dodgestoplimit) och lägger ihop de värdena i Dodgestopfinal
 {
    DodgeStartPos = body.transform.position.x;
    DodgeSave1 += DodgeStartPos;
    DodgeSave1 += DodgeStopLimit;
     DodgeStopFinal += DodgeSave1;
    StartPosGotten = true;
 }

 body.AddForce(DodgeV); // de som gör så att den rör sig
 dodgecurrentpos = body.transform.position.x;
 animcontrol.SetBool("Dodge", true);
if (StartPosGotten == true)
{
 if(dodgecurrentpos > DodgeStopFinal || touchedwall == true)
{
    dodgeprio = false;
    body.constraints = RigidbodyConstraints2D.FreezePositionX;
}
}
if(dodgeprio == false)
{
    body.constraints = RigidbodyConstraints2D.None;
    body.constraints = RigidbodyConstraints2D.FreezeRotation;
    StartPosGotten = false;
    DodgeStopFinal = 0;
    DodgeStartPos = 0;
    animcontrol.SetBool("Dodge", false);
    DodgeReactPrio = false;
    touchedwall = false;
    activatedodge = false;
}
}
 }


}