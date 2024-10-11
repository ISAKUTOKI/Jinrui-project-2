using com;
using System.Collections;
using UnityEngine;


public class ChatBoxSystem : MonoBehaviour
{
    public static ChatBoxSystem instance;

    [SerializeField] UiImageAnimation _uia;

    public UiImageAnimation.UiImageAnimationClip clip_in { get { return _uia.GetClip(0); } }
    public UiImageAnimation.UiImageAnimationClip clip_out { get { return _uia.GetClip(1); } }
    public UiImageAnimation.UiImageAnimationClip clip_idle { get { return _uia.GetClip(2); } }
    public UiImageAnimation.UiImageAnimationClip clip_shock { get { return _uia.GetClip(3); } }
    public UiImageAnimation.UiImageAnimationClip clip_show { get { return _uia.GetClip(4); } }
    public UiImageAnimation.UiImageAnimationClip clip_talk { get { return _uia.GetClip(5); } }
    public UiImageAnimation.UiImageAnimationClip clip_hurt { get { return _uia.GetClip(6); } }

    [SerializeField] int idleLoopCount = 3;
    [SerializeField] int stateLoopCount = 3;

    private void Awake()
    {
        instance = this;
        _uia.SetPlayEndCallback(() => { _uia.ToggleDisplay(false); });
    }

    public void IWantShow()
    {
        AddStateAnim(clip_show);
    }

    public void IWantTalk()
    {
        AddStateAnim(clip_talk);
    }
    public void IWantShock()
    {
        AddStateAnim(clip_shock);
    }
    public void IWantStop()
    {
        _uia.AbortPlayQueue();
        AddBasicPostStateQueue();
    }
    public void IWantFastStop()
    {
        _uia.AbortPlayQueue();
        if (IsPlayingInOrStates())
        {
            _uia.Play(clip_out);
        }
    }
    public void IWantHurt()
    {
        AddStateAnim(clip_hurt);
    }

    void AddBasicPostStateQueue()
    {
        for (int i = 0; i < idleLoopCount; i++)
            _uia.AddPlayQueue(clip_idle);

        _uia.AddPlayQueue(clip_out);
    }

    void AddStateAnim(UiImageAnimation.UiImageAnimationClip clip)
    {
        _uia.ToggleDisplay(true);
        if (IsPlayingInOrStates())
        {
            _uia.AbortPlayQueue();
            for (int i = 0; i < stateLoopCount; i++)
                _uia.AddPlayQueue(clip);
            AddBasicPostStateQueue();
        }
        else if (_uia.IsPlayingClip(clip_out))
        {
            _uia.AbortPlayQueue();
            _uia.AddPlayQueue(clip_in);
            for (int i = 0; i < stateLoopCount; i++)
                _uia.AddPlayQueue(clip);
            AddBasicPostStateQueue();
        }
        else
        {
            _uia.Play(clip_in);
            _uia.AbortPlayQueue();
            for (int i = 0; i < stateLoopCount; i++)
                _uia.AddPlayQueue(clip);
            AddBasicPostStateQueue();
        }
    }

    bool IsPlayingInOrStates()
    {
        return
            _uia.IsPlayingClip(clip_idle) ||
            _uia.IsPlayingClip(clip_shock) ||
            _uia.IsPlayingClip(clip_show) ||
            _uia.IsPlayingClip(clip_talk) ||
            _uia.IsPlayingClip(clip_hurt) ||
            _uia.IsPlayingClip(clip_in);
    }

}