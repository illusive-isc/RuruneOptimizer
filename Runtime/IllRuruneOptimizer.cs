using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
using VRC.SDKBase;
using Debug = UnityEngine.Debug;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.RuruneOptimizer
{
    [AddComponentMenu("RuruneOptimizer")]
    public class IllRuruneOptimizer : MonoBehaviour, IEditorOnly
    {
        string pathDirPrefix = "Assets/RuruneOptimizer/";
        string pathDirSuffix = "/FX/";
        string pathName = "paryi_FX.controller";

        [SerializeField]
        private bool petFlg = false;

        public bool heelFlg1 = true;

        public bool heelFlg2 = false;

        [SerializeField]
        private bool ClothFlg = false;

        public bool ClothFlg1 = false;

        public bool ClothFlg2 = true;

        public bool ClothFlg3 = false;

        public bool ClothFlg4 = false;

        public bool ClothFlg5 = false;

        public bool ClothFlg6 = false;

        public bool ClothFlg7 = false;

        public bool ClothFlg8 = false;

        [SerializeField]
        private bool HairFlg = false;

        public bool HairFlg1 = false;

        public bool HairFlg11 = false;

        public bool HairFlg12 = false;

        public bool HairFlg2 = false;

        public bool HairFlg22 = false;

        public bool HairFlg3 = false;

        public bool HairFlg4 = false;

        public bool HairFlg5 = false;

        public bool HairFlg51 = false;

        public bool HairFlg6 = false;

        public bool TailFlg = false;

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

        public bool HeartGunFlg = false;

        public bool FaceGestureFlg = false;

        public bool FaceLockFlg = false;

        public bool FaceValFlg = false;

        public bool kamitukiFlg = false;

        public bool nadeFlg = false;

        public bool blinkFlg = false;

        [SerializeField]
        private bool IKUSIA_emote = false;

        [SerializeField]
        private bool IKUSIA_emote1 = true;

        public bool questFlg1 = false;

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

        public bool plane_tail_collider;
        public AnimatorController controllerDef;
        public VRCExpressionsMenu menuDef;
        public VRCExpressionParameters paramDef;

        public AnimatorController controller;
        public VRCExpressionsMenu menu;
        public VRCExpressionParameters param;

        private string pathDir;

        public enum TextureResizeOption
        {
            LowerResolution,
            Delete,
        }

        public TextureResizeOption textureResize = TextureResizeOption.LowerResolution;

        private static readonly Dictionary<Type, System.Reflection.MethodInfo[]> methodCache =
            new();

        private struct ParamProcessConfig
        {
            public Func<bool> condition;
            public Action processAction;
            public Action afterAction;
        }

        private void ProcessParam<T>(VRCAvatarDescriptor descriptor)
            where T : ScriptableObject
        {
            var instance = ScriptableObject.CreateInstance<T>();
            var type = typeof(T);

            if (!methodCache.TryGetValue(type, out var methods))
            {
                methods = new[]
                {
                    type.GetMethod("Initialize"),
                    type.GetMethod("DeleteFx"),
                    type.GetMethod("DeleteFxBT"),
                    type.GetMethod("DeleteParam"),
                    type.GetMethod("DeleteVRCExpressions"),
                    type.GetMethod("ParticleOptimize"),
                    type.GetMethod("ChangeObj"),
                };
                methodCache[type] = methods;
            }

            var initializeMethod = methods[0];
            var deleteFxMethod = methods[1];
            var deleteFxBTMethod = methods[2];
            var deleteParamMethod = methods[3];
            var deleteVRCExpressionsMethod = methods[4];
            var ParticleOptimizeMethod = methods[5];
            var changeObjMethod = methods[6];

            if (initializeMethod != null)
            {
                try
                {
                    int count = initializeMethod.GetParameters().Length;
                    object result = initializeMethod.Invoke(
                        instance,
                        count == 3
                            ? new object[] { descriptor, controller, this }
                            : new object[] { descriptor, controller }
                    );

                    deleteFxMethod?.Invoke(result, null);
                    deleteFxBTMethod?.Invoke(result, null);
                    deleteParamMethod?.Invoke(result, null);
                    deleteVRCExpressionsMethod?.Invoke(result, new object[] { menu, param });
                    ParticleOptimizeMethod?.Invoke(result, null);
                    changeObjMethod?.Invoke(result, null);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"[ProcessParam] Error processing {type.Name}: {ex.Message}");
                    Debug.LogError($"[ProcessParam] Stack trace: {ex.StackTrace}");
                }
            }
        }

        private ParamProcessConfig[] GetParamConfigs(VRCAvatarDescriptor descriptor)
        {
            return new ParamProcessConfig[]
            {
                new()
                {
                    condition = () => true,
                    processAction = () => ProcessParam<IllRuruneParamDef>(descriptor),
                },
                new()
                {
                    condition = () => petFlg,
                    processAction = () => ProcessParam<IllRuruneParamPet>(descriptor),
                },
                new()
                {
                    condition = () => TailFlg,
                    processAction = () => ProcessParam<IllRuruneParamTail>(descriptor),
                    afterAction = () =>
                    {
                        if (TailFlg1)
                            IllRuruneParam.DestroyObj(descriptor.transform.Find("tail_ribbon"));
                    },
                },
                new()
                {
                    condition = () => ClothFlg,
                    processAction = () => ProcessParam<IllRuruneParamCloth>(descriptor),
                },
                new()
                {
                    condition = () => HairFlg,
                    processAction = () => ProcessParam<IllRuruneParamHair>(descriptor),
                    afterAction = () =>
                    {
                        if (descriptor.transform.Find("Advanced/Particle/4"))
                            if (questFlg1)
                            {
                                IllRuruneParam.DestroyObj(
                                    descriptor.transform.Find(
                                        "Armature/Hips/Spine/Chest/Neck/Head/headphone_particle"
                                    )
                                );
                                IllRuruneParam.DestroyObj(
                                    descriptor.transform.Find("Advanced/Particle/4")
                                );
                            }
                    },
                },
                new()
                {
                    condition = () => TPSFlg,
                    processAction = () => ProcessParam<IllRuruneParamTPS>(descriptor),
                },
                new()
                {
                    condition = () => ClairvoyanceFlg,
                    processAction = () => ProcessParam<IllRuruneParamClairvoyance>(descriptor),
                },
                new()
                {
                    condition = () => colliderJumpFlg,
                    processAction = () => ProcessParam<IllRuruneParamCollider>(descriptor),
                },
                new()
                {
                    condition = () => pictureFlg,
                    processAction = () => ProcessParam<IllRuruneParamPicture>(descriptor),
                },
                new()
                {
                    condition = () => BreastSizeFlg,
                    processAction = () => ProcessParam<IllRuruneParamBreastSize>(descriptor),
                },
                new()
                {
                    condition = () => LightGunFlg,
                    processAction = () => ProcessParam<IllRuruneParamLightGun>(descriptor),
                },
                new()
                {
                    condition = () => WhiteBreathFlg,
                    processAction = () => ProcessParam<IllRuruneParamWhiteBreath>(descriptor),
                },
                new()
                {
                    condition = () => BubbleBreathFlg,
                    processAction = () => ProcessParam<IllRuruneParamBubbleBreath>(descriptor),
                },
                new()
                {
                    condition = () => WaterStampFlg,
                    processAction = () => ProcessParam<IllRuruneParamWaterStamp>(descriptor),
                },
                new()
                {
                    condition = () => eightBitFlg,
                    processAction = () => ProcessParam<IllRuruneParam8bit>(descriptor),
                },
                new()
                {
                    condition = () => HeartGunFlg,
                    processAction = () => ProcessParam<IllRuruneParamHeartGun>(descriptor),
                },
                new()
                {
                    condition = () => PenCtrlFlg,
                    processAction = () => ProcessParam<IllRuruneParamPenCtrl>(descriptor),
                },
                new()
                {
                    condition = () => FaceGestureFlg || FaceLockFlg || FaceValFlg,
                    processAction = () => ProcessParam<IllRuruneParamFaceGesture>(descriptor),
                },
                new()
                {
                    condition = () => kamitukiFlg || nadeFlg || blinkFlg,
                    processAction = () => ProcessParam<IllRuruneParamFaceContact>(descriptor),
                },
            };
        }

        public void Execute(VRCAvatarDescriptor descriptor)
        {
            var stopwatch = Stopwatch.StartNew();
            var stepTimes = new Dictionary<string, long>
            {
                ["InitializeAssets"] = InitializeAssets(descriptor),
                ["editProcessing"] = Edit(descriptor),
                ["FinalizeAssets"] = FinalizeAssets(descriptor)
            };
            stopwatch.Stop();
            Debug.Log(
                $"最適化を実行しました！総処理時間: {stopwatch.ElapsedMilliseconds}ms ({stopwatch.Elapsed.TotalSeconds:F2}秒)"
            );

            foreach (var kvp in stepTimes)
            {
                Debug.Log($"[Performance] {kvp.Key}: {kvp.Value}ms");
            }
        }

        private long Edit(VRCAvatarDescriptor descriptor)
        {
            var step2 = Stopwatch.StartNew();
            var body_b = descriptor.transform.Find("Body_b");
            if (body_b)
                if (body_b.TryGetComponent<SkinnedMeshRenderer>(out var body_bSMR))
                {
                    body_bSMR.SetBlendShapeWeight(28, heelFlg1 || heelFlg2 ? 0 : 100);
                    body_bSMR.SetBlendShapeWeight(29, heelFlg2 ? 100 : 0);
                }
            foreach (var config in GetParamConfigs(descriptor))
            {
                if (config.condition())
                {
                    config.processAction();
                }
                config.afterAction?.Invoke();
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

            IllRuruneDel4Quest.Edit4Quest(descriptor, this);

            step2.Stop();
            return step2.ElapsedMilliseconds;
        }

        private long FinalizeAssets(VRCAvatarDescriptor descriptor)
        {
            var step4 = Stopwatch.StartNew();
            RemoveUnusedMenuControls(menu, param);
            EditorUtility.SetDirty(controller);
            MarkAllMenusDirty(menu);
            EditorUtility.SetDirty(param);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            descriptor.baseAnimationLayers[4].animatorController = controller;
            descriptor.expressionsMenu = menu;
            descriptor.expressionParameters = param;
            EditorUtility.SetDirty(descriptor);
            step4.Stop();
            return step4.ElapsedMilliseconds;
        }

        private long InitializeAssets(VRCAvatarDescriptor descriptor)
        {
            var step1 = Stopwatch.StartNew();
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

            if (!menuDef)
            {
                if (!descriptor.expressionsMenu)
                    descriptor.expressionsMenu = AssetDatabase.LoadAssetAtPath<VRCExpressionsMenu>(
                        AssetDatabase.GUIDToAssetPath("78032fce499b8cd4c9590b79ccdf3166")
                    );
                menuDef = descriptor.expressionsMenu;
            }

            var iconPath = pathDir + "/icon";
            if (!Directory.Exists(iconPath))
            {
                Directory.CreateDirectory(iconPath);
            }
            menu = DuplicateExpressionMenu(menuDef, pathDir, iconPath, questFlg1, textureResize);

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
            step1.Stop();
            return step1.ElapsedMilliseconds;
        }

        private static void RemoveUnusedMenuControls(
            VRCExpressionsMenu menu,
            VRCExpressionParameters param
        )
        {
            if (menu == null)
                return;

            for (int i = menu.controls.Count - 1; i >= 0; i--)
            {
                var control = menu.controls[i];
                bool shouldRemove = true;

                if (string.IsNullOrEmpty(control.parameter.name))
                {
                    shouldRemove = false;
                }
                else
                {
                    if (param.parameters.Any(p => p.name == control.parameter.name))
                    {
                        shouldRemove = false;
                    }
                }

                if (control.subMenu != null)
                {
                    RemoveUnusedMenuControls(control.subMenu, param);
                    if (control.subMenu.controls.Count > 0)
                    {
                        shouldRemove = false;
                    }
                }

                if (shouldRemove)
                {
                    menu.controls.RemoveAt(i);
                }
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

        public static VRCExpressionsMenu DuplicateExpressionMenu(
            VRCExpressionsMenu originalMenu,
            string parentPath,
            string iconPath,
            bool questFlg1,
            TextureResizeOption textureResize
        )
        {
            return DuplicateExpressionMenu(
                originalMenu,
                parentPath,
                iconPath,
                questFlg1,
                textureResize,
                null,
                null,
                null
            );
        }

        private static VRCExpressionsMenu DuplicateExpressionMenu(
            VRCExpressionsMenu originalMenu,
            string parentPath,
            string iconPath,
            bool questFlg1,
            TextureResizeOption textureResize,
            VRCExpressionsMenu rootMenuAsset = null,
            Dictionary<VRCExpressionsMenu, VRCExpressionsMenu> processedMenus = null,
            Dictionary<string, Texture2D> processedIcons = null
        )
        {
            if (originalMenu == null)
            {
                Debug.LogError("元のExpression Menuがありません");
                return null;
            }

            bool isRootCall = processedMenus == null;
            if (isRootCall)
            {
                processedMenus = new Dictionary<VRCExpressionsMenu, VRCExpressionsMenu>();
                processedIcons = new Dictionary<string, Texture2D>();
            }

            if (processedMenus.ContainsKey(originalMenu))
            {
                return processedMenus[originalMenu];
            }

            VRCExpressionsMenu newMenu = Instantiate(originalMenu);
            newMenu.name = originalMenu.name;

            processedMenus[originalMenu] = newMenu;

            if (isRootCall)
            {
                string menuAssetPath = Path.Combine(parentPath, originalMenu.name + ".asset");
                AssetDatabase.CreateAsset(newMenu, menuAssetPath);
                rootMenuAsset = newMenu;
            }
            else if (rootMenuAsset != null)
            {
                AssetDatabase.AddObjectToAsset(newMenu, rootMenuAsset);
            }

            for (int i = 0; i < newMenu.controls.Count; i++)
            {
                var control = newMenu.controls[i];
                if (questFlg1)
                {
                    if (textureResize == TextureResizeOption.LowerResolution)
                    {
                        var originalControl = originalMenu.controls[i];

                        if (originalControl.icon != null)
                        {
                            string iconAssetPath = AssetDatabase.GetAssetPath(originalControl.icon);
                            if (!string.IsNullOrEmpty(iconAssetPath))
                            {
                                string iconFileName = Path.GetFileName(iconAssetPath);
                                string destPath = Path.Combine(iconPath, iconFileName);

                                if (processedIcons.ContainsKey(iconAssetPath))
                                {
                                    control.icon = processedIcons[iconAssetPath];
                                }
                                else
                                {
                                    if (!File.Exists(destPath))
                                    {
                                        File.Copy(iconAssetPath, destPath, true);
                                        AssetDatabase.ImportAsset(destPath);
                                    }

                                    var copiedIcon = AssetDatabase.LoadAssetAtPath<Texture2D>(
                                        destPath
                                    );
                                    if (copiedIcon != null)
                                    {
                                        var importer =
                                            AssetImporter.GetAtPath(destPath) as TextureImporter;
                                        if (importer != null)
                                        {
                                            importer.maxTextureSize = 32;
                                            importer.SaveAndReimport();
                                        }

                                        processedIcons[iconAssetPath] = copiedIcon;
                                        control.icon = copiedIcon;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        control.icon = null;
                    }
                }
                if (control.subMenu != null)
                {
                    control.subMenu = DuplicateExpressionMenu(
                        control.subMenu,
                        parentPath,
                        iconPath,
                        questFlg1,
                        textureResize,
                        rootMenuAsset,
                        processedMenus,
                        processedIcons
                    );
                }
            }
            EditorUtility.SetDirty(newMenu);
            if (isRootCall)
            {
                AssetDatabase.SaveAssets();
            }
            return newMenu;
        }
    }
}
#endif
