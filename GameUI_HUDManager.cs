using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI_HUDManager : MonoBehaviour
{
	public static GameUI_HUDManager instance;
	public GameObject winPanel;
	public GameObject newWaveIncomingFlash;
	public GameObject newGunEffect;
	public GameObject enemyDeathEffect;
	public Transform gunSpring;
	private Level_Spawn_Manager LSM;

	private void Awake()
	{
		instance = this;
		LSM = Level_Spawn_Manager.instance;
	}

	public IEnumerator newwaveIncoming()
	{
		Debug.Log("Incoming");
		newWaveIncomingFlash.SetActive(true);
		yield return new WaitForSecondsRealtime(3f);
		newWaveIncomingFlash.SetActive(false);
	}

	public void showNewGunEffect()
	{
		Instantiate(newGunEffect,
			LSM.level[PlayerPrefs.GetInt("SelectedLevel")]
				.wavesInLevel[LSM._currentWaveToKeepActiveIndex].waveWeapon.transform.position,
			LSM.level[PlayerPrefs.GetInt("SelectedLevel")]
				.wavesInLevel[LSM._currentWaveToKeepActiveIndex].waveWeapon.transform
				.rotation);
	}

	public void loadHome()
	{
		SceneManager.LoadScene("MainMenuScene");
	}

	public void NextLevel()
	{
		if (LSM.level[PlayerPrefs.GetInt("SelectedLevel")+1].levelMap == MainMenuManager.LevelMap.Subway)
		{
			PlayerPrefs.SetInt("SelectedLevel",PlayerPrefs.GetInt("SelectedLevel")+1);
			SceneManager.LoadScene("Subway");
		}
		if (LSM.level[PlayerPrefs.GetInt("SelectedLevel")+1].levelMap == MainMenuManager.LevelMap.Cargo)
		{
			Debug.Log("No Functionality Available For Selected Operation");
		}

		
	}

	public IEnumerator LevelCompleteRoutine()
	{
		yield return new WaitForSecondsRealtime(2f);
		winPanel.SetActive(true);
		

	}


}