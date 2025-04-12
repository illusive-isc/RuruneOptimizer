using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.RuruneOptimizer
{
    [AddComponentMenu("")]
    internal class IllRuruneParamDef : IllRuruneParam
    {
        HashSet<string> paramList = new();
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;

        private static readonly List<string> Layers = new() { "AllParts", "LipSynk" };

        private static readonly List<string> MenuParametersOnly = new()
        {
            "HeartGunCollider L",
            "PlayerCollisionHit",
        };
        private static readonly List<string> NotSyncParameters = new()
        {
            "takasa",
            "takasa_Toggle",
            "Action_Mode_Reset",
            "Action_Mode",
            "Mirror",
            "Mirror Toggle",
            "paryi_change_Standing",
            "paryi_change_Crouching",
            "paryi_change_Prone",
            "paryi_floating",
            "paryi_change_all_reset",
            "paryi_change_Mirror_S",
            "paryi_change_Mirror_P",
            "paryi_change_Mirror_H",
            "paryi_change_Mirror_C",
            "paryi_chang_Loco",
            "paryi_Jump",
            "paryi_Jump_cancel",
            "paryi_change_Standing_M",
            "paryi_change_Crouching_M",
            "paryi_change_Prone_M",
            "paryi_floating_M",
            "leg fixed",
            "JumpCollider",
            "SpeedCollider",
            "ColliderON",
            "clairvoyance",
            "TPS",
        };

        //Pet_Move_Contact
        private static readonly List<string> MenuParameters = new()
        {
            "butterfly_Gesture_Set",
            "cameraLight&eyeLookHide",
            "AvatarScale",
            "Mirror Toggle",
            "koukando",
            "Look_Y",
            "Look_X",
            "blink",
        };

        public IllRuruneParamDef Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            return this;
        }

        public IllRuruneParamDef DeleteFx()
        {
            foreach (var layer in animator.layers)
            {
                if (layer.name == "butterfly")
                {
                    foreach (var state in layer.stateMachine.states)
                    {
                        if (state.state.name == "New State" || state.state.name == "New State 0")
                        {
                            layer.stateMachine.RemoveState(state.state);
                            continue;
                        }
                    }
                }
                if (layer.name == "MainCtrlTree")
                {
                    foreach (var state in layer.stateMachine.states)
                    {
                        if (state.state.name == "MainCtrlTree 0")
                        {
                            layer.stateMachine.RemoveState(state.state);
                            break;
                        }
                    }
                    foreach (var state in layer.stateMachine.states)
                    {
                        if (state.state.motion is BlendTree blendTree)
                        {
                            BlendTree newBlendTree = new()
                            {
                                name = "VRMode",
                                blendParameter = "VRMode",
                                blendParameterY = "Blend",
                                blendType = BlendTreeType.Simple1D,
                                useAutomaticThresholds = false,
                                maxThreshold = 1.0f,
                                minThreshold = 0.0f,
                            };
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
                            AssetDatabase.AddObjectToAsset(newBlendTree, animator);
                            AssetDatabase.SaveAssets();
                        }
                    }

                    foreach (var state in layer.stateMachine.states)
                    {
                        if (state.state.motion is BlendTree blendTree)
                        {
                            BlendTree newBlendTree = new();
                            newBlendTree.name = "LipSynk";
                            newBlendTree.blendParameter = "Viseme";
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
                                if (children[i].motion.name == "LipSynk")
                                {
                                    children[i].threshold = 1;
                                }
                            }

                            // 修正した children 配列を再代入（これをしないと変更が反映されない）
                            blendTree.children = children;

                            var LipSynk = animator.layers.FirstOrDefault(layer =>
                                layer.name == "LipSynk"
                            );
                            newBlendTree.children = new ChildMotion[]
                            {
                                new()
                                {
                                    motion = LipSynk.stateMachine.defaultState.motion,
                                    threshold = 0.0f,
                                    timeScale = 1,
                                },
                                new()
                                {
                                    motion = LipSynk
                                        .stateMachine
                                        .defaultState
                                        .transitions[0]
                                        .destinationState
                                        .motion,
                                    threshold = 0.01f,
                                    timeScale = 1,
                                },
                            };
                            AssetDatabase.AddObjectToAsset(newBlendTree, animator);
                            AssetDatabase.SaveAssets();
                        }
                    }
                    break;
                }
            }
            // "VRMode" パラメータが Float でない場合は再設定
            foreach (var p in animator.parameters.Where(p => p.name == "VRMode").ToList())
            {
                if (p.type != AnimatorControllerParameterType.Float)
                {
                    animator.RemoveParameter(p);
                    animator.AddParameter("VRMode", AnimatorControllerParameterType.Float);
                }
            }
            var removedLayers = animator
                .layers.Where(layer => Layers.Contains(layer.name))
                .ToList();

            animator.layers = animator
                .layers.Where(layer => !Layers.Contains(layer.name))
                .ToArray();

            foreach (var layer in removedLayers)
            {
                foreach (var state in layer.stateMachine.states)
                {
                    AddIfNotInParameters(
                        paramList,
                        exsistParams,
                        state.state.cycleOffsetParameter,
                        state.state.cycleOffsetParameterActive
                    );
                    AddIfNotInParameters(
                        paramList,
                        exsistParams,
                        state.state.timeParameter,
                        state.state.timeParameterActive
                    );
                    AddIfNotInParameters(
                        paramList,
                        exsistParams,
                        state.state.speedParameter,
                        state.state.speedParameterActive
                    );
                    AddIfNotInParameters(
                        paramList,
                        exsistParams,
                        state.state.mirrorParameter,
                        state.state.mirrorParameterActive
                    );

                    foreach (var transition in state.state.transitions)
                    {
                        foreach (var condition in transition.conditions)
                        {
                            AddIfNotInParameters(paramList, exsistParams, condition.parameter);
                        }
                    }
                }

                foreach (var transition in layer.stateMachine.anyStateTransitions)
                {
                    foreach (var condition in transition.conditions)
                    {
                        AddIfNotInParameters(paramList, exsistParams, condition.parameter);
                    }
                }
            }

            return this;
        }

        public IllRuruneParamDef DeleteFxBT()
        {
            foreach (var layer in animator.layers.Where(layer => layer.name == "MainCtrlTree"))
            {
                foreach (var state in layer.stateMachine.states)
                {
                    if (state.state.motion is BlendTree blendTree)
                    {
                        blendTree.children = blendTree
                            .children.Where(c =>
                                CheckBT(c.motion, paramList.Concat(MenuParametersOnly).ToList())
                            )
                            .ToArray();
                    }
                }
            }
            return this;
        }

        public IllRuruneParamDef DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !paramList.Contains(parameter.name))
                .ToArray();
            animator.parameters = animator
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();
            return this;
        }

        public IllRuruneParamDef DeleteVRCExpressions(
            VRCExpressionsMenu menu,
            VRCExpressionParameters param
        )
        {
            param.parameters = param
                .parameters.Where(parameter =>
                    !paramList.Concat(MenuParameters).Contains(parameter.name)
                )
                .ToArray();

            foreach (var parameter in param.parameters)
            {
                if (NotSyncParameters.Contains(parameter.name))
                {
                    parameter.networkSynced = false;
                }
            }

            // foreach (var control in menu.controls)
            // {
            //     if (control.name == "Gimmick")
            //     {
            //         var expressionsSubMenu = control.subMenu;

            //         foreach (var control2 in expressionsSubMenu.controls)
            //         {
            //             if (control2.name == "Pet")
            //             {
            //                 expressionsSubMenu.controls.Remove(control2);
            //                 break;
            //             }
            //         }
            //         control.subMenu = expressionsSubMenu;
            //         break;
            //     }
            // }
            return this;
        }

        public IllRuruneParamDef DestroyObj()
        {
            DestroyObj(descriptor.transform.Find("Advanced/Object"));
            DestroyObj(descriptor.transform.Find("Advanced/FaceEffect"));
            DestroyObj(descriptor.transform.Find("Advanced/Particle/6"));
            DestroyObj(descriptor.transform.Find("Advanced/Gimmick1/8"));
            DestroyObj(descriptor.transform.Find("Advanced/Gimmick2/3"));
            DestroyObj(descriptor.transform.Find("Advanced/Gimmick2/5"));
            DestroyObj(descriptor.transform.Find("Advanced/Gimmick2/6"));
            DestroyObj(descriptor.transform.Find("Advanced/Gimmick2/7"));
            DestroyObj(descriptor.transform.Find("Advanced/Constraint/Hand_R_Constraint"));
            DestroyObj(descriptor.transform.Find("Advanced/Constraint/Hand_L_Constraint"));
            DestroyObj(descriptor.transform.Find("Advanced/cameraLight&eyeLookHide"));

            return this;
        }
    }
}
#endif
