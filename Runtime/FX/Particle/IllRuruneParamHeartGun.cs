using System.Collections.Generic;
using System.Linq;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.RuruneOptimizer
{
    public class IllRuruneParamHeartGun : IllRuruneParam
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

        public IllRuruneParamHeartGun(VRCAvatarDescriptor descriptor, AnimatorController animator)
        {
            this.descriptor = descriptor;
            this.animator = animator;
        }

        public IllRuruneParamHeartGun DeleteFx()
        {
            var removedLayers = animator
                .layers.Where(layer => Layers.Contains(layer.name))
                .ToList();

            animator.layers = animator
                .layers.Where(layer => !Layers.Contains(layer.name))
                .ToArray();

            foreach (var layer in removedLayers)
            {
                foreach (var state in layer.stateMachine.states)
                {
                    AddIfNotInParameters(
                        paramList,
                        exsistParams,
                        state.state.cycleOffsetParameter,
                        state.state.cycleOffsetParameterActive
                    );
                    AddIfNotInParameters(
                        paramList,
                        exsistParams,
                        state.state.timeParameter,
                        state.state.timeParameterActive
                    );
                    AddIfNotInParameters(
                        paramList,
                        exsistParams,
                        state.state.speedParameter,
                        state.state.speedParameterActive
                    );
                    AddIfNotInParameters(
                        paramList,
                        exsistParams,
                        state.state.mirrorParameter,
                        state.state.mirrorParameterActive
                    );

                    foreach (var transition in state.state.transitions)
                    {
                        foreach (var condition in transition.conditions)
                        {
                            AddIfNotInParameters(paramList, exsistParams, condition.parameter);
                        }
                    }
                }

                foreach (var transition in layer.stateMachine.anyStateTransitions)
                {
                    foreach (var condition in transition.conditions)
                    {
                        AddIfNotInParameters(paramList, exsistParams, condition.parameter);
                    }
                }
            }
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
            DestroySafety(descriptor.transform.Find("Advanced/HeartGunR"));
            DestroySafety(descriptor.transform.Find("Advanced/HeartGunL"));
            DestroySafety(descriptor.transform.Find("Advanced/HeartGunR2"));
            DestroySafety(descriptor.transform.Find("Advanced/HeartGunL2"));
            return this;
        }
    }
}
#endif
