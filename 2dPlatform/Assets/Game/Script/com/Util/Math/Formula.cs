

namespace com
{
    [System.Serializable]
    public class Formula
    {
        public float Pow3Weight;
        public float Pow2Weight;
        public float Pow1Weight;
        public float Pow0Weight;

        public int GetIntValue(float inputValue)
        {
            return (int)GetValue(inputValue);
        }

        public float GetValue(float inputValue)
        {
            float result = Pow0Weight;
            if (Pow1Weight != 0)
            {
                result += inputValue * Pow1Weight;
            }
            if (Pow2Weight != 0)
            {
                result += inputValue * inputValue * Pow2Weight;
            }
            if (Pow3Weight != 0)
            {
                result += inputValue * inputValue * inputValue * Pow3Weight;
            }
            return result;
        }

    }
}
