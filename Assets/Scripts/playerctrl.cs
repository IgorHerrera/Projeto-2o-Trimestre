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

    public Transform feet;

    public float feetWidth = 0.5f;
    public float feetHeigth = 0.1f;

    public bool isGrounded;
    public LayerMask whatisGround;

	// Use this for initialization
	void Start () {
		
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
	}

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(feet.position, new Vector3(feetWidth, feetHeigth, 0.5f));
    }


    // Update is called once per frame
    void Update () {

        if (transform.position.y < GM.instance.yMinLive)
        {
            GM.instance.KillPlayer();
        }

        isGrounded = Physics2D.OverlapBox(new Vector2(feet.position.x, feet.position.y), new Vector2(feetWidth, feetHeigth), 360.0f, whatisGround);
		
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
        if (isGrounded) {
        isJumping = true;
            AudioManager.instance.PlayJumpSound(gameObject);
        rb.AddForce(new Vector2(0f, JumpSpeed));
        anim.SetInteger("State", 5);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            isJumping = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            AudioManager.instance.PlayCoinPickupSound(other.gameObject);
            SFxManager.instance.ShowCoinParticles(other.gameObject);
            GM.instance.IncrementCoinCount();
            Destroy(other.gameObject);
        }
    }

}
