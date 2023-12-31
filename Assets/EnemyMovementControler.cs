using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using JetBrains.Annotations;
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

     Transform RisBollDodgePrefab;

     Transform SwiftBoltPrefab;
     
[SerializeField]
float temptimer;

[SerializeField]
public bool ActivateTracking = false;

public bool ActivateSwiftTracking = false;

public bool ActivateDodgeTracking = false;

public bool activatecrouch = false;
    public bool activatedodge = false;

    public bool ActivateJump = false;

    public bool shutdown = false;

    public bool allowshutdown = false;

    public bool jumpshutdown = false;

    public bool allowjumpshutdown = false;

    public bool dodgemethodprio = false;

    bool allowjump;

    public bool LockWalk = false;

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
     if(dodgemethodprio == false)
     {
        if(GameObject.FindWithTag("Risboll_Tag").activeInHierarchy && shutdown == false)  
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

if (YDistance < 5 && XDistance < 3 && dodgeprio == false)
{
 activatecrouch = true;
}
     

if(activatecrouch == true && allowshutdown == false)
{
    crouch = true;
    allowshutdown = false;
    animcontrol.SetBool("Crouch", true);
    dodgeprio = true;
  ReactionTimer += Time.deltaTime;
  if(transform.position.x > 11.2f && shootokay == true)
  {
  shootokay = false;
  Instantiate(ricedeflect, gun.transform.position, Quaternion.identity);
  //print("CORNERED SHOOT SHOOT");
  }
}
 if (ReactionTimer > 0.5 || shutdown == true)
 {
    animcontrol.SetBool("Crouch", false);
     dodgeprio = false;
     ReactionTimer = 0;
     activatecrouch= false;
     shootokay = true;
    ActivateTracking = false;
    shutdown = false;
    crouch = false;
   // print ("timer ended it");
     }
}
    }
public void DodgeMethod() // DODGE METHOD GJORt SÅ ATT CROUCH buggar typ och dodge funkar inte alls. 
{
    dodgemethodprio = true;
    print("Dodge method active");

    if(GameObject.FindWithTag("Risboll_Tag").activeInHierarchy) 
     {
         RisBollDodgePrefab = GameObject.FindWithTag("Risboll_Tag").GetComponent<Transform>();
     }

float yattackdodge = RisBollDodgePrefab.transform.position.y;
float xattackdodge = RisBollDodgePrefab.transform.position.x;
float yenemydodge = MePrefab.transform.position.y;
float xenemydodge = MePrefab.transform.position.x;

      Vector2 YattackDPos = new Vector2(yattackdodge, yattackdodge);

      Vector2 XattackDPos = new Vector2(xattackdodge, xattackdodge);

      Vector2 YenemyDPos = new Vector2(yenemydodge, yenemydodge);

      Vector2 XenemyDPos = new Vector2(xenemydodge, xenemydodge);


    float XDistanceDodge = Vector2.Distance(XenemyDPos, XattackDPos);

    float YDistanceDodge = Vector2.Distance(YenemyDPos, YattackDPos);
    print("DODGE" + XDistanceDodge);

    if(YDistanceDodge < 2.3f && XDistanceDodge < 9.2f )// && crouch == false)
    {
        activatedodge = true;
        print("DODGE SHOULD GO");
    }
    //print("Y__DISTANCE" + YDistanceDodge);

if(activatedodge == true)
{
    float DodgeSave1 = 0f;
    float dodgecurrentpos;
 dodgeprio = true;
 Vector2 DodgeV = Vector2.right * DodgeForce;
 if(activatedodge == true)
 {
    DodgeStartPos = body.transform.position.x;
    DodgeSave1 += DodgeStartPos;
    DodgeSave1 += DodgeStopLimit;
     DodgeStopFinal += DodgeSave1;
    StartPosGotten = true;
 }
 body.AddForce(DodgeV);
 dodgecurrentpos = body.transform.position.x;
 animcontrol.SetBool("Dodge", true);
if (StartPosGotten == true)
{
// print("Current pos" + transform.position.x);
 if(dodgecurrentpos > DodgeStopFinal && patrolswitch == false || touchedwall == true)
{
    dodgeprio = false;
    body.constraints = RigidbodyConstraints2D.FreezePositionX;
}
}
}
if(dodgeprio == false || crouch == true)
{
    body.constraints = RigidbodyConstraints2D.None;
    body.constraints = RigidbodyConstraints2D.FreezeRotation;
    StartPosGotten = false;
    touchedwall = false;
    DodgeStopFinal = 0;
    ReactNumber = 0;
    DodgeStartPos = 0;
    animcontrol.SetBool("Dodge", false);
    dodgeprio = false;
    activatedodge = false;
    ActivateDodgeTracking = false;
    dodgemethodprio = false;
}
}

public void dodgecode()
{

    activatedodge = true;
    
if(activatedodge == true)
{
    float DodgeSave1 = 0f;
    float dodgecurrentpos;
 dodgeprio = true;
 Vector2 DodgeV = Vector2.right * DodgeForce;
 if(activatedodge == true)
 {
    DodgeStartPos = body.transform.position.x;
    DodgeSave1 += DodgeStartPos;
    DodgeSave1 += DodgeStopLimit;
     DodgeStopFinal += DodgeSave1;
    StartPosGotten = true;
 }
 body.AddForce(DodgeV);
 dodgecurrentpos = body.transform.position.x;
 animcontrol.SetBool("Dodge", true);
if (StartPosGotten == true)
{
 if(dodgecurrentpos > DodgeStopFinal && patrolswitch == false || touchedwall == true)
{
    dodgeprio = false;
    body.constraints = RigidbodyConstraints2D.FreezePositionX;
}
}
}
if(dodgeprio == false || crouch == true)
{
    body.constraints = RigidbodyConstraints2D.None;
    body.constraints = RigidbodyConstraints2D.FreezeRotation;
    StartPosGotten = false;
    touchedwall = false;
    DodgeStopFinal = 0;
    ReactNumber = 0;
    DodgeStartPos = 0;
    animcontrol.SetBool("Dodge", false);
    dodgeprio = false;
    activatedodge = false;
    ActivateDodgeTracking = false;
    dodgemethodprio = false;
    shutdown = true;
}
}
public void CrouchCode()
{

if (dodgeprio == false)
{

 activatecrouch = true;
}
 //print ("Close.... activating!");

     if(dodgemethodprio == false)
 {  
if(activatecrouch == true && allowshutdown == false)
{
    crouch = true;
    allowshutdown = false;
    animcontrol.SetBool("Crouch", true);
    dodgeprio = true;
  ReactionTimer += Time.deltaTime;
  if(transform.position.x > 11.2f && shootokay == true) //Cornered shoot
  {
  shootokay = false;
  Instantiate(ricedeflect, gun.transform.position, Quaternion.identity);
  }
}
 if (ReactionTimer > 0.5 || shutdown == true)
 {
    animcontrol.SetBool("Crouch", false);
     dodgeprio = false;
     ReactionTimer = 0;
     activatecrouch= false;
     shootokay = true;
    ActivateTracking = false;
    shutdown = false;
    crouch = false;
   // print ("timer ended it");
     }
}
}

public void trackmethod()
{

        if(GameObject.FindWithTag("Risboll_Tag").activeInHierarchy && shutdown == false)  
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

if (YDistance < 5 && XDistance < 3 && dodgeprio == false)
{
CrouchCode();
}
if (YDistance < 2f && XDistance < 4f)
{
    dodgecode();
}
}
public void JumpMethod()
{
       //REACT 2 (Hopp)
   
    if(GameObject.FindWithTag("swiftrisbolltag").activeInHierarchy && jumpshutdown == false)
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

    
print(XDistanceSwift);

       if(YDistanceSwift < 3.3f && XDistanceSwift < 8.5f)
       {
        ActivateJump = true;
       }
        if(ActivateJump == true && allowjump == true && allowjumpshutdown == false)
        {
           
            if(ActivateTracking == true)
            {
                shutdown = true;
                allowshutdown = true;
            }
            temporarypoint += 1f;
        }
        if (temporarypoint == 1f)
        {
             if(patrolswitch == false)
            {
                dodgeprio = true;
            }
            temporarypoint = 2;
            Vector2 JumpV = Vector2.up * JumpForce;

            body.AddForce(JumpV);
            ActivateJump = false;
            ActivateSwiftTracking = false;
        }
        if(temporarypoint == 2 || jumpshutdown == true)
        {
            dodgeprio = false;
            ActivateSwiftTracking = false;
            jumpshutdown = true;
            allowjumpshutdown = true;
        }

     
     

}

    void Update()
    {  
        if(shutdown == true && allowshutdown == true)
        {
            animcontrol.SetBool("Crouch", false);
     dodgeprio = false;
     ReactionTimer = 0;
     activatecrouch = false;
     shootokay = true;
    ActivateTracking = false;
    shutdown = false;
    allowshutdown = false;
    crouch = false;
        }
        
        if(ActivateTracking == true && shutdown == false && allowshutdown == false && dodgemethodprio == false)
        {
            CrouchMethod();
        }
        if(detectscript.transformbool == true)
        {
            ActivateTracking = true;
        }

if (detectscript.Swiftbool == true)
{
    ActivateSwiftTracking = true;
}
if(ActivateSwiftTracking == true && jumpshutdown == false && allowjumpshutdown == false)
{
    JumpMethod();
}

 if(jumpshutdown == true || allowjumpshutdown == true)
        {
     dodgeprio = false;
     ActivateSwiftTracking = false;
    jumpshutdown = false;
    allowjumpshutdown = false;
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
     //KOD SOM BESTÄMMER VAD FÖR TYP AV REAKTION SOM SKA HÄNDA
    // Float som innehåller de möjliga siffrorna för reaktioner. (randomizern)
     float numberrandom = Random.Range(1, 2);

    if (detectTwoScript.closedetectbool == true && RandomNumberLock == true && transform.position.x < 10.5f )
    {
        ReactNumber += numberrandom;
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

if(touchedwall == true)
{
    dodgeprio = false;
}
if(activatedodge == true)
{
 dodgeprio = true;
    float DodgeSave1 = 0f;
    float dodgecurrentpos;
 Vector2 DodgeV = Vector2.right * DodgeForce;
 dodgemethodprio = true;
 if(StartPosGotten == false)
 {
    DodgeStartPos = body.transform.position.x;
    DodgeSave1 += DodgeStartPos;
    DodgeSave1 += DodgeStopLimit;
     DodgeStopFinal += DodgeSave1;
    StartPosGotten = true;
 }
 body.AddForce(DodgeV);
 dodgecurrentpos = body.transform.position.x;
 animcontrol.SetBool("Dodge", true);
if (StartPosGotten == true)
{
 if(dodgecurrentpos > DodgeStopFinal || touchedwall == true)//Dodge to the left
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
    touchedwall = false;
    DodgeStopFinal = 0;
    DodgeStartPos = 0;
    animcontrol.SetBool("Dodge", false);
    dodgeprio = false;
    activatedodge = false;
    dodgemethodprio = false;
}
}
 }

}
