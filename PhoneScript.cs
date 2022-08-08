using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneScript : MonoBehaviour
{
	private Level_Spawn_Manager LSM;
	private int x;

	private void Awake()
	{
		LSM=Level_Spawn_Manager.instance;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag=="Player")
		{
			Debug.Log("Player In Range");
			StartCoroutine(WaitBeforePhoneUpdate());
			
		}
	}

	private IEnumerator WaitBeforePhoneUpdate()
	{
		yield return new WaitForSecondsRealtime(3f);
		x = LSM.level[0]
			.wavesInLevel[LSM._currentWaveToKeepActiveIndex].enemiesGameObjectInWave
			.IndexOf(gameObject.transform);

		LSM.level[0]
			.wavesInLevel[LSM._currentWaveToKeepActiveIndex].enemiesGameObjectInWave
			.RemoveAt(x);
		LSM.CheckEnemiesInActiveWave();
		
		
	}
}
