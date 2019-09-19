using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// store a public reference to the Player game object, so we can refer to it's Transform
	public GameObject player;

	// Store a Vector3 offset from the player (a distance to place the camera from the player at all times)
	private Vector3 offset;
	private float cameraYOffset = 1.2f;

	// At the start of the game..
	void Start ()
	{
		int scale = GameObject.FindGameObjectsWithTag("Environment Spawner")[0].GetComponent<EnvSpawner>().scale;
		// Create an offset by subtracting the Camera's position from the player's position
		transform.position = new Vector3(transform.position.x, (cameraYOffset * scale) * transform.position.y, transform.position.z);
		transform.LookAt(player.transform.position);
		offset = transform.position - player.transform.position;

	}

	// After the standard 'Update()' loop runs, and just before each frame is rendered..
	void LateUpdate ()
	{
		transform.position = player.transform.position + offset;
	}
}