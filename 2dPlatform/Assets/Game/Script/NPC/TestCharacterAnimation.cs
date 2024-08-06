using System.Collections;
using UnityEngine;

public class TestCharacterAnimation : MonoBehaviour
{
    public NpcController boy;
    public NpcController minion;
    public NpcController boss;
    public NpcController girl;
    public NpcController girlFight;
    //按下方向键移动
    //按下上键 boy是求救 boss吐火 minion攻击  girl Afraid
    //按下下键 boy是切换走路模式 boss死亡 minion

    void Start()
    {
        GameFlowSystem.instance.character = this;
    }

    void Update()
    {
        //attack stop move anim
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            boy.SetAnimTrigger("sos");
            minion.SetAnimTrigger("attack");
            boss.SetAnimTrigger("attack normal");
            girl.SetAnimTrigger("afraid");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            boy.SetAnimBool("pride", true);
            minion.SetAnimTrigger("die");
            boss.SetAnimTrigger("die");
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            boy.SetMove(true, true);
            minion.SetMove(true, true);
            boss.SetMove(true, true);
            girl.SetMove(true, true);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            boy.SetMove(false, true);
            minion.SetMove(false, true);
            boss.SetMove(false, true);
            girl.SetMove(false, true);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            boy.SetMove(true, false);
            minion.SetMove(true, false);
            boss.SetMove(true, false);
            girl.SetMove(true, false);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            boy.SetMove(false, false);
            minion.SetMove(false, false);
            boss.SetMove(false, false);
            girl.SetMove(false, false);
        }
    }
}