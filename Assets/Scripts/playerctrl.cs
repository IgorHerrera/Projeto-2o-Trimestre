using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerctrl : MonoBehaviour {

	public float HorizontalSpeed = 10f;
	public float JumpSpeed = 300f;
	Rigidbody2D rb;
	SpriteRenderer sr;
    Animator anim;

    bool isJumping = false;



	// Use this for initialization
	void Start () {
		
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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

        if (!isJumping)
        {
            anim.SetInteger("State", 1);
        }
    }

    void StopMovingHorizontal() {
		rb.velocity = new Vector2(0f, rb.velocity.y);
        if (!isJumping)
        {
            anim.SetInteger("State", 0);
        }
    }

    void ShowFalling()
    {
        if (rb.velocity.y < 0f)
        {
            anim.SetInteger("State", 4);
        }
    }

	void Jump() {
        isJumping = true;
        rb.AddForce(new Vector2(0f, JumpSpeed));
        anim.SetInteger("State", 5);
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            isJumping = false;
        }
    }

}
