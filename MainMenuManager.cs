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
	}

	void Start()
	{
		for (int i = 3; i < levelsButtons.Count; i++)
		{
			levelsButtons[i].interactable = false;
			levelsButtons[i].gameObject.transform.GetChild(2).gameObject.SetActive(true);
			levelsButtons[i].gameObject.transform.GetChild(3).gameObject.SetActive(true);
		}
	}

	public void EnableReferencePanel(int panelID)
	{
		DisableAllPanels();
		ReferencePanels[panelID].SetActive(true);
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
		loadingPanel.SetActive(true);
		yield return new WaitForSeconds(2f);

		switch (map)
		{
			case mapType.Cargo:
				Debug.Log("No Functionality for current level");
				break;
			case mapType.Subway:
				SceneManager.LoadScene("Subway");
				break;
		}
	}

	private enum mapType
	{
		Cargo,
		Subway,
	}
}