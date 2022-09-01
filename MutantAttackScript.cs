using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantAttackScript : MonoBehaviour
{
	
	public Transform leftAttackarmTransform;
	public Transform rightAttackarmTransform;
	private MutantHealthScript mutantHealthScript;

	private void Awake()
	{
		mutantHealthScript = gameObject.GetComponent<MutantHealthScript>();
	}


	public void ProjectileAttackLeftArm(GameObject projectile) // Added as an event in animation 1
	{
		if (!mutantHealthScript.isDead)
		{
			Instantiate(projectile, leftAttackarmTransform.transform.position, transform.rotation);
			SoundController.instance.playFromPool(AudioType.PeojectileWhoosh);
		}
	}

	public void ProjectileAttackRightArm(GameObject projectile) // Added as an event in animation 2
	{
		if (!mutantHealthScript.isDead)
		{
			Instantiate(projectile, rightAttackarmTransform.transform.position, transform.rotation);
			SoundController.instance.playFromPool(AudioType.PeojectileWhoosh);

		}
	}
}