using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	// Use this for initialization
    public float torchMax = 100;
    public float torchMin = 10;
    float MIN_TORCH_DISTANCE = 0.5f; //Minimum distance torches allowed


    float mapWidthMin = -10; float mapWidthMax = 10;
    float mapHeightMin = -5; float mapHeightMax = 5;
    
    public GameObject torchprefab;

    int finalNumTorches = 0;

    void Start () {
        //Randomly Generate Torches
        int numTorches = (int)Random.Range(torchMin, torchMax);    
        Vector3[] torchArray = new Vector3[numTorches]; //All Torch Positions
        int n = 0;
        for (n = 0; n < numTorches; n++){
            float x = Random.Range(mapWidthMin,mapWidthMax);
            float y = Random.Range(mapHeightMin,mapHeightMax);
            Vector3 newVec = new Vector3(x,y,0f);

            bool placementAllowed = true;
            for (int n2 = 0; n2 < n; n2++){
                if (Vector3.Distance(newVec, torchArray[n2]) <
                MIN_TORCH_DISTANCE) placementAllowed = false;
            }
            if (placementAllowed == true){
                torchArray[n] = new Vector3(x,y,0f);
            }
            else{n-=1;}
        }
        for (int i = 0; i < numTorches; i++){
            GameObject go = Instantiate(torchprefab,torchArray[i],Quaternion.identity);
        }
        finalNumTorches = n;
        //Place Walls Between Torches

	}
	
	// Update is called once per frame
	void Update () {
		//Calculate Torches

	}
    public int getNumTorches(){
        return finalNumTorches;
    }
}
