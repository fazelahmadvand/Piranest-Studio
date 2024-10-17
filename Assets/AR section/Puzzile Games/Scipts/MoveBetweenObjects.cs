using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Piranest.AR
{
    public class MoveBetweenObjects : MonoBehaviour
    {
        [SerializeField] private GameObject targetA;
        [SerializeField] private GameObject targetB;
        [SerializeField] private Transform baseTransform;
        [SerializeField] private float Turbulance = 0.8f;
        [SerializeField] private float speed;
        //
        Transform target;
        private void Start()
        {
            target = targetA.transform;
        }
        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x , baseTransform.position.y + Turbulance , target.position.z), speed * Time.deltaTime);
            if (transform.position.x >= targetA.transform.position.x) 
            {
                target = targetB.transform;
                Debug.Log("B");
            }
            else if (transform.position.x <= targetB.transform.position.x)  
            {
                target = targetA.transform;
                Debug.Log("A");
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            
        }
    }
}
