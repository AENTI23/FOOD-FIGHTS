using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class risboll : MonoBehaviour
{
    [SerializeField]
    float movestop = 0;

    [SerializeField]
    float timer = 0;

    [SerializeField]
    float minusbank = 2;

    [SerializeField]
    GameObject slash;

    [SerializeField]
    float flight = 2.5f;

    [SerializeField]
    float minus = 1;

    [SerializeField]
    GameObject Explode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D Other)
    {
        if(Other.gameObject.tag == "E_Attack_Tag")
        {
          Instantiate(Explode, transform.position, Quaternion.identity);
          Destroy(this.gameObject);
        }
        if(Other.gameObject.tag == "Evilricetag")
        {
            Destroy(this.gameObject);
        }
    }
    void Update()
    {
        movestop += Time.deltaTime;
        timer += Time.deltaTime;
       
//BEVARA DETTA IF STATEMENT OCH DESS FLOATSENS VALUES I INSPECTOR. swift attack! råka.
        if (timer > minusbank)
        {
            flight -= minus;
            minusbank -= minus;
            timer = 0;
            
        }
        Vector2 rörelse = new Vector2(flight,4f);
       // Vector2 rörelse2 = new Vector2(flight,0);
     
        
        transform.Translate(rörelse * flight * Time.deltaTime);


        if (Mathf.Abs (transform.position.y) > 10)
        {
            Destroy(this.gameObject);
        }


    }
}
