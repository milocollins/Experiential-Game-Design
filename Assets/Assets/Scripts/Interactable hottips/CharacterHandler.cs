using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterHandler : MonoBehaviour
{
    public string npcName;
    //public DialogueContainer myDialogue;
    public CharacterProfile myProfile;
    private NavMeshAgent myAgent;
    public Camera myCamera;
    private Animator myAnim;
    public List<Transform> targets = new List<Transform>();
    private bool idling = false;
    public enum Emotion
    {

    }
    public enum State
    {
        Walking,
        Idle,
        Talking
    }
    public State currentState { get; set; }
    public Transform currentTarget;
    private void Awake()
    {
        //myCamera.enabled = false;
        myAnim = GetComponent<Animator>();
        myAgent = GetComponent<NavMeshAgent>();
        myCamera = GetComponentInChildren<Camera>();
        currentTarget = targets[0];
        currentState = State.Walking;
    }
    private void Start()
    {
    }
    private void Update()
    {
        switch (currentState)
        {
            case State.Walking:
                WalkTo(currentTarget);
                break;
            case State.Idle:
                if (!idling)
                {
                    StartCoroutine(Idle(3f));
                }
                break;
            case State.Talking:
                break;
            default:
                break;
        }
        HandleAnimation();
    }
    private void WalkTo(Transform t)
    {
        myAgent.SetDestination(t.position);
        if (myAgent.remainingDistance == 0)
        {
            currentState = State.Idle;
        }
    }
    private IEnumerator Idle(float t)
    {
        idling = true;
        ChangeTarget();
        yield return new WaitForSeconds(t);
        idling = false;
        currentState = State.Walking;
    }
    private void ChangeTarget()
    {
        Transform previousTransform = currentTarget;
        targets.Remove(previousTransform);
        targets.Add(previousTransform);
        currentTarget = targets[0];
    }
    private void HandleAnimation()
    {
        if (Mathf.Abs(myAgent.velocity.x) > 0.01 || Mathf.Abs(myAgent.velocity.y) > 0.01 || Mathf.Abs(myAgent.velocity.z) > 0.01)
        {
            myAnim.SetBool("Moving", true);
        }
        else
        {
            myAnim.SetBool("Moving", false);
        }
    }
    //public DialogueContainer GetMyDialogue()
    //{
    //    return myDialogue;
    //}
    public Camera GetMyCamera()
    {
        return myCamera;
    }

}
