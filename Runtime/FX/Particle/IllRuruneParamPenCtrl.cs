using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.RuruneOptimizer
{
    [AddComponentMenu("")]
    internal class IllRuruneParamPenCtrl : IllRuruneParam
    {
        HashSet<string> paramList = new();
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;

        private static readonly List<string> Layers = new() { "PenCtrl_R", "PenCtrl_L" };

        private static readonly List<string> MenuParameters = new()
        {
            "PenColor",
            "Pen1",
            "Pen1Grab",
            "Pen2",
            "Pen2Grab",
        };

        public IllRuruneParamPenCtrl Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            return this;
        }

        public IllRuruneParamPenCtrl DeleteFx(bool HeartGunFlg)
        {
            if (!HeartGunFlg)
            {
                foreach (
                    var layer in animator.layers.Where(layer =>
                        layer.name is "PenCtrl_R" or "PenCtrl_L"
                    )
                )
                {
                    layer.stateMachine.states = layer
                        .stateMachine.states.Where(state =>
                            !(
                                state.state.name
                                is "particlePen1draw R"
                                    or "particlePen1draw off R"
                                    or "particlePenGrabCtrl1 R"
                                    or "PenEraserR"
                                    or "particlePen1draw L"
                                    or "particlePen1draw off L"
                                    or "particlePenGrabCtrl1 L"
                                    or "PenEraserL"
                            )
                        )
                        .ToArray();
                    var states = layer.stateMachine.states;
                    foreach (var state in states)
                    {
                        if (state.state.name == "off")
                            foreach (var transition in state.state.transitions)
                            {
                                if (transition.destinationState.name == "on")
                                {
                                    transition.conditions = transition
                                        .conditions.Where(condition =>
                                            condition.parameter == "HeartGun"
                                        )
                                        .ToArray();
                                }
                            }
                    }
                    layer.stateMachine.states = states;
                }
            }
            else
            {
                animator.layers = animator
                    .layers.Where(layer => !Layers.Contains(layer.name))
                    .ToArray();
            }

            return this;
        }

        public IllRuruneParamPenCtrl DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !paramList.Contains(parameter.name))
                .ToArray();
            animator.parameters = animator
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();
            return this;
        }

        public IllRuruneParamPenCtrl DeleteFxBT()
        {
            foreach (var layer in animator.layers.Where(layer => layer.name == "MainCtrlTree"))
            {
                foreach (var state in layer.stateMachine.states)
                {
                    if (state.state.motion is BlendTree blendTree)
                    {
                        blendTree.children = blendTree
                            .children.Where(c =>
                                CheckBT(c.motion, paramList.Concat(MenuParameters).ToList())
                            )
                            .ToArray();
                    }
                }
            }
            return this;
        }

        public IllRuruneParamPenCtrl DeleteVRCExpressions(
            VRCExpressionsMenu menu,
            VRCExpressionParameters param
        )
        {
            param.parameters = param
                .parameters.Where(parameter =>
                    !paramList
                        .Where(obj =>
                            !(obj == "HeartGunCollider R") || !(obj == "HeartGunCollider L")
                        )
                        .Concat(MenuParameters)
                        .Contains(parameter.name)
                )
                .ToArray();

            foreach (var control in menu.controls)
            {
                if (control.name == "Particle")
                {
                    var expressionsSubMenu = control.subMenu;

                    foreach (var control2 in expressionsSubMenu.controls)
                    {
                        if (control2.name == "Pen")
                        {
                            expressionsSubMenu.controls.Remove(control2);
                            break;
                        }
                    }
                    control.subMenu = expressionsSubMenu;
                    break;
                }
            }
            return this;
        }

        public IllRuruneParamPenCtrl DestroyObj()
        {
            DestroyObj(descriptor.transform.Find("Advanced/Particle/7"));
            DestroyObj(descriptor.transform.Find("Advanced/Constraint/Index_R_Constraint"));
            DestroyObj(descriptor.transform.Find("Advanced/Constraint/Index_L_Constraint"));
            DestroyObj(descriptor.transform.Find("Advanced/Constraint/Hand_R_Constraint0"));
            DestroyObj(descriptor.transform.Find("Advanced/Constraint/Hand_L_Constraint0"));
            return this;
        }
    }
}
#endif
