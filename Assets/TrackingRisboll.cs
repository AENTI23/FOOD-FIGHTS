using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingRisboll : MonoBehaviour
{

    [SerializeField]
    GameObject Risboll; // Fienden, så scriptet vet vad den ska tracka

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         //tracking kod
        float x = Risboll.transform.position.x;
        float y = Risboll.transform.position.y;

        Vector2 pos = new Vector2(x, y);

        transform.position = pos; 

        if(transform.position.y < -10)
        {
          Destroy(this.gameObject);
        }
    }
}
