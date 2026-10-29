using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Attack : State
{
	//El NPC debe seguir al jugador y girar mirándolo.
    float rotationSpeed = 2.0f;
    AudioSource shoot;
    public Attack(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.ATTACK;
        //Fuente de audio que lleva adjunta el NPC (Ugg)
        shoot = _npc.GetComponent<AudioSource>();
		//Podriamos hacerlo en la clase State en vez de cargarlo cada vez
        //que se ataca, pero aqui lo hemos hecho asi.
    }
	
    public override void Enter()
    {
        //TODO
        agent.isStopped = true;
        base.Enter();
    }
	
    public override void Update()
    {
        //TODO
        anim.SetTrigger("isShooting");
        shoot.Play();
        Vector3 direction = player.position - npc.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (!CanAttackPlayer())
        {
            nextState = new Pursue(npc, agent, anim, player);
            if (!CanSeePlayer())
                nextState = new Idle(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }
    public override void Exit()
    {
        //TODO
        base.Exit();
    }
}
