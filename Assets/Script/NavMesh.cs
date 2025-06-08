using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh : MonoBehaviour
{
    public NavMeshAgent Nav;
    public EnemyDetectionScript eDS;
    public Transform player;
    public Transform Enemy;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Track()
    {
        if (!eDS.TargetSeen)
        {
            Nav.SetDestination(Enemy.position);
        }
        else
        {
            Nav.SetDestination(player.position);
        }
       
    }
}
