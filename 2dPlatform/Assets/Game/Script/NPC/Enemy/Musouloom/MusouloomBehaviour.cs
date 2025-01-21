using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusouloomBehaviour : MonoBehaviour
{
    public bool isInTest;

    public static MusouloomBehaviour instance;

    public MusouloomMove move { get; private set; }
    public MusouloomHealth health { get; private set; }
    public MusouloomJump jump { get; private set; }
    public MusouloomExposion exposion { get; private set; }
    public MusouloomAlarmSpot alarmSpot { get; private set; }
    public MusouloomJumpSpot jumpSpot { get; private set; }
    public MusouloomAnimeEvents animeEvents { get; private set; }
    public MusouloomExplosionCheckArea explosionCheckArea { get; private set; }
    public MusouloomExplosionDamageArea explosionDamageArea { get; private set; }


    public GameObject Neko;

    public Animator animator { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        move = GetComponent<MusouloomMove>();
        health=GetComponent<MusouloomHealth>();
        jump = GetComponent<MusouloomJump>();
        exposion =GetComponent<MusouloomExposion>();


        alarmSpot = GetComponentInChildren<MusouloomAlarmSpot>();
        jumpSpot = GetComponentInChildren<MusouloomJumpSpot>();
        animeEvents = GetComponentInChildren<MusouloomAnimeEvents>();
        explosionCheckArea = GetComponentInChildren<MusouloomExplosionCheckArea>();
        explosionDamageArea = GetComponentInChildren<MusouloomExplosionDamageArea>();
        animator = GetComponentInChildren<Animator>();
    }



}
