using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //add or remove the interactable from the list of interactables
    public bool useEvents;
    [SerializeField]
    //message to display when the player is in range of the interactable
    public string promptMessage;

    public void BaseInteract(){
        if(useEvents){
            GetComponent<InteractionEvent>().onInteract.Invoke();
        }
        Interact();
    }

    protected virtual void Interact(){

    }

}
