using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float maxDist = 50;
    public Transform playerPos;
    private Vector3 playerDir;
    private Vector3 playerAimSpot;
    private enum EnemyState
    {
        Idle,
        MoveTowardsPlayer,
        AttackPlayer
    }
    private EnemyState enemyState;
    private float randomMoveTime;
    private Vector2 randomMoveDir;

    private bool collidingWithPlayer = false;

    public float damage = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AddDamage());
    }

    public IEnumerator AddDamage() {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 1f));
        
        while (true) {
            yield return new WaitForSeconds(1.0f);
            if (collidingWithPlayer)
                PlayerCar.instance.Damage(damage);
        }
    }

    // Update is called once per frame
    void Update()
    { 
        playerAimSpot = playerPos.position;
        playerAimSpot.y += 5;
        playerDir = playerAimSpot - transform.position;
        if (playerDir.magnitude<maxDist && 1<playerDir.magnitude)enemyState = EnemyState.MoveTowardsPlayer;
        else enemyState = EnemyState.Idle;
        // switch statement to apply enemy state
        switch (enemyState)
        {
            case EnemyState.MoveTowardsPlayer:
                MoveTowardsPlayer(playerDir);
                break;
            case EnemyState.AttackPlayer:
                break;
            case EnemyState.Idle:
                MoveIdle();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void MoveIdle()
    {
        randomMoveTime = UnityEngine.Random.Range(100, 500);
        randomMoveDir = new Vector2(UnityEngine.Random.Range(0, 1), UnityEngine.Random.Range(0, 1));
    }

    // function to move enemy towards player
    public void MoveTowardsPlayer(Vector3 dir)
    {
        dir.Normalize();
        transform.Translate(dir * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider c) {
        if (c.gameObject.tag == "Player") collidingWithPlayer = true;
    }
    private void OnTriggerExit(Collider c) {
        if (c.gameObject.tag == "Player") collidingWithPlayer = false;
    }
}
