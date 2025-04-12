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
    internal class IllRuruneParamHeartGun : IllRuruneParam
    {
        HashSet<string> paramList = new();
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;

        private static readonly List<string> Layers = new() { "HeartGun" };

        private static readonly List<string> MenuParameters = new()
        {
            "HeartGun",
            "HeartGunCollider R",
        };

        public IllRuruneParamHeartGun Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            return this;
        }

        public IllRuruneParamHeartGun DeleteFx()
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
                            is "on"
                                or "Head"
                                or "shot"
                                or "Head 0"
                                or "shot 0"
                                or "shot 0 0"
                        )
                    )
                    .ToArray();
                var states = layer.stateMachine.states;

                foreach (var state in states)
                {
                    if (state.state.name == "off")
                    {
                        state.state.transitions = state
                            .state.transitions.Where(transition =>
                                transition.destinationState.name is not "on"
                            )
                            .ToArray();
                    }
                }
                layer.stateMachine.states = states;
            }

            animator.layers = animator
                .layers.Where(layer => !Layers.Contains(layer.name))
                .ToArray();

            return this;
        }

        public IllRuruneParamHeartGun DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !paramList.Contains(parameter.name))
                .ToArray();
            animator.parameters = animator
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();
            return this;
        }

        public IllRuruneParamHeartGun DeleteFxBT()
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

        public IllRuruneParamHeartGun DeleteVRCExpressions(
            VRCExpressionsMenu menu,
            VRCExpressionParameters param
        )
        {
            param.parameters = param
                .parameters.Where(parameter =>
                    !paramList.Concat(MenuParameters).Contains(parameter.name)
                )
                .ToArray();

            foreach (var control in menu.controls)
            {
                if (control.name == "Particle")
                {
                    var expressionsSubMenu = control.subMenu;

                    foreach (var control2 in expressionsSubMenu.controls)
                    {
                        if (control2.name == "HartGun")
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

        public IllRuruneParamHeartGun DestroyObj()
        {
            DestroyObj(descriptor.transform.Find("Advanced/HeartGunR"));
            DestroyObj(descriptor.transform.Find("Advanced/HeartGunL"));
            DestroyObj(descriptor.transform.Find("Advanced/HeartGunR2"));
            DestroyObj(descriptor.transform.Find("Advanced/HeartGunL2"));
            return this;
        }
    }
}
#endif
