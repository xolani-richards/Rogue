using System.Collections.Generic;
using UnityEngine;
using ROGUE.Characters;
using MEC;

public class NPCController : CharacterController
{
    [Header("Set up")]
    [SerializeField] List<Goal> goals = new List<Goal>();
    [SerializeField, Range(1, 60)] int frequency = 10; 

    [Header("Status")]
    [SerializeField] List<string> debug = new();
    [SerializeField] Goal currentGoal = null;

    NPC npc;
    float lastRun;
    float delay;

    void Awake()
    {
        delay = (float)1f/frequency;
        npc = GetComponent<NPC>();
        Init();
    }

    void OnValidate()
    {
        delay = (float)1f/frequency;
        Debug.Log(delay);
    }

    void Init()
    {
        List<Goal> list = new ();
        foreach (var goal in goals)
        {
            Goal tmpGoal = Instantiate(goal);
            tmpGoal.Bind(npc);
            list.Add(tmpGoal);
        }
        goals = list;

        Timing.RunCoroutine(Process());
    }

    IEnumerator<float> Process ()
    {
        while (enabled)
        {
            lastRun = delay + Random.Range(-0.01f, 0.01f);
            yield return Timing.WaitForSeconds(lastRun);
            OnTick(lastRun);
        }
    }

    // void FixedUpdate()
    // {
    //     float deltaTime = Time.time - lastRun;
    //     OnTick(deltaTime);
    //     lastRun = Time.time;
    // }

    public void OnTick(float deltaTime)
    {
        if (goals.Count == 0) return;
        CalculateGoals();

        Goal highestScore = goals[0];
        if (highestScore != currentGoal) SwitchGoal(highestScore);

        Node.Status status = currentGoal.Process(deltaTime);
        if (status != Node.Status.RUNNING)
        {
            currentGoal.OnExit();
            currentGoal = null;
        }
    }

    void CalculateGoals()
    {
        debug.Clear();
        foreach (Goal goal in goals)
        {
            float score = goal.Evaluate();
            debug.Add($"{goal.displayName}: {score}");
        }
        goals.Sort((a, b) => b.score.CompareTo(a.score));
    }

    void SwitchGoal(Goal newGoal)
    {
        Debug.Log($"Switching Goal: {newGoal.displayName}");
        if (currentGoal != null) currentGoal.OnExit();
        currentGoal = newGoal;
        currentGoal.OnStart();
    }
}