using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Riscontroller : MonoBehaviour
{
    public bool React1 = false;

    public bool swiftbool = false;

    [SerializeField]
    float movespeed = 1f;

    [SerializeField]
    float crouchspeed = 0f;


    [SerializeField]
    GameObject Swiftbolt;

    [SerializeField]
    GameObject Risboll;

    [SerializeField]
    GameObject Tracker;

    [SerializeField]
    float shtimer = 0;

    [SerializeField]
    float Swiftshtimer = 0;

    [SerializeField]
    GameObject ricespawn;

// SHIFT timer Bar variabler
[SerializeField]
Slider Shiftbar;

[SerializeField]
float MaximalShiftValue = 0f;

[SerializeField]
float ShiftValueNow = 0f;

[SerializeField]
float ShiftTimer = 0f;

// HP Bar variabler 
    [SerializeField]
    Slider HPbar;

    [SerializeField]

    float maximalhp = 3f;

    [SerializeField]
    float hpnow = 0f;
// Jump variabler
    [SerializeField]
    float Jumpforce; //float för kraften på hoppet.

    [SerializeField]
    LayerMask groundLayer; //Lager specification så att jumpswitch bool bara blir true när den nuddar marken eftersom den har groundlayer

    [SerializeField]
    Transform groundCheck; // Kollar "transform" positioner o sånt på groundcheck objektet.

    [SerializeField]
    float groundRadius = 0.1f; //Bestämmer storleken på jumpswitch bool boxen

    [SerializeField]
    float groundWidth = 0f; //Bestämmer storleken på jumpswitch bool boxen

    bool mayJump = true; //Mayjump blir true ifall man inte klickat på hopp flera gånger

// Animation variabler

Animator animator;





    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //Healthbar värdesättning
        hpnow = maximalhp;
        HPbar.maxValue = maximalhp;
        HPbar.value = maximalhp;
       
    }

    void OnTriggerEnter2D(Collider2D Other)
    {
        if(Other.gameObject.tag == "E_Attack_Tag")
        {
            hpnow -= 1f;
            HPbar.value = hpnow;
            if (hpnow == 0f)
            {
                Destroy(this.gameObject);
            }
        }
        if(Other.gameObject.tag == "Floortag")
        {
            animator.SetBool("IsJumping", false);
        }

    }
 private Vector3 MakeGroundcheckSize() => new Vector3(groundWidth, groundRadius);
    // Update is called once per frame
    void Update()
    {
      //Keybinds
    float bollfire = Input.GetAxisRaw("Fire1");
    float swiftfire = Input.GetAxisRaw("Fire2");
    float movebutton = Input.GetAxisRaw("Horizontal");
    float jumpbutton = Input.GetAxisRaw("Jump");
    float crouch = Input.GetAxisRaw("Crouch");
    
    
     //Rörelse KOD 
     if(animator.GetBool("Crouching") == false)
     {
        Vector2 Movement = new Vector2(movebutton, 0) * movespeed * Time.deltaTime;
        transform.Translate(Movement);
     }
     else if(animator.GetBool("Crouching") == true)
     {

     Vector2 Movement = new Vector2(movebutton, 0) * crouchspeed * Time.deltaTime;
     transform.Translate(Movement);
     }

     if(movebutton == 1)
     {
        animator.SetFloat("Xinput", 1f);
        animator.SetBool("IsWalking", true);
     }
     else if(movebutton == -1)
     {
        animator.SetFloat("Xinput", -1f);
        animator.SetBool("IsWalking", true);
     }
     if(movebutton == 0)
     {
        animator.SetBool("IsWalking", false);
     }
     if(crouch == 1)
     {
        animator.SetBool("Crouching", true);
     }
     if(crouch == 0)
     {
        animator.SetBool("Crouching", false);
     }
     
      
//print(animator.GetBool("IsWalking"));

    

     // if (Movement)
// Jump KOD
 Vector3 size = MakeGroundcheckSize();
bool jumpswitch = Physics2D.OverlapBox(groundCheck.position, size, 0, groundLayer); // en bool som är positiv ifall groundcheck nuddar marken.

        if (jumpbutton > 0 && mayJump == true && jumpswitch)
        {
            Rigidbody2D RB = GetComponent<Rigidbody2D>();
            Vector2 jumper = Vector2.up * Jumpforce;
            RB.AddForce(jumper);
            mayJump = false;
            animator.SetBool("IsJumping", true);
           
         }
        if (jumpbutton == 0)
        {
            mayJump = true;
        }
//Barrier kod.//Byt ut till Collider!
/*
        if (transform.position.x > -1 || transform.position.x < -14)
        {
            transform.Translate(-Movement);
        }
        */
// Risboll attack kod
 shtimer += Time.deltaTime;
if (bollfire > 0 && shtimer > 1)
{
    Instantiate(Risboll, ricespawn.transform.position, Quaternion.identity);
    shtimer = 0;
}
// Swift attack KOD
Swiftshtimer += Time.deltaTime;
        if (swiftfire > 0 && Swiftshtimer > 8f)
        {
            swiftbool = true;
          Instantiate(Swiftbolt, ricespawn.transform.position, Quaternion.identity);
            Swiftshtimer = 0;
        }

        if (swiftbool == true)
        {
        //ShiftTimerBar värdesättning
        ShiftValueNow = MaximalShiftValue;
        Shiftbar.maxValue = MaximalShiftValue;
        Shiftbar.value = MaximalShiftValue;
        // 
        ShiftTimer += Time.deltaTime;
        ShiftValueNow -= ShiftTimer;
        Shiftbar.value = ShiftValueNow;
        }
        if(ShiftValueNow < 0f)
        {
            Shiftbar.value += 10f * Time.deltaTime;
            swiftbool = false;
            ShiftTimer = 0f;
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 size = new Vector3(groundWidth, groundRadius);
        Gizmos.DrawWireCube(groundCheck.position, size);
    }
}
