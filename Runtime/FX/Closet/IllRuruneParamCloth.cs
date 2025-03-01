using System.Collections.Generic;
using System.Linq;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.RuruneOptimizer
{
    public class IllRuruneParamCloth : IllRuruneParam
    {
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;
        private static readonly List<string> MenuParameters = new()
        {
            "accesary",
            "boots",
            "Cloth",
            "Glove",
            "jacket",
            "socks",
            "string",
            "AllOff",
        };

        public IllRuruneParamCloth(VRCAvatarDescriptor descriptor, AnimatorController animator)
        {
            this.descriptor = descriptor;
            this.animator = animator;
        }

        public IllRuruneParamCloth DeleteFxBT()
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

        public IllRuruneParamCloth DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();
            return this;
        }

        public IllRuruneParamCloth DeleteVRCExpressions(
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
                        if (control2.name == "cloth")
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

        public IllRuruneParamCloth DestroyObj()
        {
            DestroySafety(descriptor.transform.Find("acce"));
            DestroySafety(descriptor.transform.Find("boots"));
            DestroySafety(descriptor.transform.Find("cloth"));
            DestroySafety(descriptor.transform.Find("gloves"));
            DestroySafety(descriptor.transform.Find("jacket"));
            DestroySafety(descriptor.transform.Find("knee-socks"));
            DestroySafety(
                descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_L/Skirt_back")
            );
            DestroySafety(
                descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_L/Skirt_L")
            );
            DestroySafety(
                descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_L/Skirt_L.007")
            );
            DestroySafety(
                descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_L/Skirt_L.012")
            );
            DestroySafety(
                descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_L/Skirt_L.017")
            );
            DestroySafety(
                descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_R/Skirt_R")
            );
            DestroySafety(
                descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_R/Skirt_R.007")
            );
            DestroySafety(
                descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_R/Skirt_R.012")
            );
            DestroySafety(
                descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_R/Skirt_R.017")
            );
            return this;
        }
    }
}
#endif
