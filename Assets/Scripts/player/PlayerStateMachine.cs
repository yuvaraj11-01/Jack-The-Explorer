using UnityEngine;
using KevinCastejon.HierarchicalFiniteStateMachine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerStateMachine : AbstractHierarchicalFiniteStateMachine
{
    public enum PlayerState
    {
        IDLE,
        WALK,
        DEAD
    }
    public PlayerStateMachine()
    {
        Init(PlayerState.IDLE,
            Create<IdleState, PlayerState>(PlayerState.IDLE, this),
            Create<WalkState, PlayerState>(PlayerState.WALK, this),
            Create<DeadState, PlayerState>(PlayerState.DEAD, this)
        );
    }


    public Rigidbody rb;
    public InputControll inputs;
    public float moveSpeed;
    public Transform transform;
    public PlayerAnimation playerAnimation;
    public Transform SpawnPoint;

    public override void OnStateMachineEntry()
    {
        inputs = new InputControll();
        inputs.PlayerMap.Enable();
    }
    public override void OnStateMachineExit()
    {
    }
    public class IdleState : AbstractState
    {
        PlayerStateMachine _psm;
        public override void OnEnter()
        {
            _psm = (PlayerStateMachine)_parentStateMachine;
            Debug.Log($"{_psm.ToString()} Idle State");
        }
        public override void OnUpdate()
        {
            var readInput = _psm.inputs.PlayerMap.Move.ReadValue<Vector2>();
            if (readInput.magnitude > 0)
            {
                TransitionToState(PlayerState.WALK);
            }
            else
            {
                _psm.playerAnimation.SetMoveSpeed(0);
            }
        }
        public override void OnFixedUpdate()
        {
            _psm.rb.velocity = Vector2.zero;
        }
        public override void OnExit()
        {
        }
    }
    public class WalkState : AbstractState
    {
        PlayerStateMachine _psm;
        Vector3 InputDir;

        public override void OnEnter()
        {
            _psm = (PlayerStateMachine)_parentStateMachine;
            Debug.Log($"{_psm.ToString()} Walk State");
        }
        bool lookTween;
        public override void OnUpdate()
        {
            var readInput = _psm.inputs.PlayerMap.Move.ReadValue<Vector2>();
            InputDir = new Vector3(readInput.normalized.x, 0, readInput.normalized.y);
            if (readInput.magnitude <= 0)
            {
                TransitionToState(PlayerState.IDLE);
            }
            else
            {
                var lookVector = _psm.transform.position + InputDir.normalized;
                if (!lookTween)
                {
                    //lookTween = true;
                    _psm.transform.DOLookAt(lookVector, 0.1f, AxisConstraint.Y, _psm.transform.up).OnComplete(() =>
                    {
                        lookTween = false;
                    });
                }
                _psm.playerAnimation.SetMoveSpeed(1);
            }
        }
        public override void OnFixedUpdate()
        {
            _psm.rb.velocity = InputDir * _psm.moveSpeed;
        }
        public override void OnExit()
        {
        }
    }

    public class DeadState : AbstractState
    {
        PlayerStateMachine _psm;

        float totalTime = 4;
        float timeLeft;
        public override void OnEnter()
        {
            _psm = (PlayerStateMachine)_parentStateMachine;
            Debug.Log($"{_psm.ToString()} Dead State");
            _psm.playerAnimation.EnableRagdoll();
            timeLeft = Time.time ;
            finish = false;
        }
        bool lookTween;
        bool finish;
        public override void OnUpdate()
        {
            if (!finish)
                if (Time.time >= timeLeft + totalTime)
                {
                    Restart();
                    finish = true;
                }
        }
        public override void OnFixedUpdate()
        {
        }
        public override void OnExit()
        {
        }

        void Restart()
        {
            _psm.transform.gameObject.SetActive(false);
            _psm.transform.DOMove(_psm.SpawnPoint.position, 1).OnComplete(() =>
            {
                _psm.playerAnimation.DisableRagdoll();
                _psm.transform.gameObject.SetActive(true);
                TransitionToState(PlayerState.IDLE);
            });
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


    }

    public override string ToString()
    {
        return "[PLAYER]";
    }
}
