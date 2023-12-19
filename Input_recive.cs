using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Input_recive : MonoBehaviour
{
    [SerializeField] public float Speed, JumpStrength, DashStrength, GPpower, accelerate, targetspeed, Vmax, GPscapX, SpeedCapX, OGSpeedCapX, CurrentVx, CurrentVy, WallCheckDis, GroundCheckDis, CeilingDis, VoidPhaseDuration, wallslideSpeed, WallJumpStrength, WallControlDura, AntiDashClipOnDuration;
    private Vector2 moveInput;
    public int JumpCount, DashCount, DashCDscale, Dashlength, DiveWindowtime, RespawnTime, Directionasint;
    public int JumpCounter, DashCounter;
    public bool WallSliding, DashState, Grounded, GPstate, FacingRight, GroundPounding, DashonCD, DiveWindow, CeilingHit, Alive, VoidPhasing, ControlLocked, Walled, Winning, RecallAllowed = false,AntiDashing;
    [SerializeField] private LayerMask PlatformLayerMask, UnclimableWall, Ceiling, Vground, ClimableWall, PlayerLayer, AntiDashGround, AntiDashClimableWall;
    public BoxCollider2D Pcollider { get; private set; }
    public BoxCollider2D belowplatform;
    public GameObject DieExo, RecallPos, PlayerAnimation;
    private float DefGravityScale, SpeedDif, Movement, TwidthOrigin;
    public Rigidbody2D Player;
    public TrailRenderer WhiteTrail;
    private RaycastHit2D DetectedGround, PlatformCheckUp, PlatformCheckown;
    public Animator Playeranimation;
    public AudioClip Winsound, JumpSound, WJumpSound, DashSound;
    private TrailRenderer Ptrail;
    public AntiDash[] AntiDashers;
    public Coroutine AntiDashRoutineStopper,VoidPhaseRoutineStopper;



 

    void Start()
    {
        ControlLocked = false;
        DefGravityScale = Player.gravityScale;
        FacingRight = true;
        Alive = true;
        Pcollider = GetComponent<BoxCollider2D>();
        StartCoroutine(StartFreeze());
        StartCoroutine(StartFreeze());
        Ptrail = gameObject.GetComponent<TrailRenderer>();
        TwidthOrigin = Ptrail.startWidth;
        AntiDashers = FindObjectsOfType<AntiDash>();
        Physics2D.IgnoreLayerCollision(10, 14, true); AntiDashing = false;
    }
    void Update()
    {   //walking
        moveInput.x = Input.GetAxisRaw("Horizontal");
        if (moveInput.x != 0)
            DirectionCheck(moveInput.x > 0);

        if (Input.GetKeyDown(KeyCode.Space) && (GPstate == false) && WallSliding == false && (WallCheck()|| (AntiDashWallCheck()&&AntiDashing ))) { WallJump(); }
        else if (Input.GetKeyDown(KeyCode.Space) && JumpCounter < JumpCount && (GPstate == false) && WallSliding == false)
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && (GPstate == false) && WallSliding) { WallJump(); }


        if (Input.GetKeyDown(KeyCode.G)&&RecallAllowed)
        { gameObject.transform.position = RecallPos.transform.position; new Vector2(0, 0); }
        if (Input.GetKeyDown(KeyCode.R)&&Alive&&!Winning) { gameObject.GetComponent<CheckpointSystem>().Death(); PlayerAnimation.GetComponent<Animator>().SetTrigger("DeadAnim"); }
        if ((Input.GetKeyDown(KeyCode.LeftShift)||Input.GetKeyDown(KeyCode.J)) && DashCounter < DashCount && (DashonCD == false) && (GPstate == false))
        if (FacingRight)
            { Dashsequence(1); }
        else
            { Dashsequence(-1); }
        if (Input.GetKeyDown(KeyCode.S))
        { if (Grounded == false&&GroundPounding==false) { GroundPound(); } }
        //checkers//   //REWRITE NEEDED//
        CeilingHit = CeilingCheck();
        if (!GPstate) { Grounded = (GroundCheck() || (!VoidPhasing && VGroundCheck()) || (AntiDashing && AntiDashGroundCheck())); }

        //Ray tracers//
        Debug.DrawRay(Pcollider.bounds.center - new Vector3 (0,Pcollider.bounds.extents.y,0), Vector2.down * GroundCheckDis, Color.red);
        Debug.DrawRay(Pcollider.bounds.center, Vector2.up * (Pcollider.bounds.extents.y + CeilingDis), Color.red);
        if (FacingRight) { Directionasint = 1; }
        else { Directionasint = -1; }

    }
    private void FixedUpdate()
    {
        run();
        if (Mathf.Abs(Player.velocity.x) >= SpeedCapX) { Player.velocity = new Vector2(SpeedCapX * moveInput.x, Player.velocity.y); }
        if (CeilingHit && Player.velocity.y > 0) { Player.velocity = new Vector2(Player.velocity.x, 0); }
        if (GroundPounding) { GPaccel(); }
        else if (Grounded) { SpeedCapX = OGSpeedCapX; }
        CurrentVx = Player.velocity.x;
        CurrentVy = Player.velocity.y;
        if ((WallCheck() || (AntiDashWallCheck() && AntiDashing)) && Grounded == false && Player.velocity.x <= 0.1 && (Mathf.Abs(moveInput.x)) == 1) { Player.velocity = new Vector2(Player.velocity.x, Mathf.Clamp(Player.velocity.y, -wallslideSpeed, float.MaxValue)); WallSliding = true; }
        else { WallSliding = false; }


        if (Grounded && Player.velocity.y <= 0.1)
        {
            JumpCounter = 0;
            if (!DashonCD) { DashCounter = 0; }
        }




    }
    private void Dashsequence(int DashDi)
    {
        Player.velocity = new Vector2(DashDi, 0) * DashStrength;
        DashCounter += 1;
        DashReset(); VoidPhase();
        Dashcooldown();
        DiveOppo();
        gameObject.GetComponent<AudioSource>().PlayOneShot(DashSound, 0.2f);
        Ptrail.startWidth = 0.4f;
        foreach (AntiDash AntiDashers in AntiDashers) { AntiDashers.Dashed(); }
        AntiDash();
    }

    async void DashReset()
    {
        DashState = true;
        await Task.Delay(Dashlength);
        DashState = false;
        Ptrail.startWidth = TwidthOrigin;
   
    }
    async void DiveOppo()
    {
        DiveWindow = true;
        await Task.Delay(DiveWindowtime);
        DiveWindow = false;
    }


    public void VoidPhase()
    {
        if (!VoidPhasing) { Physics2D.IgnoreLayerCollision(10, 11, true); VoidPhaseRoutineStopper = StartCoroutine(VoidPhaseRoutine()); VoidPhasing = true; }
        else { RestartVoidPhasingRoutine(); }
    }
    IEnumerator VoidPhaseRoutine() {yield return new WaitForSeconds(VoidPhaseDuration); if (GPstate == false) { Physics2D.IgnoreLayerCollision(10, 11, false); } VoidPhasing = false; }
    void RestartVoidPhasingRoutine() { StopCoroutine(VoidPhaseRoutineStopper); VoidPhaseRoutineStopper = StartCoroutine(VoidPhaseRoutine());  }
    public void AntiDash()
    {
        if (!AntiDashing) { Physics2D.IgnoreLayerCollision(10, 14,false); AntiDashRoutineStopper = StartCoroutine(AntiDashRoutine()); AntiDashing = true; }
        else { RestartAntiDashRoutine(); }
    }
    IEnumerator AntiDashRoutine() { yield return new WaitForSeconds(AntiDashClipOnDuration); Physics2D.IgnoreLayerCollision(10, 14, true); AntiDashing = false; }
    void RestartAntiDashRoutine() { StopCoroutine(AntiDashRoutineStopper); AntiDashRoutineStopper = StartCoroutine(AntiDashRoutine()); }

    async void Dashcooldown()
    {
        DashonCD = true;
        await Task.Delay(DashCDscale);
        DashonCD = false;
    }
    async void GroundPound()
    {
        GPstate = true;
        Player.gravityScale = 0;
        SpeedCapX = GPscapX;
        Player.velocity = new Vector2(Player.velocity.x, 0);
        Playeranimation.SetTrigger("GPexpand");
        await Task.Delay(150);
        Playeranimation.ResetTrigger("GPexpand");
        Playeranimation.SetTrigger("Gprevert");
        Player.gravityScale = DefGravityScale;
        GroundPounding = true;
        Physics2D.IgnoreLayerCollision(10, 11);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6&&GroundPounding)
        {
            GPstate = false;
            Physics2D.IgnoreLayerCollision(10, 11, false);
            GroundPounding = false;
        }
    }

    void run()        //Causes no Decel when the thing happened// //ima skip this for now//
    {
        if (GPstate == false && DashState == false && ControlLocked == false)
        {
            targetspeed = moveInput.x * Vmax;
            SpeedDif = targetspeed - Player.velocity.x;
            Movement = SpeedDif * accelerate;
            Player.AddForce((Movement * Vector2.right), ForceMode2D.Force);
        }
    }
    public void DirectionCheck(bool MovingRight)
    { if (MovingRight != FacingRight) { Turn(); } }
    private void Turn()
    {
        FacingRight = !FacingRight;
    }
    private void Jump()
    {
        JumpCounter += 1;
         Player.velocity = new Vector2(Player.velocity.x, JumpStrength);
        gameObject.GetComponent<AudioSource>().PlayOneShot(JumpSound, 0.2f);

    }
    private void WallJump()
    {

;        StartCoroutine(WallJumpControlLock());
        if (Physics2D.BoxCast(Pcollider.bounds.center, Pcollider.bounds.size, 0f, Vector2.right, CeilingDis, ClimableWall)|| Physics2D.BoxCast(Pcollider.bounds.center, Pcollider.bounds.size, 0f, Vector2.right, CeilingDis, AntiDashClimableWall))
        { Player.velocity = new Vector2(-WallJumpStrength, JumpStrength); gameObject.GetComponent<AudioSource>().PlayOneShot(WJumpSound, 0.2f); }
        else if (Physics2D.BoxCast(Pcollider.bounds.center, Pcollider.bounds.size, 0f, Vector2.left, CeilingDis, ClimableWall)|| Physics2D.BoxCast(Pcollider.bounds.center, Pcollider.bounds.size, 0f, Vector2.left, CeilingDis, AntiDashClimableWall))
        { Player.velocity = new Vector2(WallJumpStrength, JumpStrength); gameObject.GetComponent<AudioSource>().PlayOneShot(WJumpSound, 0.2f); }
    }
    IEnumerator WallJumpControlLock() { ControlLocked = true; yield return new WaitForSeconds(WallControlDura); ControlLocked = false; }
    private void GPaccel()
    { Player.AddForce(Vector2.down * GPpower); }

   

    public bool GroundCheck()
    {

        DetectedGround = Physics2D.BoxCast(
        Pcollider.bounds.center - new Vector3(0, Pcollider.bounds.extents.y, 0),
        new Vector3(Pcollider.bounds.size.x - 0.05f, GroundCheckDis, Pcollider.bounds.size.z), 0f,
        Vector2.down,
        GroundCheckDis / 2 + Pcollider.bounds.extents.y,
        PlatformLayerMask);
        if (DetectedGround) { if (DetectedGround.collider.gameObject.tag == "Platform")
            {
                PlatformCheckown = Physics2D.BoxCast(DetectedGround.collider.bounds.center,DetectedGround.collider.bounds.size,0f,Vector2.zero,0,PlayerLayer);
                PlatformCheckUp = Physics2D.BoxCast
                (DetectedGround.collider.bounds.center + new Vector3(0, DetectedGround.collider.bounds.extents.y, 0),
                new Vector3(DetectedGround.collider.bounds.size.x, GroundCheckDis, DetectedGround.collider.bounds.size.z), 0f,
                Vector2.up,
                (GroundCheckDis/2) + DetectedGround.collider.bounds.extents.y,
                PlayerLayer);
                if (PlatformCheckUp && !PlatformCheckown && Player.velocity.y < 0.1) { if (PlatformCheckUp.collider.gameObject.tag == "Player") { return true; } else { return false; } } else { return false; }
            }
            else { return true; }
        }
        else {return false;}



    }
    public bool VGroundCheck()
    {
        return Physics2D.BoxCast(Pcollider.bounds.center, Pcollider.bounds.size, 0f, Vector2.down, GroundCheckDis, Vground);
    }
    public bool AntiDashGroundCheck()
    {
        return Physics2D.BoxCast(Pcollider.bounds.center, Pcollider.bounds.size, 0f, Vector2.down, GroundCheckDis,AntiDashGround);
    }
    public bool CeilingCheck()
    {
        return Physics2D.BoxCast(Pcollider.bounds.center, Pcollider.bounds.size, 0f, Vector2.up, CeilingDis, Ceiling);
    }
    public bool WallCheck()
    { return Physics2D.BoxCast(Pcollider.bounds.center, Pcollider.bounds.size, 0f, Vector2.right, CeilingDis, ClimableWall)||Physics2D.BoxCast(Pcollider.bounds.center, Pcollider.bounds.size, 0f, Vector2.left, CeilingDis, ClimableWall); }
    public bool AntiDashWallCheck()
    { return Physics2D.BoxCast(Pcollider.bounds.center, Pcollider.bounds.size, 0f, Vector2.right, CeilingDis, AntiDashClimableWall)||Physics2D.BoxCast(Pcollider.bounds.center, Pcollider.bounds.size, 0f, Vector2.left, CeilingDis, AntiDashClimableWall); }
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "DeathZone" || (other.gameObject.tag == "DashDeathZone" && !VoidPhasing))&& Alive) { gameObject.GetComponent<CheckpointSystem>().Death(); PlayerAnimation.GetComponent<Animator>().SetTrigger("DeadAnim");Alive = false; }
    }
    public void WinState()
    {
        Winning = true;
        gameObject.GetComponent<AudioSource>().PlayOneShot(Winsound, 0.2f);
        Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        PlayerAnimation.GetComponent<Animator>().SetTrigger("Win");
    }
    IEnumerator StartFreeze()
    {
        Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(1.08f);
        Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }
}
