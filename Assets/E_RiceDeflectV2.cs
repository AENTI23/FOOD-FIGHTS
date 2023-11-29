using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_RiceDeflectV2 : MonoBehaviour
{
    [SerializeField]
    GameObject Ricebolt;

    [SerializeField]
    float timer = 0f;

    
    bool one = false;
    bool two = false;
    bool three = false;
    bool four = false;
    bool five = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 0.1 && one == false)
        {
            Instantiate(Ricebolt, transform.position, Quaternion.Euler (0,0,-180));
            one = true;
        }
        if(timer > 0.2 && two == false)
        {
             Instantiate(Ricebolt, transform.position, Quaternion.Euler (0,0,-200));
             Instantiate(Ricebolt, transform.position, Quaternion.Euler (0,0,-160));
             two = true;
        }
        if(timer > 0.3 && three == false)
        {
             Instantiate(Ricebolt, transform.position, Quaternion.Euler (0,0,-220));
             Instantiate(Ricebolt, transform.position, Quaternion.Euler (0,0,-140));
             three = true;
        }
        if(timer > 0.4 && four == false)
        {
             Instantiate(Ricebolt, transform.position, Quaternion.Euler (0,0,-240));
             Instantiate(Ricebolt, transform.position, Quaternion.Euler (0,0,-120));
             four = true;
        }
        if(timer == 0.5 && five == false)
        {
             Instantiate(Ricebolt, transform.position, Quaternion.Euler (0,0,-260));
             Instantiate(Ricebolt, transform.position, Quaternion.Euler (0,0,-100));
             five = true;
        }
        if (timer > 0.7f)
        {
            Destroy(this.gameObject);
        }
    }
}
