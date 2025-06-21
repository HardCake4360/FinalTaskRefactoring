using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum NodeState { Success, Failure, Running }

public abstract class Node
{
    public abstract NodeState Evaluate();
}

public class Selector : Node
{
    private List<Node> children;

    public Selector(List<Node> children)
    {
        this.children = children;
    }

    public override NodeState Evaluate()
    {
        foreach (var child in children)
        {
            var result = child.Evaluate();
            if (result != NodeState.Failure)
                return result;
        }
        return NodeState.Failure;
    }
}

public class Sequence : Node
{
    private List<Node> children;

    public Sequence(List<Node> children)
    {
        this.children = children;
    }

    public override NodeState Evaluate()
    {
        foreach (var child in children)
        {
            var result = child.Evaluate();
            if (result != NodeState.Success)
                return result;
        }
        return NodeState.Success;
    }
}

public class Enemy_B : MonoBehaviour
{
    public float Hp, MaxHp, DetectRange, AttackRange;
    public Transform Target;
    public GameObject blood;
    private NavMeshAgent agent;
    private Animator animator;

    private Node behaviorTree;

    void Start()
    {
        Target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        behaviorTree = new Selector(new List<Node>
        {
            new Sequence(new List<Node> { new IsDeadNode(this), new DieNode(this) }),
            new Sequence(new List<Node> { new LowHpNode(this), new FleeNode(this) }),
            new Sequence(new List<Node> { new InAttackRangeNode(this), new AttackNode(this) }),
            new Sequence(new List<Node> { new InChaseRangeNode(this), new ChaseNode(this) })
        });
    }

    void Update()
    {
        behaviorTree.Evaluate();
    }

    public void Die()
    {
        ObjectPoolManager.Instance.GetFromPool("Blood", transform.position, transform.rotation);
        Destroy(gameObject);
    }
    public void FleeFromPlayer() {
        if (!Target) return;
        agent.enabled = true;
        agent.destination = (transform.position - Target.transform.position).normalized * 5 + transform.position;
    }
    public void ChaseTarget() => GetComponent<NavMeshAgent>().SetDestination(Target.position);
    public void Attack_() { animator.SetBool("near", true); }

    // 애니메이션 이벤트용 공격 함수
    public void Attack()
    {
        GetComponentInChildren<EnemyAnimEvent>().attackFlag = true;
    }

    public void End()
    {
        GetComponentInChildren<EnemyAnimEvent>().attackFlag = false;
    }
}


//조건 노드
public class IsDeadNode : Node
{
    private Enemy_B enemy;
    public IsDeadNode(Enemy_B e) { enemy = e; }

    public override NodeState Evaluate()
    {
        return enemy.Hp <= 0 ? NodeState.Success : NodeState.Failure;
    }
}

public class LowHpNode : Node
{
    private Enemy_B enemy;
    public LowHpNode(Enemy_B e) { enemy = e; }

    public override NodeState Evaluate()
    {
        return enemy.Hp <= enemy.MaxHp * 0.5f ? NodeState.Success : NodeState.Failure;
    }
}

public class InAttackRangeNode : Node
{
    private Enemy_B enemy;
    public InAttackRangeNode(Enemy_B e) { enemy = e; }

    public override NodeState Evaluate()
    {
        float dist = Vector3.Distance(enemy.transform.position, enemy.Target.position);
        return dist <= enemy.AttackRange ? NodeState.Success : NodeState.Failure;
    }
}

public class InChaseRangeNode : Node
{
    private Enemy_B enemy;
    public InChaseRangeNode(Enemy_B e) { enemy = e; }

    public override NodeState Evaluate()
    {
        float dist = Vector3.Distance(enemy.transform.position, enemy.Target.position);
        return dist <= enemy.DetectRange ? NodeState.Success : NodeState.Failure;
    }
}

//행동노드
public class DieNode : Node
{
    private Enemy_B enemy;
    public DieNode(Enemy_B e) { enemy = e; }

    public override NodeState Evaluate()
    {
        enemy.Die(); // 죽음 애니메이션 + 제거
        return NodeState.Success;
    }
}

public class FleeNode : Node
{
    private Enemy_B enemy;
    public FleeNode(Enemy_B e) { enemy = e; }

    public override NodeState Evaluate()
    {
        enemy.FleeFromPlayer(); // 후퇴
        return NodeState.Running;
    }
}

public class AttackNode : Node
{
    private Enemy_B enemy;
    public AttackNode(Enemy_B e) { enemy = e; }

    public override NodeState Evaluate()
    {
        enemy.Attack_();
        return NodeState.Success;
    }
}

public class ChaseNode : Node
{
    private Enemy_B enemy;
    public ChaseNode(Enemy_B e) { enemy = e; }

    public override NodeState Evaluate()
    {
        enemy.ChaseTarget();
        return NodeState.Running;
    }
}
