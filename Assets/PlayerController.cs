using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public int playerNumber = 1; // 1 or 2
	public float stepSize = 0.03f;
	public GameObject characterGameobject;
	public GameObject stickGameobject;

	private Player myPlayer;
	private StickHandler myStick;

	// Use this for initialization
	void Start () {
		myPlayer = characterGameobject.GetComponent<Player> ();
		myStick = stickGameobject.GetComponent<StickHandler> ();
	}

	public Dictionary<string, bool> keys() {
		Dictionary<string, bool> k = new Dictionary<string, bool> ();

		if (this.playerNumber == 1) {
			k.Add("d", Input.GetKey(KeyCode.S));
			k.Add("u", Input.GetKey(KeyCode.W));
			k.Add("l", Input.GetKey(KeyCode.A));
			k.Add("r", Input.GetKey(KeyCode.D));
		} else {
			k.Add("d", Input.GetKey (KeyCode.DownArrow));
			k.Add("u", Input.GetKey (KeyCode.UpArrow));
			k.Add("l", Input.GetKey (KeyCode.LeftArrow));
			k.Add("r", Input.GetKey (KeyCode.RightArrow));			
		}
		return k;
	}

	public bool isDiagonal(bool keyDown, bool keyUp, bool keyLeft, bool keyRight) {
		if (keyDown && keyLeft)
			return true;
		if (keyDown && keyRight)
			return true;
		if (keyUp && keyLeft)
			return true;
		if (keyUp && keyRight)
			return true;
		return false;
	}

	// Update is called once per frame
	void Update () {
		Vector3 position = this.gameObject.transform.position;
		Dictionary<string, bool> k = this.keys ();
		bool diagonal = this.isDiagonal (k["d"], k["u"], k["l"], k["r"]);

		if (k["d"] && !diagonal) {
			position.y -= stepSize;
			myPlayer.changeSprite (myPlayer.downSprite, myPlayer.downAnimation);
			myStick.updateLocalPosition (myStick.bottomOffset);
		}
		if (k["u"] && !diagonal) {
			position.y += stepSize;
			myPlayer.changeSprite (myPlayer.upSprite, myPlayer.upAnimation);
			myStick.updateLocalPosition (myStick.topOffset);
		}
		if (k["l"] && !diagonal) {
			position.x -= stepSize;
			myPlayer.changeSprite (myPlayer.leftSprite, myPlayer.leftAnimation);
			myStick.updateLocalPosition (myStick.leftOffset);
		}
		if (k["r"] && !diagonal) {
			position.x += stepSize;
			myPlayer.changeSprite (myPlayer.rightSprite, myPlayer.rightAnimation);
			myStick.updateLocalPosition (myStick.rightOffset);
		}

		this.gameObject.transform.position = position;
	}
}
