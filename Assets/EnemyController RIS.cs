using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Risenemycontrol : MonoBehaviour
{

 


     enemyattackdetect detectScript; // kod för att få tag på attack detect boolen

     Realrisboll risbollscript; // kod för att få tag på damagecheck boolen

     public bool reactnumberlock = true; // Bool för att låsa reactnumber randomiseringen så de inte sker flera reaktioner

     public bool React1switch = false; // Bool som agerar som en switch för FALLRISBOLT react så att de sker en gång och inte flera gånger
     public bool React2switch = false; // Bool som agerar som en switch för RISBOLT react så att de sker en gång och inte flera gånger
     public bool React3switch = false; // Bool som agerar som en switch för RICEGRAINSDEFLECT react så att de sker en gång och inte flera gånger
     public bool React4switch = false; // Bool som agerar som en switch för RISBOLL react så att de sker en gång och inte flera gånger
    

    [SerializeField]
    GameObject RiceBolt;

    [SerializeField]
    GameObject FallRisBolt;

    [SerializeField]
    GameObject RiceDeflect;

    [SerializeField]
    GameObject RiceDeflectV2;

    [SerializeField]
    GameObject Risboll;

    [SerializeField]
    GameObject AttackSpawn;



    [SerializeField]
    float Reactnumber = 0;

    [SerializeField]
    float decidertimer = 0;

// Health bar values.
    [SerializeField]
    Slider HP;

    [SerializeField]
    int maxhp = 0;
    [SerializeField]
    int currenthp = 0;



    
    
    // Start is called before the first frame update
    void Start()
    {
        // Healthbar värdesättning
        currenthp = maxhp;
        HP.maxValue = maxhp;
        HP.value = maxhp;

        //Få tag på script komponenten för att kunna detecta attacker.
       detectScript = GameObject.FindWithTag("Attack_Detect_One").GetComponent<enemyattackdetect>(); 
    }


    void OnTriggerEnter2D(Collider2D Other)
    {
        //KOD för healthbar (IFall de objekt med tagsen collidar med enemy så tar den skada )
        if(Other.gameObject.tag == "swiftrisbolltag")
        {
            currenthp -= 1;
            HP.value = currenthp;
            if (currenthp == 0 )
            {
                Destroy(this.gameObject);
            }
        }
        if(Other.gameObject.tag == "Risboll_Tag")
        {
            risbollscript.damagecheck = true;
            currenthp -= 1;
            HP.value = currenthp;
            Debug.Log("ENEMYHP" + currenthp);
            if (currenthp == 0 )
            {
                Destroy(this.gameObject);
            }
            
        }

        
    }

    // Update is called once per frame
    void Update()
    {

     // Float som innehåller de möjliga siffrorna för reaktioner. (randomizern)
     float randomnumb = Random.Range(1, 7); // Reaktioner counterattack 
     // just nu finns de fyra olika reaktioner men det kan bli 7 olika siffror, det är för att minska antalet counterattacks
     // det blir då chans att en sker blir mindre.
     

// KOD så att ifall det blir en av det 4 siffror som inte leder till en reaktion så nollställs reaktions siffran
if (Reactnumber > 4)
{
    Reactnumber = 0;
}

    // KOD SOM BESTÄMMER VAD FÖR TYP AV COUNTERATTACK REACT SOM SKA HÄNDA
    if (detectScript.detectbool == true && reactnumberlock == true)
    {
        Reactnumber += randomnumb;
        reactnumberlock = false;
        risbollscript = GameObject.FindWithTag("Risboll_Tag").GetComponent<Realrisboll>(); //Få tag på script komponenten i Player risboll för att låta den göra skada innan den raderas
        //Placerade här eftersom den kan ba hämta komponenten när risbollen finns och här upptäcks det ifall risbollen finns.
    }
    if (detectScript.detectbool == false) 
    {
        reactnumberlock = true;
    }



    // REACT 1 (Rice_Fall_Bolt) KOD
        if (Reactnumber == 1f && React1switch == false)
       {
        Instantiate(FallRisBolt, AttackSpawn.transform.position, Quaternion.Euler(0,0,90));
        Debug.Log("React 1 (Fall RiceBolt)");
        React1switch = true;
       }
        if (Reactnumber == 1f)
        {
        decidertimer += Time.deltaTime;
        if (decidertimer > 0.2f)
        {
            Reactnumber = 0;
            decidertimer = 0;
           React1switch = false;
        }
        }
        
        
       // REACT 2 (Rice_Bolt) KOD

       if (Reactnumber == 2f && React2switch == false)
       {
        Instantiate(RiceBolt, AttackSpawn.transform.position, Quaternion.Euler(0,0,-180));
        Debug.Log("React 2 (Rice bolt)");
        React2switch = true;
       }
        if (Reactnumber == 2f)
        {
        decidertimer += Time.deltaTime;

        if (decidertimer > 0.2f)
        {
            Reactnumber = 0;
            decidertimer = 0;
           React2switch = false;
        }
        }
// REACT 3 (Ricegrains deflect) KOD
       if (Reactnumber == 3f && React3switch == false)
       {
        // Instantiate(RiceDeflect, AttackSpawn.transform.position, Quaternion.Euler(0,0,-50)); //Gamla ricedeflect
        Instantiate(RiceDeflectV2, AttackSpawn.transform.position, Quaternion.identity);
        Debug.Log("React 3 (Ricegrains deflect)");
    
        React3switch = true;
       }

       if (Reactnumber == 3f)
       {
        decidertimer += Time.deltaTime;
        if (decidertimer > 0.2f)
        {
            Reactnumber = 0;
            decidertimer = 0;
            React3switch = false;
        }
       }
       
    // REACT 4 (Risboll) KOD

    if (Reactnumber == 4f && React4switch == false)
    {
        Instantiate(Risboll, AttackSpawn.transform.position, Quaternion.identity);
        Debug.Log("React 4 (risboll attack)");
        React4switch = true; 
        Reactnumber = 0;
        decidertimer = 0;
    }
        if (React4switch == true)
        {
        decidertimer += Time.deltaTime;
        if (decidertimer > 0.2f)
        {
            React4switch = false;
        }
        }





}
}