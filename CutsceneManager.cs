using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

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
	public Cutscene[] cutscenes;
	
	


	private void Awake()
	{
		// PlayerPrefs.SetInt("SelectedLevel", 0);
		currentLevel = PlayerPrefs.GetInt("SelectedLevel");
		for (int i = 0; i < deactivationsForCutscenes.Length; i++)
		{
			deactivationsForCutscenes[i].SetActive(false);
		}

		StartCoroutine(waitBeforeCutsceneCompletes());
	}


	private IEnumerator waitBeforeCutsceneCompletes()
	{
		storytextPanel.SetActive(true);
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
}