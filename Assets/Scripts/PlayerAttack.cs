using UnityEngine;

public class PlayerAttack : MonoBehaviour
{    
    private Animator animator;
    private PlayerController playerController;

    private float coolDownTimer = Mathf.Infinity;

    [Header("Controls")]
    private float attackCoolDown = 1.5f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.X) && canAttack())
        {
            //arrow shoots here
            timerReset();
        }
    }
    private void FixedUpdate()
    {
        coolDownTimer += Time.deltaTime;        
    }
    bool canAttack()
    {
        if (coolDownTimer > attackCoolDown) return true; else return false;
    }
    void timerReset()
    {
        coolDownTimer = 0f;
    }
}