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
    internal class IllRuruneParamPet : IllRuruneParam
    {
        HashSet<string> paramList = new();
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;

        private static readonly List<string> PetLayers = new()
        {
            "Pet",
            "Pet_Animation",
            "Pet_Sleep",
        };

        private static readonly List<string> PetParameters = new()
        {
            "Pet_position.X",
            "Pet_position.Y",
            "Pet_position.Z",
            "Head_search_X+",
            "Head_search_X-",
            "Head_search_Y+",
            "Head_search_Y-",
            "Head_search_Z+",
            "Head_search_Z-",
            "Pet_RandomPosition_off",
            // 実体なし
            "Pet_Move_Contact",
            "Pet_position.X_Local",
            "Pet_position.Y_Local",
            "Pet_position.Z_Local",
            "PlayerDistance_Pet",
            "Head_search_Distance",
            "Pet_Grab_bone_IsGrabbed",
            "Pet_Head_Contact",
            "Pet_Move_Stop",
            "Head_search_off",
            "Pet_Player_Position",
            "Pet_ON",
            "Pet_Head_Stay",
            "Pet_Hand_hit",
            "Pet_Head_Position",
        };

        public IllRuruneParamPet(VRCAvatarDescriptor descriptor, AnimatorController animator)
        {
            this.descriptor = descriptor;
            this.animator = animator;
        }

        public IllRuruneParamPet DeleteFx()
        {
            var removedLayers = animator
                .layers.Where(layer => PetLayers.Contains(layer.name))
                .ToList();

            animator.layers = animator
                .layers.Where(layer => !PetLayers.Contains(layer.name))
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

        public IllRuruneParamPet DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !paramList.Contains(parameter.name))
                .ToArray();
            animator.parameters = animator
                .parameters.Where(parameter => !PetParameters.Contains(parameter.name))
                .ToArray();
            return this;
        }

        public IllRuruneParamPet DeleteFxBT()
        {
            foreach (var layer in animator.layers.Where(layer => layer.name == "MainCtrlTree"))
            {
                foreach (var state in layer.stateMachine.states)
                {
                    if (state.state.motion is BlendTree blendTree)
                    {
                        blendTree.children = blendTree
                            .children.Where(c =>
                                CheckBT(c.motion, paramList.Concat(PetParameters).ToList())
                            )
                            .ToArray();
                    }
                }
            }
            return this;
        }

        public IllRuruneParamPet DeleteVRCExpressions(
            VRCExpressionsMenu menu,
            VRCExpressionParameters param
        )
        {
            param.parameters = param
                .parameters.Where(parameter =>
                    !paramList.Concat(PetParameters).Contains(parameter.name)
                )
                .ToArray();

            foreach (var control in menu.controls)
            {
                if (control.name == "Gimmick")
                {
                    var expressionsSubMenu = control.subMenu;

                    foreach (var control2 in expressionsSubMenu.controls)
                    {
                        if (control2.name == "Pet")
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

        public IllRuruneParamPet DestroyObj()
        {
            DestroyObj(descriptor.transform.Find("Advanced/Pet model"));
            DestroyObj(descriptor.transform.Find("Advanced/Pet_Player_Position"));
            DestroyObj(descriptor.transform.Find("Advanced/Pet_follow"));
            DestroyObj(descriptor.transform.Find("Advanced/PlayerDistance_Pet"));
            return this;
        }
    }
}
#endif
