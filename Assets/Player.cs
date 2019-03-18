using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed = 10f;
	private Rigidbody rb;
	private NetworkView nw;

	private float lastSyncTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;

	// Move player based on input
	void InputMovement() {
		if (Input.GetKey(KeyCode.W))
      rb.MovePosition(rb.position + Vector3.forward * speed * Time.deltaTime);

    if (Input.GetKey(KeyCode.S))
      rb.MovePosition(rb.position - Vector3.forward * speed * Time.deltaTime);

    if (Input.GetKey(KeyCode.D))
      rb.MovePosition(rb.position + Vector3.right * speed * Time.deltaTime);

    if (Input.GetKey(KeyCode.A))
      rb.MovePosition(rb.position - Vector3.right * speed * Time.deltaTime);
	}

	// Serialize function
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		Vector3 syncPosition = Vector3.zero;
		Vector3 syncVelocity = Vector3.zero;
		if (stream.isWriting) {
			syncPosition = rb.position;
			stream.Serialize(ref syncPosition);

			syncVelocity = rb.velocity;
			stream.Serialize(ref syncVelocity);
		} else {
			stream.Serialize(ref syncPosition);
			stream.Serialize(ref syncVelocity);

			syncTime = 0f;
			syncDelay = Time.time - lastSyncTime;
			lastSyncTime = Time.time;

			syncStartPosition = rb.position;
			syncEndPosition = syncPosition + syncVelocity * syncDelay;
		}
	}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		nw = GetComponent<NetworkView>();
	}

	// Update is called once per frame
	void Update () {
		if (nw.isMine)
			InputMovement();
		else
			SyncedMovement();
	}

	private void SyncedMovement() {
		syncTime += Time.deltaTime;
		rb.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime/syncDelay);
	}

}
