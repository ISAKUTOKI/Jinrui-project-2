using UnityEngine;

namespace com
{
    public class MmoCameraBehaviour : MonoBehaviour
    {
        public Transform target;
        public MmoCameraParameters parameters;

        void LateUpdate()
        {
            Sync();
        }

        private void OnEnable()
        {
            Sync();
        }

        public void Sync()
        {
            var backward = -Vector3.forward;
            var yawed = backward * Mathf.Cos(parameters.yaw) + Vector3.right * Mathf.Sin(parameters.yaw);
            var ideaPos = target.position + (yawed * Mathf.Cos(parameters.pitch) + Vector3.up * Mathf.Sin(parameters.pitch)) * parameters.distance;
            transform.position = ideaPos;
            transform.rotation = Quaternion.LookRotation(target.position - transform.position);
            transform.position += parameters.offset;
        }

        public void SetPosAndRot(ref Vector3 pos, ref Quaternion rot)
        {
            var backward = -Vector3.forward;
            var yawed = backward * Mathf.Cos(parameters.yaw) + Vector3.right * Mathf.Sin(parameters.yaw);
            var ideaPos = target.position + (yawed * Mathf.Cos(parameters.pitch) + Vector3.up * Mathf.Sin(parameters.pitch)) * parameters.distance;
            pos = ideaPos;
            rot = Quaternion.LookRotation(target.position - pos);
            pos = pos + parameters.offset;
        }
    }
}