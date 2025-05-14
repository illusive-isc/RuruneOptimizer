using System.IO;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
using VRC.SDKBase;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.RuruneOptimizer
{
    [AddComponentMenu("RuruneOptimizer")]
    public class IllRuruneOptimizer : MonoBehaviour, IEditorOnly
    {
        // 保存先のパス設定
        string pathDirPrefix = "Assets/RuruneOptimizer/";
        string pathDirSuffix = "/FX/";
        string pathName = "paryi_FX.controller";

        [SerializeField]
        private bool petFlg = false;

        [SerializeField]
        private bool ClothFlg = false;

        [SerializeField]
        private bool HairFlg = false;

        [SerializeField]
        private bool TailFlg = false;

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
        private bool FaceFlg = false;

        [SerializeField]
        private bool FaceGestureFlg = false;

        [SerializeField]
        private bool FaceLockFlg = false;

        [SerializeField]
        private bool FaceValFlg = false;

        [SerializeField]
        private bool kamitukiFlg = false;

        [SerializeField]
        private bool nadeFlg = false;

        [SerializeField]
        private bool blinkFlg = false;

        [SerializeField]
        private bool IKUSIA_emote = false;

        public AnimatorController controllerDef;
        public VRCExpressionsMenu menuDef;
        public VRCExpressionParameters paramDef;

        public AnimatorController controller;
        public VRCExpressionsMenu menu;
        public VRCExpressionParameters param;

        private string pathDir;

        public void Execute(VRCAvatarDescriptor descriptor)
        {
            // 保存先ディレクトリの作成
            pathDir = pathDirPrefix + descriptor.gameObject.name + pathDirSuffix;
            if (AssetDatabase.LoadAssetAtPath<AnimatorController>(pathDir + pathName) != null)
            {
                AssetDatabase.DeleteAsset(pathDir + pathName);
                AssetDatabase.DeleteAsset(pathDir + "Menu");
                AssetDatabase.DeleteAsset(pathDir + "paryi_paraments.asset");
            }
            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
            }

            // 基本コントローラの参照取得（なければ baseAnimationLayers[4] から取得）
            if (!controllerDef)
            {
                controllerDef =
                    descriptor.baseAnimationLayers[4].animatorController as AnimatorController;
            }
            AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(controllerDef), pathDir + pathName);

            controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(pathDir + pathName);

            // ExpressionMenu の複製
            if (!menuDef)
            {
                menuDef = descriptor.expressionsMenu;
            }
            menu = DuplicateExpressionMenu(menuDef, pathDir);

            // ExpressionParameters の複製
            if (!paramDef)
            {
                paramDef = descriptor.expressionParameters;
                paramDef.name = descriptor.expressionParameters.name;
            }
            param = ScriptableObject.CreateInstance<VRCExpressionParameters>();
            EditorUtility.CopySerialized(paramDef, param);
            param.name = paramDef.name;
            EditorUtility.SetDirty(param);
            AssetDatabase.CreateAsset(param, pathDir + param.name + ".asset");
            IllRuruneParamDef illRuruneParamDef =
                ScriptableObject.CreateInstance<IllRuruneParamDef>();
            illRuruneParamDef
                .Initialize(descriptor, controller)
                .DeleteFx()
                .DeleteFxBT()
                .DeleteParam()
                .DeleteVRCExpressions(menu, param)
                .ParticleOptimize()
                .DestroyObj();

            if (petFlg)
            {
                IllRuruneParamPet illRuruneParamPet =
                    ScriptableObject.CreateInstance<IllRuruneParamPet>();
                illRuruneParamPet
                    .Initialize(descriptor, controller)
                    .DeleteFx()
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (TailFlg)
            {
                IllRuruneParamTail illRuruneParamTail =
                    ScriptableObject.CreateInstance<IllRuruneParamTail>();
                illRuruneParamTail
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DestroyObj();
            }
            if (ClothFlg)
            {
                IllRuruneParamCloth illRuruneParamCloth =
                    ScriptableObject.CreateInstance<IllRuruneParamCloth>();
                illRuruneParamCloth
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObjAll(TailFlg);
            }
            if (HairFlg)
            {
                IllRuruneParamHair illRuruneParamHair =
                    ScriptableObject.CreateInstance<IllRuruneParamHair>();
                illRuruneParamHair
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }

            if (TPSFlg)
            {
                IllRuruneParamTPS illRuruneParamTPS =
                    ScriptableObject.CreateInstance<IllRuruneParamTPS>();
                illRuruneParamTPS
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (ClairvoyanceFlg)
            {
                IllRuruneParamClairvoyance illRuruneParamClairvoyance =
                    ScriptableObject.CreateInstance<IllRuruneParamClairvoyance>();
                illRuruneParamClairvoyance
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (colliderJumpFlg)
            {
                IllRuruneParamCollider illRuruneParamCollider =
                    ScriptableObject.CreateInstance<IllRuruneParamCollider>();
                illRuruneParamCollider
                    .Initialize(descriptor, controller)
                    .DeleteFx()
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (pictureFlg)
            {
                IllRuruneParamPicture illRuruneParamPicture =
                    ScriptableObject.CreateInstance<IllRuruneParamPicture>();
                illRuruneParamPicture
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (BreastSizeFlg)
            {
                IllRuruneParamBreastSize illRuruneParamBreastSize =
                    ScriptableObject.CreateInstance<IllRuruneParamBreastSize>();
                illRuruneParamBreastSize
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param);
            }
            if (LightGunFlg)
            {
                IllRuruneParamLightGun illRuruneParamLightGun =
                    ScriptableObject.CreateInstance<IllRuruneParamLightGun>();
                illRuruneParamLightGun
                    .Initialize(descriptor, controller)
                    .DeleteFx()
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (WhiteBreathFlg)
            {
                IllRuruneParamWhiteBreath illRuruneParamWhiteBreath =
                    ScriptableObject.CreateInstance<IllRuruneParamWhiteBreath>();
                illRuruneParamWhiteBreath
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (BubbleBreathFlg)
            {
                IllRuruneParamBubbleBreath illRuruneParamBubbleBreath =
                    ScriptableObject.CreateInstance<IllRuruneParamBubbleBreath>();
                illRuruneParamBubbleBreath
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (WaterStampFlg)
            {
                IllRuruneParamWaterStamp illRuruneParamWaterStamp =
                    ScriptableObject.CreateInstance<IllRuruneParamWaterStamp>();
                illRuruneParamWaterStamp
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }

            if (eightBitFlg)
            {
                IllRuruneParam8bit illRuruneParam8bit =
                    ScriptableObject.CreateInstance<IllRuruneParam8bit>();
                illRuruneParam8bit
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (HeartGunFlg)
            {
                IllRuruneParamHeartGun illRuruneParamHeartGun =
                    ScriptableObject.CreateInstance<IllRuruneParamHeartGun>();
                illRuruneParamHeartGun
                    .Initialize(descriptor, controller)
                    .DeleteFx()
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (PenCtrlFlg)
            {
                IllRuruneParamPenCtrl illRuruneParamPenCtrl =
                    ScriptableObject.CreateInstance<IllRuruneParamPenCtrl>();
                illRuruneParamPenCtrl
                    .Initialize(descriptor, controller)
                    .DeleteFx(HeartGunFlg)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }

            if (FaceGestureFlg || FaceLockFlg || FaceValFlg)
            {
                IllRuruneParamFaceGesture illRuruneParamFaceGesture =
                    ScriptableObject.CreateInstance<IllRuruneParamFaceGesture>();
                illRuruneParamFaceGesture
                    .Initialize(descriptor, controller, FaceGestureFlg, FaceLockFlg, FaceValFlg)
                    .DeleteFx()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param);
            }
            if (kamitukiFlg || nadeFlg || blinkFlg)
            {
                IllRuruneParamFaceContact illRuruneParamFaceGesture =
                    ScriptableObject.CreateInstance<IllRuruneParamFaceContact>();
                illRuruneParamFaceGesture
                    .Initialize(descriptor, controller, kamitukiFlg, nadeFlg, blinkFlg)
                    .DeleteVRCExpressions(menu, param);
            }
            if (
                (FaceGestureFlg || (FaceLockFlg && FaceValFlg))
                && kamitukiFlg
                && nadeFlg
                && blinkFlg
            )
            {
                foreach (var control in menu.controls)
                {
                    if (control.name == "Gimmick")
                    {
                        var expressionsSubMenu = control.subMenu;

                        foreach (var control2 in expressionsSubMenu.controls)
                        {
                            if (control2.name == "Face")
                            {
                                expressionsSubMenu.controls.Remove(control2);
                                break;
                            }
                        }
                        control.subMenu = expressionsSubMenu;
                        break;
                    }
                }
            }
            // （必要に応じて各種フラグに合わせた調整処理を実施）
            if (IKUSIA_emote)
            {
                foreach (var control in menu.controls)
                {
                    if (control.name == "IKUSIA_emote")
                    {
                        menu.controls.Remove(control);
                        break;
                    }
                }
            }
            // （必要に応じて各種フラグに合わせた調整処理を実施）
            if (ClothFlg && HairFlg)
            {
                foreach (var control in menu.controls)
                {
                    if (control.name == "closet")
                    {
                        menu.controls.Remove(control);
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
                foreach (var control in menu.controls)
                {
                    if (control.name == "Particle")
                    {
                        menu.controls.Remove(control);
                        break;
                    }
                }
            }

            // 新規に複製した AnimatorController をアセットとして保存
            EditorUtility.SetDirty(controller);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            // AvatarDescriptor への適用と変更登録
            descriptor.baseAnimationLayers[4].animatorController = controller;
            descriptor.expressionsMenu = menu;
            descriptor.expressionParameters = param;
            EditorUtility.SetDirty(descriptor);

            Debug.Log("最適化を実行しました！");
        }

        /// <summary>
        /// Expression Menu の複製（サブメニューも再帰的に複製）
        /// </summary>
        public static VRCExpressionsMenu DuplicateExpressionMenu(
            VRCExpressionsMenu originalMenu,
            string parentPath
        )
        {
            if (originalMenu == null)
            {
                Debug.LogError("元のExpression Menuがありません");
                return null;
            }
            // このメニュー用のフォルダを作成
            string menuFolderPath = Path.Combine(parentPath, originalMenu.name);
            if (!Directory.Exists(menuFolderPath))
            {
                Directory.CreateDirectory(menuFolderPath);
                AssetDatabase.Refresh();
            }
            // メニューの新規保存パス
            string menuAssetPath = Path.Combine(menuFolderPath, originalMenu.name + ".asset");

            VRCExpressionsMenu newMenu = AssetDatabase.LoadAssetAtPath<VRCExpressionsMenu>(
                menuAssetPath
            );
            if (newMenu != null)
            {
                return newMenu;
            }
            newMenu = Instantiate(originalMenu);
            // サブメニューの複製
            for (int i = 0; i < newMenu.controls.Count; i++)
            {
                var control = newMenu.controls[i];
                if (control.subMenu != null)
                {
                    string subMenuFolderPath = Path.Combine(menuFolderPath, control.subMenu.name);
                    VRCExpressionsMenu existingSubMenu =
                        AssetDatabase.LoadAssetAtPath<VRCExpressionsMenu>(
                            Path.Combine(subMenuFolderPath, control.subMenu.name + ".asset")
                        );
                    if (existingSubMenu == null)
                    {
                        control.subMenu = DuplicateExpressionMenu(control.subMenu, menuFolderPath);
                    }
                    else
                    {
                        control.subMenu = existingSubMenu;
                    }
                }
            }
            EditorUtility.SetDirty(newMenu);
            AssetDatabase.CreateAsset(newMenu, menuAssetPath);
            return newMenu;
        }
    }
}
#endif
