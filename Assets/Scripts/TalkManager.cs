using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    // Start is called before the first frame update
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    // Update is called once per frame
    void GenerateData(){
        talkData.Add(1000, new string[] {"나는야 홍학.", "넌 누구야?"}); //문장이 여러개일 수 있으니 배열string
        talkData.Add(100, new string[] {"터치가 가능한 최신식 TV이다.(Z키로 시작)"});
        talkData.Add(200, new string[] {"노트북이다. 게임이 가능할 것 같다.(Z키로 시작)"});
    }

    public string GetTalk(int id, int talkIndex) {   //지정된 대화 문장을 반환하는 함수
        return talkData[id][talkIndex];
    }
}
