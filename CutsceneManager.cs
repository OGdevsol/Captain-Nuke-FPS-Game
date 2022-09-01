using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
	public GameObject[] deactivationsForCutscenes;

//	public GameObject[] LevelsCutscenes;
	private int currentLevel;
	public GameObject cutsceneObjectiveTextPanel;
	public TMP_Text cutsceneText;
	public GameObject cutsceneCanvas;
	public GameObject storytextPanel;
	public TMP_Text storyText;
	public GameObject brainCam;
	public GameObject miniMap;
	public Cutscene[] cutscenes;
	private GameObject storyImage;
	private GameObject player;


	private void Awake()
	{
		

		// PlayerPrefs.SetInt("SelectedLevel", 0);
		if (PlayerPrefs.GetString("Mode")=="Campaign")
		{
			currentLevel = PlayerPrefs.GetInt("SelectedLevel");
			
			storyImage = storytextPanel.transform.GetChild(0).gameObject;
	
		
			for (int i = 0; i < deactivationsForCutscenes.Length; i++)
			{
				deactivationsForCutscenes[i].SetActive(false);
			}

			StartCoroutine(waitBeforeCutsceneCompletes());
			Debug.LogError(PlayerPrefs.GetInt("SelectedLevel"));
		}

		if (PlayerPrefs.GetString("Mode")=="Shootout")
		{
			Debug.LogError("CutsceneArea,");
			
		}
//		player = FindObjectOfType<playercontroller>().transform.gameObject;
	
		
	}


	private IEnumerator waitBeforeCutsceneCompletes()
	{
	
		storytextPanel.SetActive(true);
		
		if (cutscenes[currentLevel].levelStoryImage!=null)
		{
			storyImage.GetComponent<Image>().sprite =
				cutscenes[currentLevel].levelStoryImage;
		}
		
		storyText.text = cutscenes[currentLevel].storyText;


		yield return new WaitForSecondsRealtime(5);
		if (storytextPanel.activeInHierarchy)
		{
			storytextPanel.SetActive(false);
		}

		cutsceneObjectiveTextPanel.SetActive(true);
		brainCam.SetActive(true);
		cutsceneText.text = cutscenes[currentLevel].cutSceneObjectiveText;
		if (cutscenes[currentLevel] != null)
		{
			cutscenes[currentLevel].cutsceneGameObject.SetActive(true);
		}

		yield return new WaitForSecondsRealtime(cutscenes[currentLevel].cutSceneDuration);
		cutsceneCanvas.SetActive(false);

		for (int i = 0; i < deactivationsForCutscenes.Length; i++)
		{
			deactivationsForCutscenes[i].SetActive(true);
		}

		if (cutscenes[currentLevel] != null)
		{
			cutscenes[currentLevel].cutsceneGameObject.SetActive(false);
			brainCam.SetActive(false);
		}
	}
}

[Serializable]
public class Cutscene
{
	public GameObject cutsceneGameObject;
	public string storyText;
	public float cutSceneDuration;
	public string cutSceneObjectiveText;
	public Sprite levelStoryImage;
}