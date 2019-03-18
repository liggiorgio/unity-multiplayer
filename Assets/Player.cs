﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed = 10f;
	private Rigidbody rb;
	private NetworkView nw;

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

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		nw = GetComponent<NetworkView>();
	}

	// Update is called once per frame
	void Update () {
		if (nw.isMine)
			InputMovement();
	}

}
