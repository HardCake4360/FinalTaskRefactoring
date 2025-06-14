using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Transform References")]
    [SerializeField] private Transform movementOrientation;
    [SerializeField] private Transform characterMesh;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float gravitationalAcceleration;
    [SerializeField] private float jumpForce;
    [Space(10.0f)]
    [SerializeField, Range(0.0f, 1.0f)] private float lookForwardThreshold;
    [SerializeField] private float lookForwardSpeed;
    [SerializeField] GameObject steppingObj;

    [Header("Status")]
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject shoes;
    [SerializeField] GameObject wings;
    [SerializeField] public float hp;
    [SerializeField] public float maxHp;
    [SerializeField] private float stamina;
    [SerializeField] private float maxStamina;
    [SerializeField] private float flightVelocity;
    [SerializeField] public bool armed;
    [SerializeField] public bool shoed;
    [SerializeField] public bool winged;
    [SerializeField] public bool imuneToStep;

    [Header("Stat Useage")]
    [SerializeField] private float hpMinus;
    [SerializeField] private float staminaMinus;
    [SerializeField] private float staminaRecover;

    [Header("Debug")]
    [SerializeField]private bool isGround;
    private float horizontalInput;
    private float verticalInput;
    public bool jumpFlag = false;
    public bool flyFlag = false;
    private bool runFlag = false;
    public bool attackFlag = false;
    public bool staCoolDown = false;

    private CharacterController m_characterController;
    private GroundChecker m_groundChecker;
    private playerAnimEvent m_aniEvent;
    private Animator animator;
    private Vector3 velocity;
    private Vector3 lastFixedPosition;
    private Quaternion lastFixedRotation;
    private Vector3 nextFixedPosition;
    private Quaternion nextFixedRotation;

    float time;

    private static Player player;
    public static Player GetInstance()
    {
        return player;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = this;
        m_characterController = GetComponent<CharacterController>();
        m_groundChecker = GetComponent<GroundChecker>();
        animator = GetComponent<Animator>();
        m_aniEvent = GetComponentInChildren<playerAnimEvent>();
        velocity = new Vector3(0, 0, 0);
        lastFixedPosition = transform.position;
        lastFixedRotation = transform.rotation;
        nextFixedPosition = transform.position;
        nextFixedRotation = transform.rotation;

        horizontalInput = 0.0f;
        verticalInput = 0.0f;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        isGround = m_groundChecker.IsGrounded();
        animator.SetBool("isGrounded", isGround);

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetMouseButtonDown(0) && armed)
        {
            animator.SetBool("attack", true);
            SoundManager.Instance.playSound(2);
            attackFlag = true;
        }
        if (attackFlag == true)
        {
            time += Time.deltaTime;
            if (time > 1f)
            {
                animator.SetBool("attack", false);
                attackFlag = false;
                time = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_groundChecker.IsGrounded())
            {
                jumpFlag = true;
                animator.SetBool("jump", true);
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) && !staCoolDown)
        {
            runFlag = true;
            animator.SetBool("run", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            runFlag = false;
            animator.SetBool("run", false);
        }

        if (!m_groundChecker.IsGrounded())
        {
            if (winged)
            {
                if (Input.GetKeyDown(KeyCode.Space) && stamina >= 0 && !staCoolDown)
                {
                    flyFlag = true;
                }
                else if (Input.GetKeyUp(KeyCode.Space))
                {
                    flyFlag = false;
                }
                if (staCoolDown) flyFlag = false;
            }
            steppingObj = null;
        }
        if (jumpFlag == false)
        {
            animator.SetBool("jump", false);
        }

        if (stamina <= 0)
        {
            staCoolDown = true;
        }
        if(stamina == maxStamina)
        {
            staCoolDown = false;
        }

        Vector3 planeVel = GetXZVelocity(horizontalInput, verticalInput);
        if (planeVel.magnitude == 0) runFlag = false;
        if (stamina <= 0)
        {
            runFlag = false;
            animator.SetBool("run", false);
        }

        if (runFlag || flyFlag)
        {
            if (stamina > 0)
            {
                if (runFlag) stamina -= staminaMinus * Time.deltaTime;
                else if (flyFlag) stamina -= staminaMinus * 3 * Time.deltaTime;
            }
            else
                stamina = 0;
        }
        else
        {
            if (stamina < maxStamina)
                stamina += staminaRecover * Time.deltaTime;
            else
                stamina = maxStamina;
        }
        animator.SetBool("run", runFlag);

        if (armed)
        {
            weapon.SetActive(true);
        }
        else
        {
            weapon.SetActive(false);
        }
        if (shoed)
        {
            shoes.SetActive(true);
        }
        else
        {
            shoes.SetActive(false);
        }
        if (winged)
        {
            wings.SetActive(true);
        }
        else
        {
            wings.SetActive(false);
        }


        float interpolationAlpha = (Time.time - Time.fixedTime) / Time.fixedDeltaTime;
        m_characterController.Move(velocity*Time.deltaTime/*Vector3.Lerp(lastFixedPosition, nextFixedPosition, interpolationAlpha) - transform.position*/);
        characterMesh.rotation = Quaternion.Slerp(lastFixedRotation, nextFixedRotation, interpolationAlpha);


    }

    private void FixedUpdate()
    {
        //if (!IsOwner) return;

        lastFixedPosition = nextFixedPosition;
        lastFixedRotation = nextFixedRotation;

        Vector3 planeVelocity = GetXZVelocity(horizontalInput, verticalInput);
        float yVelocity = GetYVelocity();
        velocity = new Vector3(planeVelocity.x, yVelocity, planeVelocity.z);
        
        if (m_groundChecker.IsGrounded() 
            && steppingObj != null)
        {
            if(steppingObj.TryGetComponent(out Rigidbody rb))
                velocity += rb.velocity;
        }
        
        if (planeVelocity.magnitude / speed >= lookForwardThreshold)
        {
            nextFixedRotation = Quaternion.Slerp(characterMesh.rotation, Quaternion.LookRotation(planeVelocity), lookForwardSpeed * Time.fixedDeltaTime);
        }
        animator.SetFloat("fall", yVelocity);
        /*
        if (yVelocity < 0) animator.SetBool("fall", true);
        else animator.SetBool("fall", false);*/

        if (planeVelocity.magnitude != 0 && !runFlag) animator.SetBool("walk", true);
        else animator.SetBool("walk", false);

        nextFixedPosition += velocity * Time.fixedDeltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (m_groundChecker.IsGrounded() && other.tag == "Ground")
        {
            steppingObj = other.gameObject;
        }

    }

    private Vector3 GetXZVelocity(float horizontalInput, float verticalInput)
    {
        Vector3 moveVelocity = movementOrientation.forward * verticalInput + movementOrientation.right * horizontalInput;
        Vector3 moveDirection = moveVelocity.normalized;
        float moveSpeed;
        if (!runFlag)
        {
            moveSpeed = Mathf.Min(moveVelocity.magnitude, 1.0f) * speed;
        }
        else
        {
            moveSpeed = Mathf.Min(moveVelocity.magnitude, 1.0f) * (speed * 2);
        }

        return moveDirection * moveSpeed;
    }

    /// <remarks>
    /// This function must be called only in FixedUpdate()
    /// </remarks>
    private float GetYVelocity()
    {

        if (!m_groundChecker.IsGrounded())
        {
            if (flyFlag) return flightVelocity;
            return velocity.y - gravitationalAcceleration * Time.fixedDeltaTime;
        }

        if (jumpFlag)
        {
            jumpFlag = false;
            return velocity.y + jumpForce;
        }
        else
        {
            return Mathf.Max(0.0f, velocity.y);
        };
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.rigidbody)
        {
            hit.rigidbody.AddForce(velocity / hit.rigidbody.mass);
        }
    }

    public void GetDemaged(float damage)
    {
        hp -= damage;
    }
    public void GetDamagedDeltaTime(float damage)
    {
        hp -= damage * Time.deltaTime;
    }

    public float GetHp()
    {
        return hp;
    }
    public float GetHpPer()
    {
        return hp / maxHp;
    }
    public float GetStamina()
    {
        return stamina;
    }
    public float GetStaminaPer()
    {
        return stamina / maxStamina;
    }

    public void Attack()
    {
        m_aniEvent.attackFlag = true;
    }
    public void End()
    {
        m_aniEvent.attackFlag = false;
    }
}
