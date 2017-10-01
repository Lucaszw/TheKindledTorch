using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	// Use this for initialization
    int mapWidth = 100;
    int mapHeight = 100;
    double WALL_TRIGGER = 0.5;
	
    public GameObject torchprefab;

    void Start () {
        //TODO:Generate Random Seed
		SimplexNoise.initSimplexNoise();
        for (int i = 0; i < mapWidth; i++){
            for(int j = 0; j < mapHeight; j++){
                //Generate Corridors
            
                //Debug.Log(SimplexNoise.noise((double)i/mapWidth,(double)j/mapHeight));
                //Generate Torch placement
                double sn = SimplexNoise.noise((double)i/mapWidth, (double)j/mapHeight); 
                //if (sn < 0){
                    if (Random.Range(0f,1.0f) * sn > 0){
                        Vector3 pos = new Vector3(-5.0f,-2.0f, 240.0f);
                        Debug.Log(pos);
                        GameObject go = Instantiate(torchprefab,pos,Quaternion.identity);
                        Debug.Log(go);
                    }
                //}
            }

        }
	}
	
	// Update is called once per frame
	void Update () {
		//Calculate Torches

	}
}
