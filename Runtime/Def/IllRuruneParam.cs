using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.RuruneOptimizer
{
    public class IllRuruneParam : MonoBehaviour
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

        public static void DestroySafety(Transform obj)
        {
            if (obj)
            {
                DestroySafety(obj.gameObject);
            }
        }

        public static void DestroySafety(GameObject obj)
        {
            if (obj)
            {
                obj.tag = "EditorOnly";
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
