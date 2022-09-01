using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class GameUI_HUDManager : MonoBehaviour
{
	public static GameUI_HUDManager instance;
	public GameObject winPanel;
	public GameObject losePanel;
	public GameObject newWaveIncomingFlash;
	public GameObject newGunEffect;
	public GameObject enemyDeathEffect;
	public Transform gunSpring;
	private Level_Spawn_Manager LSM;
	public GameObject pausePanel;
	public GameObject actionImage;
	public Slider PlayerHealthSlider;
	public Button NextLevelButton;

	private void Awake()
	{
		instance = this;
		LSM = Level_Spawn_Manager.instance;
		if (PlayerPrefs.GetInt("SelectedLevel")>=9)
		{
			NextLevelButton.interactable = false;
		}
	}

	public IEnumerator newwaveIncoming()
	{
		Debug.Log("Incoming");
		newWaveIncomingFlash.SetActive(true);
		SoundController.instance.playFromPool(AudioType.IncomingWave);
		yield return new WaitForSecondsRealtime(3f);
		newWaveIncomingFlash.SetActive(false);
		
	}

	public void showNewGunEffect()
	{
		if (PlayerPrefs.GetString("Mode")=="Campaign")
		{
			Instantiate(newGunEffect,
				LSM.level[PlayerPrefs.GetInt("SelectedLevel")]
					.wavesInLevel[LSM._currentWaveToKeepActiveIndex].waveWeapon.transform.position,
				LSM.level[PlayerPrefs.GetInt("SelectedLevel")]
					.wavesInLevel[LSM._currentWaveToKeepActiveIndex].waveWeapon.transform
					.rotation);
		}if (PlayerPrefs.GetString("Mode")=="Shootout")
		{
			Instantiate(newGunEffect, LSM.shootoutModeDetails.playerWeapon[LSM.shootoutModeWeaponIndexRandom].transform.position,  LSM.shootoutModeDetails.playerWeapon[LSM.shootoutModeWeaponIndexRandom].transform.rotation);

		}
		
	}

	public void loadHome()
	{
		SceneManager.LoadScene("MainMenuScene");
	}

	private int currentLevel;
	
	public void NextLevel()
	{
		//currentLevel = PlayerPrefs.GetInt("SelectedLevel") ;
		/*Debug.LogError("Plus one inside  " + LSM.level[PlayerPrefs.GetInt("SelectedLevel" + 1) ].levelMap);
		Debug.LogError("Plus one outside  " + LSM.level[PlayerPrefs.GetInt("SelectedLevel" )+ 1 ].levelMap);*/
		/*Debug.LogError("Plus one value  " + PlayerPrefs.GetInt("SelectedLevel" )+ 1 );
		Debug.LogError("Plus one value  " + PlayerPrefs.GetInt("SelectedLevel" + 1 ) );*/
		//Debug.LogError("CURRENT" + PlayerPrefs.GetInt("SelectedLevel"));
		if (PlayerPrefs.GetInt("SelectedLevel") <= 9)
		{
			/*if (LSM.level[PlayerPrefs.GetInt("SelectedLevel" ) + 1].levelMap == MainMenuManager.LevelMap.Subway)
			{
				PlayerPrefs.SetInt("SelectedLevel", PlayerPrefs.GetInt("SelectedLevel")+ 1 );
				SceneManager.LoadScene("Subway");
			}

			if (LSM.level[PlayerPrefs.GetInt("SelectedLevel") + 1].levelMap == MainMenuManager.LevelMap.Cargo)
			{
				PlayerPrefs.SetInt("SelectedLevel", PlayerPrefs.GetInt("SelectedLevel") + 1);
				SceneManager.LoadScene("Cargo2");
			}*/
			PlayerPrefs.SetInt("SelectedLevel", PlayerPrefs.GetInt("SelectedLevel") + 1);
			if ((LSM.level[PlayerPrefs.GetInt("SelectedLevel")].levelMap == MainMenuManager.LevelMap.Subway))
			{
				SceneManager.LoadScene("Subway");
			}

			if (LSM.level[PlayerPrefs.GetInt("SelectedLevel")].levelMap == MainMenuManager.LevelMap.Cargo)
			{

				SceneManager.LoadScene("Cargo2");
			}
		}


	}

	public IEnumerator LevelCompleteRoutine()
	{

		
		
		Debug.LogError("MAX "+PlayerPrefs.GetInt("MaxLvl"));
		
		yield return new WaitForSecondsRealtime(0.2f);
		winPanel.SetActive(true);
		SoundController.instance.playFromPool(AudioType.GameWinPanelEffect);
		if (pausePanel.activeInHierarchy)
		{
			pausePanel.SetActive(false);
		}

		if (PlayerPrefs.GetInt("SelectedLevel")>=PlayerPrefs.GetInt("Unlockable"))
			
		{
			if (PlayerPrefs.GetInt("SelectedLevel")<9)
			{
				PlayerPrefs.SetInt("Unlockable",PlayerPrefs.GetInt("SelectedLevel")+1);
			}
			
		}
		
	}

	public IEnumerator LevelFailRoutine()
	{
		yield return new WaitForSecondsRealtime(0.25f);
		losePanel.SetActive(true);
		if (pausePanel.activeInHierarchy)
		{
			pausePanel.SetActive(false);
		}

	
		
			
		
		
			
		
	
		
			
		/*if (PlayerPrefs.GetInt("MaxLvl")>=14)
		{
			PlayerPrefs.SetInt("MaxLvl",14);
		}*/
		// Time.timeScale = 0;
		// Level_Spawn_Manager.instance.player.SetActive(false);
		
	}

	public void RETRY()
	{
		Time.timeScale = 1;
		if (PlayerPrefs.GetString("Mode")=="Campaign")
		{
			if (LSM.level[PlayerPrefs.GetInt("SelectedLevel")].levelMap == MainMenuManager.LevelMap.Cargo)
			{
				SceneManager.LoadScene("Cargo2");
			}

			if (LSM.level[PlayerPrefs.GetInt("SelectedLevel")].levelMap == MainMenuManager.LevelMap.Subway)
			{
				SceneManager.LoadScene("Subway");
			}
		}

		if (PlayerPrefs.GetString("Mode")=="Shootout")
		{
			SceneManager.LoadScene("Cargo2");
		}
		
	}

	public void HOME()
	{
		SceneManager.LoadScene("MainMenuScene");
	}

	public void PAUSE()
	{
		pausePanel.SetActive(true);
		// Time.timeScale = 0;
	}

	public void RESUME()
	{
		pausePanel.SetActive(false);
		// Time.timeScale = 1;
	}
}