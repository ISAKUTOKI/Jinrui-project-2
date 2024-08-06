using UnityEngine;
using System.Collections.Generic;

namespace com
{
    public class ListUtil
    {
        //pick num random Elements from source
        public static List<T> GetRandomElements<T>(List<T> source, int num)
        {
            List<T> res = new List<T>();
            if (num > source.Count)
            {
                return null;
            }

            var tpSource = source.GetRange(0, source.Count);

            while (res.Count < num)
            {
                var pickIndex = Random.Range(0, tpSource.Count);
                res.Add(tpSource[pickIndex]);
                tpSource.RemoveAt(pickIndex);
            }

            return res;
        }

        //Allocate [amount] items by given [possibilities], for eg: 1000 apples, 17% is red, 83% is yellow
        //this will fast return a reasonable result without 1000 times pick, just like 166 red, 834 yellow
        //don't input a very small amount for a large possibilities count
        //fastRatio smaller will be more accuracy but slow
        public static List<int> FastRandomAllocate(int amount, List<int> pickweights, float quotaRatio = 0.7f, float fastRatio = 0.05f)
        {
            var res = new List<int>();
            int restAmount = amount;
            int totalWeight = 0;
            //Debug.Log("FastRandomAllocate " + amount + " pickweights " + pickweights.Count);
            foreach (var i in pickweights)
            {
                totalWeight += i;
            }

            float quota = amount * quotaRatio / (float)totalWeight;
            foreach (var i in pickweights)
            {
                int delta = Mathf.FloorToInt(i * quota);
                restAmount -= delta;
                res.Add(delta);
            }

            bool continueWhile = true;
            int pickedTime = 0;
            int getPickedIndex()
            {
                int r = Random.Range(0, totalWeight);
                int tpWeight = 0;
                for (var i = 0; i < pickweights.Count; i++)
                {
                    tpWeight += pickweights[i];
                    if (tpWeight > r)
                    {
                        return i;
                    }
                }
                return pickweights.Count - 1;
            }

            var tpFastRatio = fastRatio;
            while (continueWhile)
            {
                int deltaToAllocate = Mathf.FloorToInt(restAmount * tpFastRatio);
                if (deltaToAllocate < 2)
                {
                    continueWhile = false;
                    break;
                }

                pickedTime++;
                restAmount -= deltaToAllocate;
                int pickedIndex = getPickedIndex();
                res[pickedIndex] += deltaToAllocate;
            }

            while (restAmount > 0)
            {
                int pickedIndex = getPickedIndex();
                res[pickedIndex] += 1;
                restAmount -= 1;
                pickedTime++;
            }

            //Debug.Log("pickedTime " + pickedTime);
            //LogList(res);
            return res;
        }

        public static void LogList<T>(List<T> l)
        {
            foreach (var i in l)
            {
                Debug.Log(i);
            }
        }

    }
}