using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {
	public Sprite upSprite;
	public Sprite downSprite;
	public Sprite leftSprite;
	public Sprite rightSprite;

	public RuntimeAnimatorController downAnimation;
	public RuntimeAnimatorController leftAnimation;
	public RuntimeAnimatorController rightAnimation;
	public RuntimeAnimatorController upAnimation;

	private SpriteRenderer mySpriteRenderer;
	private Animator myAnimator;
	private Sprite mySprite;

	public int playerNumber = 1; // 1 or 2

	// Use this for initialization
	void Start () {
		mySpriteRenderer = this.gameObject.GetComponent<SpriteRenderer> ();
		myAnimator = this.gameObject.GetComponent<Animator> ();
		mySprite = downSprite;
		print ("Hello World");
		print (mySpriteRenderer);
	}

	bool isDiagonal(bool keyDown, bool keyUp, bool keyLeft, bool keyRight) {
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
		const float stepSize = 0.03f;

		bool keyDown, keyUp, keyLeft, keyRight;


		if (playerNumber == 1) {
			keyDown = Input.GetKey(KeyCode.S);
			keyUp = Input.GetKey(KeyCode.W);
			keyLeft = Input.GetKey(KeyCode.A);
			keyRight = Input.GetKey(KeyCode.D);
		} else {
			keyDown = Input.GetKey (KeyCode.DownArrow);
			keyUp = Input.GetKey (KeyCode.UpArrow);
			keyLeft = Input.GetKey (KeyCode.LeftArrow);
			keyRight = Input.GetKey (KeyCode.RightArrow);			
		}

		bool diagonal = this.isDiagonal (keyDown, keyUp, keyLeft, keyRight);

		if (keyDown && !diagonal) {
			mySprite = downSprite;
			myAnimator.runtimeAnimatorController = downAnimation;
			position.y -= stepSize;
		}
		if (keyUp && !diagonal) {
			mySprite = upSprite;
			myAnimator.runtimeAnimatorController = upAnimation;
			position.y += stepSize;
		}
		if (keyLeft && !diagonal) {
			mySprite = leftSprite;
			myAnimator.runtimeAnimatorController = leftAnimation;
			position.x -= stepSize;
		}
		if (keyRight && !diagonal) {
			mySprite = rightSprite;
			myAnimator.runtimeAnimatorController = rightAnimation;
			position.x += stepSize;
		}


		if (!keyDown && !keyUp && !keyLeft && !keyRight) {
			myAnimator.enabled = false;
			mySpriteRenderer.sprite = mySprite;
			// pass
		} else {
			myAnimator.enabled = true;
		}

//		mySpriteRenderer.sprite = mySprite;

		this.gameObject.transform.position = position;
	}
}
