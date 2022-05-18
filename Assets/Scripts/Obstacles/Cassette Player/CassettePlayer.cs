using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

public enum CassetteType { Blue, Red, Gray, Orange, Green, Black }

public class CassettePlayer : MonoBehaviour
{
    [SerializeField] InteractableButton button;
    CassetteSlot[] casseteSlots;
    CassetteType[] casseteSolutionArray;
    public static Action<string> LogCassettePlayerMessage;
    AudioSource cassetteAudioPlayer;
    Coroutine tryCouroutine;


    private void Awake()
    {
        casseteSlots = GetComponentsInChildren<CassetteSlot>();
        cassetteAudioPlayer = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        button.PressButton += TrySolve;
    }

    private void OnDisable()
    {
        button.PressButton -= TrySolve;
    }

    void Start()
    {
        casseteSolutionArray = (CassetteType[])Enum.GetValues(typeof(CassetteType));
    }


    void TrySolve()
    {
        if (tryCouroutine != null) return;
        tryCouroutine = StartCoroutine(SolveRoutine());
    }

    IEnumerator SolveRoutine()
    {
        foreach (CassetteSlot slot in casseteSlots)
        {
            if (slot._currentCassetteModel == null)
            {
                LogCassettePlayerMessage?.Invoke("Won't play without a cassette in each slot");
                yield break;
            }
        }
        yield return StartCoroutine(PlayTapes());
        if (!CheckSolution()) yield break;
        LogCassettePlayerMessage?.Invoke("Solved!");
    }

    private IEnumerator PlayTapes()
    {


        AudioClip[] tapeClips = new AudioClip[casseteSlots.Length];

        for (int i = 0; i < casseteSlots.Length; i++)
        {
            CassetteItem cassetteItem = casseteSlots[i]._currentCassetteModel.GetComponent<Pickup>()._item as CassetteItem;
            tapeClips[i] = cassetteItem._tapeClip;
            Debug.Log(tapeClips[i].name);
        }

        foreach (AudioClip tape in tapeClips)
        {
            cassetteAudioPlayer.clip = tape;
            cassetteAudioPlayer.Play();
            yield return new WaitForSeconds(tape.length);
        }
    }

    bool CheckSolution()
    {

        int idx = 0;

        foreach (CassetteSlot slot in casseteSlots)
        {
            CassetteItem cassetteItem = slot._currentCassetteModel.GetComponent<Pickup>()._item as CassetteItem;

            if (cassetteItem._cassetteType != casseteSolutionArray[idx])
            {
                LogCassettePlayerMessage?.Invoke("Incorrect Order");
                return false;
            }

            idx++;
        }
        return true;
    }
}


