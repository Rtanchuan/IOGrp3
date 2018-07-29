using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class PlayerController : MonoBehaviour {

//public AudioClip JumpFx;

		public float MaxSpeed = 6f;
		public float JumpSpeed = 14f;
		public LayerMask WhatIsGround;
		public Transform GroundCheck;

		public delegate void PlayerDeathDelegate();
		public static event PlayerDeathDelegate PlayerDeathEvent;

		private const float GROUND_CHECK_RADIUS = 0.02f;

		private bool _grounded = false;
		private bool isgrounded = false;
		private bool _jumping = false;
		private bool _facingRight = true;
		private bool _doubleJump = true;
		private float _xVel;
		private float _yVel;
		private Collider2D _collider;
		private Rigidbody2D _groundRigidBody;
		//private Animator _anim;
		private bool _playerDied = false;

		private void Awake () {
			//_anim = GetComponent<Animator> ();
		}

		private void Update () {
			if (Input.GetKeyDown(KeyCode.Space) && isgrounded==true) {
				_jumping = true;
				
			}
		}
		 //make sure u replace "floor" with your gameobject name.on which player is standing
 void OnCollisionEnter(Collision theCollision)
 {
     if (theCollision.gameObject.tag == "floor")
     {
         isgrounded = true;
         EditorUtility.DisplayDialog ("Title here", "Your text", "Ok");
     }
 }
 
 //consider when character is jumping .. it will exit collision.
 void OnCollisionExit(Collision theCollision)
 {
     if (theCollision.gameObject.tag == "floor")
     {
         isgrounded = false;
         EditorUtility.DisplayDialog ("Title here", "Your text", "Ok");
     }
 }

		public void StartPlayerDeath() {
			Debug.Log("StartPlayerDeath called...");
			if(!_playerDied){
				_playerDied = true;
				if(PlayerDeathEvent != null) {
					PlayerDeathEvent();
				}

			}
		}

		private void FixedUpdate () {
			_xVel = GetComponent<Rigidbody2D>().velocity.x;
			_yVel = GetComponent<Rigidbody2D>().velocity.y;

//			_grounded = IsGrounded ();

			if (_groundRigidBody != null && !_grounded) {
				_groundRigidBody = null;
			}
			// Process Horizontal
			if (Input.GetKey(KeyCode.RightArrow)) {
				_xVel = 1 * MaxSpeed;
			} else if (Input.GetKey(KeyCode.LeftArrow)) {
				_xVel = -1 * MaxSpeed;
			} else {
				_xVel = 0;
			}
//			if ((_xVel > 0 && !_facingRight) || (_xVel < 0 && _facingRight)) {
//				Flip ();
//			}	
			_xVel += PlatformVelocity ().x;

			// Process Vertical
//			if (isgrounded) {
//				_yVel = PlatformVelocity ().y - 0.01f; // maintain velocity of platform, with slight downward pressure to keep the collision.
//				_doubleJump = true;
//			}
			if (_jumping && isgrounded) {
				_yVel = JumpSpeed;
				//AudioPlayer.Instance.PlaySfx (JumpFx);
			} //else if (_jumping && _doubleJump) {
//				_yVel = JumpSpeed;
//				_doubleJump = false;
//				//AudioPlayer.Instance.PlaySfx (JumpFx);
//			}
			_jumping = false;
//
			GetComponent<Rigidbody2D>().velocity = new Vector2 (_xVel, _yVel);		
//			UpdateAnimationParams();
		}

//		private bool IsGrounded () {
//			_collider = Physics2D.OverlapCircle (GroundCheck.position, GROUND_CHECK_RADIUS, WhatIsGround);
//			if (_collider != null) {
//				_groundRigidBody = _collider.gameObject.GetComponent<Rigidbody2D> ();
//				return true;
//			} else {
//				return false;
//			}
//		}

		private Vector2 RelativeVelocity () {
			return GetComponent<Rigidbody2D>().velocity - PlatformVelocity ();
		}

		private Vector2 PlatformVelocity () {
			return (_groundRigidBody == null) ? Vector2.zero : _groundRigidBody.velocity;
		}

//		private void Flip () {
//			_facingRight = !_facingRight;
//			Vector3 scale = transform.localScale;
//			scale.x *= -1;
//			transform.localScale = scale;
//		}

//		private void UpdateAnimationParams() {
//			_anim.SetFloat ("HorizontalSpeed", Mathf.Abs (RelativeVelocity ().x));
//			_anim.SetFloat ("VerticalSpeed", RelativeVelocity ().y);
//			_anim.SetBool ("Ground", _grounded);
//		}
	
}
