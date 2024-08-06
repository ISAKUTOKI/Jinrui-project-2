using UnityEngine;
using System.Collections.Generic;
namespace com
{
    public class UiController : MonoBehaviour
    {
        public List<CanvasGroup> CgList;

        public static UiController Instance;

        void Start()
        {
            Instance = this;
        }

        private void ToggleGroup(bool isActive, CanvasGroup cg)
        {
            cg.interactable = isActive;
            cg.blocksRaycasts = isActive;
        }
    }
}