using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantDeathScript : MonoBehaviour
{
	public static MutantDeathScript instance;
	public Rigidbody[] rigidBodies;
	public bool isDead;

	private void Awake()
	{
		instance = this;
	}

	

	
}