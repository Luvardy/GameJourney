using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private Transform tran;

    public AudioClip m_jump;
    public AudioClip m_doubleJump;
    public AudioClip m_dash;
    public AudioClip m_Walk1;
    public AudioClip m_Walk2;
    public AudioClip m_fallOn;
    public AudioClip m_climb;

    [Header("移动参数 (注：Crouch为下蹲状态参数,下蹲后的速度可以通过修改参数实现变化!)")]
    public float speed = 8f;                //默认为 8
    public float climbSpeed = 5f;
    public float crouchSpeedDivisor = 3f;   //默认为 3

    [Header("跳跃参数")]
    public float jumpForce = 20f;         //初始跳跃力度
    //public float jumpHoldForce = 1.9f;     //长按跳跃力度
    //public float jumpHoldDuration = 0.1f;  //按键可按时间
    //public float crouchJumpBoost = 2.5f;   //下蹲后跳跃的额外加成
    public float hangingJumpForce = 5f;   //挂壁时的额外弹跳力
    public float canJumpCount;
    float jumpTime;

    [Header("冲刺参数")]
    public float dashSpeed;//冲刺速度
    private float lastTime = -10f;//上一次冲刺时间
    public float dashCoolDown;//冲刺冷却时间
    public float dashTime; //冲刺时间
    public bool isDashing;//是否在冲刺状态
    float startDashTimer;//计数冲刺时间
    
    [Header("人物状态判定")]
    public bool isCrouch;
    public bool isOnGround;
    public bool isJumping;
    public bool doJump;
    public bool isHeadBlocked;
    public bool isHanging;
    public bool readyToJumpOnWall;
    public bool readyToClimb;
    public bool canClimb;
    public bool _Climb;
    public bool isHangingTop;
    public bool isHangingDown;


    [Header("环境检测")]
    public float footOffset = 0.37f;       //检测左右两个脚的距离位置
    public float headClearance = 0.5f;    //头顶的检测距离
    public float groundDistance = 0.3f;   //与地面之间的距离
    public LayerMask groundLayer;
    float playerHeight;
    public float eyeHeight = 1.6f;
    public float grabDistance = 0.8f;
    public float reachOffset = 0.7f;

    public float rataionData = 0f;//获取人物旋转值

    public int fileNum ;//档案袋收集数量

    //X轴速度

    public float xVelocity;

    //y轴攀爬
    public float yVelocity;

    //按键设置
    bool jumpPressed;  //单次按动跳跃
    bool jumpHeld;     //长按跳跃
    bool crouchHeld;   //长按下蹲
    bool crouchPressed; //挂壁下落


    //碰撞体尺寸

    Vector2 colliderStandSize;
    Vector2 colliderStandOffset;
    Vector2 colliderCrouchSize;
    Vector2 colliderCrouchOffest;

    public void SaveData()
    {
        GlobalControl.instance.fileNum = fileNum;
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        tran = GetComponent<Transform>();

        canJumpCount = 2f;
        playerHeight = coll.size.y;
        colliderStandSize = coll.size;
        colliderStandOffset = coll.offset;
        colliderCrouchSize = new Vector2(coll.size.x, coll.size.y / 2f);
        colliderCrouchOffest = new Vector2(coll.offset.x, -1.5f);

        fileNum = GlobalControl.instance.fileNum;

    }

 
    void Update()
    {
        yVelocity = Input.GetAxisRaw("Vertical");
        jumpPressed = Input.GetButtonDown("Jump");
        jumpHeld = Input.GetButton("Jump");
        crouchHeld = Input.GetButton("Crouch");
        crouchPressed = Input.GetButtonDown("Crouch");

       /* if(rb.velocity.y != 0)
        {
            isOnGround = false;
        }

        else
        {
            isJumping = false;
            isOnGround = true;
            canJumpCount = 2;
        }*/

        rataionData = tran.localEulerAngles.y;

        if(Input.GetButtonDown("Jump"))
        {
            if(isOnGround)
            {
                doJump = true;
            }

            else if(!_Climb)
            {
                
                if(!isJumping)
                {
                    canJumpCount--;
                }
                
                
                if(canJumpCount>0)
                {
                    if (canJumpCount == 1)
                        rb.velocity = transform.up * 0;
                   doJump = true;
                }
  
            }

        }

          if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.time >= (lastTime + dashCoolDown))
            {
                //执行冲刺
                ReadyToDash();
            }
        }

        if(isHanging)
        {
            isDashing = false;
            isJumping = false;
            if (jumpPressed)
            {
                readyToJumpOnWall = true;
            }

            if(_Climb)
            {
                readyToClimb = true;
            }

        }

        else  _Climb = false;


        

        if(isOnGround && Input.GetButtonDown("Horizontal"))
        {
            SoundManager.instance.RandomizeSfx(m_Walk1, m_Walk2);
        }

    }
    
    private void FixedUpdate()
    {

        Dash();
        Climb();
        JumpOnWall();

        if(isDashing)
        {   
            return;
        }
        PhysicsCheck();
        GroundMovement();
        MidAirMovement();
    }

    void PhysicsCheck()
    {
        /*Vector2 pos = transform.position;
        Vector2 offset = new Vector2(-footOffset, 0f);
        RaycastHit2D leftCheck = Physics2D.Raycast(pos + offset, Vector2.down, groundDistance, groundLayer);
        Debug.DrawRay(pos + offset, Vector2.down, Color.red, 0.2f);*/
        
        //左右脚射线
        RaycastHit2D leftCheck = Raycast(new Vector2(-footOffset,-2.88f), Vector2.down, groundDistance, groundLayer);
        RaycastHit2D rightCheck = Raycast(new Vector2(footOffset,-2.88f), Vector2.down, groundDistance, groundLayer);
        if (leftCheck || rightCheck)
        {   
            anim.SetBool("climb", false);
            anim.SetBool("climbIdel", false);
            anim.SetBool("doubleJump", false);
            anim.SetBool("jump", false);
            isJumping = false;
            isOnGround = true;
            canJumpCount = 2f;
            isHanging = false;
        }
        else isOnGround = false;


        //头顶射线
        RaycastHit2D headCheck = Raycast(new Vector2(0f, coll.size.y/2f), Vector2.up, headClearance, groundLayer);

        if (headCheck)
        {
            isHeadBlocked = true;
        }
        else isHeadBlocked = false;

        float dircection = transform.localScale.x;
        Vector2 grabDir = new Vector2(dircection, 0f);

        //爬墙
        // RaycastHit2D climbCheckLeft = Raycast(new Vector2(footOffset + 0.8f * dircection, 1.5f), Vector2.down, 2f, groundLayer);
        // RaycastHit2D climbCheckRight = Raycast(new Vector2(-footOffset - 0.8f * dircection, 1.5f), Vector2.down, 2f, groundLayer);
        // if(climbCheckLeft || climbCheckRight)
        // {
        //     _Climb = true;
        // }

        RaycastHit2D wallCheckRight = Raycast(new Vector2(footOffset - 0.1f * dircection, eyeHeight), grabDir, grabDistance, groundLayer);
        RaycastHit2D wallCheckLeft = Raycast(new Vector2(-footOffset + 0.1f * dircection, eyeHeight), -grabDir, grabDistance, groundLayer);

        if (!isOnGround && rb.velocity.y < 0f && (wallCheckRight || wallCheckLeft) && Input.GetKey(KeyCode.X))
        {   
            
            
            if((wallCheckLeft && rataionData == 180) || (wallCheckRight && rataionData == 0))
            {   
                anim.SetBool("climbIdel", true);
                anim.SetBool("jump", false);
                anim.SetBool("doubleJump", false);
                anim.SetBool("run", false);
                rb.bodyType = RigidbodyType2D.Static;
                isHanging = true;
            }
                
            else 
                isHanging = false;
                
            
        }


        if(!(wallCheckRight || wallCheckLeft)) 
        {
            isHangingTop = true;
            isHanging = false;
            anim.SetBool("climbIdel", false);
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {   
            _Climb = true;
            isHangingTop = false;
        }
    }

    void GroundMovement()
    {
        if (isHanging)
        {
            return;
        }
        if (crouchHeld && !isCrouch && isOnGround)
        {
            Crouch();
        }
        else if (!crouchHeld && isCrouch && !isHeadBlocked)
        {
            StandUp();
        }
        else if (!isOnGround)
        {
            StandUp();
        }
        xVelocity = Input.GetAxisRaw("Horizontal");


        if (isCrouch)
        {
            xVelocity /= crouchSpeedDivisor;
        }
        rb.velocity = new Vector2(xVelocity * speed, rb.velocity.y);

        FilpDirction();
    }

    void MidAirMovement()
    {
       /* if (isHanging)
        {   isJumping = false;
            if (jumpPressed)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.AddForce(Vector2.up * hangingJumpForce, ForceMode2D.Impulse);
                isHanging = false;
            }

        }*/
        /*if (jumpPressed && isOnGround && !isJump && !isHeadBlocked)
        {
            if (isCrouch)
            {
                StandUp();
                rb.AddForce(new Vector2(0f, crouchJumpBoost), ForceMode2D.Impulse);
            }

            isOnGround = false;
            isJump = true;

            jumpTime = Time.time + jumpHoldDuration;

            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

            AudioManager.PlayJumpAudio();
        }

        else if (isJump)
        {
            if (jumpHeld)
            {
                rb.AddForce(new Vector2(0f, jumpHoldForce), ForceMode2D.Impulse);
                if (jumpTime < Time.time)
                {
                    isJump = false;
                }
            }
        }*/

         if(doJump)
        {   
            isJumping = true;
            canJumpCount--;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            doJump = false;
            if(!readyToClimb)
            {
                if(canJumpCount == 0)
                {
                    anim.SetBool("doubleJump", true);
                    SoundManager.instance.PlaySingle(m_doubleJump);
                }
                    
                if(canJumpCount == 1)
                {
                    anim.SetBool("jump",true);
                    SoundManager.instance.PlaySingle(m_jump);
                }
                    
            }
           
            
        }
    }

    void FilpDirction()
    {
        if (xVelocity < 0)
        {
            if(isJumping)
            {
                anim.SetBool("run", false);
                rb.transform.eulerAngles = new Vector3(0f, 180f, 0f);  
            }

            else if(isCrouch)
            {
                anim.SetBool("crouch",true);
                rb.transform.eulerAngles = new Vector3(0f,180f,0f);
            }

            else
            {   
                if(!_Climb)
                {
                    rb.transform.eulerAngles = new Vector3(0f, 180f, 0f);
                    anim.SetBool("run", true);  
                }
                
            }
            
        }
        if (xVelocity > 0)
        {
            if(isJumping)
            {
                anim.SetBool("run", false);
                rb.transform.eulerAngles = new Vector3(0f, 0f, 0f); 
            }
            
            else if(isCrouch)
            {
                anim.SetBool("crouch",true);
                rb.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }


            else
            {
                if(!_Climb)
                {
                    rb.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                    anim.SetBool("run", true);  
                }
            }
        }
        if(xVelocity < 0.001f && xVelocity > -0.001f)
        {   
            if(isCrouch)
            {
                anim.SetBool("crouchIdel", true);
            }
            anim.SetBool("run",false);
        }
    }

    void Crouch()
    {
        isCrouch = true;
        coll.size = colliderCrouchSize;
        coll.offset = colliderCrouchOffest;
    }

    void StandUp()
    {
        isCrouch = false;
        anim.SetBool("crouchIdel", false);
        anim.SetBool("crouch", false);
        coll.size = colliderStandSize;
        coll.offset = colliderStandOffset;
    }

    private void ReadyToDash()
    {
        isDashing = true;
        startDashTimer = dashTime;
        lastTime = Time.time;
    }

    private void Dash()
    {
        if(isDashing)
        {
            if(startDashTimer > 0)
            {   
                SoundManager.instance.PlaySingle(m_dash);
                rb.velocity = transform.right * dashSpeed;
                startDashTimer -= Time.deltaTime;
                anim.SetBool("jump", true);
                anim.SetBool("doubleJump", true);
                anim.SetBool("dash", true);
            }

            if(startDashTimer <= 0)
            {   
                rb.velocity = transform.right * 0;
                isDashing = false;
                anim.SetBool("dash", false);
            }
        }

    }

    private void JumpOnWall()
    {
        if(readyToJumpOnWall)
        {   
            SoundManager.instance.PlaySingle(m_jump);
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.AddForce(Vector2.up * hangingJumpForce, ForceMode2D.Impulse);
            isHanging = false;
            readyToJumpOnWall = false;
            anim.SetBool("climbIdel", false);
            anim.SetBool("climb", false);
            anim.SetBool("jump", true);
        }
    }

    private void Climb()
    {
        if(readyToClimb)
        {   
            SoundManager.instance.PlaySingle(m_climb);
            rb.bodyType = RigidbodyType2D.Dynamic;
            if(isHangingTop && yVelocity > 0)
            {
                Debug.Log(yVelocity);
                rb.velocity = new Vector2(rb.velocity.x, yVelocity * 0 );  
            }
            else
                rb.velocity = new Vector2(rb.velocity.x, yVelocity * climbSpeed );  
            anim.SetBool("climb", true);
                
            if(yVelocity < 0.001f && yVelocity > -0.001f)
            {
                rb.bodyType = RigidbodyType2D.Static;
                anim.SetBool("climb", false);
            }
            readyToClimb = false;

        }



    }

        private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Spike")
        {   
            GameController.instance.ShowGameOverPanel();
        }

        if(other.gameObject.tag == "Boss")
        {
            GameController.instance.ShowGameOverPanel();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene(3);
        }

        if(other.gameObject.tag == "Win")
        { 
            SceneManager.LoadScene(4);
        }

        if(other.gameObject.tag == "End")
        {
            SoundManager.instance.backGround.Stop();
            if(GlobalControl.instance.fileNum == 4)
                SceneManager.LoadScene(5);
            else
                SceneManager.LoadScene(6);
        }

        if(other.gameObject.tag == "File")
        {   
            fileNum++;
            other.gameObject.SetActive(false);
            SaveData();
            GlobalControl.instance.file = true;
        }

        if(other.gameObject.tag == "ruler")
        {   
            fileNum++;
            other.gameObject.SetActive(false);
            SaveData();
            GlobalControl.instance.ruler = true;
        }

        if(other.gameObject.tag == "mp3")
        {   
            fileNum++;
            other.gameObject.SetActive(false);
            SaveData();
            GlobalControl.instance.mp3 = true;
        }

        if(other.gameObject.tag == "diary")
        {   
            fileNum++;
            other.gameObject.SetActive(false);
            SaveData();
            GlobalControl.instance.diary = true;
        }
    }


    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDiraction, float length, LayerMask layer)
    {   
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDiraction, length, layer);
        Color color = hit ? Color.red : Color.green;

        Debug.DrawRay(pos + offset, rayDiraction * length,color);
        return hit;
    }
}
