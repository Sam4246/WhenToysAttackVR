using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthPosition : MonoBehaviour {

    public GameObject gun;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = gun.transform.position;
        transform.rotation = gun.transform.rotation;
	}
}
