using System.Collections.Generic;
using System.Linq;
using VRC.Dynamics;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.RuruneOptimizer
{
    public class IllRuruneParamHair : IllRuruneParam
    {
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;
        private static readonly List<string> MenuParameters = new()
        {
            "Object1",
            "Object2",
            "Object3",
            "Object4",
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
            return this;
        }

        public IllRuruneParamHair DestroyObj()
        {
            DestroySafety(descriptor.transform.Find("hair"));
            DestroySafety(descriptor.transform.Find("hair_2"));
            DestroySafety(descriptor.transform.Find("Armature/plane_collider"));
            DestroySafety(descriptor.transform.Find("Advanced/Hair_Ground"));
            DestroySafety(descriptor.transform.Find("Advanced/Hair_Contact"));
            DestroySafety(descriptor.transform.Find("hair"));
            DestroySafety(
                descriptor.transform.Find("Armature/Hips/Spine/Chest/Neck/Head/headphone_particle")
            );

            DestroySafety(descriptor.transform.Find("Advanced/Particle/4"));
            DestroySafety(
                descriptor.transform.Find("Armature/Hips/Spine/Chest/Neck/Head/rurune_headphone")
            );
            DestroySafety(
                descriptor.transform.Find("Armature/Hips/Spine/Chest/Neck/Head/Hair_root")
            );
            foreach (
                var physBoneCollider in descriptor
                    .transform.Find("Armature")
                    .GetComponentsInChildren<VRCPhysBoneColliderBase>()
            )
            {
                if (physBoneCollider.gameObject.name != "plane_tail_collider")
                    DestroyImmediate(physBoneCollider);
            }

            return this;
        }
    }
}
#endif
