using System.Collections;
using UnityEngine;

namespace Assets.Game.Script.HUD_interface.Combat
{
    public class WeaponPowerSystem : MonoBehaviour
    {
        public static WeaponPowerSystem instance;

        public WeaponUiBehaviour weaponUiBehaviour;

        public float power;//{ get; private set; }

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            power = 3;
            weaponUiBehaviour.SyncPowerValue(power);
        }

        /// <summary>
        /// 当玩家使出攻击时，如果有能量，就消耗能量并使出更强的攻击
        /// </summary>
        /// <returns>玩家的这一击是否消耗能量</returns>
        public bool OnPlayerAttackPerformed(bool consumeThePower = true)
        {
            if (power >= 0.1f)
            {
                if (consumeThePower)
                {
                    ConsumePower_cell(1);
                    //Debug.Log(power);
                }
                return true;
            }
            return false;
        }

        public bool OnPlayerDefendPerformed(bool consumeThePower = true)
        {
            if (power >= 0.1f)
            {
                if (consumeThePower)
                {
                    ConsumePower_cell(1);
                    //Debug.Log(power);
                }
                return true;
            }
            return false;
        }

        public bool OnPlayerDefending(float v)
        {
            if (power >= v)
            {
                ConsumePower_smoothly(v);
                return true;
            }

            return false;
        }

        public void ConsumePower_smoothly(float v)
        {
            power -= v;
            weaponUiBehaviour.SyncPowerValue(power);
        }

        public void ConsumePower_cell(int c)
        {
            if (power == 3)
            {
                power -= 1;//2
                //Debug.Log(power);
            }
            else if (power > 2 && power < 3)
            {
                power = 2;
                // Debug.Log(power);
            }
            else if (power == 2)
            {
                power = 1;
                //Debug.Log(power);
            }
            else if (power > 1 && power < 2)
            {
                power = 1;
                // Debug.Log(power);
            }
            else if (power == 1)
            {
                power = 0;
                // Debug.Log(power);
            }
            else if (power > 0.1f && power < 1)
            {
                power = 0;
                // Debug.Log(power);
            }

            weaponUiBehaviour.SyncPowerValue(power);
        }

        public void GainOnePower()
        {
            if (power < 1)
            {
                power = 1;
                weaponUiBehaviour.SyncPowerValue(power);
                weaponUiBehaviour.PlayP0_P1Anim();
                //  Debug.Log(power);
            }
            else if (power < 2)
            {
                power = 2;
                weaponUiBehaviour.SyncPowerValue(power);
                weaponUiBehaviour.PlayP1_P2Anim();
                // Debug.Log(power);
            }
            else
            {
                power = 3;
                weaponUiBehaviour.SyncPowerValue(power);
                weaponUiBehaviour.PlayP2_P3Anim();
                //  Debug.Log(power);
            }
        }

        public void GainFullPower()
        {
            power = 3;
            weaponUiBehaviour.SyncPowerValue(power);
            weaponUiBehaviour.PlayPMaxAnim();
            // Debug.Log(power);
        }

        public void 测试_开始防御()
        {
            defending = true;
        }

        public void 测试_结束防御()
        {
            defending = false;
        }

        public bool defending;

        private void Update()
        {
            if (defending)
            {
                float v = Time.deltaTime * 0.5f;
                var res = OnPlayerDefending(v);
                if (!res)
                {
                    defending = false;
                    Debug.Log("能量耗尽，自动结束防御");
                }
            }
        }

        public void 测试_格挡或攻击触发()
        {
            OnPlayerAttackPerformed(true);
            //OnPlayerDefendPerformed
        }

        public void 测试_弹反触发()
        {
            GainOnePower();
        }

        public void 测试_超级弹反触发()
        {
            GainFullPower();
        }
    }
}