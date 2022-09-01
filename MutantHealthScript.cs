using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantHealthScript : MonoBehaviour
{
	public static MutantHealthScript instance;
	public float damageToInflict = 10;
	private int x;
	public Rigidbody[] rigidBodies;
	public bool isDead;
	private GameObject player;
	private MutantHealthScript mutantHealthScript;
	private healthUIScript HealthUIScript;
	private Animator animator;
	private Level_Spawn_Manager LSM;

	private void Awake()
	{
		instance = this;
		player = FindObjectOfType<playercontroller>().transform.gameObject;
		mutantHealthScript = gameObject.GetComponent<MutantHealthScript>();
		HealthUIScript = gameObject.GetComponent<healthUIScript>();
		rigidBodies = gameObject.GetComponentsInChildren<Rigidbody>();
		animator = gameObject.GetComponent<Animator>();
		LSM = Level_Spawn_Manager.instance;
		
		EnableIsKinematic(); // After creating the ragdoll for the enemy character, set isKinematic to true for all its rigidbodies as ragdoll and animator will malfunction together
	}

	void Update()
	{
		if (!mutantHealthScript.isDead)
		{
			transform.LookAt(player
				.transform); // Make the enemy character always look in the direction of the player as long as the mutant is alive
		}
	}

	public float health = 100f;

	public void Damage(float damage)
	{
		if (!isDead)
		{
			health -= damage;
			HealthUIScript.healthUISlider.value = health;
			if (health <= 0)
			{
				KillMutant();
			}
		}
	}

	void EnableIsKinematic()
	{
		

		for (int i = 0; i < rigidBodies.Length; i++)
		{
			rigidBodies[i].isKinematic = true;
		}
	}


	public void
		KillMutant() // Set isKinematic to false as soon as the enemy character's health reaches zero. Used for a realistic ragdoll effect
	{
		animator.enabled = false;
		HealthUIScript.healthUISlider.gameObject.SetActive(false);
		foreach (Rigidbody rb in rigidBodies)
		{
			rb.isKinematic = false;
		}

		isDead = true;


		x = LSM.level[PlayerPrefs.GetInt("SelectedLevel")]
			.wavesInLevel[Level_Spawn_Manager.instance._currentWaveToKeepActiveIndex].enemiesGameObjectInWave
			.IndexOf(gameObject.transform);

		LSM.level[PlayerPrefs.GetInt("SelectedLevel")]
			.wavesInLevel[Level_Spawn_Manager.instance._currentWaveToKeepActiveIndex].enemiesGameObjectInWave
			.RemoveAt(x);
		LSM.CheckEnemiesInActiveWave();
		StartCoroutine(destroyGameObject());
	}

	private IEnumerator destroyGameObject()
	{
		yield return new WaitForSecondsRealtime(3);
		Destroy(gameObject);
	}
}