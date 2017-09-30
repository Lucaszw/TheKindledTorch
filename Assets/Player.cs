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

	// Use this for initialization
	void Start () {
		mySpriteRenderer = this.gameObject.GetComponent<SpriteRenderer> ();
		myAnimator = this.gameObject.GetComponent<Animator> ();
		mySprite = downSprite;
		print ("Hello World");
		print (mySpriteRenderer);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = this.gameObject.transform.position;
		const float stepSize = 0.03f;

		bool keyDown = Input.GetKey (KeyCode.DownArrow);
		bool keyUp = Input.GetKey (KeyCode.UpArrow);
		bool keyLeft = Input.GetKey (KeyCode.LeftArrow);
		bool keyRight = Input.GetKey (KeyCode.RightArrow);

		if (keyDown) {
			mySprite = downSprite;
			myAnimator.runtimeAnimatorController = downAnimation;
			position.y -= stepSize;
		}
		if (keyUp) {
			mySprite = upSprite;
			myAnimator.runtimeAnimatorController = upAnimation;
			position.y += stepSize;
		}
		if (keyLeft) {
			mySprite = leftSprite;
			myAnimator.runtimeAnimatorController = leftAnimation;
			position.x -= stepSize;
		}
		if (keyRight) {
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
