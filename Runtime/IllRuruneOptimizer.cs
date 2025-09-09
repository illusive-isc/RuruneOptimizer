using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VRC.Dynamics;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
using VRC.SDKBase;
#if AVATAR_OPTIMIZER_FOUND
using Anatawa12.AvatarOptimizer;
#endif
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
        private bool heelFlg1 = true;

        [SerializeField]
        private bool heelFlg2 = false;

        [SerializeField]
        private bool ClothFlg = false;

        [SerializeField]
        private bool ClothFlg1 = false;

        [SerializeField]
        private bool ClothFlg2 = true;

        [SerializeField]
        private bool ClothFlg3 = false;

        [SerializeField]
        private bool ClothFlg4 = false;

        [SerializeField]
        private bool ClothFlg5 = false;

        [SerializeField]
        private bool ClothFlg6 = false;

        [SerializeField]
        private bool ClothFlg7 = false;

        [SerializeField]
        private bool ClothFlg8 = false;

        [SerializeField]
        private bool HairFlg = false;

        public bool HairFlg1 = false;

        [SerializeField]
        private bool HairFlg11 = false;

        [SerializeField]
        private bool HairFlg12 = false;

        public bool HairFlg2 = false;

        [SerializeField]
        private bool HairFlg22 = false;

        [SerializeField]
        private bool HairFlg3 = false;

        [SerializeField]
        private bool HairFlg4 = false;

        [SerializeField]
        private bool HairFlg5 = false;

        [SerializeField]
        private bool HairFlg51 = false;

        [SerializeField]
        private bool HairFlg6 = false;

        [SerializeField]
        private bool TailFlg = false;

        [SerializeField]
        private bool TailFlg1 = false;

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
        public bool BreastSizeFlg1 = false;
        public bool BreastSizeFlg2 = false;
        public bool BreastSizeFlg3 = true;

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

        [SerializeField]
        private bool IKUSIA_emote1 = true;

        [SerializeField]
        private bool questFlg1 = false;

        public bool Skirt_Root;

        public bool Breast;

        public bool backhair;

        public bool back_side_root;

        public bool Head_002;

        public bool Front_hair2_root;

        public bool side_1_root;

        public bool hair_2;

        public bool sidehair;

        public bool side_3_root;

        public bool Side_root;

        public bool tail_044;

        public bool tail_022;

        public bool tail_024;

        public bool chest_collider1;
        public bool chest_collider2;

        public bool upperleg_collider1;
        public bool upperleg_collider2;
        public bool upperleg_collider3;

        public bool upperArm_collider;

        public bool head_collider1;
        public bool head_collider2;

        public bool Breast_collider;

        public bool plane_collider;
        public bool AAORemoveFlg;

        [SerializeField]
        bool plane_tail_collider;
        public AnimatorController controllerDef;
        public VRCExpressionsMenu menuDef;
        public VRCExpressionParameters paramDef;

        public AnimatorController controller;
        public VRCExpressionsMenu menu;
        public VRCExpressionParameters param;

        private string pathDir;

        public enum TextureResizeOption
        {
            LowerResolution, // 下げる
            Delete, // 削除
        }

        // Inspector で選択する値
        public TextureResizeOption textureResize = TextureResizeOption.LowerResolution;

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
                if (!descriptor.baseAnimationLayers[4].animatorController)
                    descriptor.baseAnimationLayers[4].animatorController =
                        AssetDatabase.LoadAssetAtPath<AnimatorController>(
                            AssetDatabase.GUIDToAssetPath("3eece7cfaddb2fe4fb361c09935d2231")
                        );
                controllerDef =
                    descriptor.baseAnimationLayers[4].animatorController as AnimatorController;
            }
            AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(controllerDef), pathDir + pathName);

            controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(pathDir + pathName);

            // ExpressionMenu の複製
            if (!menuDef)
            {
                if (!descriptor.expressionsMenu)
                    descriptor.expressionsMenu = AssetDatabase.LoadAssetAtPath<VRCExpressionsMenu>(
                        AssetDatabase.GUIDToAssetPath("78032fce499b8cd4c9590b79ccdf3166")
                    );
                menuDef = descriptor.expressionsMenu;
            }
            Dictionary<string, string> menu1 = new();
            var iconPath = pathDir + "/icon";
            if (!Directory.Exists(iconPath))
            {
                Directory.CreateDirectory(iconPath);
            }
            menu = DuplicateExpressionMenu(menuDef, pathDir, iconPath, questFlg1, textureResize);

            // ExpressionParameters の複製
            if (!paramDef)
            {
                if (!descriptor.expressionParameters)
                    descriptor.expressionParameters =
                        AssetDatabase.LoadAssetAtPath<VRCExpressionParameters>(
                            AssetDatabase.GUIDToAssetPath("ab33368960825474eb83487d302f6743")
                        );
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
            if (TailFlg1)
                IllRuruneParam.DestroyObj(descriptor.transform.Find("tail_ribbon"));
            if (TailFlg)
            {
                IllRuruneParamTail illRuruneParamTail =
                    ScriptableObject.CreateInstance<IllRuruneParamTail>();
                illRuruneParamTail
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DestroyObj(ClothFlg1, ClothFlg2);
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
                    .DestroyObjAll(
                        TailFlg,
                        ClothFlg1,
                        ClothFlg2,
                        ClothFlg3,
                        ClothFlg4,
                        ClothFlg5,
                        ClothFlg6,
                        ClothFlg7,
                        ClothFlg8,
                        heelFlg1,
                        heelFlg2
                    );
            }
            var body_b = descriptor.transform.Find("Body_b");
            if (body_b)
                if (body_b.TryGetComponent<SkinnedMeshRenderer>(out var body_bSMR))
                {
                    body_bSMR.SetBlendShapeWeight(28, heelFlg1 || heelFlg2 ? 0 : 100);
                    body_bSMR.SetBlendShapeWeight(29, heelFlg2 ? 100 : 0);
                }

            if (HairFlg || questFlg1)
            {
                IllRuruneParamHair illRuruneParamHair =
                    ScriptableObject.CreateInstance<IllRuruneParamHair>();
                illRuruneParamHair.Initialize(descriptor, controller);
                if (HairFlg)
                    illRuruneParamHair
                        .DeleteFxBT()
                        .DeleteParam()
                        .DeleteVRCExpressions(menu, param)
                        .DestroyObj(
                            HairFlg1,
                            HairFlg11,
                            HairFlg12,
                            HairFlg2,
                            HairFlg22,
                            HairFlg3,
                            HairFlg4,
                            HairFlg5,
                            HairFlg51,
                            HairFlg6,
                            TailFlg
                        );
                if (questFlg1)
                    illRuruneParamHair.DestroyObj4Quest(questFlg1);
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
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj(BreastSizeFlg1, BreastSizeFlg2, BreastSizeFlg3);
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

            if (
                LightGunFlg
                && TPSFlg
                && pictureFlg
                && BreastSizeFlg
                && ClairvoyanceFlg
                && petFlg
                && kamitukiFlg
                && nadeFlg
                && blinkFlg
                && (FaceGestureFlg || (FaceLockFlg && FaceValFlg))
            )
            {
                foreach (var control in menu.controls)
                {
                    if (control.name == "Gimmick")
                    {
                        menu.controls.Remove(control);
                        break;
                    }
                }
            }

            if (IKUSIA_emote && IKUSIA_emote1)
            {
                foreach (var control in menu.controls)
                    if (control.name == "IKUSIA_emote")
                        foreach (var control2 in control.subMenu.controls)
                            if (control2.name == "姿勢変更")
                                foreach (var ctl in control2.subMenu.controls)
                                    if (ctl.name == "AFK")
                                    {
                                        menu.controls.Add(
                                            new VRCExpressionsMenu.Control
                                            {
                                                name = ctl.name,
                                                icon = ctl.icon,
                                                type = ctl.type,
                                                parameter = ctl.parameter,
                                                value = ctl.value,
                                            }
                                        );
                                        goto BreakAllLoops;
                                    }

                                BreakAllLoops:
                ;
            }
            if (IKUSIA_emote)
                foreach (var control in menu.controls)
                    if (control.name == "IKUSIA_emote")
                    {
                        menu.controls.Remove(control);
                        break;
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
            if (questFlg1)
            {
                var AFK_World = descriptor.transform.Find("Advanced/AFK_World/position");

                IllRuruneParam.DestroyObj(AFK_World.Find("water2"));
                IllRuruneParam.DestroyObj(AFK_World.Find("water3"));
                IllRuruneParam.DestroyObj(AFK_World.Find("AFKIN Particle"));
                IllRuruneParam.DestroyObj(AFK_World.Find("swim"));
                IllRuruneParam.DestroyObj(AFK_World.Find("IdolParticle"));

                if (Skirt_Root)
                    DelPBByPathArray(
                        descriptor,
                        new string[]
                        {
                            "Armature/Hips/Skirt_Root/Skirt_Root_L",
                            "Armature/Hips/Skirt_Root/Skirt_Root_R",
                        }
                    );

                if (Breast)
                {
                    DelPBByPathArray(
                        descriptor,
                        new string[]
                        {
                            "Armature/Hips/Spine/Chest/Breast_L",
                            "Armature/Hips/Spine/Chest/Breast_R",
                        }
                    );
                }
                if (backhair)
                {
                    IllRuruneParam.DestroyComponent<VRCPhysBoneBase>(
                        descriptor.transform.Find(
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/back_hair_root/back_hair_root_L/backhair_L"
                        )
                    );

                    IllRuruneParam.DestroyComponent<VRCPhysBoneBase>(
                        descriptor.transform.Find(
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/back_hair_root/back_hair_root_R/backhair_R"
                        )
                    );
                }
                if (back_side_root)
                {
                    IllRuruneParam.DestroyComponent<VRCPhysBoneBase>(
                        descriptor.transform.Find(
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/back_side_root"
                        )
                    );
                }
                if (Head_002)
                {
                    IllRuruneParam.DestroyComponent<VRCPhysBoneBase>(
                        descriptor.transform.Find(
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Front_hair1_root/Head.002"
                        )
                    );
                }
                if (Front_hair2_root)
                {
                    IllRuruneParam.DestroyComponent<VRCPhysBoneBase>(
                        descriptor.transform.Find(
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Front_hair2_root"
                        )
                    );
                }
                if (side_1_root)
                {
                    IllRuruneParam.DestroyComponent<VRCPhysBoneBase>(
                        descriptor.transform.Find(
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/side_1_root"
                        )
                    );
                }
                if (sidehair)
                {
                    IllRuruneParam.DestroyComponent<VRCPhysBoneBase>(
                        descriptor.transform.Find(
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/side_2_root/side_ani_L/sidehair_L.003"
                        )
                    );
                    IllRuruneParam.DestroyComponent<VRCPhysBoneBase>(
                        descriptor.transform.Find(
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/side_2_root/side_ani_R/sidehair_R.003"
                        )
                    );
                }
                if (sidehair)
                {
                    IllRuruneParam.DestroyComponent<VRCPhysBoneBase>(
                        descriptor.transform.Find(
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/side_2_root/sidehair_L"
                        )
                    );

                    IllRuruneParam.DestroyComponent<VRCPhysBoneBase>(
                        descriptor.transform.Find(
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/side_2_root/sidehair_R"
                        )
                    );
                }
                if (side_3_root)
                {
                    IllRuruneParam.DestroyComponent<VRCPhysBoneBase>(
                        descriptor.transform.Find(
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/side_3_root"
                        )
                    );
                }
                if (Side_root)
                {
                    IllRuruneParam.DestroyComponent<VRCPhysBoneBase>(
                        descriptor.transform.Find(
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Side_root"
                        )
                    );
                    IllRuruneParam.DestroyComponent<VRCPhysBoneBase>(
                        descriptor.transform.Find("Armature/Hips/Spine/Chest/Breast_L")
                    );

                    IllRuruneParam.DestroyComponent<VRCPhysBoneBase>(
                        descriptor.transform.Find("Armature/Hips/Spine/Chest/Breast_R")
                    );
                }
                if (tail_044)
                {
                    IllRuruneParam.DestroyComponent<VRCPhysBoneBase>(
                        descriptor.transform.Find("Armature/Hips/tail/tail.044")
                    );
                }
                if (tail_022)
                {
                    IllRuruneParam.DestroyComponent<VRCPhysBoneBase>(
                        descriptor.transform.Find(
                            "Armature/Hips/tail/tail.044/tail.001/tail.002/tail.003/tail.004/tail.005/tail.006/tail.007/tail.008/tail.009/tail.010/tail.011/tail.012/tail.013/tail.014/tail.018/tail.021/tail.022"
                        )
                    );

                    IllRuruneParam.DestroyComponent<VRCPhysBoneBase>(
                        descriptor.transform.Find(
                            "Armature/Hips/tail/tail.044/tail.001/tail.002/tail.003/tail.004/tail.005/tail.006/tail.007/tail.008/tail.009/tail.010/tail.011/tail.012/tail.013/tail.014/tail.018/tail.021/tail.024"
                        )
                    );
                }
                if (chest_collider1)
                {
                    {
                        if (
                            descriptor.transform.Find(
                                "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/back_hair_root/back_hair_root_L/backhair_L"
                            )
                        )
                        {
                            var physBone = descriptor
                                .transform.Find(
                                    "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/back_hair_root/back_hair_root_L/backhair_L"
                                )
                                .GetComponent<VRCPhysBoneBase>();
                            if (physBone != null && physBone.colliders != null)
                            {
                                // chest_collider という名前が付いたコライダーのみ削除
                                for (int i = physBone.colliders.Count - 1; i >= 0; i--)
                                {
                                    var collider = physBone.colliders[i];
                                    if (
                                        collider != null
                                        && collider.name.Contains("chest_collider")
                                    )
                                    {
                                        physBone.colliders.RemoveAt(i);
                                    }
                                }
                            }
                        }
                    }
                    {
                        if (
                            descriptor.transform.Find(
                                "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/back_hair_root/back_hair_root_R/backhair_R"
                            )
                        )
                        {
                            var physBone = descriptor
                                .transform.Find(
                                    "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/back_hair_root/back_hair_root_R/backhair_R"
                                )
                                .GetComponent<VRCPhysBoneBase>();
                            if (physBone != null && physBone.colliders != null)
                            {
                                // chest_collider という名前が付いたコライダーのみ削除
                                for (int i = physBone.colliders.Count - 1; i >= 0; i--)
                                {
                                    var collider = physBone.colliders[i];
                                    if (
                                        collider != null
                                        && collider.name.Contains("chest_collider")
                                    )
                                    {
                                        physBone.colliders.RemoveAt(i);
                                    }
                                }
                            }
                        }
                    }
                }
                if (chest_collider2)
                {
                    {
                        if (descriptor.transform.Find("Armature/Hips/tail/tail.044"))
                        {
                            var physBone = descriptor
                                .transform.Find("Armature/Hips/tail/tail.044")
                                .GetComponent<VRCPhysBoneBase>();
                            if (physBone != null && physBone.colliders != null)
                            {
                                // chest_collider という名前が付いたコライダーのみ削除
                                for (int i = physBone.colliders.Count - 1; i >= 0; i--)
                                {
                                    var collider = physBone.colliders[i];
                                    if (
                                        collider != null
                                        && collider.name.Contains("chest_collider")
                                    )
                                    {
                                        physBone.colliders.RemoveAt(i);
                                    }
                                }
                            }
                        }
                    }
                }

                if (chest_collider1 && chest_collider2)
                    IllRuruneParam.DestroyComponent<VRCPhysBoneColliderBase>(
                        descriptor.transform.Find("Armature/Hips/Spine/Chest/chest_collider")
                    );
                if (upperleg_collider1)
                {
                    if (
                        descriptor.transform.Find(
                            "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/back_hair_root/back_hair_root_L/backhair_L"
                        )
                    )
                    {
                        {
                            var physBone = descriptor
                                .transform.Find(
                                    "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/back_hair_root/back_hair_root_L/backhair_L"
                                )
                                .GetComponent<VRCPhysBoneBase>();
                            if (physBone != null && physBone.colliders != null)
                            {
                                // chest_collider という名前が付いたコライダーのみ削除
                                for (int i = physBone.colliders.Count - 1; i >= 0; i--)
                                {
                                    var collider = physBone.colliders[i];
                                    if (
                                        collider != null
                                        && (
                                            collider.name.Contains("upperleg_L_collider")
                                            || collider.name.Contains("upperleg_R_collider")
                                        )
                                    )
                                    {
                                        physBone.colliders.RemoveAt(i);
                                    }
                                }
                            }
                        }
                    }
                    {
                        if (
                            descriptor.transform.Find(
                                "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/back_hair_root/back_hair_root_R/backhair_R"
                            )
                        )
                        {
                            var physBone = descriptor
                                .transform.Find(
                                    "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/back_hair_root/back_hair_root_R/backhair_R"
                                )
                                .GetComponent<VRCPhysBoneBase>();
                            if (physBone != null && physBone.colliders != null)
                            {
                                // chest_collider という名前が付いたコライダーのみ削除
                                for (int i = physBone.colliders.Count - 1; i >= 0; i--)
                                {
                                    var collider = physBone.colliders[i];
                                    if (
                                        collider != null
                                        && (
                                            collider.name.Contains("upperleg_L_collider")
                                            || collider.name.Contains("upperleg_R_collider")
                                        )
                                    )
                                    {
                                        physBone.colliders.RemoveAt(i);
                                    }
                                }
                            }
                        }
                    }
                }
                if (upperleg_collider2)
                {
                    {
                        if (descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_L"))
                        {
                            var physBone = descriptor
                                .transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_L")
                                .GetComponent<VRCPhysBoneBase>();
                            if (physBone != null && physBone.colliders != null)
                            {
                                // chest_collider という名前が付いたコライダーのみ削除
                                for (int i = physBone.colliders.Count - 1; i >= 0; i--)
                                {
                                    var collider = physBone.colliders[i];
                                    if (
                                        collider != null
                                        && (
                                            collider.name.Contains("upperleg_L_collider")
                                            || collider.name.Contains("upperleg_R_collider")
                                        )
                                    )
                                    {
                                        physBone.colliders.RemoveAt(i);
                                    }
                                }
                            }
                        }
                    }
                    {
                        if (descriptor.transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_R"))
                        {
                            var physBone = descriptor
                                .transform.Find("Armature/Hips/Skirt_Root/Skirt_Root_R")
                                .GetComponent<VRCPhysBoneBase>();
                            if (physBone != null && physBone.colliders != null)
                            {
                                // chest_collider という名前が付いたコライダーのみ削除
                                for (int i = physBone.colliders.Count - 1; i >= 0; i--)
                                {
                                    var collider = physBone.colliders[i];
                                    if (
                                        collider != null
                                        && (
                                            collider.name.Contains("upperleg_L_collider")
                                            || collider.name.Contains("upperleg_R_collider")
                                        )
                                    )
                                    {
                                        physBone.colliders.RemoveAt(i);
                                    }
                                }
                            }
                        }
                    }
                }
                if (upperleg_collider3)
                {
                    {
                        if (descriptor.transform.Find("Armature/Hips/tail/tail.044"))
                        {
                            var physBone = descriptor
                                .transform.Find("Armature/Hips/tail/tail.044")
                                .GetComponent<VRCPhysBoneBase>();
                            if (physBone != null && physBone.colliders != null)
                            {
                                // chest_collider という名前が付いたコライダーのみ削除
                                for (int i = physBone.colliders.Count - 1; i >= 0; i--)
                                {
                                    var collider = physBone.colliders[i];
                                    if (
                                        collider != null
                                        && (
                                            collider.name.Contains("upperleg_L_collider")
                                            || collider.name.Contains("upperleg_R_collider")
                                        )
                                    )
                                    {
                                        physBone.colliders.RemoveAt(i);
                                    }
                                }
                            }
                        }
                    }
                }
                if (upperleg_collider1 && upperleg_collider2 && upperleg_collider3)
                {
                    IllRuruneParam.DestroyComponent<VRCPhysBoneColliderBase>(
                        descriptor.transform.Find("Armature/Hips/Upperleg_L/upperleg_L_collider")
                    );
                    IllRuruneParam.DestroyComponent<VRCPhysBoneColliderBase>(
                        descriptor.transform.Find("Armature/Hips/Upperleg_R/upperleg_R_collider")
                    );
                }
                if (upperArm_collider)
                {
                    IllRuruneParam.DestroyComponent<VRCPhysBoneColliderBase>(
                        descriptor.transform.Find(
                            "Armature/Hips/Spine/Chest/Shoulder_L/Upperarm_L/upperArm_L_collider"
                        )
                    );
                    IllRuruneParam.DestroyComponent<VRCPhysBoneColliderBase>(
                        descriptor.transform.Find(
                            "Armature/Hips/Spine/Chest/Shoulder_R/Upperarm_R/upperArm_R_collider"
                        )
                    );
                }
                if (plane_collider)
                    IllRuruneParam.DestroyComponent<VRCPhysBoneColliderBase>(
                        descriptor.transform.Find("Armature/plane_collider")
                    );
                if (head_collider1)
                {
                    {
                        if (
                            descriptor.transform.Find(
                                "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/back_hair_root/back_hair_root_L/backhair_L"
                            )
                        )
                        {
                            var physBone = descriptor
                                .transform.Find(
                                    "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/back_hair_root/back_hair_root_L/backhair_L"
                                )
                                .GetComponent<VRCPhysBoneBase>();
                            if (physBone != null && physBone.colliders != null)
                            {
                                // chest_collider という名前が付いたコライダーのみ削除
                                for (int i = physBone.colliders.Count - 1; i >= 0; i--)
                                {
                                    var collider = physBone.colliders[i];
                                    if (collider != null && collider.name.Contains("head_collider"))
                                    {
                                        physBone.colliders.RemoveAt(i);
                                    }
                                }
                            }
                        }
                    }
                    {
                        if (
                            descriptor.transform.Find(
                                "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/back_hair_root/back_hair_root_R/backhair_R"
                            )
                        )
                        {
                            var physBone = descriptor
                                .transform.Find(
                                    "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/back_hair_root/back_hair_root_R/backhair_R"
                                )
                                .GetComponent<VRCPhysBoneBase>();
                            if (physBone != null && physBone.colliders != null)
                            {
                                // chest_collider という名前が付いたコライダーのみ削除
                                for (int i = physBone.colliders.Count - 1; i >= 0; i--)
                                {
                                    var collider = physBone.colliders[i];
                                    if (collider != null && collider.name.Contains("head_collider"))
                                    {
                                        physBone.colliders.RemoveAt(i);
                                    }
                                }
                            }
                        }
                    }
                }
                if (head_collider2)
                {
                    {
                        if (descriptor.transform.Find("Armature/Hips/tail/tail.044"))
                        {
                            var physBone = descriptor
                                .transform.Find("Armature/Hips/tail/tail.044")
                                .GetComponent<VRCPhysBoneBase>();
                            if (physBone != null && physBone.colliders != null)
                            {
                                // chest_collider という名前が付いたコライダーのみ削除
                                for (int i = physBone.colliders.Count - 1; i >= 0; i--)
                                {
                                    var collider = physBone.colliders[i];
                                    if (collider != null && collider.name.Contains("head_collider"))
                                    {
                                        physBone.colliders.RemoveAt(i);
                                    }
                                }
                            }
                        }
                    }
                }
                if (head_collider1 && head_collider2)
                    IllRuruneParam.DestroyComponent<VRCPhysBoneColliderBase>(
                        descriptor.transform.Find(
                            "Armature/Hips/Spine/Chest/Neck/Head/head_collider"
                        )
                    );
                if (Breast_collider)
                {
                    IllRuruneParam.DestroyComponent<VRCPhysBoneColliderBase>(
                        descriptor.transform.Find("Armature/Hips/Spine/Chest/Breast_R")
                    );
                    IllRuruneParam.DestroyComponent<VRCPhysBoneColliderBase>(
                        descriptor.transform.Find("Armature/Hips/Spine/Chest/Breast_L")
                    );
                }
                if (plane_tail_collider)
                    IllRuruneParam.DestroyComponent<VRCPhysBoneColliderBase>(
                        descriptor.transform.Find("Armature/plane_tail_collider")
                    );
            }

            if (AAORemoveFlg)
            {
#if AVATAR_OPTIMIZER_FOUND
                if (
                    !descriptor
                        .transform.Find("Body")
                        .TryGetComponent<RemoveMeshByBlendShape>(out var removeMesh)
                )
                {
                    removeMesh = descriptor
                        .transform.Find("Body")
                        .gameObject.AddComponent<RemoveMeshByBlendShape>();
                    removeMesh.Initialize(1);
                }
                removeMesh.ShapeKeys.Add("照れ");
#endif
            }

            var assetGuids = AssetDatabase.FindAssets(
                "t:VRCExpressionsMenu",
                new[] { pathDir + "Menu" }
            );

            Dictionary<string, VRCExpressionsMenu> menus = new();
            foreach (var guid in assetGuids)
            {
                menus.Add(
                    guid,
                    AssetDatabase.LoadAssetAtPath<VRCExpressionsMenu>(
                        AssetDatabase.GUIDToAssetPath(guid)
                    )
                );
            }
            foreach (var menuItem in menus)
            {
                var delFlg = true;
                if (menuItem.Value.controls.Any(p => p.parameter.name == ""))
                    continue;
                foreach (var control in menuItem.Value.controls)
                    if (!string.IsNullOrEmpty(control.parameter.name))
                        if (param.parameters.Any(p => p.name == control.parameter.name))
                        {
                            delFlg = false;
                            break;
                        }
                if (delFlg)
                    AssetDatabase.DeleteAsset(AssetDatabase.GUIDToAssetPath(menuItem.Key));
            }
            // 新規に複製した AnimatorController をアセットとして保存
            EditorUtility.SetDirty(controller);
            MarkAllMenusDirty(menu);
            EditorUtility.SetDirty(param);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            // AvatarDescriptor への適用と変更登録
            descriptor.baseAnimationLayers[4].animatorController = controller;
            descriptor.expressionsMenu = menu;
            descriptor.expressionParameters = param;
            EditorUtility.SetDirty(descriptor);

            Debug.Log("最適化を実行しました！");
        }

        private static void DelPBByPathArray(VRCAvatarDescriptor descriptor, string[] paths)
        {
            foreach (var path in paths)
            {
                IllRuruneParam.DestroyComponent<VRCPhysBoneBase>(descriptor.transform.Find(path));
            }
        }

        private static void MarkAllMenusDirty(VRCExpressionsMenu menu)
        {
            if (menu == null)
                return;

            EditorUtility.SetDirty(menu);

            foreach (var control in menu.controls)
            {
                if (control.subMenu != null)
                {
                    MarkAllMenusDirty(control.subMenu);
                }
            }
        }

        /// <summary>
        /// Expression Menu の複製（サブメニューも再帰的に複製）
        /// </summary>
        public static VRCExpressionsMenu DuplicateExpressionMenu(
            VRCExpressionsMenu originalMenu,
            string parentPath,
            string iconPath,
            bool questFlg1,
            TextureResizeOption textureResize
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
            // サブメニューの複製とアイコンのディープコピー
            for (int i = 0; i < newMenu.controls.Count; i++)
            {
                var control = newMenu.controls[i];
                if (questFlg1)
                {
                    if (textureResize == TextureResizeOption.LowerResolution)
                    {
                        var originalControl = originalMenu.controls[i];

                        // --- アイコンのディープコピー処理 ---
                        if (originalControl.icon != null)
                        {
                            string iconAssetPath = AssetDatabase.GetAssetPath(originalControl.icon);
                            if (!string.IsNullOrEmpty(iconAssetPath))
                            {
                                string iconFileName = Path.GetFileName(iconAssetPath);
                                string destPath = Path.Combine(iconPath, iconFileName);
                                // 既にコピー済みでなければコピー
                                if (!File.Exists(destPath))
                                {
                                    File.Copy(iconAssetPath, destPath, true);
                                    AssetDatabase.ImportAsset(destPath);
                                }
                                // コピーしたアイコンをロードしてcontrol.iconにセット
                                var copiedIcon = AssetDatabase.LoadAssetAtPath<Texture2D>(destPath);
                                if (copiedIcon != null)
                                {
                                    // Max Sizeを変更
                                    var importer =
                                        AssetImporter.GetAtPath(destPath) as TextureImporter;
                                    if (importer != null)
                                    {
                                        importer.maxTextureSize = 32;
                                        importer.SaveAndReimport();
                                    }
                                    control.icon = copiedIcon;
                                }
                            }
                        }
                    }
                    else
                    {
                        control.icon = null;
                    }
                }
                // サブメニューの複製
                if (control.subMenu != null)
                {
                    string subMenuFolderPath = Path.Combine(menuFolderPath, control.subMenu.name);
                    VRCExpressionsMenu existingSubMenu =
                        AssetDatabase.LoadAssetAtPath<VRCExpressionsMenu>(
                            Path.Combine(subMenuFolderPath, control.subMenu.name + ".asset")
                        );

                    if (existingSubMenu == null)
                    {
                        control.subMenu = DuplicateExpressionMenu(
                            control.subMenu,
                            menuFolderPath,
                            iconPath,
                            questFlg1,
                            textureResize
                        );
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
