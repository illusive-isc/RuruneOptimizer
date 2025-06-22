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

        public IllRuruneParamCloth DestroyObjAll(
            bool TailFlg,
            bool clothFlg1,
            bool clothFlg2,
            bool clothFlg3,
            bool clothFlg4,
            bool clothFlg5,
            bool clothFlg6,
            bool clothFlg7,
            bool clothFlg8,
            bool heelFlg1,
            bool heelFlg2
        )
        {
            var underwear = descriptor.transform.Find("underwear");
            if (underwear)
                if (underwear.TryGetComponent<SkinnedMeshRenderer>(out var underwearSMR))
                {
                    underwearSMR.SetBlendShapeWeight(3, clothFlg1 || clothFlg2 ? 0 : 100);
                    underwearSMR.SetBlendShapeWeight(4, clothFlg4 ? 100 : 0);
                }
            var jacket = descriptor.transform.Find("jacket");
            if (jacket)
                if (jacket.TryGetComponent<SkinnedMeshRenderer>(out var jacketSMR))
                {
                    jacketSMR.SetBlendShapeWeight(2, TailFlg ? 100 : 0);
                }
            var cloth = descriptor.transform.Find("cloth");
            if (cloth)
                if (cloth.TryGetComponent<SkinnedMeshRenderer>(out var clothSMR))
                {
                    clothSMR.SetBlendShapeWeight(2, clothFlg1 ? 0 : 100);
                    clothSMR.SetBlendShapeWeight(4, TailFlg ? 100 : 0);
                }
            var acce = descriptor.transform.Find("acce");
            if (acce)
                if (acce.TryGetComponent<SkinnedMeshRenderer>(out var acceSMR))
                {
                    acceSMR.SetBlendShapeWeight(3, clothFlg2 ? 100 : 0);
                    acceSMR.SetBlendShapeWeight(5, TailFlg ? 100 : 0);
                }
            var knee_socks = descriptor.transform.Find("knee-socks");
            if (knee_socks)
                if (knee_socks.TryGetComponent<SkinnedMeshRenderer>(out var knee_socksSMR))
                {
                    knee_socksSMR.SetBlendShapeWeight(0, heelFlg1 ? 0 : 100);
                    knee_socksSMR.SetBlendShapeWeight(1, heelFlg2 ? 100 : 0);
                }

            if (clothFlg1)
                DestroyObj(jacket);
            if (clothFlg2)
                DestroyObj(cloth);

            if (clothFlg1 && clothFlg2)
                DestroyObjcloth(TailFlg);
            if (clothFlg3)
                DestroyObj(acce);

            if (clothFlg5)
                DestroyObj(descriptor.transform.Find("gloves"));
            if (clothFlg6)
                DestroyObj(knee_socks);
            if (clothFlg7)
                DestroyObj(descriptor.transform.Find("boots"));
            if (clothFlg8)
                DestroyObj(underwear);

            return this;
        }

        private void DestroyObjcloth(bool TailFlg)
        {
            if (TailFlg)
                DestroyObj(descriptor.transform.Find("Armature/Hips/Skirt_Root"));
            else
            {
                DestroyObj(
                    descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_L/Skirt_back")
                );
                DestroyObj(
                    descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_L/Skirt_L")
                );
                DestroyObj(
                    descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_L/Skirt_L.007")
                );
                DestroyObj(
                    descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_L/Skirt_L.012")
                );
                DestroyObj(
                    descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_L/Skirt_L.017")
                );
                DestroyObj(
                    descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_R/Skirt_R")
                );
                DestroyObj(
                    descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_R/Skirt_R.007")
                );
                DestroyObj(
                    descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_R/Skirt_R.012")
                );
                DestroyObj(
                    descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_R/Skirt_R.017")
                );
            }
        }
    }
}
#endif
