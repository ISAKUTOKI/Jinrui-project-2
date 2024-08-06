using UnityEngine;

namespace com
{
    public class PickOne : MonoBehaviour
    {
        static int pickIndex = 0;
        public bool staticOrder;

        void Start()
        {
            int i = transform.childCount;
            if (staticOrder)
            {
                if (pickIndex >= i)
                {
                    pickIndex -= i;
                }
                for (int c = 0; c < i; c++)
                {
                    transform.GetChild(c).gameObject.SetActive(c == pickIndex);
                }
                pickIndex++;
            }
            else
            {
                int r = Random.Range(0, i);
                for (int c = 0; c < i; c++)
                {
                    transform.GetChild(c).gameObject.SetActive(c == r);
                }
            }
        }
    }
}
