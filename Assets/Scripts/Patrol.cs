using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Mover el personaje por los wayPoints, necesita un index para saber por donde vamos
//Recuerda que no tenemos que guardar los waypoints en ningún sitio
//puesto que la clase GameEnvirontment (Singleton) ya lo hace
public class Patrol: State
{
    int completedLoops;
    int currentIndex = 0;

    public float accuracy = 0.25f;
    public Patrol(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.PATROL;
       //TODO
    }

    public override void Enter()
    {
        //TODO
        completedLoops = 0;
        GameEnvironment ge = GameEnvironment.Singleton;
        agent.isStopped = false;
        agent.speed = 2;
        agent.SetDestination(ge.Checkpoints[currentIndex].transform.position);
        base.Enter();
    }
    public override void Update()
    {
        //TODO
        anim.SetTrigger("isWalking");
        GameEnvironment ge = GameEnvironment.Singleton;
        float distance = Mathf.Infinity;
        distance = Vector3.Distance(npc.transform.position, ge.Checkpoints[currentIndex].transform.position);
        if (distance <= accuracy)
        {
            currentIndex = (currentIndex + 1) % ge.Checkpoints.Count;
            if (currentIndex == 0) completedLoops++;
            agent.SetDestination(ge.Checkpoints[currentIndex].transform.position);
        }
        if (CanSeePlayer())
        {
            nextState = new Pursue(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
        if (IsPlayerBehind())
        {
            nextState = new RunAway(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        //TODO
        base.Exit();
    }
}