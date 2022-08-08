using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionColliderScript : MonoBehaviour
{
	private int x;

	public float waitTime;

	private bool actionDone;
	private Level_Spawn_Manager LSM;

	private void Awake()
	{
		LSM=Level_Spawn_Manager.instance;
	}

	private void OnTriggerStay(Collider other)
	{
		if (actionDone) return;
		if (other.gameObject.CompareTag("Player")  )
		{
			Debug.Log("Player In Range");
			StartCoroutine(WaitBeforeActionUpdate());
			actionDone = true;
		}
	}

	private IEnumerator WaitBeforeActionUpdate()
	{
		yield return new WaitForSecondsRealtime(waitTime);
		x = LSM.level[PlayerPrefs.GetInt("SelectedLevel")]
			.wavesInLevel[LSM._currentWaveToKeepActiveIndex].enemiesGameObjectInWave
			.IndexOf(gameObject.transform);

		LSM.level[PlayerPrefs.GetInt("SelectedLevel")]
			.wavesInLevel[LSM._currentWaveToKeepActiveIndex].enemiesGameObjectInWave
			.RemoveAt(x);


		LSM.CheckEnemiesInActiveWave();
	}

	private void OnTriggerExit(Collider other)
	{
		StopCoroutine(WaitBeforeActionUpdate());
	}
}