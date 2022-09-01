using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLoader : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(LoadMainMenu());

    }

    private IEnumerator LoadMainMenu()
    {
        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadScene("MainMenuScene");
    }
}
