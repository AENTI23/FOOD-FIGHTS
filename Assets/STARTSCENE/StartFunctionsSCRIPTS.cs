using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;

public class StartFunctionsSCRIPTS : MonoBehaviour
{

   TextMeshProUGUI SongText;

    TextMeshProUGUI selected;

    Image CharButton;

    Image diffbutton;


    SongSourceController SongScript; // Script Variable

[SerializeField]
    Image StartButton;

    [SerializeField]
    Image MapButton;

    [SerializeField]
    GameObject BlackCurtain;

    [SerializeField]
    GameObject SpawnTop;

    [SerializeField]
    GameObject SpawnBottom;

    [SerializeField]
    TextMeshProUGUI StartText;

    [SerializeField]
    GameObject loadingtext;

    [SerializeField]
    AudioSource Errorsound;

    [SerializeField]
    AudioSource Approved;

    bool chara = false;
    bool map = false;

    bool diffu = false;

    int one = 1;

    float Changescenetimer = 0;

    bool startloading = false;

    bool once = false;


    // Start is called before the first frame update
  public void Start()
    {
         SongScript = GameObject.FindWithTag("MusicSourceTag").GetComponent<SongSourceController>(); 
         //Får tag på script komponenten för SongScriptet
    }

    // Update is called once per frame

   public void SongSwitchPlus()
    {
        if(SongScript.SongNumber < 3)
        {
        SongScript.SongNumber += 1;
        }
    }
    
   public void SongSwitchMinus()
    {
        if(SongScript.SongNumber != 1)
        {
            SongScript.SongNumber -= one;
        }
    }

    public void SongTextChange()
    {
        SongText = GameObject.FindWithTag("DisplaySongTag").GetComponent<TextMeshProUGUI>();
        if(SongScript.SongNumber == 1)
        {
            SongText.text = "Insekter i min gård";
        }
        if(SongScript.SongNumber == 2)
        {
            SongText.text = "Meckishh";
        }
        if(SongScript.SongNumber == 3)
        {
            SongText.text = "MF MINECRAFT";
        } 
    }
    public void ChangeSelectedNumber()
    {
        selected = GameObject.FindWithTag("SelectNumber").GetComponent<TextMeshProUGUI>();
        selected.text = "1/3";
        CharButton = GameObject.FindWithTag("CharacterButton").GetComponent<Image>();
        CharButton.color = Color.green;
        chara = true;
    }

    public void ChangeSelectedNumber2()
    {
        selected = GameObject.FindWithTag("SelectNumber").GetComponent<TextMeshProUGUI>();
        MapButton = GameObject.FindWithTag("MapButton").GetComponent<Image>();
        selected.text = "2/3";
        map = true;
        MapButton.color = Color.green;
    }

    public void ChangeSelectedNumber3()
    {
        selected = GameObject.FindWithTag("SelectNumber").GetComponent<TextMeshProUGUI>();
        diffbutton = GameObject.FindWithTag("DiffButton").GetComponent<Image>();
        diffbutton.color = Color.green;
        selected.text = "3/3";
        diffu = true;
        
    }

    public void StartGame()
    {
        if(diffu == false || map == false || chara == false)
        {
            StartButton.color = Color.red;
            StartText.text = "Select!";
            Errorsound.Play();
        }
    }

    public void BytSceneToGame()
    {
        if(diffu == true && map == true && chara == true)
        {
        Approved.Play();
        StartButton.color = Color.green;
            StartText.text = "Loading!";
            startloading = true;
            Instantiate(BlackCurtain, SpawnBottom.transform.position, Quaternion.identity);
            Instantiate(BlackCurtain, SpawnTop.transform.position, Quaternion.Euler(0, 0, -180));
        }
    }

    public void RestartToGame()
    {
        SceneManager.LoadScene(1);
    }
    public void GoBackToMenu()
    {
        SceneManager.LoadScene(0);
    }
void Update()
{
    if(startloading == true)
    {
        if(once == false)
        {
     loadingtext.SetActive(true);
       once = true;
        }
       
         Changescenetimer += Time.deltaTime;

 if(Changescenetimer > 2f)
        {
        SceneManager.LoadScene(1);
        }
    }
}
}
