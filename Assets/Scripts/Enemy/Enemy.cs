using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHP = 100f;
    private float currentHP;
    public float attackRange {get; private set;} = 2f;
    public int damage {get; private set;} = 25;
    
    [SerializeField] private Image hpFillAmount;
    [SerializeField] protected ParticleSystem bloodParticles;

    public Animator animator {get; private set;}
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    public List<Transform> waypontList = new List<Transform>();

    public NavMeshAgent Agent {get => agent;}  //геттер с указателем на сам агент
    [SerializeField] Transform playerTransform;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        currentHP = maxHP;
        stateMachine.Initialize();
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Death(this);
        } else {
            hpFillAmount.fillAmount = currentHP / maxHP;
            stateMachine.PlayerDetection();
        }
        bloodParticles.gameObject.SetActive(true);
        bloodParticles.Play();
    }

    private void Death(Enemy enemy)
    {
        GameManager.Instance.EnemyDefeated(enemy);
        //animator.SetTrigger("Defeated");
        this.gameObject.SetActive(false);
    }

    public Transform GetPlayerTransform()
    {
        return playerTransform;
    }

    private void OnColisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        damageable?.TakeDamage(damage);
        Debug.Log("Player collison");
    }
}
