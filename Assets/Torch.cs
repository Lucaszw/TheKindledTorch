using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour {

    public bool torchState = false;
    public Vector3 pos; //This is randomly generated on initialization
    public float torchWork = 0;

	// Use this for initialization
	void Start () {
		//TODO:Get list of potential placement locations
        //Vector3[] locArray = {new Vector3(0f,0f,0f)}; 
        //int arrayLength = locArray.Length;
        //Random number
        //int placementIndex = Random.Range(0, arrayLength - 1);
        //Place torch
        //TODO:HARD CODED FOR TESTING
        //pos = locArray[placementIndex];
        //transform.position = pos;
	}


    // Run to initialize when it appears on screen
    void Awake() {

    }
	
	// Update is called once per frame
	void Update () {
        //Check torchWork, to determine what the state should be
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (torchWork >= 100 && !torchState){
            torchState = true;
            //TODO: Update graphics to reflect torch state
            renderer.color = new Color(1f,1f,1f,1f);
        }
        else if (torchWork <= 0 && torchState){
            torchState = false;
            //TODO: Update graphics to reflect torch state
            renderer.color = new Color(0f,0f,0f,1f);
        }

		/*if (torchState == true){
            //TODO: Update graphics to reflect torch state
            
        }

        else (torchState == false){
            //TODO: Update graphics to reflect torch state
        }*/
	}

    public Vector3 getPosition(){
        return pos;
    }
    
    public bool isLit(){
        return torchState;
    }

    public float workCompleted(){
        return torchWork;
    }
    
    public void lightTorch(float rate){ //% lit/second
        torchWork += rate * Time.deltaTime;
        if (torchWork >= 100){
            torchWork = 100;
        }
        if (torchWork <= 0){
            torchWork = 0;
        }
        
    }

    public void debugToggleTorch(){
        if (torchState){
            lightTorch(-10000); //Instantaneous unlight
        }
        else{
            lightTorch(10000); //Instantaneous light
        }
    }
}
