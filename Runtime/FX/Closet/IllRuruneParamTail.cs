using System.Collections.Generic;
using System.Linq;
using VRC.SDK3.Avatars.Components;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.RuruneOptimizer
{
    internal class IllRuruneParamTail : IllRuruneParam
    {
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;
        private static readonly List<string> MenuParameters = new() { "Hair_Ground" };

        public IllRuruneParamTail Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            return this;
        }

        public IllRuruneParamTail DeleteFxBT()
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

        public IllRuruneParamTail DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();
            return this;
        }

        public IllRuruneParamTail DestroyObj()
        {
            DestroyObj(descriptor.transform.Find("sharktail"));
            DestroyObj(descriptor.transform.Find("tail_ribbon"));
            DestroyObj(descriptor.transform.Find("Armature/plane_tail_collider"));
            DestroyObj(descriptor.transform.Find("Armature/Hips/tail"));
            DestroyObj(descriptor.transform.Find("Advanced/sippo_contact"));
            DestroyObj(descriptor.transform.Find("Advanced/tail_Ground"));
            return this;
        }
    }
}
#endif
