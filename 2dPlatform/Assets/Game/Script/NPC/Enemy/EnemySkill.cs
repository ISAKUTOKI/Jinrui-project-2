using System.Collections;
using UnityEngine;

[System.Serializable]
public class EnemySkill
{
    public string id;
    public bool canUseWhenNotAlerted;
    public int damage;
    [Multiline]
    public string desc;

    public GameObject prefab;
    public float duration;
    public float cd;

    public float minDist;
    public float maxDist;
    [HideInInspector]
    public float cdTimer;


    public ParticleSystem launchEffect;
}