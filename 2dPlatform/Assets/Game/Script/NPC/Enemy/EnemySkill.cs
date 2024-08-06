using System.Collections;
using UnityEngine;

[System.Serializable]
public class EnemySkill
{
    public string id;
    public int damage;
    [Multiline]
    public string desc;

    public GameObject prefab;
    public float duration;
    public float cd;

    public float minDist;
    public float maxDist;

    public float cdTimer;


    public ParticleSystem launchEffect;
}