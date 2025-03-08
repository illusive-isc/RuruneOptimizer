using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        // 新しい AnimatorController を作成
        string pathDirPrefix = "Assets/RuruneOptimizer/";
        string pathDirSuffix = "/FX/";
        string pathName = "paryi_FX.controller";

        [SerializeField]
        private bool petFlg = false;

        [SerializeField]
        private bool ClothFlg = false;

        [SerializeField]
        private bool ClothDelFlg = false;

        [SerializeField]
        private bool HairFlg = false;

        [SerializeField]
        private bool HairDelFlg = false;

        [SerializeField]
        private bool TailFlg = false;

        [SerializeField]
        private bool HeadphoneFlg = false;

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
        private AnimatorController controllerDef;

        [SerializeField]
        private VRCExpressionsMenu menuDef;

        [SerializeField]
        private VRCExpressionParameters paramDef;

        [SerializeField]
        private AnimatorController controller;

        [SerializeField]
        private VRCExpressionsMenu menu;

        [SerializeField]
        private VRCExpressionParameters param;

        public void Execute(VRCAvatarDescriptor descriptor)
        {
            var pathDir = pathDirPrefix + descriptor.gameObject.name + pathDirSuffix;
            if (!string.IsNullOrEmpty(pathDir + pathName))
            {
                AssetDatabase.DeleteAsset(pathDir + pathName);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
                AssetDatabase.Refresh();
            }
            if (!controllerDef)
            {
                controllerDef = (AnimatorController)
                    descriptor.baseAnimationLayers[4].animatorController;
            }

            controller = AnimatorControllerStateMachineCopy(
                AnimatorControllerBTCopy(controller = controllerDef)
            );
            foreach (var param in controller.parameters.Where(p => p.name == "VRMode").ToList())
            {
                if (param.type != AnimatorControllerParameterType.Float)
                {
                    controller.RemoveParameter(param);
                    controller.AddParameter("VRMode", AnimatorControllerParameterType.Float);
                }
            }
            // アセットとして保存（エディタ専用）
            AssetDatabase.CreateAsset(controller, pathDir + pathName);
            AssetDatabase.SaveAssets();

            if (!menuDef)
            {
                menuDef = descriptor.expressionsMenu;
            }
            menu = DuplicateExpressionMenu(menuDef, pathDir);
            if (!paramDef)
            {
                paramDef = descriptor.expressionParameters;
                paramDef.name = descriptor.expressionParameters.name;
            }
            param = Instantiate(paramDef);
            param.name = paramDef.name;
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
            if (TailFlg)
            {
                IllRuruneParamTail illRuruneParamTail = new(descriptor, controller);
                illRuruneParamTail.DeleteFxBT().DeleteParam().DestroyObj();
            }
            if (ClothFlg)
            {
                IllRuruneParamCloth illRuruneParamCloth = new(descriptor, controller);
                illRuruneParamCloth
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .EditorOnlyAll(TailFlg);
                if (ClothDelFlg)
                    illRuruneParamCloth.DestroyObjAll(TailFlg);
            }
            if (HairFlg)
            {
                IllRuruneParamHair illRuruneParamHair = new(descriptor, controller);
                illRuruneParamHair
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .EditorOnly();
                if (HairDelFlg)
                    illRuruneParamHair.DestroyObj();
            }

            if (HeadphoneFlg)
            {
                IllRuruneParamHeadphone illRuruneParamHeadphone = new(descriptor, controller);
                illRuruneParamHeadphone
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
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
            if (HeartGunFlg)
            {
                IllRuruneParamHeartGun illRuruneParamHeartGun = new(descriptor, controller);
                illRuruneParamHeartGun
                    .DeleteFx()
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (PenCtrlFlg)
            {
                IllRuruneParamPenCtrl illRuruneParamPenCtrl = new(descriptor, controller);
                illRuruneParamPenCtrl
                    .DeleteFx(HeartGunFlg)
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
                illRuruneParamDef.ExeDestroyObj(descriptor.transform.Find("Advanced/Particle"));
            }
            descriptor.baseAnimationLayers[4].animatorController = controller;
            descriptor.expressionsMenu = menu;
            descriptor.expressionParameters = param;
            Debug.Log("最適化を実行しました！");
        }

        private AnimatorController AnimatorControllerBTCopy(AnimatorController controller)
        {
            if (controller == null)
                return null;

            AnimatorController newController = Instantiate(controller);
            newController.name = controller.name;

            // "MainCtrlTree" を除外したレイヤーをコピー
            var filteredLayers = controller
                .layers.Where(layer => layer.name != "MainCtrlTree")
                .ToList();

            // "MainCtrlTree" の元レイヤーを取得
            var def = controller.layers.FirstOrDefault(layer => layer.name == "MainCtrlTree");

            if (def != null && def.stateMachine.states.Length >= 1)
            {
                // 新しい StateMachine を作成
                AnimatorStateMachine newStateMachine = new AnimatorStateMachine();
                newStateMachine.name = "MainCtrlTree_SM";

                // ステートをコピー
                var states = def.stateMachine.states.Take(2).ToArray();
                AnimatorState state1 = newStateMachine.AddState(
                    states[0].state.name,
                    states[0].position
                );

                // MotionがBlendTreeなら新しいインスタンスを作成
                state1.motion = CopyMotion(states[0].state.motion);

                // スピードのコピー
                state1.speed = states[0].state.speed;

                // デフォルトステートの設定
                newStateMachine.defaultState = state1;

                // 新しいレイヤーを作成
                AnimatorControllerLayer newLayer = new AnimatorControllerLayer
                {
                    name = "MainCtrlTree",
                    avatarMask = def.avatarMask,
                    blendingMode = def.blendingMode,
                    defaultWeight = def.defaultWeight,
                    iKPass = def.iKPass,
                    syncedLayerIndex = def.syncedLayerIndex,
                    syncedLayerAffectsTiming = def.syncedLayerAffectsTiming,
                    stateMachine = newStateMachine,
                };

                foreach (var state in newLayer.stateMachine.states)
                {
                    if (state.state.motion is BlendTree blendTree)
                    {
                        blendTree.children = blendTree
                            .children.Where(c => !(c.motion.name == "VRMode0"))
                            .ToArray();

                        BlendTree newBlendTree = new();
                        newBlendTree.name = "VRMode";
                        newBlendTree.blendParameter = "VRMode";
                        newBlendTree.blendParameterY = "Blend";
                        newBlendTree.blendType = BlendTreeType.Simple1D;
                        newBlendTree.useAutomaticThresholds = false;
                        newBlendTree.maxThreshold = 0.01f;
                        newBlendTree.minThreshold = 0.0f;
                        blendTree.AddChild(newBlendTree);
                        // BlendTreeの子モーションを取得
                        var children = blendTree.children;

                        // "LipSynk" のモーションがある場合、threshold を変更
                        for (int i = 0; i < children.Length; i++)
                        {
                            if (children[i].motion.name == "VRMode")
                            {
                                children[i].threshold = 1;
                            }
                        }

                        // 修正した children 配列を再代入（これをしないと変更が反映されない）
                        blendTree.children = children;

                        newBlendTree.children = new ChildMotion[]
                        {
                            new()
                            {
                                motion = AssetDatabase.LoadAssetAtPath<Motion>(
                                    AssetDatabase.GUIDToAssetPath(
                                        AssetDatabase.FindAssets("VRMode0")[0]
                                    )
                                ),
                                threshold = 0.0f,
                                timeScale = 1,
                            },
                            new()
                            {
                                motion = AssetDatabase.LoadAssetAtPath<Motion>(
                                    AssetDatabase.GUIDToAssetPath(
                                        AssetDatabase.FindAssets("VRMode1")[0]
                                    )
                                ),
                                threshold = 1.0f,
                                timeScale = 1,
                            },
                        };
                    }
                }
                // レイヤーを追加
                filteredLayers.Add(newLayer);
            }

            // レイヤーを適用
            newController.layers = filteredLayers.ToArray();

            return newController;
        }

        private AnimatorController AnimatorControllerStateMachineCopy(AnimatorController controller)
        {
            if (controller == null)
                return null;

            // Instantiate で基本コピー（その他のプロパティはそのまま）
            AnimatorController newController = Instantiate(controller);
            newController.name = controller.name;

            // "MainCtrlTree" を除外したレイヤーをコピー
            var filteredLayers = controller
                .layers.Where(layer => !(layer.name is "PenCtrl_R" or "PenCtrl_L" or "butterfly"))
                .ToList();

            foreach (
                var layer in controller.layers.Where(layer =>
                    layer.name is "PenCtrl_R" or "PenCtrl_L" or "butterfly"
                )
            )
            {
                // ステートマシーンの基本情報をコピー
                AnimatorStateMachine newStateMachine = new AnimatorStateMachine
                {
                    name = layer.stateMachine.name,
                    anyStatePosition = layer.stateMachine.anyStatePosition,
                    entryPosition = layer.stateMachine.entryPosition,
                    exitPosition = layer.stateMachine.exitPosition,
                };

                // 元のステートと新規ステートの対応を保持する辞書を作成
                Dictionary<AnimatorState, AnimatorState> stateMapping =
                    new Dictionary<AnimatorState, AnimatorState>();

                // ① 各ステートをコピー（モーションは参照そのまま、追加パラメーターもコピー）

                foreach (
                    var child in layer.stateMachine.states.Where(state =>
                        !(layer.name == "butterfly" && state.state.name.Contains("New State"))
                    )
                )
                {
                    AnimatorState oldState = child.state;
                    AnimatorState newState = newStateMachine.AddState(
                        oldState.name,
                        child.position
                    );
                    newState.speed = oldState.speed;
                    newState.motion = oldState.motion;
                    // 追加パラメーターのコピー（必要に応じて deep copy も検討）
                    newState.mirror = oldState.mirror;
                    newState.writeDefaultValues = oldState.writeDefaultValues;
                    newState.cycleOffset = oldState.cycleOffset;
                    newState.behaviours = oldState.behaviours; // ※ shallow copy（参照渡し）
                    stateMapping.Add(oldState, newState);
                }

                // ② デフォルトステートのコピー
                if (
                    layer.stateMachine.defaultState != null
                    && stateMapping.ContainsKey(layer.stateMachine.defaultState)
                )
                {
                    newStateMachine.defaultState = stateMapping[layer.stateMachine.defaultState];
                }

                // ③ 各ステートのトランジションをコピー
                foreach (
                    var child in layer.stateMachine.states.Where(state =>
                        !(layer.name == "butterfly" && state.state.name.Contains("New State"))
                    )
                )
                {
                    AnimatorState oldState = child.state;
                    AnimatorState newState = stateMapping[oldState];

                    foreach (var transition in oldState.transitions)
                    {
                        if (!transition.isExit)
                        {
                            if (
                                stateMapping.TryGetValue(
                                    transition.destinationState,
                                    out AnimatorState newDest
                                )
                            )
                            {
                                AnimatorStateTransition newTransition = newState.AddTransition(
                                    newDest
                                );
                                newTransition.hasExitTime = transition.hasExitTime;
                                newTransition.exitTime = transition.exitTime;
                                newTransition.duration = transition.duration;
                                newTransition.offset = transition.offset;
                                newTransition.mute = transition.mute;
                                newTransition.interruptionSource = transition.interruptionSource;

                                // 遷移条件のコピー
                                foreach (var cond in transition.conditions)
                                {
                                    if (layer.name == "butterfly" && cond.parameter == "VRMode")
                                    {
                                        newTransition.AddCondition(
                                            AnimatorConditionMode.Greater,
                                            0.5f,
                                            cond.parameter
                                        );
                                    }
                                    else
                                    {
                                        newTransition.AddCondition(
                                            cond.mode,
                                            cond.threshold,
                                            cond.parameter
                                        );
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (var cond in transition.conditions)
                            {
                                AnimatorStateTransition newTransition =
                                    newState.AddExitTransition();
                                newTransition.AddCondition(
                                    cond.mode,
                                    cond.threshold,
                                    cond.parameter
                                );
                            }
                        }
                    }
                }

                // ④ AnyState のトランジションもコピー
                foreach (var transition in layer.stateMachine.anyStateTransitions)
                {
                    if (transition.destinationState == null)
                        continue;

                    if (
                        stateMapping.TryGetValue(
                            transition.destinationState,
                            out AnimatorState newDest
                        )
                    )
                    {
                        AnimatorStateTransition newTransition =
                            newStateMachine.AddAnyStateTransition(newDest);
                        newTransition.hasExitTime = transition.hasExitTime;
                        newTransition.exitTime = transition.exitTime;
                        newTransition.duration = transition.duration;
                        newTransition.offset = transition.offset;
                        newTransition.mute = transition.mute;
                        newTransition.interruptionSource = transition.interruptionSource;

                        foreach (var cond in transition.conditions)
                        {
                            newTransition.AddCondition(cond.mode, cond.threshold, cond.parameter);
                        }
                    }
                }

                // レイヤー情報を再構築
                AnimatorControllerLayer newLayer = new AnimatorControllerLayer
                {
                    name = layer.name,
                    avatarMask = layer.avatarMask,
                    blendingMode = layer.blendingMode,
                    defaultWeight = layer.defaultWeight,
                    iKPass = layer.iKPass,
                    syncedLayerIndex = layer.syncedLayerIndex,
                    syncedLayerAffectsTiming = layer.syncedLayerAffectsTiming,
                    stateMachine = newStateMachine,
                };

                filteredLayers.Insert(7, newLayer);
            }

            newController.layers = filteredLayers.ToArray();
            return newController;
        }

        private Motion CopyMotion(Motion originalMotion)
        {
            if (originalMotion == null)
                return null;

            // BlendTree の場合、新しいインスタンスを作成
            if (originalMotion is BlendTree originalBlendTree)
            {
                BlendTree newBlendTree = new BlendTree();
                newBlendTree.name = originalBlendTree.name;
                newBlendTree.blendParameter = originalBlendTree.blendParameter;
                newBlendTree.blendParameterY = originalBlendTree.blendParameterY;
                newBlendTree.blendType = originalBlendTree.blendType;
                newBlendTree.useAutomaticThresholds = false;

                EditorUtility.SetDirty(newBlendTree);
                // BlendTree のモーションをコピー（リンクを切る）
                newBlendTree.children = originalBlendTree
                    .children.Select(child => new ChildMotion
                    {
                        motion = child.motion,
                        position = child.position,
                        threshold = 1,
                        timeScale = 1,
                        directBlendParameter = child.directBlendParameter,
                    })
                    .ToArray();

                return newBlendTree;
            }

            // 通常の Motion（AnimationClip）はそのまま
            return originalMotion;
        }

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

            // このメニュー用のフォルダを作成（親フォルダの中に作る）
            string menuFolderPath = Path.Combine(parentPath, originalMenu.name);
            if (!Directory.Exists(menuFolderPath))
            {
                Directory.CreateDirectory(menuFolderPath);
                AssetDatabase.Refresh(); // Unity にフォルダの追加を認識させる
            }

            // メニューの新しい保存パス
            string menuAssetPath = Path.Combine(menuFolderPath, originalMenu.name + ".asset");

            // すでにアセットが存在する場合はそれを読み込む
            VRCExpressionsMenu newMenu = AssetDatabase.LoadAssetAtPath<VRCExpressionsMenu>(
                menuAssetPath
            );
            if (newMenu != null)
            {
                return newMenu; // 既にコピー済みならそのまま返す
            }

            // 新規作成
            newMenu = Instantiate(originalMenu);

            AssetDatabase.CreateAsset(newMenu, menuAssetPath);

            // サブメニューを再帰的に複製（新しいフォルダを作りながら）
            for (int i = 0; i < newMenu.controls.Count; i++)
            {
                var control = newMenu.controls[i];

                if (control.subMenu != null)
                {
                    string subMenuFolderPath = Path.Combine(menuFolderPath, control.subMenu.name);

                    // 既にあるサブメニューは再作成しない
                    VRCExpressionsMenu existingSubMenu =
                        AssetDatabase.LoadAssetAtPath<VRCExpressionsMenu>(
                            Path.Combine(subMenuFolderPath, control.subMenu.name + ".asset")
                        );

                    if (existingSubMenu == null)
                    {
                        // 再帰的にサブメニューを複製
                        control.subMenu = DuplicateExpressionMenu(control.subMenu, menuFolderPath);
                    }
                    else
                    {
                        control.subMenu = existingSubMenu;
                    }
                }
            }

            // 変更を保存
            EditorUtility.SetDirty(newMenu);
            AssetDatabase.SaveAssets();

            return newMenu;
        }
    }
}
#endif
