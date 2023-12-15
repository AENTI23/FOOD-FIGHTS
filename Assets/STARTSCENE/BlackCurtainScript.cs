using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCurtainScript : MonoBehaviour
{
    [SerializeField]
    float speed = 3.5f;

    [SerializeField]
    GameObject LoadingComplete;

    public bool switchedscene;

    // Start is called before the first frame update
    void Start()
    {
        if(switchedscene == true)
        {
            LoadingComplete.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = new Vector2(0, speed);
        transform.Translate(move * speed * Time.deltaTime);

        if(switchedscene == true)
        {
            if(transform.position.y > 20 || transform.position.y < -16)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
