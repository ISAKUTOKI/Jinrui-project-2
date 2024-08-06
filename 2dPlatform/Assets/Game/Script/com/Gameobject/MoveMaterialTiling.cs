using UnityEngine;
using System.Collections;
namespace com
{
    public class MoveMaterialTiling : MonoBehaviour
    {
        public float SpeedY;
        public float SpeedX;
        public Material material;

        private float _speedX;
        private float _speedY;

        public bool hasRandom;

        private void Start()
        {
            if (hasRandom)
            {
                SetRandomSpeed();
            }
            else
            {
                _speedY = SpeedY;
                _speedX = SpeedX;
            }
        }

        void Update()
        {
            material.mainTextureOffset = material.mainTextureOffset
                + com.GameTime.deltaTime * new Vector2(_speedX, _speedY);
        }

        public void SetRandomSpeed()
        {
            _speedY = Random.Range(-1f, 1f) * SpeedY;
            _speedX = Random.Range(-1f, 1f) * SpeedX;
        }
    }
}