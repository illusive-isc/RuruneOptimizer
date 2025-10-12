using System.Collections.Generic;
using System.Linq;
using VRC.Dynamics;
using VRC.SDK3.Avatars.Components;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.RuruneOptimizer
{
    internal class IllRuruneParamTail : IllRuruneParam
    {
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;

        bool clothFlg1,
            clothFlg2;
        private static readonly List<string> MenuParameters = new() { "tail_Ground" };

        public IllRuruneParamTail Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator,
            IllRuruneOptimizer optimizer
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            clothFlg1 = optimizer.ClothFlg1;
            clothFlg2 = optimizer.ClothFlg2;
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

        public IllRuruneParamTail ChangeObj()
        {
            DestroyObj(descriptor.transform.Find("sharktail"));
            DestroyObj(descriptor.transform.Find("Armature/plane_tail_collider"));
            if (clothFlg1 & clothFlg2)
                DestroyObj(descriptor.transform.Find("Armature/Hips/tail"));
            else
            {
                DestroyObj(
                    descriptor.transform.Find(
                        "Armature/Hips/tail/tail.044/tail.001/tail.002/tail.003"
                    )
                );
                var tail = descriptor.transform.Find("Armature/Hips/tail/tail.044");
                DestroyImmediate(tail.GetComponent<VRCPhysBoneBase>());
            }
            DestroyObj(descriptor.transform.Find("Advanced/sippo_contact"));
            DestroyObj(descriptor.transform.Find("Advanced/tail_Ground"));
            return this;
        }
    }
}
#endif
