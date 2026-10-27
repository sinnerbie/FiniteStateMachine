using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Pursue: State
{
    public Pursue(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.PURSUE;
        //TODO
    }
    public override void Enter()
    {
        //TODO
        anim.SetTrigger("isRunning");
        agent.speed = 3;
        base.Enter();
    }
    public override void Update()
    {
        //TODO
        if (CanSeePlayer())
            agent.SetDestination(player.position);
        else
        {
            nextState = new Idle(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }

        if (CanAttackPlayer())
        {
            nextState = new Attack(npc, agent, anim, player);
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
