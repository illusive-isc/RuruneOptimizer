using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
            "Object4",
            "Particle4",
        };

        public IllRuruneParamHair Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            return this;
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
                if (control1.name == "Particle")
                {
                    var expressionsSubMenu1 = control1.subMenu;

                    foreach (var control2 in expressionsSubMenu1.controls)
                    {
                        if (control2.name == "headphone")
                        {
                            expressionsSubMenu1.controls.Remove(control2);
                            break;
                        }
                    }
                    control1.subMenu = expressionsSubMenu1;
                    break;
                }
            }
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

        public IllRuruneParamHair DestroyObj(
            bool HairFlg1,
            bool HairFlg11,
            bool HairFlg12,
            bool HairFlg2,
            bool HairFlg22,
            bool HairFlg3,
            bool HairFlg4,
            bool HairFlg5,
            bool HairFlg51,
            bool HairFlg6,
            bool tailFlg
        )
        {
            var hair = descriptor.transform.Find("hair");
            if (hair)
                if (hair.TryGetComponent<SkinnedMeshRenderer>(out var hairSMR))
                {
                    hairSMR.SetBlendShapeWeight(0, HairFlg1 ? 100 : 0);
                    hairSMR.SetBlendShapeWeight(6, HairFlg11 ? 100 : 0);
                    hairSMR.SetBlendShapeWeight(3, HairFlg2 ? 0 : 100);
                    hairSMR.SetBlendShapeWeight(4, HairFlg22 ? 100 : 0);
                    hairSMR.SetBlendShapeWeight(1, HairFlg12 ? 0 : 100);
                    hairSMR.SetBlendShapeWeight(5, HairFlg3 ? 0 : 100);

                    hairSMR.SetBlendShapeWeight(2, HairFlg5 ? 0 : 100);
                    var hairRoot = descriptor.transform.Find(
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root"
                    );
                    if (hairRoot)
                    {
                        if (
                            hairRoot
                                .Find("Front_hair1_root/Head.002")
                                .TryGetComponent<VRCPhysBoneBase>(out var Front_hair1_root)
                        )
                        {
                            Front_hair1_root.enabled = !HairFlg1;
                        }

                        if (
                            hairRoot
                                .Find("Front_hair2_root")
                                .TryGetComponent<VRCPhysBoneBase>(out var Front_hair2_root)
                        )
                        {
                            Front_hair2_root.enabled = HairFlg1;
                        }

                        if (
                            hairRoot
                                .Find("side_1_root")
                                .TryGetComponent<VRCPhysBoneBase>(out var side_1_root)
                        )
                        {
                            side_1_root.enabled = HairFlg12;
                        }

                        if (
                            hairRoot
                                .Find("Side_root")
                                .TryGetComponent<VRCPhysBoneBase>(out var Side_root)
                        )
                        {
                            Side_root.enabled = HairFlg3;
                        }
                    }
                }

            if (HairFlg4)
                DestroyObj(descriptor.transform.Find("hair_2"));
            if (HairFlg6)
            {
                DestroyObj(descriptor.transform.Find("hair"));
                DestroyObj(descriptor.transform.Find("Armature/plane_collider"));
                DestroyObj(descriptor.transform.Find("Advanced/Hair_Ground"));
                DestroyObj(descriptor.transform.Find("Advanced/Hair_Contact"));
                DestroyObj(
                    descriptor.transform.Find("Armature/Hips/Spine/Chest/Neck/Head/Hair_root")
                );
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
                            || !tailFlg
                                && physBoneCollider.gameObject.name
                                    is "head_collider"
                                        or "chest_collider"
                                        or "upperleg_L_collider"
                                        or "upperleg_R_collider"
                        )
                    )
                        DestroyImmediate(physBoneCollider.gameObject);
                    else if (physBoneCollider.gameObject.name is "Breast_L" or "Breast_R")
                        DestroyImmediate(physBoneCollider);
                }
            }

            var headphone = descriptor.transform.Find(
                "Armature/Hips/Spine/Chest/Neck/Head/rurune_headphone"
            );
            if (headphone)
                if (HairFlg5)
                    DestroyObj(headphone);
                else
                    headphone.gameObject.SetActive(true);

            var particle = descriptor.transform.Find("Advanced/Particle/4");
            if (particle)
                if (HairFlg51)
                {
                    DestroyObj(
                        descriptor.transform.Find(
                            "Armature/Hips/Spine/Chest/Neck/Head/headphone_particle"
                        )
                    );
                    DestroyObj(descriptor.transform.Find("Advanced/Particle/4"));
                }
                else
                    descriptor.transform.Find("Advanced/Particle/4").gameObject.SetActive(true);
            return this;
        }
    }
}
#endif
