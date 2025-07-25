using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    
    public float speed = 5f;
    private float jumpForce = 1f;
    private float jumpDuration = 0.3f; // 점프 시간
    private float jumpTimer = 0f;
    private bool isJumping = false;
    public AudioClip jumpSound;
    private AudioSource audioSource;
    public Transform person; 

    Animator animator;
    private Rigidbody2D rb; //상위 클래스에 rigidbody가 있어서 _를 붙임
    SpriteRenderer spriteRenderer; //현재 보여지는 이미지 변경하기 위한 스프라이트
    public Sprite idleSprite;   //정지 상태에서 보여줄 단일 스프라이트
    private Vector2 moveVelocity;   //최종 이동 방향, 속도 계산 값

    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();  //하위 오브젝트에서 animator 가져오기
        rb = GetComponent<Rigidbody2D>();   //rigidbody2D는 플레이어 오브젝트에 붙어있어야 함
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); //하위 오브젝트에서 스프라이트렌더럴 가져오기

        audioSource = GetComponent<AudioSource>(); //플레이어에게 붙어있는 오디오소스 가져오기
    }

    // Update is called once per frame
    void Update()
    {
        //입력 받기
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed; //속도 반영

        bool isMoving = moveInput != Vector2.zero; //움직임 여부 확인

        if(moveInput.x != 0){
            spriteRenderer.flipX = moveInput.x < 0; //왼쪽으로 갈 경우 좌우반전
        }

        if(isMoving){
            animator.enabled = true; //움직일 때 애니메이션 작동
        }
        else{
            animator.enabled = false; //정지 시 애니메이션 끄기
            if(idleSprite != null && spriteRenderer != null){ //정지 이미지로 변경
            spriteRenderer.sprite = idleSprite;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            jumpTimer = jumpDuration;
            if(jumpSound != null && audioSource != null){ //점프사운드가 null이 아닐 때 실행
                audioSource.PlayOneShot(jumpSound);
            }
        }

        if (isJumping)
        {
            jumpTimer -= Time.deltaTime;
            float offsetY = Mathf.Sin((1 - jumpTimer / jumpDuration) * Mathf.PI) * jumpForce;
            person.localPosition = new Vector3(0, offsetY, 0); // 위로 점프 효과
            Debug.Log("offsetY = " + offsetY);
            
            if (jumpTimer <= 0f)
            {
                isJumping = false;
                person.localPosition = Vector3.zero;
            }
        }
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}