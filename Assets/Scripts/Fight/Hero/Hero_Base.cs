using UnityEngine;
using System.Collections;
using GameProtocol.dto;

public class Hero_Base : MonoBehaviour
{
    public DTOFightPlayerModel data;

    protected Animator anim;

    private NavMeshAgent agent;

    void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// 申请攻击 申请攻击的目标
    /// </summary>
    public virtual void Attack(Transform[] target)
    {

    }

    /// <summary>
    /// 攻击动画播放结束回调方法
    /// </summary>
    public virtual void Attacked()
    {

    }

    /// <summary>
    /// 释放技能
    /// </summary>
    /// <param name="code"></param>
    public virtual void Skill(int code,Transform[] target)
    {

    }

    /// <summary>
    /// 移动
    /// </summary>
    /// <param name="target"></param>
    public virtual void Move(Vector3 target)
    {
        agent.ResetPath();
        agent.SetDestination(target);
        anim.SetInteger(AnimState.STATE, AnimState.RUN);
    }
}
