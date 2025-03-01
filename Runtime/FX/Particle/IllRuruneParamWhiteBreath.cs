using System.Collections.Generic;
using System.Linq;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.RuruneOptimizer
{
    public class IllRuruneParamWhiteBreath : IllRuruneParam
    {
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;

        private static readonly List<string> MenuParameters = new() { "Particle1" };

        public IllRuruneParamWhiteBreath(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
        }

        public IllRuruneParamWhiteBreath DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();
            return this;
        }

        public IllRuruneParamWhiteBreath DeleteFxBT()
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

        public IllRuruneParamWhiteBreath DeleteVRCExpressions(
            VRCExpressionsMenu menu,
            VRCExpressionParameters param
        )
        {
            param.parameters = param
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();

            foreach (var control in menu.controls)
            {
                if (control.name == "Particle")
                {
                    var expressionsSubMenu = control.subMenu;

                    foreach (var control2 in expressionsSubMenu.controls)
                    {
                        if (control2.name == "White_breath")
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

        public IllRuruneParamWhiteBreath DestroyObj()
        {
            DestroySafety(descriptor.transform.Find("Advanced/Particle/1"));
            return this;
        }
    }
}
#endif
