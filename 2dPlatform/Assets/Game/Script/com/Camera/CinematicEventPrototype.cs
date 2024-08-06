using DG.Tweening;
using System;
using UnityEngine;

public enum CinematicActionTypes
{
    None,

    SetPosition,
    SetRotation,
    SetPositionAndRotation,

    TweenPosition,
    TweenRotation,
    TweenPositionAndRotation,

    TweenFov,
    CallFunc,
    KillTween,
    Shake,
}

[Serializable]
public class CinematicEventPrototype
{
    public float TimeToNext = 1;
    public CinematicActionTypes type;
    public Ease ease = Ease.Linear;
    public float duration = -1;

    public Transform trans;
    public bool usePositionAndRotation;
    public Vector3 position;
    public Quaternion rotation;

    [HideInInspector]
    public Action action;
    [HideInInspector]
    public float floatParam;
}
