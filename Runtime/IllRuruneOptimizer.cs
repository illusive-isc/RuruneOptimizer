using System.ComponentModel;
using System.IO;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.RuruneOptimizer
{
    public class IllRuruneOptimizer : IllRuruneOptimizerBase
    {
        // 新しい AnimatorController を作成
        string pathDir = "Assets/IKUSIA/rurune/Clon/FX/";
        string pathName = "paryi_FX.controller";

        [SerializeField]
        private bool petFlg = false;

        [SerializeField]
        private bool ClothFlg = false;

        [SerializeField]
        private bool HairFlg = false;

        [SerializeField]
        private bool TPSFlg = false;

        [SerializeField]
        private bool ClairvoyanceFlg = false;

        [SerializeField]
        private bool colliderJumpFlg = false;

        [SerializeField]
        private bool pictureFlg = false;

        [SerializeField]
        private bool BreastSizeFlg = false;

        [SerializeField]
        private bool LightGunFlg = false;

        [SerializeField]
        private bool WhiteBreathFlg = false;

        [SerializeField]
        private bool BubbleBreathFlg = false;

        [SerializeField]
        private bool WaterStampFlg = false;

        [SerializeField]
        private bool eightBitFlg = false;

        [SerializeField]
        private bool PenCtrlFlg = false;

        [SerializeField]
        private bool HeartGunFlg = false;

        [SerializeField]
        private AnimatorController controller;

        [SerializeField]
        private VRCExpressionsMenu menu;

        [SerializeField]
        private VRCExpressionParameters param;

        public void Execute(VRCAvatarDescriptor descriptor)
        {
            if (!string.IsNullOrEmpty(pathDir + pathName))
            {
                AssetDatabase.DeleteAsset(pathDir + pathName);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
                AssetDatabase.Refresh(); // Unity にフォルダの追加を認識させる
            }
            controller = AnimatorControllerBTCopy(
                (AnimatorController)descriptor.baseAnimationLayers[4].animatorController
            );
            // アセットとして保存（エディタ専用）
            AssetDatabase.CreateAsset(controller, pathDir + pathName);
            AssetDatabase.SaveAssets();

            menu = DuplicateExpressionMenu(descriptor.expressionsMenu, pathDir);
            param = Instantiate(descriptor.expressionParameters);
            param.name = descriptor.expressionParameters.name;
            AssetDatabase.CreateAsset(param, pathDir + param.name + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            IllRuruneParamDef illRuruneParamDef = new(descriptor, controller);
            illRuruneParamDef
                .DeleteFx()
                .DeleteFxBT()
                .DeleteParam()
                .DeleteVRCExpressions(menu, param)
                .DestroyObj();

            if (petFlg)
            {
                IllRuruneParamPet illRuruneParamPet = new(descriptor, controller);
                illRuruneParamPet
                    .DeleteFx()
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }

            if (ClothFlg)
            {
                IllRuruneParamCloth illRuruneParamCloth = new(descriptor, controller);
                illRuruneParamCloth.DeleteFxBT().DeleteParam().DeleteVRCExpressions(menu, param)
                // .DestroyObj()
                ;
            }
            if (HairFlg)
            {
                IllRuruneParamHair illRuruneParamHair = new(descriptor, controller);
                illRuruneParamHair.DeleteFxBT().DeleteParam().DeleteVRCExpressions(menu, param)
                // .DestroyObj()
                ;
            }
            if (TPSFlg)
            {
                IllRuruneParamTPS illRuruneParamTPS = new(descriptor, controller);
                illRuruneParamTPS
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (ClairvoyanceFlg)
            {
                IllRuruneParamClairvoyance illRuruneParamClairvoyance = new(descriptor, controller);
                illRuruneParamClairvoyance
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (colliderJumpFlg)
            {
                IllRuruneParamCollider illRuruneParamCollider = new(descriptor, controller);
                illRuruneParamCollider
                    .DeleteFx()
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (pictureFlg)
            {
                IllRuruneParamPicture illRuruneParamPicture = new(descriptor, controller);
                illRuruneParamPicture
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (BreastSizeFlg)
            {
                IllRuruneParamBreastSize illRuruneParamBreastSize = new(descriptor, controller);
                illRuruneParamBreastSize
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param);
            }
            if (LightGunFlg)
            {
                IllRuruneParamLightGun illRuruneParamLightGun = new(descriptor, controller);
                illRuruneParamLightGun
                    .DeleteFx()
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (WhiteBreathFlg)
            {
                IllRuruneParamWhiteBreath illRuruneParamWhiteBreath = new(descriptor, controller);
                illRuruneParamWhiteBreath
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (BubbleBreathFlg)
            {
                IllRuruneParamBubbleBreath illRuruneParamBubbleBreath = new(descriptor, controller);
                illRuruneParamBubbleBreath
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (WaterStampFlg)
            {
                IllRuruneParamWaterStamp illRuruneParamWaterStamp = new(descriptor, controller);
                illRuruneParamWaterStamp
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }

            if (eightBitFlg)
            {
                IllRuruneParam8bit illRuruneParam8bit = new(descriptor, controller);
                illRuruneParam8bit
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (PenCtrlFlg)
            {
                IllRuruneParamPenCtrl illRuruneParamPenCtrl = new(descriptor, controller);
                illRuruneParamPenCtrl
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (HeartGunFlg)
            {
                IllRuruneParamHeartGun illRuruneParamHeartGun = new(descriptor, controller);
                illRuruneParamHeartGun
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            // if (FaceFlg)
            // {

            // }
            // IllRuruneParamFace illRuruneParamFace = new(descriptor, controller);
            // illRuruneParamFace
            //     .DeleteFxBT()
            //     .DeleteParam()
            //     .DeleteVRCExpressions(menu, param)
            //     .DestroyObj();
            if (ClothFlg && HairFlg)
            {
                foreach (var control1 in menu.controls)
                {
                    if (control1.name == "closet")
                    {
                        menu.controls.Remove(control1);
                        break;
                    }
                }
            }

            if (
                eightBitFlg
                && BubbleBreathFlg
                && HeartGunFlg
                && PenCtrlFlg
                && WaterStampFlg
                && WhiteBreathFlg
            )
            {
                foreach (var control1 in menu.controls)
                {
                    if (control1.name == "Particle")
                    {
                        menu.controls.Remove(control1);
                        break;
                    }
                }
            }
            foreach (var item in gameObject.GetComponents<IllRuruneParam>())
            {
                item.tag = "EditorOnly";
            }
            Debug.Log("最適化を実行しました！");
        }
    }
}
#endif
