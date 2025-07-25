using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;

    public float flapForce = 6f; //점프 높이
    public float forwardSpeed = 3f; //전진 스피드
    public bool isDead = false; //사망여부
    float deathCooldown = 0f; //충돌하고 바로 죽는게 아니라 일정 시간 이후에 사망

    bool isFlap = false; //점프 여부
    public bool godMode = false; //게임 테스트용 관리자모드

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance; //클래스명으로 접근

        animator = GetComponentInChildren<Animator>(); //플레이어에게 있는 animator을 가져옴
        _rigidbody = GetComponent<Rigidbody2D>();

        if(animator == null){
            Debug.LogError("오류");
        }
        if(_rigidbody == null){
            Debug.LogError("오류");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead){
            if(deathCooldown <= 0){ //cooldown이 0보다 작지 않다는건, 아직 남아있다는 뜻임
                //게임 재시작
                if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){ //스페이스, 마우스 왼쪽(스마트폰 기준 터치)을 누를 때 true
                    gameManager.RestartGame();
                }
            }
            else{
                deathCooldown -= Time.deltaTime;
            }
        }
        else{
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){ //스페이스, 마우스 왼쪽(스마트폰 기준 터치)을 누를 때 true
                isFlap = true;
            }
        }
    }

    private void FixedUpdate(){ //물리 처리
        if(isDead) return; //죽었으면 실행x

        Vector3 velocity = _rigidbody.velocity; //가속도를 가져옴
        velocity.x = forwardSpeed; //같은 속도로 계속 이동

        if(isFlap){ //점프
            velocity.y += flapForce;
            isFlap = false;
        }

        _rigidbody.velocity = velocity; //복사해서 가져온 값을 다시 넣어주는 작업이 필요함

        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f), -90, 90); //떨어지거나 올라갔을 때
        transform.rotation = Quaternion.Euler(0,0,angle); //z축 회전시키기
    }

    private void OnCollisionEnter2D(Collision2D collision) { //충돌했을 때
        if(godMode) return;
        if(isDead) return;

        isDead = true; //사망 체크
        deathCooldown = 1f; //죽고 1초의 딜레이

        animator.SetInteger("IsDie", 1); //애니메이터의 IsDie 값을 1로 변경 -> flap를 타고 die로 가서 die의 애니메이션이 실행됨
        gameManager.GameOver();
    }
}
