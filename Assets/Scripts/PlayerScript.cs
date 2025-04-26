using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerScript : MonoBehaviour
{
    
   	public Animator playerAnimator;

	public ItemShop lifeItemShop;

	public float speed;

	public float maxSpeed;

	public float speedJump;

	public float maxSpeedJump;

	private Rigidbody2D rigidbodyPlayer;

	private bool lookToRight = true;

	private Vector3 positionRight;

	private Vector3 positionLeft;

	private bool isOnPlatform;

	private bool isOnLadder;

	private float initialGravity;

	public float jumpMultiplyer = 1f;

	private bool isGrounded;

	public Transform footCollision;

	private LifePlayer life;

	public List<BaseMagic> magics;

	private List<BaseMagic> equipedMagics = new List<BaseMagic>();

	public int currentMagic;

	private float currentTimeToRefresh;

	public AudioSource soundAttack;

	public float manaRecoverTime;

	private float currentManaRecoverTime;

	public float totalMana;

	private float currentMana;

	private Vector3 startSizeManaBar;

	private Vector3 currentSizeManaBar;

	public Transform manaBar;

	public float specialTime;

	private bool inSpecialMode;

	private float currentSpecialTime;

	public GameObject specialGlow;

	private float currentTimeToStun;

	private bool inStun;

	public float timeToRecoverStun;

	private GameController gameController;

	private void Start()
	{
		rigidbodyPlayer = GetComponent<Rigidbody2D>();
		life = GetComponent<LifePlayer>();
		positionRight = base.transform.localScale;
		positionLeft = positionRight;
		positionLeft.x *= -1f;
		life.SetNewLife(lifeItemShop.GetNewValueByLevel(life.totalLife));
		initialGravity = rigidbodyPlayer.gravityScale;
		specialGlow.SetActive(false);
		gameController = Object.FindObjectOfType(typeof(GameController)) as GameController;
		foreach (BaseMagic magic in magics)
		{
			if (magic.itemShop.IsEquiped())
			{
				equipedMagics.Add(magic);
			}
		}
		if (equipedMagics.Count == 1)
		{
			UIController.instance.SetCanShowSwitchButton(false);
		}
		ShowIconMagic();
		currentTimeToRefresh = equipedMagics[currentMagic].timeToRefresh;
		currentMana = totalMana;
		startSizeManaBar = manaBar.localScale;
		currentSizeManaBar = manaBar.localScale;
	}

	private void Update()
	{
		if (gameController.currentState == GAME_STATE.INGAME && life.IsAlive())
		{
			if (inSpecialMode)
			{
				currentTimeToRefresh = ((!(currentTimeToRefresh < 0.5f)) ? currentTimeToRefresh : 0.5f);
				currentSpecialTime += Time.deltaTime;
				if (currentSpecialTime > specialTime)
				{
					currentSpecialTime = 0f;
					inSpecialMode = false;
					specialGlow.SetActive(false);
				}
			}
			if (currentMana < totalMana)
			{
				currentManaRecoverTime += Time.deltaTime;
				if (currentManaRecoverTime > manaRecoverTime)
				{
					currentManaRecoverTime = 0f;
					currentMana = ((!inSpecialMode) ? (currentMana + 1f) : (currentMana + 3f));
					UpdateManaUI();
				}
			}
			Vector2 vector = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal") * speed * Time.deltaTime, CrossPlatformInputManager.GetAxis("Vertical") * speed * Time.deltaTime);
			if (vector.x > 0f)
			{
				lookToRight = true;
			}
			if (vector.x < 0f)
			{
				lookToRight = false;
			}
			if (lookToRight)
			{
				base.transform.localScale = positionRight;
			}
			else
			{
				base.transform.localScale = positionLeft;
			}
			if (isOnLadder)
			{
				float y = rigidbodyPlayer.linearVelocity.y;
				if (vector.y != 0f)
				{
					y = vector.y;
				}
				rigidbodyPlayer.linearVelocity = new Vector2(vector.x, y);
			}
			else
			{
				rigidbodyPlayer.gravityScale = initialGravity;
				rigidbodyPlayer.linearVelocity = new Vector2(vector.x, rigidbodyPlayer.linearVelocity.y);
			}
			if (rigidbodyPlayer.linearVelocity.x > maxSpeed)
			{
				rigidbodyPlayer.linearVelocity = new Vector2(maxSpeed, rigidbodyPlayer.linearVelocity.y);
			}
			if (rigidbodyPlayer.linearVelocity.x < 0f - maxSpeed)
			{
				rigidbodyPlayer.linearVelocity = new Vector2(0f - maxSpeed, rigidbodyPlayer.linearVelocity.y);
			}
			playerAnimator.SetFloat("velocity", Mathf.Abs(vector.x));
			if (CrossPlatformInputManager.GetButtonDown("Jump"))
			{
				Jump();
			}
			if ((bool)Physics2D.Linecast(base.transform.position, footCollision.position, 1 << LayerMask.NameToLayer("Ground")))
			{
				isGrounded = true;
			}
			else
			{
				isGrounded = false;
				LeavePlatform();
			}
			if ((bool)Physics2D.Linecast(base.transform.position, footCollision.position, 1 << LayerMask.NameToLayer("Platform")))
			{
				isOnPlatform = true;
			}
			playerAnimator.SetBool("isGrounded", isGrounded);
			if (CrossPlatformInputManager.GetButton("Fire1") && currentTimeToRefresh > equipedMagics[currentMagic].timeToRefresh && !inStun && equipedMagics[currentMagic].itemShop.IsEquiped() && currentMana >= equipedMagics[currentMagic].ManaRequest())
			{
				GameObject gameObject = Object.Instantiate(equipedMagics[currentMagic].gameObject, base.transform.position, base.transform.rotation);
				gameObject.GetComponent<BaseMagic>().SetDirection(lookToRight);
				currentTimeToRefresh = 0f;
				playerAnimator.SetTrigger("attack");
				soundAttack.clip = gameObject.GetComponent<BaseMagic>().launchSound;
				soundAttack.Play();
				currentMana -= equipedMagics[currentMagic].ManaRequest();
				UpdateManaUI();
			}
			if (CrossPlatformInputManager.GetButtonDown("Switch"))
			{
				SwitchMagic();
			}
			currentTimeToRefresh += Time.deltaTime;
			if (inStun)
			{
				currentTimeToStun += Time.deltaTime;
				if (currentTimeToStun > timeToRecoverStun)
				{
					inStun = false;
					currentTimeToStun = 0f;
				}
			}
		}
		else
		{
			Vector2 velocity = rigidbodyPlayer.linearVelocity;
			velocity.x = 0f;
			rigidbodyPlayer.linearVelocity = velocity;
		}
	}

	public void Jump()
	{
		if (isGrounded || jumpMultiplyer != 1f)
		{
			rigidbodyPlayer.linearVelocity = new Vector2(rigidbodyPlayer.linearVelocity.x, speedJump * jumpMultiplyer);
			playerAnimator.SetTrigger("jump");
		}
		jumpMultiplyer = 1f;
	}

	public void ApplyStun()
	{
		inStun = !inSpecialMode;
	}

	private void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Platform") && isOnPlatform)
		{
			base.transform.SetParent(other.transform.parent, true);
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Platform") && isOnPlatform)
		{
			base.transform.SetParent(other.transform.parent, true);
		}
		if (!other.gameObject.CompareTag("Platform"))
		{
			isOnPlatform = false;
		}
		if (other.gameObject.CompareTag("PlatformDestructible"))
		{
			other.gameObject.GetComponent<Animator>().SetTrigger("Destroy");
		}
		if (!other.gameObject.CompareTag("Potion"))
		{
			return;
		}
		Potion component = other.gameObject.GetComponent<Potion>();
		if (component != null)
		{
			if (component.typePotion == POTION_TYPE.Life)
			{
				life.AddLife(component.amount);
			}
			else if (component.typePotion == POTION_TYPE.Mana)
			{
				currentMana += component.amount;
				if (currentMana > totalMana)
				{
					currentMana = totalMana;
				}
				UpdateManaUI();
			}
			else if (component.typePotion == POTION_TYPE.Special)
			{
				specialTime = component.amount;
				inSpecialMode = true;
				currentSpecialTime = 0f;
				specialGlow.SetActive(true);
			}
		}
		Object.Destroy(other.gameObject);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Gold"))
		{
			Object.Destroy(other.gameObject, 0.8f);
			other.GetComponent<Collider2D>().enabled = false;
			other.GetComponent<Gold>().isCollected = true;
			ApplicationController.AddGold();
		}
		if (other.gameObject.CompareTag("Hole"))
		{
			life.ApplyDamage(life.totalLife);
			GetComponent<Collider2D>().isTrigger = true;
		}
		if (other.gameObject.CompareTag("Trampoline"))
		{
			Trampoline component = other.GetComponent<Trampoline>();
			jumpMultiplyer = component.jumpMultiplyerTramp;
			component.PlayJumpAnimation();
			Jump();
		}
		if (other.gameObject.CompareTag("DeathZone"))
		{
			life.ApplyDamage(life.totalLife);
		}
		if (other.gameObject.CompareTag("DangerObject"))
		{
			life.ApplyDamage(other.GetComponent<DangerObject>().damage);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Ladder"))
		{
			isOnLadder = false;
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Ladder"))
		{
			isOnLadder = true;
		}
	}

	private void LeavePlatform()
	{
		base.transform.SetParent(null);
	}

	public void SwitchMagic()
	{
		if (currentMagic < equipedMagics.Count - 1)
		{
			currentMagic++;
		}
		else
		{
			currentMagic = 0;
		}
		ShowIconMagic();
	}

	private void ShowIconMagic()
	{
		UIController.instance.SetIconMagic(equipedMagics[currentMagic].itemShop.icon);
	}

	private void UpdateManaUI()
	{
		currentSizeManaBar.x = currentMana * startSizeManaBar.x / totalMana;
		if (currentSizeManaBar.x < 0f)
		{
			currentSizeManaBar.x = 0f;
		}
		manaBar.localScale = currentSizeManaBar;
	}
}
