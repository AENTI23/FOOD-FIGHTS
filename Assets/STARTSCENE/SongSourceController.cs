using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongSourceController : MonoBehaviour
{

    public float SongNumber = 0;

    public float SongVolume = 0;


    [SerializeField]
    Slider VolumeSlider;

    [SerializeField]

  public AudioSource Song1;
    //Insekter i min gård

    [SerializeField]
   public AudioSource Song2;
    //Meckishh

    [SerializeField]
  public  AudioSource Song3;

    //MF MINECRAFT

    [SerializeField]
    AudioSource[] Alltracks;

    // Start is called before the first frame update
    void Start()
    {
        SongNumber = 1;
        Song1.volume = 0.5f;
        Song2.volume = 0.5f;
        Song3.volume = 0.5f;
        VolumeSlider.value = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (SongNumber == 1)
        {
          Song1.enabled = true;

          //Disable other
          Song2.enabled = false;
          Song3.enabled = false;
        }
        if (SongNumber == 2)
        {
          Song2.enabled = true;
          
          //Disable other
          Song1.enabled = false;
          Song3.enabled = false;
        }
        if (SongNumber == 3)
        {
          Song3.enabled = true;

          //Disable other
          Song1.enabled = false;
          Song2.enabled = false;
        }

        print("SongNumber" + SongNumber);

        Song1.volume = VolumeSlider.value;
        Song2.volume = VolumeSlider.value;
        Song3.volume = VolumeSlider.value;

        

    

        

    

        


        /*

        float songplus = Input.GetAxisRaw("Fire1");

        float songminus = Input.GetAxisRaw("Jump");

        if(songplus == 1 && SongNumber != 3)
        {
            SongNumber += 1;
            print("SongNumber" + SongNumber);
        }
        if(songminus == 1 && SongNumber != 1)
        {
            SongNumber -= 1;
            print("SongNumber" + SongNumber);
        }

        print("SongNumber" + SongNumber);
        print("Plus" + songplus);
        print("Minus" + songminus);

        */

    }
}
