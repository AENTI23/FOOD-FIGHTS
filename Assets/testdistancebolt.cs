using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class testdistancebolt : MonoBehaviour
{
    [SerializeField]
    GameObject onding;
    
    [SerializeField]
    GameObject me;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = me.transform.position.x;
        float y = me.transform.position.y;

        float xond = onding.transform.position.x;
        float yond = onding.transform.position.y;

        Vector2 posond = new Vector2(xond, yond);

        Vector2 pos = new Vector2(x, y);


       // transform.position = pos;

        
      
       // float nyspawn = Random.Range(-0,8, 0,4);
       //transform.position = lowest;


       float 
    
    Klong = Vector2.Distance(pos, posond);
   
   float inputer = Input.GetAxisRaw("Jump");
   if(Klong < 10)
   {
    //print("close");
   }
if (inputer > 0)
{
print("vomp");
   print(Klong);
}

//Kollar Y distans
   float inputer2 = Input.GetAxisRaw("Fire1");
      Vector2 Ypos = new Vector2(y, y);

      Vector2 Yposond = new Vector2(yond, yond);

        float KlongY = Vector2.Distance(Ypos, Yposond);

if(inputer2 > 0)
{
   print(KlongY);
}
if(KlongY < 5)
{
   print("YDistance");
  
}
    }
}
