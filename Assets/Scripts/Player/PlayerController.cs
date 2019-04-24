using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using ActionCode2D.Renderers;
using UnityEngine.Experimental.Input;
using System;
public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Player PlayerModel;
    public MasterInput Controls;
    public AudioClip HitSound;
    public Transform EffectsContainer;
    public float Speed, JumpHeight, JumpTime, WallJumpTime, InvunerableBlinks;
    public Vector2 WallJumpForce;
    public LayerMask _raycastLayerMask;
    [HideInInspector]
    public bool IsAttacking = false;
    RectTransform HeathFill;
    AudioSource audioSource;
    Rigidbody2D _rb;
    Vector2 _direction, _directionBeforeAttack;
    bool _btnJumpPressed = false, _isGrounded, _isDucking, _isLookingUp, _isNearWallLeft, _isNearWallRight, _isWallClinging, _isHovering, _isVulnerable;
    float currentJumptime, currentJumpHeight, currentWallJumptime, currentWallJumpHeight;
    int JumpTimes = 1, _currentHealth, _currentDamage;
    Animator anim;
    CircleCollider2D _circleCollider;
    SpriteRenderer _spriteRenderer;
    EdgeCollider2D _edgeCollider;
    BoxCollider2D _boxCollider;
    LineRenderer _lineRenderer;
    public static PlayerController PlayerCTRL;
    public enum PlayerPower
    {
        ChargedJump,
        WallJump
    }

    void Awake()
    {
        PlayerCTRL = this;

        Controls.Player.Jump.performed += ctx => Jump();
        Controls.Player.Attack.performed += ctx => Attack();
        //Move
        Controls.Player.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
        //Stop moving

        Controls.Player.Movement.cancelled += ctx => { _direction.x = 0; };
        Controls.Player.Jump.cancelled += ctx => { _btnJumpPressed = false; };
    }

    void Start()
    {
        //Tries to load player data
        LoadData();

        _rb = GetComponentInChildren<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        _edgeCollider = GetComponent<EdgeCollider2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        _lineRenderer = GetComponentInChildren<LineRenderer>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        UpdateHealth();
        HealthManager.Instance.FadeIn();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        // if (PauseManager.Instance._isPaused) return;

        var x = _direction.x * Speed * Time.deltaTime * 60;
        var y = currentJumpHeight * Time.deltaTime * 60;

        //Add velocity on X
        _rb.velocity = new Vector2(x, _rb.velocity.y);
        //Add force on Y
        _rb.AddForce(new Vector2(0, y), ForceMode2D.Force);

        JumpUpdate();
        WallJumpUpdate();

        //Check if player is on ground
        _isGrounded = Physics2D.Raycast(transform.position, -Vector3.up, 0.5f, _raycastLayerMask);

        //Checl if player is near a wall
        if (PlayerModel.HaveWallJump)
        {
            _isNearWallLeft = Physics2D.Raycast(transform.position + new Vector3(0, 0.11f, 0), -Vector3.right, 0.13f, _raycastLayerMask);
            _isNearWallRight = Physics2D.Raycast(transform.position + new Vector3(0, 0.11f, 0), Vector3.right, 0.13f, _raycastLayerMask);
        }

        //Reset double jump when on ground
        if (_isGrounded) JumpTimes = 1;

        //Animation updates
        HandleMoveAnimation();
        HandleJumpAnimation();
        HandleWallAnimation();
    }


    //Load Player Data
    public void LoadData()
    {
        try
        {
            PlayerModel = SaveManager.Instance.LoadModel<Player>("PlayerData.dat");
            transform.position = PlayerModel.CurrentPosition;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            PlayerModel = new Player();
        }
        _currentHealth = PlayerModel.MaxHealth;
        _currentDamage = PlayerModel.Damage;
    }

    void Jump()
    {
        //Toggle jump
        _btnJumpPressed = !_btnJumpPressed;

        //Check if button is pressed and player is on ground and have ability to double jump and is not sliding down walls
        if (_btnJumpPressed && JumpTimes > 0 && !_isGrounded && PlayerModel.HaveChargedJump && !_isWallClinging)
        {
            JumpTimes = 0;
            // _rb.AddForce(new Vector2(_direction.x, _direction.y/2), ForceMode2D.Impulse);
            currentJumptime = JumpTime;
            currentJumpHeight = JumpHeight;
        }
        //Normal jump
        else if (_isGrounded && _btnJumpPressed)
        {
            anim.SetTrigger("Jump");
            currentJumptime = JumpTime;
            currentJumpHeight = JumpHeight;
        };

        //WallJump
        if (!_isGrounded && _isWallClinging)
        {
            currentWallJumptime = WallJumpTime;
            currentWallJumpHeight = -_direction.normalized.x * WallJumpForce.x * Time.deltaTime * 60;
        }
    }

    void JumpUpdate()
    {
        //NormalJump
        if (currentJumptime > 0 && _btnJumpPressed)
        {
            currentJumptime -= Time.deltaTime;
        }
        else
        {
            currentJumpHeight = 0;
        }
    }

    //Controls player Attack
    void Attack()
    {
        if (!IsAttacking && _isGrounded)
        {
            _directionBeforeAttack = _direction;
            _direction = Vector2.zero;
            _direction = Vector2.zero;
            IsAttacking = true;
            anim.SetTrigger("Attack");
        }
    }

    public void ResetDirectionAfterAttack()
    {
        _direction = _directionBeforeAttack;
    }

    void WallJumpUpdate()
    {
        //WallJump
        if (currentWallJumptime > 0 && _isWallClinging && _btnJumpPressed)
        {
            currentWallJumptime -= Time.deltaTime;
            var y2 = WallJumpForce.y * Time.deltaTime * 60;
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(new Vector2(currentWallJumpHeight, y2), ForceMode2D.Force);
        }
        else
        {
            currentWallJumptime = 0;
        }
    }

    void Move(Vector2 direction)
    {
        if (IsAttacking) return;
        // if (PauseManager.Instance._isPaused) return;

        _isDucking = direction.y < 0 ? true : false;
        _isLookingUp = direction.y > 0 ? true : false;
        anim.SetBool("isDucking", _isDucking);
        anim.SetBool("isLookingUp", _isLookingUp);

        if (!_isDucking && !_isLookingUp)
        {
            _direction = new Vector2(direction.x, direction.y);
            //Change facing direction
            if (_direction.x != 0)
            {
                var lookDirection = _direction.x > 0 ? 0 : 180;
                var flipX = _direction.x > 0 ? false : true;
                _spriteRenderer.flipX = flipX;

                //Change effect Rotation
                if (_spriteRenderer.flipX)
                    EffectsContainer.transform.eulerAngles = new Vector3(0, 180, 0);
                else
                    EffectsContainer.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        else
        {
            //ChargedJump
            if (_isHovering)
                _direction = new Vector2(direction.x, direction.y);
            else
                _direction = new Vector2(0, direction.y);
        }
    }

    public void EnablePower(PlayerPower powerToEnable)
    {
        switch (powerToEnable)
        {
            case PlayerPower.ChargedJump:
                PlayerModel.HaveChargedJump = true;
                break;
            case PlayerPower.WallJump:
                PlayerModel.HaveWallJump = true;
                break;
        }
    }

    //Calculate and animate player taking damage 
    public void TakeDamage(int damage)
    {
        //Checks if player is invulnerable
        if (!_isVulnerable)
        {
            //Update health variable
            _currentHealth -= damage;

            //Update health
            UpdateHealth();

            //Turn player invulnerable
            _isVulnerable = true;
            // gameObject.layer = 11;

            //Checks for player health 
            if (_currentHealth <= 0)
            {
                //Kills player
                anim.SetBool("IsDead", true);
                this.enabled = false;
                StartCoroutine("Restart");
                // gameObject.SetActive(false);
            }
            else
            {
                //Sets player taking damage animation
                audioSource.clip = HitSound;
                audioSource.Play();
                anim.SetTrigger("Hit");
                StartCoroutine("HitAnimation");
            }
        }
    }

    //Animate player blinking
    IEnumerator HitAnimation()
    {
        for (var i = 0; i < InvunerableBlinks; i++)
        {
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, i % 2);
            yield return new WaitForSeconds(.2f);
        }
        _isVulnerable = false;
        // gameObject.layer = 2;
    }

    //Update heath
    void UpdateHealth()
    {
        HealthManager.Instance.SetHealth(_currentHealth);
    }

    IEnumerator Restart()
    {
        HealthManager.Instance.FadeOut();
        yield return new WaitForSeconds(3f);

        LoadData();
        yield return new WaitForSeconds(1f);

        HealthManager.Instance.FadeIn();
        UpdateHealth();
        this.enabled = true;
        anim.SetBool("IsDead", false);
    }

    //Animate player moving
    void HandleMoveAnimation()
    {
        var isRunning = _direction.x != 0 && !_isDucking ? true : false;
        anim.SetBool("isRunning", isRunning);
    }

    //Animate player if is jumping
    void HandleJumpAnimation()
    {
        var isOnAir = _isGrounded ? false : true;
        // if (isOnAir)
        // {
        //     _circleCollider.enabled = true;
        //     _edgeCollider.enabled = false;
        //     _boxCollider.enabled = false;
        // }
        // else
        // {
        //     _circleCollider.enabled = false;
        //     _edgeCollider.enabled = true;
        //     _boxCollider.enabled = true;
        // }
        anim.SetBool("isOnAir", isOnAir);
    }

    //Player wall sliding animation
    void HandleWallAnimation()
    {
        if (!_isGrounded && _isNearWallLeft && _direction.x < 0)
        {
            JumpTimes = 1;
            _isWallClinging = true;
        }
        else if (!_isGrounded && _isNearWallRight && _direction.x > 0)
        {
            JumpTimes = 1;
            _isWallClinging = true;
        }
        else
        {
            _isWallClinging = false;
        }

        //Mirror sprite
        var mirror = _isWallClinging ? -1 : 1;
        anim.transform.localScale = new Vector3(mirror, 1, 1);
        anim.SetBool("isWallClinging", _isWallClinging);
    }

    void OnEnable()
    {
        Controls.Enable();
    }

    void OnDisable()
    {
        Controls.Disable();
    }

    //Returns current damage
    public int GetDamage()
    {
        return _currentDamage;
    }

    //Check for collisions
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            var damage = other.transform.GetComponent<EnemyController>().Damage;
            TakeDamage(damage);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            var damage = other.transform.GetComponent<EnemyController>().Damage;
            TakeDamage(damage);
        }
    }
}

