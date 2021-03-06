using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private enum AnimState
    {
        Stand,
        Run,
        Jump,
        DoubleJump,
        Fall,
        Dash,
        Attack
    }

    private const float runAcceleration = 15;
    public const float maxRunSpeed = 7;
    private const float jumpForce = 8;
    private const float doubleJumpForce = 8;
    private const float gravityForce = 15;
    private const float maxFallSpeed = 20;
    private const float dashForce = 20;
    private const float dashTime = 0.25f;
    private const float groundForceFriction = 1;
    private const float hurtInvincibleTime = 1.0f;
    private const int maxHurtFlickerFrames = 5;
    private const float pitchVariation = 0.15f;
    private const float attackDistance = 0.9f;

    private Rigidbody2D rb;
    private EdgeCollider2D ec;

    private bool triggerWasHeld = false;
    private bool jumpQueued = false;
    private bool attackQueued = false;
    private bool dashQueued = false;
    private bool canDoubleJump = true;
    private bool canDash = true;
    private float dashCountdown = 0;
    private float currentDashForce = 0;
    private float xForce = 0;

    private bool canJump = false;
    private bool wasOnGround = false;
    private Coroutine crtCancelQueuedJump;
    private const float jumpBufferTime = 0.1f; //time before hitting ground a jump will still be queued
    private const float jumpGraceTime = 0.1f; //time after leaving ground player can still jump (coyote time)

    private bool hurtInvincible = false;
    private float hurtInvincibleTimer = 0;
    private int hurtFlickerFrames = 0;

    private bool finishedLevel = false;
    private bool disabledAttack = false;
    private bool invincible = false;

    private CameraShake cameraShake;
    private Persistent persistent;

    private int maxHearts = 3;
    private int hearts;

    private float stamina = 1;
    private const float staminaAttackCost = 0.5f;
    private float[] staminaRechargeSpeeds = new float[] { 0.01f, 0.02f, 0.03f, 0.04f };
    private bool staminaRecharging = false;

    private const float runFrameTime = 0.1f;
    private SpriteRenderer sr;
    private AnimState animState = AnimState.Stand;
    private int animFrame = 0;
    private float frameTime; //max time of frame
    private float frameTimer; //goes from frameTime down to 0
    public bool facingLeft = false; //for animation (images face right)
    public Sprite standSprite;
    public Sprite jumpSprite;
    public Sprite doubleJumpSprite;
    public Sprite fallSprite;
    public Sprite attackSprite;
    public Sprite dashSprite;
    public Sprite[] runSprites;

    private AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip doubleJumpSound;
    public AudioClip landSound;
    public AudioClip dashSound;
    public AudioClip[] attackSounds;
    public AudioClip staminaFailSound;
    public AudioClip hurtSound;
    public AudioClip collectGemSound;
    public AudioClip collectHeartSound;

    public GameObject attackPrefab;
    private GameObject spawnedAttack;

    public GameObject collectParticlePrefab;
    public GameObject hurtParticlePrefab;
    public GameObject playerKilledPrefab;
    public GameObject loadingScreenPrefab;

    [HideInInspector]
    public int levelGems = 0;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        ec = gameObject.GetComponent<EdgeCollider2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        audioSource = gameObject.GetComponent<AudioSource>();
        cameraShake = FindObjectOfType<CameraShake>();

        Persistent[] persistents = FindObjectsOfType<Persistent>();
        foreach (Persistent p in persistents)
        {
            if (!p.destroying)
            {
                persistent = p;
                break;
            }
        }

        if (persistent.sacrificedHearts)
		{
            maxHearts = 2;
		}
        hearts = maxHearts;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            TryStopCoroutine(crtCancelQueuedJump);
            jumpQueued = true;
            crtCancelQueuedJump = StartCoroutine(CancelQueuedJump());
        }

        if (Input.GetButtonDown("Dash"))
        {
            dashQueued = true;
        }

        if (Input.GetButtonDown("Attack") && FindObjectOfType<Dialog>() == null && !disabledAttack)
        {
            attackQueued = true;
        }

        bool triggerHeld = Input.GetAxis("LTrigger") > 0 || Input.GetAxis("RTrigger") > 0;
        bool triggerPressed = !triggerWasHeld && triggerHeld;
        if (triggerPressed)
        {
            //TODO any trigger action?
        }
        triggerWasHeld = triggerHeld;

        /*if (Input.GetKeyDown(KeyCode.N))
		{
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
        }*/

        if (hurtInvincible)
		{
            hurtFlickerFrames++;
            if (hurtFlickerFrames >= maxHurtFlickerFrames)
            {
                sr.enabled = !sr.enabled;
                hurtFlickerFrames = 0;
            }
		}
        else
		{
            sr.enabled = true;
		}

        sr.flipX = facingLeft;
        AdvanceAnim();
        sr.sprite = GetAnimSprite();
    }

    private Collider2D RaycastTiles(Vector2 startPoint, Vector2 endPoint)
    {
        RaycastHit2D hit = Physics2D.Raycast(startPoint, endPoint - startPoint, Vector2.Distance(startPoint, endPoint), LayerMask.GetMask("Tiles"));
        return hit.collider;
    }

    private bool CheckSide(int point0, int point1, Vector2 direction)
    {
        Vector2 startPoint = rb.position + ec.points[point0] + direction * 0.02f;
        Vector2 endPoint = rb.position + ec.points[point1] + direction * 0.02f;
        Collider2D collider = RaycastTiles(startPoint, endPoint);
        return collider != null;
    }

    private void FixedUpdate()
    {
        float xInput = Input.GetAxis("Horizontal");
        float prevXVel = rb.velocity.x;
        float xVel;
        float dx = runAcceleration * Time.fixedDeltaTime * xInput;
        if (prevXVel != 0 && Mathf.Sign(xInput) != Mathf.Sign(prevXVel))
        {
            xVel = 0;
        }
        else
        {
            xVel = prevXVel + dx;
            float speedCap = Mathf.Abs(xInput * maxRunSpeed);
            xVel = Mathf.Clamp(xVel, -speedCap, speedCap);
        }

        if (xForce != 0)
        {
            //if not moving: keep xForce
            if (xInput == 0)
            {
                xVel = xForce;
            }
            else
            {
                if (Mathf.Sign(xInput) == Mathf.Sign(xForce))
                {
                    //moving in same direction
                    if (Mathf.Abs(xVel) >= Mathf.Abs(xForce))
                    {
                        //xVel has higher magnitude: set xForce to 0 (replace little momentum push)
                        xForce = 0;
                    }
                    else
                    {
                        //xForce has higher magnitude: set xVel to xForce (pushed by higher momentum)
                        xVel = xForce;
                    }
                }
                else
                {
                    //moving in other direction
                    //decrease xForce by dx (stopping at 0)
                    float prevSign = Mathf.Sign(xForce);
                    xForce += dx;
                    if (Mathf.Sign(xForce) != prevSign)
                    {
                        xForce = 0;
                    }
                    xVel = xForce;
                }
            }
        }

        if (xInput != 0)
        {
            facingLeft = xInput < 0;
        }
        else if (xVel != 0)
        {
            //facingLeft = xVel < 0;
        }

        float yVel;

        bool onGround = CheckSide(4, 3, Vector2.down); //BoxcastTiles(Vector2.down, 0.15f) != null;
        bool onCeiling = CheckSide(1, 2, Vector2.up); //BoxcastTiles(Vector2.up, 0.15f) != null;

        if (onGround)
        {
            canJump = true;
            canDoubleJump = true;

            if (!wasOnGround || dashCountdown == 0)
            {
                canDash = true;
            }

            if (xForce != 0)
            {
                xForce *= groundForceFriction;
                if (Mathf.Abs(xForce) < 0.05f)
                {
                    xForce = 0;
                }
            }

            if (rb.velocity.y < 0)
            {
                PlaySound(landSound);
            }
            yVel = 0;

            SetAnimState(xVel == 0 ? AnimState.Stand : AnimState.Run);
        }
        else
        {
            yVel = Mathf.Max(rb.velocity.y - gravityForce * Time.fixedDeltaTime, -maxFallSpeed);

            if (wasOnGround)
            {
                StartCoroutine(LeaveGround());
            }

            if (yVel < 0)
            {
                SetAnimState(AnimState.Fall);
            }
        }
        wasOnGround = onGround;

        if (onCeiling && yVel > 0)
        {
            yVel = 0;
            PlaySound(landSound);
        }

        //if on ground or just left it: first jump
        //if can double jump: second jump
        //else: keep queued
        if (jumpQueued)
        {
            if (canJump)
            {
                StopCancelQueuedJump();
                jumpQueued = false;
                canJump = false;
                dashCountdown = 0;
                xForce = 0;
                yVel = jumpForce; //Mathf.Max(jumpForce, yVel + jumpForce);
                PlaySound(jumpSound);
                SetAnimState(AnimState.Jump);
            }
            else if (canDoubleJump && !persistent.sacrificedDoubleJump)
            {
                StopCancelQueuedJump();
                jumpQueued = false;
                canDoubleJump = false;
                dashCountdown = 0;
                xForce = 0;
                yVel = doubleJumpForce; //Mathf.Max(doubleJumpForce, yVel + doubleJumpForce);
                PlaySound(doubleJumpSound);
                SetAnimState(AnimState.DoubleJump);
            }
        }

        if (dashQueued)
        {
            dashQueued = false;
            if (canDash && !persistent.sacrificedDash)
            {
                canDash = false;
                dashCountdown = dashTime;
                currentDashForce = dashForce * (facingLeft ? -1 : 1);
                xForce = currentDashForce;
                yVel = 0;
                PlaySound(dashSound);
                SetAnimState(AnimState.Dash);
            }
        }

        if (dashCountdown > 0)
        {
            yVel = 0;
            dashCountdown -= Time.fixedDeltaTime;
            if (dashCountdown < Time.fixedDeltaTime)
            {
                dashCountdown = 0;
                xForce = 0;
            }
            else
            {
                //xForce = Mathf.Lerp(0, currentDashForce, dashCountdown / dashTime);
            }
        }

        Vector2 vel = new Vector2(xVel, yVel);
        rb.velocity = vel;
        rb.MovePosition(rb.position + vel * Time.fixedDeltaTime);

        if (hurtInvincible)
        {
            hurtInvincibleTimer += Time.fixedDeltaTime;
            if (hurtInvincibleTimer >= hurtInvincibleTime)
			{
                hurtInvincible = false;
			}
        }

        if (attackQueued)
        {
            Attack();
		}
        attackQueued = false;
        if (spawnedAttack != null)
		{
            Vector3 attackPos = transform.position + (attackDistance * (facingLeft ? Vector3.left : Vector3.right));
            spawnedAttack.transform.position = attackPos;
            spawnedAttack.transform.localScale = new Vector3(facingLeft ? -1 : 1, 1, 1);
        }

        if (stamina < 1)
        {
            stamina += staminaRechargeSpeeds[persistent.NumSacrifices()];
            if (stamina >= 1)
			{
                stamina = 1;
                staminaRecharging = false;
            }
        }
    }

    private void Attack()
	{
        if (spawnedAttack != null)
		{
            return;
		}
        if (staminaRecharging)
		{
            PlaySound(staminaFailSound);
            return;
		}
        PlaySound(attackSounds[persistent.NumSacrifices()]);
        //attack.SetActive(true);
        //attack.transform.localPosition = new Vector3(facingLeft ? -1 : 1, 0, 0);
        //attack.transform.localScale = new Vector3(facingLeft ? -1 : 1, 1, 1);
        Vector3 attackPos = transform.position + (attackDistance * (facingLeft ? Vector3.left : Vector3.right));
        spawnedAttack = Instantiate(attackPrefab, attackPos, Quaternion.identity);
        spawnedAttack.transform.localScale = new Vector3(facingLeft ? -1 : 1, 1, 1);
        Attack spawnedAttackScript = spawnedAttack.GetComponent<Attack>();
        spawnedAttackScript.Activate(persistent.NumSacrifices());
        SetAnimState(AnimState.Attack);
        frameTimer = spawnedAttackScript.decayTime;
        stamina -= staminaAttackCost;
        if (stamina <= 0)
		{
            stamina = 0;
            staminaRecharging = true;
		}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!gameObject.activeSelf || finishedLevel) return;

        GameObject collider = collision.collider.gameObject;

        if (collider.CompareTag("BarrierRight"))
		{
            persistent.gems += levelGems;
            levelGems = 0;
            finishedLevel = true;
            //Instantiate(loadingScreenPrefab);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (collider.layer == LayerMask.NameToLayer("Tiles"))
        {
            if (collision.GetContact(0).normal.x != 0)
            {
                //against wall, not ceiling
                //PlaySound(bonkSound);
                if (xForce != 0)
				{
                    PlaySound(landSound);
                }
                xForce = 0;
                dashCountdown = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameObject.activeSelf) return;

        GameObject collider = collision.gameObject;

        Gem gem = collider.GetComponent<Gem>();
        if (gem != null)
		{
            Destroy(collider);
            PlaySound(collectGemSound);
            Instantiate(collectParticlePrefab, collider.transform.position, Quaternion.identity);
            levelGems++;
		}

        Heart heart = collider.GetComponent<Heart>();
        if (heart != null)
        {
            Destroy(collider);
            PlaySound(collectHeartSound);
            Instantiate(collectParticlePrefab, collider.transform.position, Quaternion.identity);
            hearts = Mathf.Min(hearts + 1, maxHearts);
        }

        ChoiceOrb choiceOrb = collider.GetComponent<ChoiceOrb>();
        if (choiceOrb != null)
		{
            choiceOrb.Activate();
		}
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!gameObject.activeSelf) return;

        GameObject collider = collision.gameObject;

        if (collider.CompareTag("Damage"))
        {
            if (!hurtInvincible && !invincible)
            {
                Damage();
            }
        }
    }

    private void Damage()
    {
        hearts--;
        cameraShake.Shake();
        Instantiate(hurtParticlePrefab, transform.position, Quaternion.identity);
        if (hearts <= 0)
        {
            GameObject playerKilled = Instantiate(playerKilledPrefab, transform.position, Quaternion.identity);
            if (facingLeft)
			{
                playerKilled.GetComponent<SpriteRenderer>().flipX = true;
			}
            gameObject.SetActive(false);
        }
        else
        {
            hurtInvincible = true;
            hurtInvincibleTimer = 0;
            hurtFlickerFrames = 0;
            PlaySound(hurtSound);
        }
    }

    private Sprite GetAnimSprite()
    {
        switch (animState)
        {
            case AnimState.Stand:
                return standSprite;
            case AnimState.Run:
                return runSprites[animFrame];
            case AnimState.Jump:
                return jumpSprite;
            case AnimState.DoubleJump:
                return doubleJumpSprite;
            case AnimState.Fall:
                return fallSprite;
            case AnimState.Dash:
                return dashSprite;
            case AnimState.Attack:
                return attackSprite;
        }
        return standSprite;
    }

    private void TryStopCoroutine(Coroutine crt)
    {
        if (crt != null)
        {
            StopCoroutine(crt);
        }
    }

    private void StopCancelQueuedJump()
    {
        TryStopCoroutine(crtCancelQueuedJump);
    }

    private IEnumerator CancelQueuedJump()
    {
        yield return new WaitForSeconds(jumpBufferTime);
        jumpQueued = false;
    }

    private IEnumerator LeaveGround()
    {
        yield return new WaitForSeconds(jumpGraceTime);
        canJump = false;
    }

    private void SetAnimState(AnimState state)
	{
        if (animState != AnimState.Attack)
		{
            animState = state;
		}
    }

    private void AdvanceAnim()
    {
        if (animState == AnimState.Run)
        {
            frameTime = runFrameTime;
            AdvanceFrame(runSprites.Length);
        }
        else
        {
            animFrame = 0;
            if (animState == AnimState.Attack)
            {
                frameTimer -= Time.deltaTime;
                if (frameTimer <= 0)
                {
                    animState = AnimState.Stand;
                }
            }
            else
			{
                frameTimer = frameTime;
            }
        }
    }

    private void AdvanceFrame(int numFrames)
    {
        if (animFrame >= numFrames)
        {
            animFrame = 0;
        }

        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0)
        {
            frameTimer = frameTime;
            animFrame = (animFrame + 1) % numFrames;
        }
    }

    public void PlaySound(AudioClip sound, bool randomizePitch = false)
    {
        if (randomizePitch)
        {
            audioSource.pitch = Random.Range(1 - pitchVariation, 1 + pitchVariation);
        }
        else
        {
            audioSource.pitch = 1;
        }
        audioSource.PlayOneShot(sound);
    }

    public int GetHearts()
	{
        return hearts;
	}

    public int GetMaxHearts()
	{
        return maxHearts;
	}

    public void DisableAttack()
	{
        disabledAttack = true;
	}

    public float GetStaminaPercent()
	{
        return stamina;
	}

    public bool GetStaminaRecharging()
	{
        return staminaRecharging;
	}

    public void SetInvincible()
	{
        invincible = true;
	}
}
