using UnityEngine;
using UnityEditor;
using System.Collections;

namespace com
{
    public class EditorWindowBase : EditorWindow 
	{

        protected bool _hasPersistent;

        protected virtual void OnEnable()
        {
            _hasPersistent = GameObject.Find("0_persistent") != null;
        }

        protected virtual bool ErrorDisplayed()
        {
            if (!_hasPersistent) {
                EditorGUILayout.HelpBox("Error. Persistent object not present in the scene.", MessageType.Error);
                return true;
            }
            return false;
        }

        protected virtual bool IsError
        {
            get
            {
                return !_hasPersistent;
            }
        }

        protected virtual void OnHierarchyChange()
        {
            _hasPersistent = GameObject.Find("0_persistent") != null;
        }

        protected virtual void OnFocus()
        {
            OnHierarchyChange();
        }
	}
}
