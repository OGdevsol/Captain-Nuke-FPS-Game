using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript : MonoBehaviour
{
   private Animator animator;

   private void Start()
   {
      animator = gameObject.GetComponent<Animator>();
   }

   private void OnTriggerEnter (Collider other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         Debug.Log("PlayerNear");
         animator.SetBool("character_nearby",true);
         Debug.Log("PlayerNear2");

      }
   }
}
