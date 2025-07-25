using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    public float highPosY = 1f; //장애물이 상하로 이동할 때 얼마나 이동시킬지
    public float lowPosY = -1f;

    public float holeSizeMin = 1f; //top, bottom 사이 공간 얼마나 가져갈지
    public float holeSizeMax = 3f;

    public Transform topObject;
    public Transform bottomObject;

    public float widthPadding = 4f; //장애물 사이의 폭
    GameManager gameManager;

    private void Start(){
        gameManager = GameManager.Instance;
    }

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount){
        float holeSize = Random.Range(holeSizeMin, holeSizeMax); //랜덤 위치
        float halfHoleSize = holeSize/2f;    //절반 사이즈

        topObject.localPosition = new Vector3(0, halfHoleSize); //holesize의 반만큼 올림
        bottomObject.localPosition = new Vector3(0, -halfHoleSize); //반만큼 내림
        //localPosition은 로컬 좌표(부모 오브젝트 기준), 그냥 position은 월드 좌표(전체 기준)
        //장애물은 월드좌표로 할 경우 매번 똑같은 위치에 생성됨. 그래서 지역좌표 사용

        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        placePosition.y = Random.Range(lowPosY, highPosY); //y도 랜덤

        transform.position = placePosition;

        return placePosition;
    }

    private void OnTriggerExit2D(Collider2D collision) { //장애물과 물리적인 충돌을 하지 않고 벗어날 때
       Player player = collision.GetComponent<Player>();
       if(player != null){ //플레이어가 맞다면
            gameManager.AddScore(1); //점수 +1
       }
    }
}