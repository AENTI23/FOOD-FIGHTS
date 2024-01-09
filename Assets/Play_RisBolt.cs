using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_RisBolt : MonoBehaviour
{
      EnemyMovementControler EMOVESCRIPT;

    // Start is called before the first frame update
[SerializeField]
GameObject explode;
    bool killbool = false;
    
    void Start()
    {
     EMOVESCRIPT = GameObject.FindWithTag("Evilricetag").GetComponent<EnemyMovementControler>();

    }

    void OnTriggerEnter2D(Collider2D Other)
    {
        if(Other.gameObject.tag == "E_Attack_Tag" + "Explode")
        {
            Instantiate(explode, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        if(Other.gameObject.tag == "Explode")
        {
            Instantiate(explode, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            //Debug.Log("RISBOLT: Destroyed by explode");
        }
        if(Other.gameObject.tag == "Evilricetag")
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float cornspeed = 3.5f;

        Vector2 grainmove = new Vector2(cornspeed, 0);
        transform.Translate(grainmove * cornspeed * Time.deltaTime);



        if(transform.position.x > 15 || killbool == true)
        {
            Destroy(this.gameObject);
        }
    }

}
