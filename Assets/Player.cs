using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {
	public Sprite upSprite;
	public Sprite downSprite;
	public Sprite leftSprite;
	public Sprite rightSprite;

	public GameObject parent;

	public RuntimeAnimatorController downAnimation;
	public RuntimeAnimatorController leftAnimation;
	public RuntimeAnimatorController rightAnimation;
	public RuntimeAnimatorController upAnimation;

	private SpriteRenderer mySpriteRenderer;
	private Animator myAnimator;
	private Sprite mySprite;
	private PlayerController playerController;

	// Use this for initialization
	void Start () {
		mySpriteRenderer = this.gameObject.GetComponent<SpriteRenderer> ();
		myAnimator = this.gameObject.GetComponent<Animator> ();
		mySprite = downSprite;
		playerController = parent.gameObject.GetComponent<PlayerController> ();
	}

	public void changeSprite(Sprite sprite, RuntimeAnimatorController animator){
		mySprite = sprite;
		myAnimator.runtimeAnimatorController = animator;
	}

	// Update is called once per frame
	void Update () {
		const float stepSize = 0.03f;

		Dictionary<string, bool> k = playerController.keys ();

		if (!k["d"] && !k["u"] && !k["l"] && !k["r"]) {
			myAnimator.enabled = false;
			mySpriteRenderer.sprite = mySprite;
			// pass
		} else {
			myAnimator.enabled = true;
		}

	}
}
