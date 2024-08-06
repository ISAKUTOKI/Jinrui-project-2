using com;
using UnityEngine;

public class GameFlowSystem : MonoBehaviour
{
    public static GameFlowSystem instance;

    [HideInInspector]
    public TestCharacterAnimation character;
    [SerializeField]
    protected CanvasGroup girlHpBarCg;
    [SerializeField]
    protected CanvasGroup bossHpBarCg;

    public GameProcess gameProcess;


    private void Awake()
    {
        instance = this;
    }

    protected void TogglePlayerControl(bool b)
    {
        character.girl.GetComponent<PlayerJump>().enabled = b;
        character.girl.GetComponent<PlayerMove>().enabled = b;
        character.girl.GetComponent<PlayerAttackBehaviour>().enabled = b;
        character.girl.GetComponent<NpcController>().enabled = !b;
    }

    public void ToggleBossHpBar(bool b)
    {
        //Debug.Log("ToggleBossHpBar " + b);
        bossHpBarCg.alpha = b ? 1 : 0;
    }

    public void TogglePlayerHpBar(bool b)
    {
        //Debug.Log("TogglePlayerHpBar " + b);
        girlHpBarCg.alpha = b ? 1 : 0;
    }
}