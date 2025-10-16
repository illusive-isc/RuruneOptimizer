using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.RuruneOptimizer
{
    [AddComponentMenu("")]
    internal class Zapu_n : IllRuruneParam
    {
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;

        private static readonly List<string> MenuParameters = new() { "TPS" };

        public Zapu_n Initialize(VRCAvatarDescriptor descriptor, AnimatorController animator)
        {
            this.descriptor = descriptor;
            this.animator = animator;
            return this;
        }

        public Zapu_n ChangeObj()
        {
            // プレハブのパスを指定してロード
            // GUIでプレハブのパスを選択
            var prefabPath = AssetDatabase.GUIDToAssetPath("ec7fbd8af3a3d86498b65f757efa7283");
            if (string.IsNullOrEmpty(prefabPath))
                return this;

            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (prefab != null)
            {
                // プレハブをインスタンス化して配置
                var instance = PrefabUtility.InstantiatePrefab(prefab, descriptor.transform);
                instance.name = prefab.name;
            }
            return this;
        }
    }
}
#endif
