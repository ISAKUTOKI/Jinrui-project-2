using UnityEngine;
using System.Collections;
namespace com
{
    public class SimpleTriggerEnterBehaviour : MonoBehaviour
    {
        public float StayInterval;
        public bool CheckEnter;
        public bool CheckStay;
        public bool CheckExit;
        public GameObject Target;

        public enum TriggerAction
        {
            Active,
            ToggleActive,
            Disactive,

        }

        public TriggerAction triggerAction;

        private void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            //common filter
        }

        private void OnTriggerExit(Collider other)
        {
            //common filter
        }

        private bool Filter(GameObject go)
        {
            return false;
        }
    }
}