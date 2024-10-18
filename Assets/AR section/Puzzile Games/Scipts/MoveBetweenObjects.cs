using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Piranest.AR
{
    public class MoveBetweenObjects : MonoBehaviour
    {
        [SerializeField] private GameObject targetA;
        [SerializeField] private GameObject targetB;
        [SerializeField] private float Turbulance = 0.8f;
        [SerializeField] private float speed;
        [SerializeField] private float switchDistanceThreshold = 0.1f; 
        //
        Transform target;

        private void Start()
        {
            target = targetA.transform;
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(target.position.x, transform.position.y, target.position.z),
                speed * Time.deltaTime);

            if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                                 new Vector3(targetA.transform.position.x, 0, targetA.transform.position.z)) <= switchDistanceThreshold)
            {
                target = targetB.transform;
                Debug.Log("Switched to B");
            }
            else if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                                      new Vector3(targetB.transform.position.x, 0, targetB.transform.position.z)) <= switchDistanceThreshold)
            {
                target = targetA.transform;
                Debug.Log("Switched to A");
            }
        }
    }
}
