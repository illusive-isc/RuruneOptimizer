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
    internal class IllRuruneParamCloth : IllRuruneParam
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

        public IllRuruneParamCloth Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            return this;
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

        public IllRuruneParamCloth DestroyObjAll(bool TailFlg)
        {
            descriptor
                .transform.Find("underwear")
                .GetComponent<SkinnedMeshRenderer>()
                .SetBlendShapeWeight(3, 0);
            descriptor
                .transform.Find("underwear")
                .GetComponent<SkinnedMeshRenderer>()
                .SetBlendShapeWeight(4, 0);
            DestroyObj(descriptor.transform.Find("acce"));
            DestroyObj(descriptor.transform.Find("boots"));
            DestroyObj(descriptor.transform.Find("cloth"));
            DestroyObj(descriptor.transform.Find("gloves"));
            DestroyObj(descriptor.transform.Find("jacket"));
            DestroyObj(descriptor.transform.Find("knee-socks"));
            DestroyObj(
                descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_L/Skirt_back")
            );
            DestroyObj(descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_L/Skirt_L"));
            DestroyObj(
                descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_L/Skirt_L.007")
            );
            DestroyObj(
                descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_L/Skirt_L.012")
            );
            DestroyObj(
                descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_L/Skirt_L.017")
            );
            DestroyObj(descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_R/Skirt_R"));
            DestroyObj(
                descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_R/Skirt_R.007")
            );
            DestroyObj(
                descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_R/Skirt_R.012")
            );
            DestroyObj(
                descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_R/Skirt_R.017")
            );
            if (TailFlg)
                DestroyObj(descriptor.transform.Find("Armature/Hips/Skirt_Root"));
            return this;
        }
    }
}
#endif
