
using System;
using UnityEngine;
using Steerings;

namespace FSM
{
    public class FSM_WanderingEnemy : FiniteStateMachine
    {
        public enum State {INITIAL, WANDER, ATTACK, DEAD}

        public State currentState;
        private Seek seek;

        
         
        //private WanderAround wanderAround;
        private KinematicState kinematicState;
    
        // Start is called before the first frame update
        void Start()
        {
         

        }
        public override void Exit()
        {
           // wanderAround.enabled = false;
            
            base.Exit();
        }

        public override void ReEnter()
        {
            currentState = State.INITIAL;
            base.ReEnter();
        }

        // Update is called once per frame
        void Update()
        {
            switch (currentState)
            {
                case State.INITIAL:
                    break;
                case State.WANDER:

                    if ((EnemyBlackboard.instance.player.transform.position - transform.position).magnitude
                        < EnemyBlackboard.instance.playerDetectionRadius)
                    {
                        ChangeState(State.ATTACK);
                    }
                    
                    break;
                case State.ATTACK:
                    break;
                case State.DEAD:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ChangeState(State _newState)
        {
            switch (currentState)
            {
                case State.INITIAL:
                    break;
                case State.WANDER:
                    break;
                case State.ATTACK:
                    seek.enabled = false;
                    seek.target = null;
                    break;
                case State.DEAD:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            switch (_newState)
            {
                case State.INITIAL:
                    break;
                case State.WANDER:
                    break;
                case State.ATTACK:
                    seek.enabled = true;
                    seek.target = EnemyBlackboard.instance.player.gameObject;
                    break;
                case State.DEAD:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            currentState = _newState;
        }
    }
}

