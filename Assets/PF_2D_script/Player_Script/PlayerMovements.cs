using System.Collections;
using UnityEngine;

public class player_behavior : MonoBehaviour
{
    //Move
    [SerializeField]
    public float smoothing = 0.3f;

    public SpriteRenderer srPlayer;
    private int playerMove;
    public Rigidbody2D rbPlayer;
    private Vector2 velocity = Vector2.zero;
    public float playerSpeed;

    //Jump
    [SerializeField]
    private bool isJumping; // le joueur est en train de sauter
    [SerializeField]
    private float originalGravity;
    [SerializeField]
    private float gravityScaleMultiplier = 3;

    public float jumpHeight; // hauteur du saut
    public float coyoteTime = 3f;

    //Checker
    [SerializeField]
    private float groundCheckHeight;
    [SerializeField]
    private float groundCheckWidth;
    [SerializeField]
    private float wallCheckHeight;
    [SerializeField]
    private float wallCheckWidth;
    [SerializeField]
    private bool IsWallingL;
    [SerializeField]
    private bool IsWallingR;
    [SerializeField]
    private LayerMask JumpLayerMask;
    [SerializeField]
    private bool isGrounded;

    Vector2 wallCheckerL;
    Vector2 wallCheckerR;
    Vector2 groundChecker;

    //Dash
    public float dashCooldown = 1;
    public bool canIDash = true;
    public int dashPower = 3;

    //changement d'état
    public bool green = true;
    public bool blue = false;

    //shield
    [SerializeField]
    private GameObject aegis;
    [SerializeField]
    private bool shielded;
    [SerializeField]
    private float shieldedTime = 3f;

    //blue shield
    [SerializeField]
    private GameObject blueShield;
    [SerializeField]
    private float blueShieldedTime = 3f;
    public PlayerMagic playerMagic;
   
    public bool blueShielded;

    //attack
    [SerializeField]
    private LayerMask enemyLayerMask;
    [SerializeField]
    private int damage = 50;
    [SerializeField]
    private bool isAttacking = false;
    [SerializeField]
    private float attackAreaHeight;
    [SerializeField]
    private float attackAreaWidth;
    //[SerializeField] 
    //private float attackRange = 1.5f;

    Vector2 attackArea;

    private void Start()
    {
        playerMagic = GetComponent<PlayerMagic>();
        shielded = false;
        rbPlayer = GetComponent<Rigidbody2D>();
        srPlayer = GetComponent<SpriteRenderer>();
        green = true;
        srPlayer.color = Color.green;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !isJumping)
        {
            Jump();
        }
        ColliderBox();
        Grounded();
        CheckPos();
        Walling();
        Flip();
        Dash();
        ChangeStat();
        AttackArea();
        Shield();
        BlueShield();

        if (Input.GetKeyDown(KeyCode.E) && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    void FixedUpdate()
    {
        Deplacement();
        Gravity();
    }

    void Deplacement() // déplacement et saut du joueur avec "Q" et "D"
    {
        playerMove = (int)Input.GetAxisRaw("Horizontal");
        Vector2 targetvelocity = new Vector2(playerMove * playerSpeed, rbPlayer.velocity.y);
        rbPlayer.velocity = Vector2.SmoothDamp(rbPlayer.velocity, targetvelocity, ref velocity, smoothing);
    }

    void Jump() // défini la puissance du saut 
    {
        if (isGrounded)
        {            
            rbPlayer.AddForce(new Vector2(0, jumpHeight));
            isJumping = true;
        }
    }

    void Grounded() // detecte le contact entre le joueur et le sol.
    {
        if (groundChecker != null)
        {
            isJumping = false;
        }
        else if (isGrounded)
        {
            isJumping = true;
            StartCoroutine(Coyote(false));
        }
    }

    private IEnumerator Coyote(bool isGroundedState) // permet pendant un certain temps de sauter un peu en dehors de la plateforme
    {
        yield return new WaitForSeconds(coyoteTime);
        isGrounded = isGroundedState;
    }

    void Gravity() // permet d'influencer la puissance du saut du player au départ et augmente la gravité du player lors de la chute
    {
        if (rbPlayer.velocity.y < 0)
        {
            rbPlayer.gravityScale = originalGravity * gravityScaleMultiplier;
        }
        else
        {
            rbPlayer.gravityScale = originalGravity;
        }
    }

    void ColliderBox() // créer les boîtes de collisions 
    {
        Collider2D GroundCheckBox = Physics2D.OverlapBox(groundChecker, new Vector2(groundCheckWidth, groundCheckHeight), 0, JumpLayerMask);
        isGrounded = (GroundCheckBox != null);

        Collider2D wallCheckBoxL = Physics2D.OverlapBox(wallCheckerL, new Vector2(wallCheckWidth, wallCheckHeight), 0, JumpLayerMask);
        IsWallingL = (wallCheckBoxL != null);

        Collider2D wallCheckBoxR = Physics2D.OverlapBox(wallCheckerR, new Vector2(wallCheckWidth, wallCheckHeight), 0, JumpLayerMask);
        IsWallingR = (wallCheckBoxR != null);
    }

    private void CheckPos() // définis la position des boîtes
    {
        SpriteRenderer rb = GetComponent<SpriteRenderer>();
        float hauteur = rb.bounds.size.y;
        float largeur = rb.bounds.size.x;
        groundChecker = new Vector2(transform.position.x, transform.position.y - hauteur / 2);
        wallCheckerL = new Vector2(transform.position.x - largeur / 2, transform.position.y);
        wallCheckerR = new Vector2(transform.position.x + largeur / 2, transform.position.y);
    }

    private void OnDrawGizmos() // rend visible les boîtes dans le moteur
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(groundChecker, new Vector2(groundCheckWidth, groundCheckHeight));
        Gizmos.DrawCube(wallCheckerL, new Vector2(wallCheckWidth, wallCheckHeight));
        Gizmos.DrawCube(wallCheckerR, new Vector2(wallCheckWidth, wallCheckHeight));
        Gizmos.DrawCube(attackArea, new Vector2(attackAreaHeight, attackAreaWidth));
        //Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void Walling() // détecte les murs
    {
        if (!((playerMove > 0 && IsWallingL) || (playerMove < 0 && IsWallingR)))
        {
            Deplacement();
        }
    }

    void Dash() // dasher le player en avant et en arrière
    {
        if (dashCooldown > 0)
        {
            canIDash = false;
            dashCooldown -= Time.deltaTime;
        }
        else
        {
            canIDash = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canIDash == true)
        {
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x * dashPower, rbPlayer.velocity.y);
        }
    }

    public void Flip() // permet au joueur de se retourner lorsqu'il change de direction
    {
        if (playerMove < 0)
        {
            srPlayer.flipX = true;
        }
        if (playerMove > 0)
        {
            srPlayer.flipX = false;
        }
    }

    public void ChangeStat()
    {
        if ( Input.GetKeyDown(KeyCode.F) && green)
        {
            green = false;
            srPlayer.color = Color.blue;
            blue = true;
        }
        else if ( Input.GetKeyDown(KeyCode.F) && blue)
        {
            blue = false;
            srPlayer.color = Color.green;
            green = true;
        }
    } // change l'état du joueur le srite du joueur lorsqu'il appuie sur F.

    void Shield() //génére un bouclier qui protège de face
    {
        
        if (Input.GetKeyDown(KeyCode.R) && !shielded)
        {
            aegis.SetActive(true);
            shielded = true;
            Invoke("ShieldOff", shieldedTime);
        }
    }

    void ShieldOff()
    {
        aegis.SetActive(false);
        shielded = false;
    }

    void BlueShield() //génére un bouclier magic qui protège de tout les côtés
    {

        if (Input.GetKeyDown(KeyCode.C) && !blueShielded && blue && playerMagic.GetCurrentMagic() > 0)
        {
            blueShield.SetActive(true);
            blueShielded = true;
            playerMagic.MagicCost(1);
            Invoke("BlueShieldOff", blueShieldedTime);
        }
    }

    void BlueShieldOff()
    {
        blueShield.SetActive(false);
        blueShielded = false;
    }

    private void AttackArea() // définis la position des boîtes
    {
        SpriteRenderer rb = GetComponent<SpriteRenderer>();
        float largeur = rb.bounds.size.x;
        attackArea = new Vector2(transform.position.x + largeur * 1.5f, transform.position.y);
    }

    IEnumerator Attack() //créer un boîte de collision que détecte et tuer
    {
        isAttacking = true;
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackArea, new Vector2(attackAreaWidth, attackAreaHeight), 0, enemyLayerMask);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.TryGetComponent(out MonsterHealth monsterHealth))
            {
                monsterHealth.takeDamage(damage);
            }
        }
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }
}