using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public static float REDUCTION_COEFF = 100;
    [SerializeField] private EnemyInfos _enemyInfos;
    private GameObject _target;
    private String _name;
    private float _health;
    private float _armor;
    private float _damage;
    private float _range;
    private float _attackSpeed;
    private float _moveSpeed;

    private bool _shouldMove = true;
    private bool _targetInRange = false;
    NavMeshAgent agent;

    //set
    public void Set(EnemyInfos infos) => _enemyInfos = infos;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetTarget(GameObject.FindWithTag("Base"));
        if(_enemyInfos != null)
        {
            _name = _enemyInfos.name;
            _health = _enemyInfos.health;
            _armor = _enemyInfos.armor;
            _damage = _enemyInfos.damage;
            _range = _enemyInfos.range;
            _attackSpeed = _enemyInfos.attackSpeed;
            _moveSpeed = _enemyInfos.moveSpeed;
            agent.speed = _moveSpeed;
            agent.stoppingDistance = _range;
            GetComponent<CapsuleCollider>().radius = _range;
        }
        StartMovement();
    }
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag(tag) && !_targetInRange)//collides with other enemies
        {
            SetPath();
        }
        else if (other.CompareTag(_target.tag))
        {
            _targetInRange = true;
            StopMovement();
        }
        
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag(_target.tag))
        {
            Debug.Log("parti de la target");
            _targetInRange = false;
            StartMovement();
        }
    }

    public bool ReceiveDamage(float damageAmount)
    {
        float armorReduction = Mathf.Sqrt(_armor)/Mathf.Sqrt(_armor + REDUCTION_COEFF);//100 is good
        float realDamage = damageAmount *(1 - armorReduction);
        _health -= realDamage;
        if(_health <= 0)
        {
            _health = 0;
            return true;
        }
        return false;
    }


    //Setters and getters
    public void SetHealth(float health)
    {
        _health = health;
    }
    public float GetHealth()
    {
        return _health;
    }
    
    public void SetArmor(float armor)
    {
        _armor = armor;
    }
    public float GetArmor()
    {
        return _armor;
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }
    public float GetDamage()
    {
        return _damage;
    }

    public void SetRange(float range)
    {
        _range = range;
    }
    public float GetRange()
    {
        return _range;
    }

    public void SetAttackSpeed(float attackSpeed)
    {
        _attackSpeed = attackSpeed;
    }
    public float GetAttackSpeed()
    {
        return _attackSpeed;
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        _moveSpeed = moveSpeed;
    }
    public float GetMoveSpeed()
    {
        return _moveSpeed;
    }

    //Target and Movements Methods
    public void SetTarget(GameObject target)
    {
        _target = target;
        SetPath();
    }
    public void ResetTarget()//reset la target à la base
    {
        SetTarget(GameObject.FindGameObjectWithTag("Base"));
    }

    public GameObject FindPlayer()
    {
        if(_target == null)
            return GameObject.FindGameObjectWithTag("Player");
        else
            return null;
    }

    private void SetPath()
    {
        agent.destination = _target.transform.position;
    }

    public void StartMovement()
    {
        if(_target != null)
        {
            SetPath();
            agent.isStopped = false;
            _shouldMove = true;
        
        }
    }
    public void StopMovement()
    {
        agent.isStopped = true;
        _shouldMove = false;
        agent.ResetPath() ;
    }

}
