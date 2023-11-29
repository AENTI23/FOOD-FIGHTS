using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeAnimSelect : MonoBehaviour
{
    Animator animato;

    public void OnMouseEnter()
    {
        animato = GameObject.FindWithTag("DisplayRis").GetComponent<Animator>();
        animato.SetBool("IsWalking", true);
        animato.SetFloat("Xinput", 1);
    }

   public void OnMouseExit()
    {
        animato = GameObject.FindWithTag("DisplayRis").GetComponent<Animator>();
        animato.SetBool("IsWalking", false);
    }

    
}
