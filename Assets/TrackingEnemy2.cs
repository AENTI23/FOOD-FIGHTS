using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingEnemy2 : MonoBehaviour
{
   public float detectedpoint = 0f;
    public bool grounddetectbool = false;

    [SerializeField]
    float boolstoptimer = 0;

    [SerializeField]
    GameObject enemyPrefab; // Fienden, så scriptet vet vad den ska tracka

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D DodgeOther)
    {
         if (DodgeOther.gameObject.tag == "Risboll_Tag")
        {
           // detectedpoint += 1f;
           // Debug.Log("CLOSEDETECTED");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (detectedpoint > 0)
        {
            grounddetectbool = true;
            boolstoptimer += Time.deltaTime;
        }

        if (boolstoptimer > 0.15f)
        {
            grounddetectbool = false;
            boolstoptimer = 0;
            detectedpoint = 0;
        }

        //tracking kod
        float x = enemyPrefab.transform.position.x;
        float y = enemyPrefab.transform.position.y;

        Vector2 pos = new Vector2(x, y);

        transform.position = pos; 
        //Debug.Log(enemyPrefab.transform.position.x);
     
      

    }
}
