using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera2 : MonoBehaviour
{
    public Transform target;
    float offsetX;
    float offsetY;

    // Start is called before the first frame update
    void Start()
    {
        if(target == null) return;

        offsetX = transform.position.x - target.position.x; //카메라x값 - 플레이어x값. 카메라와 플레이어 사이의 거리가 저장됨
        offsetY = transform.position.y- target.position.y; 
    }

    // Update is called once per frame
    void Update()
    {
        if(target ==null) return;

        Vector3 pos = transform.position; //포지션 값은 바로 가져올수 없어서, 이런식으로 한 번 변수에 저장하고 다시 가져오는 방식을 써야함.
        pos.x = target.position.x + offsetX; //캐릭터가 이동하는 걸 따라가는데, 캐릭터 위치를 따라가는 게 아니라, 처음에 배치한 거리만큼 떨어진 상태로 계속 따라감
        pos.y = target.position.y + offsetY;
        transform.position = pos;
    }
}
