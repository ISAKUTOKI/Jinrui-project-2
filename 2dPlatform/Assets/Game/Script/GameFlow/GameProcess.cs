using System.Collections;
using UnityEngine;

[CreateAssetMenu]
public class GameProcess : ScriptableObject
{
    public bool 上香;
    public bool 推下山;
    public bool 出山洞;

    public void Init()
    {
        上香 = false;
        推下山 = false;
        出山洞 = false;
    }
}