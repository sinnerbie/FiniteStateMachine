using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RunAway : State
{
    //Si definimos como global "safeLocation" podremos acceder desde toda la clase
    GameObject safeLocation;
    public float accuracy = 0.25f;
    public RunAway(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.RUNAWAY;
        safeLocation = GameObject.FindGameObjectWithTag("Safe");
    }
    public override void Enter()
    {
        //TODO
        agent.isStopped = false;
        agent.SetDestination(safeLocation.transform.position);
        base.Enter();
    }
    public override void Update()
    {
        float distance = Vector3.Distance(safeLocation.transform.position, npc.transform.position);
        if (distance <= accuracy)
        {
            nextState = new Idle(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        //TODO
        agent.isStopped = true;
        base.Exit();
    }

}
