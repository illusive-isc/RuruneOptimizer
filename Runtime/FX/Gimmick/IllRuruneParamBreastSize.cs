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
    internal class IllRuruneParamBreastSize : IllRuruneParam
    {
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;

        private static readonly List<string> MenuParameters = new() { "BreastSize" };

        public IllRuruneParamBreastSize Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            return this;
        }

        public IllRuruneParamBreastSize DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();
            return this;
        }

        public IllRuruneParamBreastSize DeleteFxBT()
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

        public IllRuruneParamBreastSize DeleteVRCExpressions(
            VRCExpressionsMenu menu,
            VRCExpressionParameters param
        )
        {
            param.parameters = param
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();

            foreach (var control in menu.controls)
            {
                if (control.name == "Gimmick")
                {
                    var expressionsSubMenu = control.subMenu;

                    foreach (var control2 in expressionsSubMenu.controls)
                    {
                        if (control2.name == "Breast_size")
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

        public IllRuruneParamBreastSize DestroyObj(
            bool breastSizeFlg1,
            bool breastSizeFlg2,
            bool breastSizeFlg3
        )
        {
            var Body_b = descriptor.transform.Find("Body_b");
            if (Body_b)
                if (Body_b.TryGetComponent<SkinnedMeshRenderer>(out var Body_bSMR))
                {
                    Body_bSMR.SetBlendShapeWeight(1, breastSizeFlg1 ? 100 : 0);
                    Body_bSMR.SetBlendShapeWeight(2, breastSizeFlg2 ? 100 : 0);
                    Body_bSMR.SetBlendShapeWeight(3, breastSizeFlg3 ? 100 : 0);
                }
            var acce = descriptor.transform.Find("acce");
            if (acce)
                if (acce.TryGetComponent<SkinnedMeshRenderer>(out var acceSMR))
                {
                    acceSMR.SetBlendShapeWeight(0, breastSizeFlg1 ? 100 : 0);
                    acceSMR.SetBlendShapeWeight(
                        1,
                        breastSizeFlg2 ? 100
                            : breastSizeFlg3 ? 200
                            : 0
                    );
                }
            var cloth = descriptor.transform.Find("cloth");
            if (cloth)
                if (cloth.TryGetComponent<SkinnedMeshRenderer>(out var clothSMR))
                {
                    clothSMR.SetBlendShapeWeight(0, breastSizeFlg1 ? 100 : 0);
                    clothSMR.SetBlendShapeWeight(
                        1,
                        breastSizeFlg2 ? 100
                            : breastSizeFlg3 ? 202
                            : 0
                    );
                }
            var jacket = descriptor.transform.Find("jacket");
            if (jacket)
                if (jacket.TryGetComponent<SkinnedMeshRenderer>(out var jacketSMR))
                {
                    jacketSMR.SetBlendShapeWeight(
                        1,
                        breastSizeFlg2 ? 100
                            : breastSizeFlg3 ? 200
                            : 0
                    );
                }
            var underwear = descriptor.transform.Find("underwear");
            if (underwear)
                if (underwear.TryGetComponent<SkinnedMeshRenderer>(out var underwearSMR))
                {
                    underwearSMR.SetBlendShapeWeight(0, breastSizeFlg1 ? 100 : 0);
                    underwearSMR.SetBlendShapeWeight(1, breastSizeFlg2 ? 100 : 0);
                    underwearSMR.SetBlendShapeWeight(2, breastSizeFlg3 ? 100 : 0);
                }
            return this;
        }
    }
}
#endif
