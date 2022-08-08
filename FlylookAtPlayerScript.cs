using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlylookAtPlayerScript : MonoBehaviour
{
    private GameObject player;
    private void Awake()
    {
        player = FindObjectOfType<playercontroller>().transform.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
    }
}
