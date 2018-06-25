﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerctrl : MonoBehaviour {

	public float HorizontalSpeed = 10f;
	public float JumpSpeed = 300f;
	Rigidbody2D rb;
	SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
		
		float HorizontalInput = Input.GetAxisRaw("Horizontal");
		float HorizontalPlayerSpeed = HorizontalSpeed * HorizontalInput;
		if (HorizontalPlayerSpeed != 0) {
			MoveHorizontal(HorizontalPlayerSpeed);
		}
		else {
			StopMovingHorizontal();
		}

		if (Input.GetButtonDown("Jump")) {
			Jump();
		}
	}

	void MoveHorizontal (float speed) {
		rb.velocity = new Vector2(speed, rb.velocity.y);

		if (speed < 0f) {
			sr.flipX = true;
		}
		else if (speed > 0f) {
			sr.flipX = false;
		}
	}

	void StopMovingHorizontal() {
		rb.velocity = new Vector2(0f, rb.velocity.y);

	}

	void Jump() {
		rb.AddForce(new Vector2(0f, JumpSpeed));

	}
}