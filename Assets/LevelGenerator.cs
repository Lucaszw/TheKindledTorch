using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	// Use this for initialization
    public int torchMax = 100;
    public int torchMin = 10;
	public float collisionRadius = 2.0f;
	public float collisionSearchStep = 0.5f;
	public int maxSearches = 5;
	public int maxCollisions = 4;
	public LineRenderer lineRenderer;
	public float MIN_TORCH_DISTANCE = 0.1f; //Minimum distance torches allowed
	public Dictionary<string, bool> visitedNodes;


	private Transform[] childPositions;
	private Transform firstChild;

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
			go.name = go.name + " : " + i.ToString ();
			go.transform.parent = this.transform;
        }
        finalNumTorches = n;
        //Place Walls Between Torches
		this.initWalls();
	}

	void initWalls() {

		// Init visited nodes:
		visitedNodes = new Dictionary<string, bool>();

		// Get Child Positions:
		childPositions = this.GetComponentsInChildren<Transform> ();

		float smallestX = 1000000f;

		// Get all nodes from children via their tag
		for (int i=0;i<childPositions.Length;i++) {
			if (childPositions [i].tag == "node") {
				visitedNodes.Add (childPositions [i].name, false);
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

		// Delete all nodes that haven't been visited
		for (int i=0; i<childPositions.Length;i++){
			if (childPositions [i].tag != "node")
				continue;
			if (visitedNodes [childPositions [i].name] == true)
				continue;
		
			print ("Deleting node: " + childPositions [i].name);
			Destroy (childPositions [i].gameObject);
		}
	}

	void getNeighbours(Transform child) {
		// Mark child as visisted:
		visitedNodes[child.name] = true;

		// Get collisions with given radius
		Collider2D[] collisions = new Collider2D[100];
		ContactFilter2D filter = new ContactFilter2D();
		Vector2 position2d = new Vector2(child.position.x, child.position.y);

		int totalSearches = 0; // Number of ray traces
		int numChildren = 0; // Number of children

		Transform[] children = new Transform[maxCollisions];

		// Perform n rounds of collision checks, increasing search breadth with each round
		for (int i = 0; i < maxSearches; i++) {
			if (totalSearches > maxSearches)
				break;
			if (numChildren >= maxCollisions)
				break;

			float radius = collisionRadius + (float)i * collisionSearchStep;

			// Perform collision detection:
			int numCollisions = Physics2D.OverlapCircle (position2d, radius, filter, collisions);

			// Get children from collisions:
			for (int j = 0; j < numCollisions; j++) {
				if (numChildren + j > maxCollisions) break;
				if (collisions [j].transform.position.x > child.transform.position.x) {
					children [numChildren] = collisions [j].transform;
					numChildren++;
				}
			}

		}

		// Get children's neighbours
		for (int i = 0; i< numChildren; i++){
			LineRenderer line = Instantiate<LineRenderer> (lineRenderer) as LineRenderer;
			line.SetPosition (0, child.position);
			line.SetPosition (1, children [i].position);


			// Check if child has already been visited before continuing:
			if (visitedNodes[children[i].transform.name] == false){
				getNeighbours (children [i].transform);
			}
		}
	}
	// Update is called once per frame
	void Update () {
		//Calculate Torches

	}
    public int getNumTorches(){
        return finalNumTorches;
    }
}
