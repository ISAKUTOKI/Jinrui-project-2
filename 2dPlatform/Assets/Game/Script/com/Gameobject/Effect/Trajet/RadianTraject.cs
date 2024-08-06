using UnityEngine;

namespace com
{
    public class RadianTraject : BasicTraject
    {
        public Vector3 CenterPos;
        public float Radius;

        public float TurnTime;
        public float RadSpeed;


        private float _turnTimer;
        private float _rad;

        void Update()
        {
            _turnTimer -= com.GameTime.deltaTime;
            if (this._turnTimer >= 0)
            {
                float f = _turnTimer / TurnTime;
                _rad += com.GameTime.deltaTime * RadSpeed;
                float d = Radius * f;
                transform.position += new Vector3(Mathf.Cos(_rad) * d, Mathf.Cos(_rad) * d, 0);
            }
        }
    }
}
