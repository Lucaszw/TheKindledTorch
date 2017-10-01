using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickHandler : MonoBehaviour {
	public Vector2 leftOffset;
	public Vector2 rightOffset;
	public Vector2 topOffset;
	public Vector2 bottomOffset;

	private Vector3 position;

	// Use this for initialization
	void Start () {
		position = this.transform.localPosition;
	}

	public void updateLocalPosition(Vector2 offset){
		Vector3 localPosition = this.transform.localPosition;

		localPosition.x = position.x + offset.x;
		localPosition.y = position.y + offset.y;


		this.transform.localPosition = localPosition;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
