using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantProjectileScript : MonoBehaviour
{
	public float speed = 100f;
	private Rigidbody rb;

	private void Awake()
	{
		rb = gameObject.GetComponent<Rigidbody>();
	}

	void Update()
	{
		rb.AddRelativeForce(0f, 0f, speed);  // Make the projectile travel through z-axis.  
	}                                                                    // The projectile should travel in player direction as the enemy is already looking at the player
	                                                                     // and arm transform from where the projectile comes from is pointed towards the player's direction at the end of the animation
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			Destroy(gameObject);
		}
	}
}