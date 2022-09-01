using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Level_Spawn_Manager : MonoBehaviour
{
	public static Level_Spawn_Manager instance;

	[Header("____Enemies Prefabs In Sequence____")] [Space(10)]
	public GameObject[] enemiesVariantsPrefabs;

	[Header("____LEVELS____")] [Space(10)] public Level[] level;
	private int x;

	[Header("____ Player and Weapons GameObjects____")] [Space(10)]
	public GameObject[] weapons;

	public GameObject player;
	[Header("____ UI and Effects____")] public TMP_Text waveObjectiveGameObject;
	public GameObject initializationEffect;
	private GameUI_HUDManager GameUImgr;

	[Header("____ShootoutMode____")] [Space(10)]
	public ShootoutMode shootoutModeDetails;

	[HideInInspector] public int shootoutModeWeaponsLength;
	[HideInInspector] public int shootoutModeWeaponIndexRandom;
	[HideInInspector] public int shootoutModeRandomPlayerPos;
	[HideInInspector] public int shootoutModePlayerPosLength;
	[HideInInspector] public int shootoutModeEnemyRandomPos;
	[HideInInspector] public int shootoutModeEnemyPosRandomLength;
	[HideInInspector] public int enemyTypeRandom;


	private int
		_currentWaveInLoop; // To only check and instantiate according to the total enemies kept in each wave while initializing enemy instantiation

	[HideInInspector] public int
		_currentWaveToKeepActiveIndex; // To activate a single wave at a given time. Next wave will be activated when all enemies of currently active wave are killed


	private void Awake()
	{
		instance = this;
		SetPlayerPosition();

	}

	private void Start()
	{
		if (PlayerPrefs.GetString("Mode") == "Campaign")
		{
			AddWavesToList();
			StartCoroutine(InitializeLevel());
		}

		if (PlayerPrefs.GetString("Mode") == "Shootout")

		{
			shootoutModeWeaponsLength = shootoutModeDetails.playerWeapon.Length;
			shootoutModeWeaponIndexRandom = Random.Range(0, shootoutModeWeaponsLength);
			shootoutModeEnemyPosRandomLength = shootoutModeDetails.enemyPositions.Length;
			shootoutModeEnemyRandomPos = Random.Range(0, shootoutModeDetails.enemyPositions.Length);

			StartCoroutine(InitializeShooutout());
			ActivateShootoutWeapon();
		}
		//	player = FindObjectOfType<playercontroller>().transform.gameObject;
	}

	private IEnumerator InitializeShooutout()
	{
		yield return new WaitForSecondsRealtime(0.5f);
		enemyTypeRandom = Random.Range(1, 7);
		// ActivateShootoutWeapon();

		yield return new WaitForSecondsRealtime(1.5f);
		for (int j = 0;
			j < enemyTypeRandom;
			j++)
		{
			var E = Instantiate(enemiesVariantsPrefabs[CheckEnemiesType(j)],
				shootoutModeDetails.enemyPositions[shootoutModeEnemyRandomPos].position,
				shootoutModeDetails.enemyPositions[shootoutModeEnemyRandomPos].rotation);
			Instantiate(initializationEffect, E.transform.position, E.transform.rotation);
			shootoutModeDetails.enemyGameObjectInWave.Add(E.transform);
			Debug.Log("Forming Enemies");
		}

		
		

	}


	private void OnEnable()
	{
		GameUImgr = GameUI_HUDManager.instance;
	}

	private void
		AddWavesToList() // Method to initialize enemies in form of waves. When all enemies in the first wave are killed in a level, the second wave will be initiated. Level in completed when all waves are killed
	{
		var wavesLengthInLevel = level[PlayerPrefs.GetInt("SelectedLevel")].waves.Length;
		int i;
		for (i = 0; i < wavesLengthInLevel; i++)
		{
			_currentWaveInLoop = i;
			level[PlayerPrefs.GetInt("SelectedLevel")].wavesInLevel
				.Add(level[PlayerPrefs.GetInt("SelectedLevel")].waves[i]);
		}
	}

	private void SetPlayerPosition()
	{
		if (PlayerPrefs.GetString("Mode") == "Campaign")
		{
			player.transform.position = level[PlayerPrefs.GetInt("SelectedLevel")].playerPosition.position;
			player.transform.rotation = level[PlayerPrefs.GetInt("SelectedLevel")].playerPosition.rotation;
		}

		if (PlayerPrefs.GetString("Mode") == "Shootout")
		{
			Debug.LogError("SETTING SHOOTOUT POSITION");
			player.transform.position = shootoutModeDetails.playerPosition.position;
			player.transform.rotation = shootoutModeDetails.playerPosition.rotation;
		}
	}

	private IEnumerator InitializeLevel()
	{
		yield return new WaitForSecondsRealtime(2.5f);
		ActivateWaveWeapon();
		StartCoroutine(GameUI_HUDManager.instance.newwaveIncoming());
		waveObjectiveGameObject.text = level[PlayerPrefs.GetInt("SelectedLevel")]
			.wavesInLevel[_currentWaveToKeepActiveIndex].waveObjective;
		yield return new WaitForSecondsRealtime(1.5f);

		for (int j = 0;
			j < level[PlayerPrefs.GetInt("SelectedLevel")].wavesInLevel[_currentWaveToKeepActiveIndex].enemyType.Length;
			j++)
		{
			var E = Instantiate(enemiesVariantsPrefabs[CheckEnemiesType(j)],
				level[PlayerPrefs.GetInt("SelectedLevel")].wavesInLevel[_currentWaveToKeepActiveIndex].enemyPosition[j]
					.position,
				level[PlayerPrefs.GetInt("SelectedLevel")].wavesInLevel[_currentWaveToKeepActiveIndex].enemyPosition[j]
					.rotation);
		
			level[PlayerPrefs.GetInt("SelectedLevel")].wavesInLevel[_currentWaveToKeepActiveIndex]
				.enemiesGameObjectInWave.Add(E.transform);
			if ( level[PlayerPrefs.GetInt("SelectedLevel")].wavesInLevel[_currentWaveToKeepActiveIndex].enemyType[j] != EnemyType.ActionCollider)
			{
				Instantiate(initializationEffect, E.transform.position, E.transform.rotation);
			}
			Debug.Log("Forming Enemies");
		}
	}


	private static int waveToBeRemovedIndex = 0;

	public void
		CheckEnemiesInActiveWave() // When there are zero enemies left in currently active wave, next enemy wave will be initialized According to the mode (Also being used in health script of the AI in AI Kit in KillAI method. NOTE: Don't forget to remove the enemy gameobject from respective wave when it is killed 
	{
		if (PlayerPrefs.GetString("Mode") == "Campaign")
		{
			if (level[PlayerPrefs.GetInt("SelectedLevel")].wavesInLevel[_currentWaveToKeepActiveIndex]
				.enemiesGameObjectInWave.Count == 0)
			{
				level[PlayerPrefs.GetInt("SelectedLevel")].wavesInLevel
					.RemoveAt(
						waveToBeRemovedIndex); // Wave with zero enemies will be removed/deactivated to initialize next wave and the next wave will take its place at waveToBeRemovedIndex. The cycle will repeat when 0 enemies are left in new wave.
                    SoundController.instance.playFromPool(AudioType.WaveCleared);
				if (level[PlayerPrefs.GetInt("SelectedLevel")].wavesInLevel.Count > 0)
				{
					StartCoroutine(InitializeLevel());
				}
			}

			if (level[PlayerPrefs.GetInt("SelectedLevel")].wavesInLevel.Count == 0)
			{
				Debug.Log("LEVEL COMPLETED");
				waveObjectiveGameObject.text = "Level Completed";
				StartCoroutine(GameUI_HUDManager.instance.LevelCompleteRoutine());
			}
		}

		if (PlayerPrefs.GetString("Mode") == "Shootout")
		{
			shootoutModeWeaponIndexRandom = Random.Range(0, shootoutModeWeaponsLength);
			if (shootoutModeDetails.enemyGameObjectInWave.Count == 0)
			{
				SoundController.instance.playFromPool(AudioType.WaveCleared);
				StartCoroutine(InitializeShooutout());
				
			}
			
		}
	}

	public void ActivateWaveWeapon()
	{
		for (int i = 0; i < weapons.Length; i++)
		{
			weapons[i].gameObject.SetActive(false);
		}

		level[PlayerPrefs.GetInt("SelectedLevel")].wavesInLevel[_currentWaveToKeepActiveIndex].waveWeapon
			.SetActive(true);
		GameUI_HUDManager.instance.showNewGunEffect();
	}


	public void ActivateShootoutWeapon()
	{
		for (int i = 0; i < shootoutModeDetails.playerWeapon.Length; i++)
		{
			shootoutModeDetails.playerWeapon[i].gameObject.SetActive(false);
		}

		shootoutModeDetails.playerWeapon[Random.Range(1,shootoutModeDetails.playerWeapon.Length)].gameObject.SetActive(true);
//		shootoutModeDetails.playerWeapon[shootoutModeWeaponIndexRandom].gameObject.GetComponent<genericShooter>().ammo =
			//200;

		GameUI_HUDManager.instance.showNewGunEffect();
	}


	private int
		CheckEnemiesType(int i) // Method to check which enemy type is selected in the editor in a level wave so THAT particular enemy can be instantiated in its respective position using InitializeEnemies() method
	{
		if (PlayerPrefs.GetString("Mode") == "Campaign")
		{
			switch (level[PlayerPrefs.GetInt("SelectedLevel")].wavesInLevel[_currentWaveToKeepActiveIndex].enemyType[i])
			{
				case EnemyType.AlienSoldier:
					x = 0;
					return x;
				case EnemyType.TurnedSoldier:
					x = 1;
					return x;
				case EnemyType.Mutant:
					x = 2;
					return x;
				case EnemyType.Fly:
					x = 3;
					return x;
				case EnemyType.Phone:
					x = 4;
					return x;
				case EnemyType.ActionCollider:
					x = 5;
					return x;
				default:
					return x;
			}
		}

		if (PlayerPrefs.GetString("Mode") == "Shootout")
		{
			switch (shootoutModeDetails.enemytype[i])
			{
				case EnemyType.AlienSoldier:
					x = 0;
					return x;
				case EnemyType.TurnedSoldier:
					x = 1;
					return x;
			}
		}

		return x;
	}

	[Serializable]
	public enum EnemyType
	{
		AlienSoldier,
		TurnedSoldier,
		Mutant,
		Fly,
		Phone,
		ActionCollider
	}

	public enum LevelMap
	{
		Subway,
		Docks,
	}
}

[Serializable]
public class Wave
{
	[Header("____Wave Objective Text to Display____")]
	public string waveObjective;

	[Header("____Wave Weapon____")] public GameObject waveWeapon;
	[Header("____Wave Enemies____")] public Level_Spawn_Manager.EnemyType[] enemyType;

	[Header("____Wave Enemies Positions____")]
	public Transform[] enemyPosition;

	[HideInInspector] public List<Transform>
		enemiesGameObjectInWave; // Each level's waves' enemies gameobjects will be placed in this list according to their waves placement. 
}

[Serializable]
public class Level
{
	public MainMenuManager.LevelMap levelMap;

	[Header("____Level Cutscene____")] [Space(10)]
	public GameObject CutsceneGameobject;

	[Header("____Player Details____")] [Space(10)]
	public Transform playerPosition; // Add PlayerPosition for level

	[Header("____Waves Details____")] [Space(10)]
	public Wave[] waves; // Add Enemies Details in this array

	[HideInInspector] public List<Wave>
		wavesInLevel; //Total waves in each level will be added to this list to add and maintain functionality control over each wave's properties
}

[Serializable]
public class ShootoutMode
{
	public Transform playerPosition;
	public Level_Spawn_Manager.EnemyType[] enemytype;
	public Transform[] enemyPositions;
	public GameObject[] playerWeapon;
	public string waveObjective;
	public List<Transform> enemyGameObjectInWave;
}
/*// yield return new WaitForSecondsRealtime(2.5f);
ActivateShootoutWeapon();
/*StartCoroutine(GameUI_HUDManager.instance.newwaveIncoming());
waveObjectiveGameObject.text = level[PlayerPrefs.GetInt("SelectedLevel")]
	.wavesInLevel[_currentWaveToKeepActiveIndex].waveObjective;
//	yield return new WaitForSecondsRealtime(1.5f);

for (int j = 0;
	j < level[PlayerPrefs.GetInt("SelectedLevel")].wavesInLevel[_currentWaveToKeepActiveIndex].enemyType.Length;
	j++)
{
	var E = Instantiate(enemiesVariantsPrefabs[CheckEnemiesType(j)],
		level[PlayerPrefs.GetInt("SelectedLevel")].wavesInLevel[_currentWaveToKeepActiveIndex].enemyPosition[j]
			.position,
		level[PlayerPrefs.GetInt("SelectedLevel")].wavesInLevel[_currentWaveToKeepActiveIndex].enemyPosition[j]
			.rotation);
	Instantiate(initializationEffect, E.transform.position, E.transform.rotation);
	level[PlayerPrefs.GetInt("SelectedLevel")].wavesInLevel[_currentWaveToKeepActiveIndex]
		.enemiesGameObjectInWave.Add(E.transform);
	Debug.Log("Forming Enemies");
}#1#*/