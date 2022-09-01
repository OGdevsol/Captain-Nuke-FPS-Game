using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
using Random = System.Random;

public class TestSpawner : MonoBehaviour
{
   public static TestSpawner instance;
   public Transform[] spawnPositions;
   public GameObject[] Enemies;
   public GameObject portalEffect;
   public List<Transform> portals;
   public List<Transform> enemies;
   public GameObject destruction;

   private void Awake()
   {
      instance = this;
   }

   private void Start()
   {
      for (int i = 0; i < spawnPositions.Length; i++)
      {
       GameObject gg =  Instantiate(Enemies[i], spawnPositions[i].transform.position, transform.rotation);
       GameObject g =  Instantiate(portalEffect, spawnPositions[i].transform.position, spawnPositions[i].transform.rotation);
         portals.Add(g.transform);
         enemies.Add(gg.transform);
      }

      StartCoroutine(waitBeforeDestroyingEffect());
   }

   private IEnumerator waitBeforeDestroyingEffect()
   {
      yield return new WaitForSecondsRealtime(3f);
      for (int i = 0; i < portals.Count; i++)
      {
         Destroy(portals[i].transform.gameObject); 
      }
      
   }
}
