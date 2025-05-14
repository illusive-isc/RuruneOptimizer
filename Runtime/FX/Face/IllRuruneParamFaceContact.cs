using System.Collections.Generic;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.RuruneOptimizer
{
    [AddComponentMenu("")]
    internal class IllRuruneParamFaceContact : IllRuruneParam
    {
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;
        HashSet<string> paramList = new();
        public bool kamitukiFlg = false;
        public bool nadeFlg = false;
        public bool blinkFlg = false;


        public IllRuruneParamFaceContact Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator,
            bool kamitukiFlg,
            bool nadeFlg,
            bool blinkFlg
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            this.kamitukiFlg = kamitukiFlg;
            this.nadeFlg = nadeFlg;
            this.blinkFlg = blinkFlg;
            return this;
        }

        public IllRuruneParamFaceContact DeleteVRCExpressions(
            VRCExpressionsMenu menu,
            VRCExpressionParameters param
        )
        {
            foreach (var parameter in param.parameters)
            {
                if (parameter.name is "Nade" or "Kamituki" or "Blink off")
                {
                    parameter.defaultValue = 1;
                    parameter.networkSynced = false;
                }
            }

            foreach (var control in menu.controls)
            {
                if (control.name == "Gimmick")
                {
                    var expressionsSubMenu = control.subMenu;

                    foreach (var control2 in expressionsSubMenu.controls)
                    {
                        if (control2.name == "Face")
                        {
                            var expressionsSub2Menu = control2.subMenu;
                            if (kamitukiFlg)
                                foreach (var control3 in expressionsSub2Menu.controls)
                                {
                                    if (control3.name is "噛みつき禁止")
                                    {
                                        expressionsSub2Menu.controls.Remove(control3);
                                        break;
                                    }
                                }
                            if (nadeFlg)
                                foreach (var control3 in expressionsSub2Menu.controls)
                                {
                                    if (control3.name is "なでなで")
                                    {
                                        expressionsSub2Menu.controls.Remove(control3);
                                        break;
                                    }
                                }
                            if (blinkFlg)
                                foreach (var control3 in expressionsSub2Menu.controls)
                                {
                                    if (control3.name is "Blink off")
                                    {
                                        expressionsSub2Menu.controls.Remove(control3);
                                        break;
                                    }
                                }
                            control2.subMenu = expressionsSub2Menu;
                            break;
                        }
                    }
                    control.subMenu = expressionsSubMenu;
                    break;
                }
            }
            return this;
        }
    }
}
#endif
