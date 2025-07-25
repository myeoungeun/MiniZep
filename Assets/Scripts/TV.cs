using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TV : MonoBehaviour
{
    private bool check = false;

    private void OnTriggerEnter2D(Collider2D other){ //플레이어가 영역에 들어오면
        if(other.CompareTag("Player")){
            Debug.Log("Z키를 눌러서 상호작용");
            check = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) { //플레이어가 트리거 영역에서 나왔을 때 
        if(other.CompareTag("Player")){ 
            check = false; //false로 초기화
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (check && Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene("FlappyPlane");
        }
    }
}
