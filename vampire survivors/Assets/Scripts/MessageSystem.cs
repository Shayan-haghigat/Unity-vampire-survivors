using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSystem : MonoBehaviour
{   
    public static MessageSystem instance;//برای ایجاد سینگتون هستش 
    private void Awake() {
        instance = this ;
    }
    [SerializeField] GameObject damageMessage ;
    List<TMPro.TextMeshPro> messagePool;
    int objectCount = 10 ;
    int Count ;
    private void Start() {
        messagePool = new List<TMPro.TextMeshPro>();
        for (int i = 0; i < objectCount; i++)
        {
            Populate();
        }
    }

    public void Populate()
    {
        GameObject go = Instantiate(damageMessage,transform);//این متد برای ایجاد یه کپی از اون پری فب یا شی هستش 
        messagePool.Add(go.GetComponent<TMPro.TextMeshPro>());
        go.SetActive(false);
    }

    public void PostMassage(string text , Vector3 WorldPostion){
        messagePool[Count].gameObject.SetActive(true);
        messagePool[Count].transform.position = WorldPostion;
        messagePool[Count].text = text;
        Count ++ ;

        if (Count >= objectCount)
        {
            Count = 0 ;
        }
        //go.transform.position = WorldPostion ;
        //go.GetComponent<TMPro.TextMeshPro>().text = text;
    }
}
