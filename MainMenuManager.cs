using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
	public List<Button> levelsButtons;

	// Start is called before the first frame update
	public List<GameObject> ReferencePanels;
	private Button button;
	public GameObject loadingPanel;
	private mapType map;

	public enum LevelMap
	{
		Cargo,
		Subway,
	}

	private void Awake()
	{
		button = gameObject.GetComponent<Button>();
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	void Start()
	{
		LevelsLock_Unlock();
	}

	public void LevelsLock_Unlock()
	{
		Debug.LogError(PlayerPrefs.GetInt("Unlockable"));
		for (var i = 0; i <= PlayerPrefs.GetInt("Unlockable"); i++)
		{
			levelsButtons[i].interactable = true;
			levelsButtons[i].gameObject.transform.GetChild(2).gameObject.SetActive(false);
			levelsButtons[i].gameObject.transform.GetChild(3).gameObject.SetActive(false);
		}
	}

	public void EnableReferencePanel(int panelID)
	{
		SoundController.instance.playFromPool(AudioType.UIclick);
		DisableAllPanels();
		ReferencePanels[panelID].SetActive(true);
	}

	public void CAMPAIGNMODE()
	{
		PlayerPrefs.SetString("Mode", "Campaign");
		EnableReferencePanel(2);
		//	SoundController.instance.playFromPool(AudioType.UIclick1);
	}

	public void SHOOTOUTMODE()
	{
		PlayerPrefs.SetString("Mode", "Shootout");
		SoundController.instance.playFromPool(AudioType.UIclick2);
		Debug.LogError(PlayerPrefs.GetString("Mode"));
		StartCoroutine(LoadLevel());
		SceneManager.LoadScene("Cargo2");
	}

	public void DisableAllPanels()
	{
		for (int i = 0; i < ReferencePanels.Count; i++)
		{
			ReferencePanels[i].SetActive(false);
		}
	}

	public void SelectedLevel(int x)
	{
		PlayerPrefs.SetInt("SelectedLevel", x);
	}

	public void LoadWithMap(String mapName)
	{
		if (mapName == "Cargo")
		{
			map = mapType.Cargo;
		}

		if (mapName == "Subway")
		{
			map = mapType.Subway;
		}

		StartCoroutine(LoadLevel());
	}


	public IEnumerator LoadLevel()
	{
		SoundController.instance.playFromPool(AudioType.UIclick2);
		if (PlayerPrefs.GetString("Mode") == "Campaign")
		{
			loadingPanel.SetActive(true);
			yield return new WaitForSeconds(2f);

			switch (map)
			{
				case mapType.Cargo:
					SceneManager.LoadScene("Cargo2");
					break;
				case mapType.Subway:
					SceneManager.LoadScene("Subway");
					break;
			}
		}

		if (PlayerPrefs.GetString("Mode") == "Shootout")
		{
			loadingPanel.SetActive(true);
			yield return new WaitForSeconds(2f);
			SceneManager.LoadScene("Cargo2");
			Debug.LogError("SceneLoaded");
		}
	}

	public void QUIT()
	{
		Application.Quit();
	}

	private enum mapType
	{
		Cargo,
		Subway,
	}
}