using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class Week10GameManager : Singleton<Week10GameManager>
{
    public static Week10GameManager Instance { get; private set; }
    public Transform[] spawnPoints;

    public List<FrogController> players;
    public GamePhase currentPhase = GamePhase.Starting;
    public Timer timer;
    public FrogController victor;


    public enum GamePhase
    {
        Starting,
        Started,
        Ending
    }

    private protected override void Awake()
    {
        base.Awake();
        timer.enabled = true;
    }

    public void OnPlayerJoined(PlayerInput input)
    {
        Transform nextSpawnPos = spawnPoints[players.Count];
        input.transform.position = nextSpawnPos.position;
        players.Add(input.GetComponent<FrogController>());

        if (players.Count == 2)
        {
            timer.enabled = false;
        }
    }

    public void KillAll()
    {
        foreach (FrogController controller in players)
        {
            controller.Die();

            StartCoroutine(EndRound());
        }
    }

    public bool CanPlayerJump() => victor == null && currentPhase == GamePhase.Started;


    public void OnPlayerJumped(FrogController player)
    {
        victor = player;
        players.Find(p => p != victor).Die();
        StartCoroutine(EndRound());
    }

    public IEnumerator EndRound()
    {
        currentPhase = GamePhase.Ending;

        yield return new WaitForSeconds(3f);

        currentPhase = GamePhase.Starting;
        timer.CountDown();
        victor = null;

        for (int i = 0; i < players.Count; i++) {
            players[i].Revive(spawnPoints[i].position);
        }
    }
}
