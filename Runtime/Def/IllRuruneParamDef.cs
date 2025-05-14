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
            "FaceLock",
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
            DestroyObj(descriptor.transform.Find("Advanced/cameraLight&eyeLookHide"));

            return this;
        }

        public IllRuruneParamDef ParticleOptimize()
        {
            SetMaxParticle("Advanced/Particle/1/breath", 100);
            SetMaxParticle("Advanced/Particle/2/WaterFoot_R/WaterFoot2/WaterFoot3", 10);
            SetMaxParticle("Advanced/Particle/2/WaterFoot_R/WaterFoot2/WaterFoot3/WaterFoot4", 10);
            SetMaxParticle("Advanced/Particle/2/WaterFoot_L/WaterFoot2/WaterFoot3", 10);
            SetMaxParticle("Advanced/Particle/2/WaterFoot_L/WaterFoot2/WaterFoot3/WaterFoot4", 10);
            SetMaxParticle("Advanced/Particle/3/Headbubbles/bubbles", 50);
            SetMaxParticle("Advanced/Particle/3/Headbubbles/bubbles/bubbles2", 50);
            SetMaxParticle("Advanced/Particle/3/Headbubbles/bubbles_Idol", 50);
            SetMaxParticle("Advanced/Particle/3/Headbubbles/bubbles_Idol/bubbles2", 50);
            SetMaxParticle("Advanced/Particle/5/8bitheart", 5);
            SetMaxParticle("Advanced/Particle/5/8bitheart/8bitheart flare", 15);

            SetMaxParticle("Advanced/HeartGunR/HeartGunFLY/HeartGunFLY2", 50);
            SetMaxParticle("Advanced/HeartGunR/HeartGunFLY/HeartGunFLY2/kira", 3200);
            SetMaxParticle("Advanced/HeartGunR/HeartGunFLY/HeartGunFLY2/Heart", 100);
            SetMaxParticle("Advanced/HeartGunR/HeartGunFLY/HeartGunFLY2/shot2", 400);
            SetMaxParticle("Advanced/HeartGunR/HeartGunFLY/HeartGunFLY2/shot2 (1)", 400);
            SetMaxParticle("Advanced/HeartGunR/HeartGunFLY/HeartGunChargeStay", 50);

            SetMaxParticle("Advanced/HeartGunR/HeartGunCollider/HeadHit/HeadHit", 50);
            SetMaxParticle("Advanced/HeartGunR/HeartGunCollider/HeadHit/Heart (1)", 50);

            SetMaxParticle("Advanced/HeartGunL/HeartGunFLY/HeartGunFLY2", 50);
            SetMaxParticle("Advanced/HeartGunL/HeartGunFLY/HeartGunFLY2/kira", 3200);
            SetMaxParticle("Advanced/HeartGunL/HeartGunFLY/HeartGunFLY2/Heart", 100);
            SetMaxParticle("Advanced/HeartGunL/HeartGunFLY/HeartGunFLY2/shot2", 400);
            SetMaxParticle("Advanced/HeartGunL/HeartGunFLY/HeartGunFLY2/shot2 (1)", 400);
            SetMaxParticle("Advanced/HeartGunL/HeartGunFLY/HeartGunChargeStay", 50);
            SetMaxParticle("Advanced/HeartGunL/HeartGunCollider/HeadHit/HeadHit", 50);
            SetMaxParticle("Advanced/HeartGunL/HeartGunCollider/HeadHit/Heart (1)", 50);
            SetMaxParticle("Advanced/HeartGunR2/HeartGunFLY/HeartGunFLY2", 50);
            SetMaxParticle("Advanced/HeartGunR2/HeartGunFLY/HeartGunFLY2/kira", 3200);
            SetMaxParticle("Advanced/HeartGunR2/HeartGunFLY/HeartGunFLY2/Heart", 100);
            SetMaxParticle("Advanced/HeartGunR2/HeartGunFLY/HeartGunFLY2/shot2", 400);
            SetMaxParticle("Advanced/HeartGunR2/HeartGunFLY/HeartGunFLY2/shot2 (1)", 400);
            SetMaxParticle("Advanced/HeartGunR2/HeartGunFLY/HeartGunChargeStay", 50);
            SetMaxParticle("Advanced/HeartGunL2/HeartGunFLY/HeartGunFLY2", 50);
            SetMaxParticle("Advanced/HeartGunL2/HeartGunFLY/HeartGunFLY2/kira", 3200);
            SetMaxParticle("Advanced/HeartGunL2/HeartGunFLY/HeartGunFLY2/Heart", 100);
            SetMaxParticle("Advanced/HeartGunL2/HeartGunFLY/HeartGunFLY2/shot2", 1200);
            SetMaxParticle("Advanced/HeartGunL2/HeartGunFLY/HeartGunFLY2/shot2 (1)", 1200);
            SetMaxParticle("Advanced/HeartGunL2/HeartGunFLY/HeartGunChargeStay", 50);
            SetMaxParticle("Advanced/AFK_World/position/swim/Particle System", 10);
            SetMaxParticle("Advanced/AFK_World/position/IdolParticle", 1);
            SetMaxParticle("Advanced/AFK_World/position/AFKIN Particle/AFK In0", 1);
            SetMaxParticle("Advanced/AFK_World/position/AFKIN Particle/AFK In1", 1);
            SetMaxParticle("Advanced/AFK_World/position/AFKIN Particle/AFK In1/IN S1", 1);
            SetMaxParticle("Advanced/AFK_World/position/AFKIN Particle/AFK In2", 20);
            SetMaxParticle("Advanced/AFK_World/position/AFKIN Particle/AFK In2/AFK In (1)", 20);
            SetMaxParticle("Advanced/AFK_World/position/AFKIN Particle/IN S3", 1);
            SetMaxParticle("Advanced/AFK_World/position/AFKIN Particle/IN S3/AFK In (1)", 1);
            SetMaxParticle("Advanced/Pet model/Constraint/Constraint/shark/bubbles_Idol", 10);
            SetMaxParticle("Advanced/Pet model/Constraint/Constraint/zzz", 1);
            SetMaxParticle("Advanced/Pet model/Constraint/Constraint/zzz/zzz2", 3);
            SetMaxParticle("Advanced/Pet model/Constraint/Constraint/zzz/zzz2/zzz3", 3);

            SetMaxParticle("Advanced/butterfly/world/target_constraint/cyou/idolParticle", 20);
            SetMaxParticle(
                "Advanced/butterfly/world/target_constraint/cyou/idolParticle/idolParticle2",
                20
            );
            SetMaxParticle(
                "Advanced/butterfly/world/target_constraint/cyou/idolParticle/idolParticle2/idolParticle3",
                20
            );
            SetMaxParticle("Advanced/butterfly/world/target_constraint/deru", 20);
            SetMaxParticle("Advanced/butterfly/world/target_constraint/orbit", 20);
            SetMaxParticle("Advanced/butterfly/world/target_constraint/kabecollision", 1);
            SetMaxParticle("Advanced/butterfly/IndexHandR/handtap_ONOFF/handtap", 10);

            SetMaxParticle("Advanced/Particle/7/2/pen1_R/PenParticle", 10000);
            SetMaxParticle("Advanced/Particle/7/2/pen1_R/PenParticle/SubEmitter0", 5000);
            SetMaxParticle("Advanced/Particle/7/4/pen1_L/PenParticle", 10000);
            SetMaxParticle("Advanced/Particle/7/4/pen1_L/PenParticle/SubEmitter0", 5000);

            return this;
        }

        private void SetMaxParticle(string path, int max)
        {
            var particleobj = descriptor.transform.Find(path);
            if (particleobj)
            {
                var mainModule = particleobj.GetComponent<ParticleSystem>().main;
                mainModule.maxParticles = max;
            }
        }
    }
}
#endif
