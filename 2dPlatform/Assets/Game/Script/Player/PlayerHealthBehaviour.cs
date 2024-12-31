using Assets.Game.Script.GameFlow;
using com;
using DG.Tweening;
using System.Security.Cryptography;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PlayerHealthBehaviour : MonoBehaviour
{
    public HpHalfHeartsBehaviour hpView;
    public int hpMax;

    private int _hpMax;
    private int _hp;
    bool _dead;

    public string dieSound;
    public float deathFadeDelay;

    public string weaponParent1; // 要禁用的GameObject的名称

    public float slowMoDuration; // 慢动作持续时间

    private void Start()
    {
        FullFill();
        Time.timeScale = 1f; // 确保开始时Time.timeScale为1
    }
    public void FullFill()
    {
        _dead = false;
        _hpMax = hpMax;
        hpView.FullFillAll();
        _hp = _hpMax;
    }

    private void Update()
    {
        DoRoutineMove();

        //if (Input.GetKeyDown(KeyCode.F12))
        //    Die();

    }

    public void TakeDamage(int dmg)
    {
        if (_dead) return;

        //Debug.Log(this.name + "TakeDamage " + dmg);
        _hp -= dmg;
        if (_hp < 0)
            _hp = 0;

        float ratio = (float)_hp / _hpMax;
        hpView.SetHp(_hp, true);
        if (_hp <= 0)
        {
            CombatSystem.instance.BloodStrong();
            Die();
        }
        else
        {
            PlayerBehaviour.instance.animator.SetTrigger("wound");
            PlayerBehaviour.instance.movePosition.StopXMovement();
            PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.hide);
            CombatSystem.instance.BloodWeak();
            ChatBoxSystem.instance.IWantHurt();
            PlayerBehaviour.instance.defend.OnWound();
        }
    }

    public AnimationClip woundClip;
    public bool isWounding
    {
        get
        {
            var clipInfo = PlayerBehaviour.instance.animator.GetCurrentAnimatorClipInfo(0);
            if (clipInfo.Length < 1)
            {
                return false;
            }

            var c = clipInfo[0];
            var clip = c.clip;
            return clip == woundClip;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Kill")
        {
            this.Die(true);
        }
    }

    public void Die(bool fromFall = false)
    {
        if (_dead) return;

        _dead = true;
        //ReviveSystem.instance.QueueDie(fromFall);
        //SoundSystem.instance.Play(dieSound);
        //ObjDisable();
        Destroy(GetComponent("Rigidbody2D"));
        PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.hide);
        PlayerBehaviour.instance.animator.SetTrigger("die");
        PlayerBehaviour.instance.movePosition.StopXMovement();
        //SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();
        //foreach (var sr in srs)
        //{
        //    sr.DOFade(0, 3).SetDelay(deathFadeDelay + Random.Range(1, 3f));
        //}
    }

    void DoRoutineMove()
    {
        if (_dead) return;
    }

    public bool isDead
    {
        get { return _dead; }
    }

    public IEnumerator SlowMoAndReload()
    {
        float timer = 0f;
        while (timer < slowMoDuration)
        {
            Time.timeScale = Mathf.Lerp(1f, 0f, timer / slowMoDuration); // 平滑减少Time.timeScale
            timer += Time.unscaledDeltaTime; // 使用unscaledDeltaTime以确保不受Time.timeScale影响
            yield return null;
        }

        // 确保Time.timeScale为0
        Time.timeScale = 0f;

        // 等待一帧时间，确保所有基于时间的操作都已停止
        yield return new WaitForEndOfFrame();

        // 重启当前场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //void ObjDisable()
    //{
    //    GameObject targetObject = GameObject.Find(weaponParent1);
    //    if (targetObject != null)
    //    {
    //        targetObject.SetActive(false);
    //        Debug.LogError("GameObject found: " + weaponParent1);
    //    }
    //    else
    //    {
    //        Debug.LogError("GameObject not found: " + weaponParent1);
    //    }
    //}
}