using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;
    private float shotTimer;

    public override void Enter()
    {

    }

    public override void Perform()
    {
      if (enemy.CanSeePlayer())
      {
        //Lock the lose player timer and increment the move and shot timers
        losePlayerTimer = 0;
        moveTimer += Time.deltaTime;
        shotTimer += Time.deltaTime;
        enemy.transform.LookAt(enemy.Player.transform);
        if (shotTimer > enemy.fireRate) {
            Shoot();
        }
        //move enemy to random direction after a certain random time
        if (moveTimer > Random.Range(3, 7))
        {
          enemy.NavMeshAgent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
          moveTimer = 0;
        }
        enemy.LastKnownPlayerPosition = enemy.Player.transform.position;
      }
      else
      {
        losePlayerTimer += Time.deltaTime;
        if (losePlayerTimer > 5)
        {
            //Change to patrol state
            stateMachine.ChangeState(new SearchState());
        }
      }
    }

    public void Shoot()
    {
        //store the gunbarrel position and instantiate a bullet prefab
        Transform gunbarrel = enemy.gunBarrel;
        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, gunbarrel.position, enemy.transform.rotation);
        Vector3 shootDirection = (enemy.Player.transform.position - gunbarrel.position).normalized;
        bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-1f, 1f), Vector3.up) * shootDirection * 40;
        shotTimer = 0;
    }


    public override void Exit()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
