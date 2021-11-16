using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterHandler : MonoBehaviour
{
    public string npcName;
    public CharacterProfile myProfile;
    public DialogueGraph dialogue;
    private NavMeshAgent myAgent;
    private Animator myAnim;
    private bool idling = false;
    //Behaviour
    [Header("Behaviour")]
    public List<Transform> targets = new List<Transform>();
    public Transform currentTarget;
    public enum State
    {
        Idle,
        Walking,
        Talking
    }
    public State currentState { get; set; }
    //Dialogue
    [Header("Dialogue")]
    //public DialogueContainer myDialogue;
    public Camera dialogueCamera;
    public AudioClip[] emotionSFX = new AudioClip[6];
    public AnimationClip[] emotionAnim = new AnimationClip[6];
    private Dictionary<Emotion, AudioClip> emotionSfxDictionary = new Dictionary<Emotion, AudioClip>();
    private Dictionary<Emotion, AnimationClip> emotionAnimDictionary = new Dictionary<Emotion, AnimationClip>();
    public enum Emotion
    {
        Agree = 0,
        Angry = 1,
        Disagree = 2,
        Happy = 3,
        Neutral = 4,
        Sad = 5
    }
    private void Awake()
    {
        npcName = myProfile.name;
        //myCamera.enabled = false;
        dialogueCamera.enabled = false;
        myAnim = GetComponent<Animator>();
        myAgent = GetComponent<NavMeshAgent>();
        dialogueCamera = GetComponentInChildren<Camera>();
        currentTarget = targets[0];
        currentState = State.Idle;
        //Emotion Dictionary
        var values = Enum.GetValues(typeof(Emotion));
        foreach(Emotion e in Enum.GetValues(typeof(Emotion)))
        {
            int i = (int)e;
            emotionSfxDictionary.Add(e, emotionSFX[i]);
            emotionAnimDictionary.Add(e, emotionAnim[i]);
        }
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
}
