using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState currentState;

    //property for the patrol state

    public void Initialise() {
        ChangeState(new PatrolState());

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState != null)
        {
            currentState.Perform();
        }
    }

    public void ChangeState(BaseState newState)
    {
        if(currentState != null)
        {
            //clean up currentState
            currentState.Exit();
        }
        currentState = newState;
        
        if(currentState != null)
        {
            //setup new state
            currentState.stateMachine = this;
            currentState.enemy = GetComponent<Enemy>();
            currentState.Enter();
        }
    }
}
