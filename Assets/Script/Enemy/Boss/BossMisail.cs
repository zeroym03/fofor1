using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMisail :Bullet
{
    public Transform target { get; set; }
    NavMeshAgent agent;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
      if (transform.position.y <= 2 )
        {
            Destroy(gameObject);
        }
        StartCoroutine(MisailDes());
    }
    private void Update()
    {
        agent.SetDestination(target.position);
    }
    IEnumerator MisailDes()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
