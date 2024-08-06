using UnityEngine;
using UnityEditor;

namespace com
{
    [CustomEditor( typeof( Transform ), true )]
    public class TransformCustomInspector : Editor
    {
        static public TransformCustomInspector instance;

        protected SerializedProperty _pos;
        protected SerializedProperty _scale;
        protected Transform _target;

        void OnEnable()
        {
            instance = this;

            _scale = serializedObject.FindProperty( "m_LocalScale" );
            _pos = serializedObject.FindProperty( "m_LocalPosition" );

            _target = ( Transform )target;
        }

        void OnDestroy() { instance = null; }

        public override void OnInspectorGUI()
        {
            EditorGUIUtility.labelWidth = 20f;
            serializedObject.Update();

            Position();
            Rotation();
            Scale();

            serializedObject.ApplyModifiedProperties();
        }

        void Position()
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label( "Position ", GUILayout.Width( 55f ) );
            bool reset = GUILayout.Button( "P", GUILayout.Width( 20f ) );
            if ( reset ) {
                _pos.vector3Value = Vector3.zero;
            }
            EditorGUILayout.PropertyField( _pos.FindPropertyRelative( "x" ) );
            EditorGUILayout.PropertyField( _pos.FindPropertyRelative( "y" ) );
            EditorGUILayout.PropertyField( _pos.FindPropertyRelative( "z" ) );

            GUILayout.EndHorizontal();
        }

        void Scale()
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label( "Scale ", GUILayout.Width( 55f ) );
            bool reset = GUILayout.Button( "S", GUILayout.Width( 20f ) );
            if ( reset ) {
                _scale.vector3Value = Vector3.one;
            }
            EditorGUILayout.PropertyField( _scale.FindPropertyRelative( "x" ) );
            EditorGUILayout.PropertyField( _scale.FindPropertyRelative( "y" ) );
            EditorGUILayout.PropertyField( _scale.FindPropertyRelative( "z" ) );

            GUILayout.EndHorizontal();
        }

        void Rotation()
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label( "Rotation ", GUILayout.Width( 55f ) );
            bool reset = GUILayout.Button( "R", GUILayout.Width( 20f ) );
            Vector3 rot;
            if ( reset ) {
                rot = Vector3.zero;
            } else {
                rot = _target.localEulerAngles;
            }
            rot.x = EditorGUILayout.FloatField( "X", rot.x );
            rot.y = EditorGUILayout.FloatField( "Y", rot.y );
            rot.z = EditorGUILayout.FloatField( "Z", rot.z );
            if ( !rot.Equals( _target.localEulerAngles ) ) {
                _target.localEulerAngles = rot;
            }
            GUILayout.EndHorizontal();
        }
    }
}
