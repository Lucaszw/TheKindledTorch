using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //TODO:Generate Random Seed
		SimplexNoise.initSimplexNoise();
        //Generate Corridors
        Debug.Log(SimplexNoise.noise(0.1,0.1));
        //Generate Torch placement
	}
	
	// Update is called once per frame
	void Update () {
		//Calculate Torches

	}
}
