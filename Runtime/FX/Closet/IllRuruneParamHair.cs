using System.Collections.Generic;
using System.Linq;
using VRC.Dynamics;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.RuruneOptimizer
{
    internal class IllRuruneParamHair : IllRuruneParam
    {
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;
        private static readonly List<string> MenuParameters = new()
        {
            "Object1",
            "Object2",
            "Object3",
            "Object5",
            "Object6",
            "Object7",
            "Hair_Ground",
        };

        public IllRuruneParamHair(VRCAvatarDescriptor descriptor, AnimatorController animator)
        {
            this.descriptor = descriptor;
            this.animator = animator;
        }

        public IllRuruneParamHair DeleteFxBT()
        {
            foreach (var layer in animator.layers.Where(layer => layer.name == "MainCtrlTree"))
            {
                foreach (var state in layer.stateMachine.states)
                {
                    if (state.state.motion is BlendTree blendTree)
                    {
                        blendTree.children = blendTree
                            .children.Where(c => CheckBT(c.motion, MenuParameters))
                            .ToArray();
                    }
                }
            }
            return this;
        }

        public IllRuruneParamHair DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();
            return this;
        }

        public IllRuruneParamHair DeleteVRCExpressions(
            VRCExpressionsMenu menu,
            VRCExpressionParameters param
        )
        {
            param.parameters = param
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();

            foreach (var control1 in menu.controls)
            {
                if (control1.name == "closet")
                {
                    var expressionsSubMenu1 = control1.subMenu;

                    foreach (var control2 in expressionsSubMenu1.controls)
                    {
                        if (control2.name == "head etc")
                        {
                            expressionsSubMenu1.controls.Remove(control2);
                            break;
                        }
                    }
                    control1.subMenu = expressionsSubMenu1;
                    break;
                }
            }
            return this;
        }

        public IllRuruneParamHair EditorOnly()
        {
            EditorOnly(descriptor.transform.Find("hair"));
            EditorOnly(descriptor.transform.Find("hair_2"));
            EditorOnly(descriptor.transform.Find("Armature/plane_collider"));
            EditorOnly(descriptor.transform.Find("Advanced/Hair_Ground"));
            EditorOnly(descriptor.transform.Find("Advanced/Hair_Contact"));
            EditorOnly(descriptor.transform.Find("hair"));

            EditorOnly(descriptor.transform.Find("Armature/Hips/Spine/Chest/Neck/Head/Hair_root"));

            return this;
        }

        public IllRuruneParamHair DestroyObj()
        {
            DestroyObj(descriptor.transform.Find("hair"));
            DestroyObj(descriptor.transform.Find("hair_2"));
            DestroyObj(descriptor.transform.Find("Armature/plane_collider"));
            DestroyObj(descriptor.transform.Find("Advanced/Hair_Ground"));
            DestroyObj(descriptor.transform.Find("Advanced/Hair_Contact"));
            DestroyObj(descriptor.transform.Find("hair"));

            DestroyObj(descriptor.transform.Find("Armature/Hips/Spine/Chest/Neck/Head/Hair_root"));
            foreach (
                var physBoneCollider in descriptor
                    .transform.Find("Armature")
                    .GetComponentsInChildren<VRCPhysBoneColliderBase>()
            )
            {
                if (
                    !(
                        physBoneCollider.gameObject.name
                        is "plane_tail_collider"
                            or "Breast_L"
                            or "Breast_R"
                    )
                )
                    DestroyImmediate(physBoneCollider.gameObject);
                else if (physBoneCollider.gameObject.name is "Breast_L" or "Breast_R")
                    DestroyImmediate(physBoneCollider);
            }

            return this;
        }
    }
}
#endif
