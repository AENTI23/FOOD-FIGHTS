using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explodecontroller : MonoBehaviour
{
    public bool thisisexplode = false;
    // Start is called before the first frame update
    void Start()
    {
        if(thisisexplode == true)
        {
        Destroy(this.gameObject, 0.32f);
        }
        if(thisisexplode == false)
        {
            Destroy(this.gameObject, 0.7f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
