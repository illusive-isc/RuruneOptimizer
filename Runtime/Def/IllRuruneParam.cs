using System.Collections.Generic;
using UnityEngine;
using VRC.Dynamics;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.RuruneOptimizer
{
    [AddComponentMenu("")]
    internal class IllRuruneParam : ScriptableObject
    {
        protected static List<string> exsistParams = new() { "TRUE", "paryi_AFK" };
        protected static readonly List<string> VRCParameters = new()
        {
            "IsLocal",
            "PreviewMode",
            "Viseme",
            "Voice",
            "GestureLeft",
            "GestureRight",
            "GestureLeftWeight",
            "GestureRightWeight",
            "AngularY",
            "VelocityX",
            "VelocityY",
            "VelocityZ",
            "VelocityMagnitude",
            "Upright",
            "Grounded",
            "Seated",
            "AFK",
            "TrackingType",
            "VRMode",
            "MuteSelf",
            "InStation",
            "Earmuffs",
            "IsOnFriendsList",
            "AvatarVersion",
        };

        protected void AddIfNotInParameters(
            HashSet<string> paramList,
            List<string> exeistParams,
            string parameter,
            bool isActive = true
        )
        {
            if (isActive && !VRCParameters.Contains(parameter) && !exeistParams.Contains(parameter))
            {
                paramList.Add(parameter);
            }
        }

        public static void EditorOnly(Transform obj)
        {
            if (obj)
            {
                EditorOnly(obj.gameObject);
            }
        }

        public static void EditorOnly(GameObject obj)
        {
            if (obj)
            {
                obj.tag = "EditorOnly";
            }
        }

        public static void DestroyObj(Transform obj)
        {
            if (obj)
            {
                DestroyObj(obj.gameObject);
            }
        }

        public static void DestroyObj(GameObject obj)
        {
            if (obj)
            {
                DestroyImmediate(obj);
            }
        }

        public static void DestroyComponent<T>(Transform obj)
            where T : Component
        {
            if (obj)
            {
                DestroyImmediate(obj.GetComponent<T>());
            }
        }

        public void ExeDestroyObj(Transform obj)
        {
            if (obj)
            {
                DestroyObj(obj);
            }
        }

        protected bool CheckBT(Motion motion, List<string> strings)
        {
            if (motion is BlendTree blendTree)
            {
                return !strings.Contains(blendTree.blendParameter);
            }
            else
            {
                return false;
            }
        }
    }
}
#endif
