using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEdges : MonoBehaviour {
	public float collisionRadius = 2.0f;
	public LineRenderer lineRenderer;

	private Transform[] childPositions;
	private Transform firstChild;

	// Use this for initialization
	void Start () {
		// Get Child Positions:
		childPositions = this.GetComponentsInChildren<Transform> ();

		float smallestX = 1000000f;

		// Get all nodes from children via their tag
		for (int i=0;i<childPositions.Length;i++) {
			if (childPositions [i].tag == "node") {
				// Get the child with the smallest x value (to start edge generation)
				if (childPositions[i].position.x < smallestX){
					smallestX = childPositions [i].position.x;
					firstChild = childPositions [i];
				}
			}
		};

		// Get neighbours of child
		print(firstChild.name);
		this.getNeighbours(firstChild);
	}

	void getNeighbours(Transform child) {
		// Get collisions with given radius
		Collider2D[] collisions = new Collider2D[100];
		ContactFilter2D filter = new ContactFilter2D();
		Vector2 position2d = new Vector2(child.position.x, child.position.y);
		int numCollisions = Physics2D.OverlapCircle (position2d, collisionRadius, filter, collisions);

		// Filter out self and children with smaller x values
		Transform[] children = new Transform[numCollisions];
		int len = 0;
		for (int i = 0; i < numCollisions; i++) {
			if (collisions [i].transform.position.x > child.transform.position.x) {
				children [len] = collisions [i].transform;
				len++;
			}
		}

		// Print The name of children:
		for (int i = 0; i< len;i++){
			print ("prev: " + child.name + "next: " + children [i].name);
		}

		// Get childrens neighbours
		for (int i = 0; i< len;i++){
			LineRenderer line = Instantiate<LineRenderer> (lineRenderer) as LineRenderer;
			line.SetPosition (0, child.position);
			line.SetPosition (1, children [i].position);
			getNeighbours (children [i].transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
