using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ricegunrandom : MonoBehaviour
{

    [SerializeField]
    GameObject ris;

    [SerializeField]
    GameObject risond;
    
    // Start is called before the first frame update
    void Start()
    {
 

    }

    // Update is called once per frame
    void Update()
    {
        float lowest = Random.Range(-3.8f, -2.45f);
        float x = ris.transform.position.x;
        float y = ris.transform.position.y;

        float xond = risond.transform.position.x;
        float yond = risond.transform.position.y;

        Vector2 posond = new Vector2(xond, yond);

        Vector2 pos = new Vector2(x, y);

       // transform.position = pos;

        
      
       // float nyspawn = Random.Range(-0,8, 0,4);
       //transform.position = lowest;


       float 
    
    Klong = Vector2.Distance(pos, posond);

    print(Klong);
      
    }
}
