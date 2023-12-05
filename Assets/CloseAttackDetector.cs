using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingScript : MonoBehaviour
{
    public float closedetectpoint = 0f;
    public bool closedetectbool= false;

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
          //  closedetectpoint += 1f;
           // Debug.Log("CLOSEDETECTED");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (closedetectpoint > 0)
        {
            closedetectbool = true;
            boolstoptimer += Time.deltaTime;
        }

        if (boolstoptimer > 0.7f)
        {
            closedetectbool = false;
            boolstoptimer = 0;
            closedetectpoint = 0;
        }

        //tracking kod
        float x = enemyPrefab.transform.position.x;
        float y = enemyPrefab.transform.position.y;

        Vector2 pos = new Vector2(x, y);

        transform.position = pos; 
        //Debug.Log(enemyPrefab.transform.position.x);
     
      

    }
}
