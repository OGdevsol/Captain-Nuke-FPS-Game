using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunProjectile : MonoBehaviour
{
    public float speed = 100f;
    public GameObject explosion;
    public float waitTime = 2.0f;
    
    
    void Start () {
        Destroy (gameObject, waitTime);
	
    }
    void Update()
    {
        transform.Translate(Vector3.forward*100f*Time.deltaTime);
    }
    
}
