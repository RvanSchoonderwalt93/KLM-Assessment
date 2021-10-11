using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class AirplaneController : MonoBehaviour
{
    public Airplane airplane;
    public GameManager manager;

    public bool park;
    bool parkCheck;
    public bool lightsOn;

    [SerializeField]
    private GameObject[] hangars;
    [SerializeField]
    private GameObject matchedHangar;

    [SerializeField]
    private Vector3 destination;

    [SerializeField]
    private GameObject planeLight;

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetAgentValues(agent);

        hangars = GameObject.FindGameObjectsWithTag("Hangar");

        for (int i = 0; i < hangars.Length; i++)
        {
            if (gameObject.name == hangars[i].name)
            {
                matchedHangar = hangars[i];
            }
        }

        park = false;
        parkCheck = false;
        lightsOn = false;

        SetRandomDestination();
    }

    void SetAgentValues(NavMeshAgent a)
    {
        a.speed = airplane.speed;
        a.angularSpeed = airplane.angularSpeed;
        a.acceleration = airplane.acceleration;
        a.autoBraking = airplane.autoBreaking;
    }

    void Update()
    {
        agent.SetDestination(destination);

        if (!agent.pathPending && !park)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    SetRandomDestination();
                }
            }            
        }

        Park();
        if (!parkCheck)
        {
            ParkCheck();
        }        
        LightControl();
    }

    void SetRandomDestination()
    {
        float x = Random.Range(-airplane.minMaxX, airplane.minMaxX);
        float z = Random.Range(-airplane.minMaxZ, airplane.minMaxZ);
        destination = new Vector3(x, transform.position.y, z);
    }

    void Park()
    {
        if (park)
        {
            agent.SetDestination(matchedHangar.transform.position);
        }
    }

    void ParkCheck()
    {
        if (Vector3.Distance(transform.position, matchedHangar.transform.position) <= 0.5f && park)
        {
            Debug.Log("Plane " + gameObject.name + " Parked");
            lightsOn = false;
            GetComponentInChildren<TextMeshPro>().text = "";
            manager.parkedPlanes++;
            parkCheck = true;
        }
    }

    void LightControl()
    {
        if (lightsOn)
        {
            planeLight.SetActive(true);
        }
        else
        {
            planeLight.SetActive(false);
        }
    }
}
