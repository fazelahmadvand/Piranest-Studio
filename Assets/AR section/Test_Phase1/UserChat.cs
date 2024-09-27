using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserChat : MonoBehaviour
{
    GameObject chatPrefab;
    private void Start()
    {
    }
    public void NextDia() 
    {
        chatPrefab = GameObject.FindGameObjectWithTag("Wolf");
        chatPrefab.GetComponent<CharTalk>().TalkT();
    }
}
