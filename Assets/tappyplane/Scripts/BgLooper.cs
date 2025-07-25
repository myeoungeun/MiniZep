using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BgLooper : MonoBehaviour
{
    public int numBgCount = 5; //개수
    
    public int obestacleCount = 0;
    public Vector3 obstacleLastPosition  = Vector3.zero;
    
    void Start()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>(); //Obstacle이 달려있는 걸 전부 찾아와서 넘겨줌
        obstacleLastPosition = obstacles[0].transform.position; //그 장애물들을 처음부터 끝까지 배치함
        obestacleCount = obstacles.Length;
        
        for(int i = 0; i < obestacleCount; i++)
        {
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obestacleCount); //배치한 위치를 받아서 다음 배치될곳을 알려줌
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) //충돌 체크
    {
        Debug.Log("Triggered: " + collision.name);
        
        if (collision.CompareTag("BackGround")) //배경태그와 충돌하면 배경을 다시 뒤로 보냄(배경 무한반복)
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x; //박스콜라이더를 찾기위해 콜리젼을 박스콜라이더로 바꿈.
            Vector3 pos = collision.transform.position; //충돌한애의 위치

            pos.x += widthOfBgObject * numBgCount; 
            collision.transform.position = pos;
            return;
        }
        
        Obstacle obstacle = collision.GetComponent<Obstacle>(); //장애물을 찾아서 랜덤위치에 설치함(장애물 무한 반복, 뒤로 넘어가는 장애물 없애는 것처럼 보임. 재활용을 함)
        if (obstacle)
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obestacleCount);
        }
    }
}