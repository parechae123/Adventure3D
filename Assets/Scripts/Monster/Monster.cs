using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using System.Runtime.InteropServices;

public class Monster : MonoBehaviour
{
    [SerializeField] Transform playerTR;
    [SerializeField] UnityEngine.AI.NavMeshAgent agent;
    float goalTraceTime= 0.2f;
    float currTraceTime;
    float agroRange;
    private void Start()
    {
        Collider col = GetComponent<Collider>();
        GameManager.GetInstance.damageDict.Add(col, Damaged);
        if (playerTR == null) playerTR = GameObject.Find("Player").transform;
        if (agent == null) agent=GetComponent<UnityEngine.AI.NavMeshAgent>();
        agroRange = (10f * 10f);
        GameManager.GetInstance.infoData.Add(col, ViewInfo);
    }
    // Update is called once per frame
    void Update()
    {
        currTraceTime += Time.deltaTime;
        if (goalTraceTime <= currTraceTime)
        {
            currTraceTime = 0f;
            if (agroRange <= GetDistSquared(transform.position, playerTR.position)) return;


            agent.SetDestination(playerTR.position);
        }
    }
    void Damaged(int damag)
    {
        Destroy(gameObject);
    }
    float GetDistSquared(Vector3 from, Vector3 to)
    {
        float x = from.x - to.x;
        float y = from.y - to.y;
        float z = from.z - to.z;
        x *= x;
        y *= y;
        z *= z;
        return x + y + z;
    }

    void ViewInfo()
    {
        GameManager.GetInstance.printInfo.Invoke(ResourceManager.GetInstance.interactionDatas["monster"]);
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.GetInstance.damageDict[collision.collider].Invoke(10);
        }
    }
}
