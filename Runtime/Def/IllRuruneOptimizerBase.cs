using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.RuruneOptimizer
{
    public class IllRuruneOptimizerBase : MonoBehaviour
    {
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

        public AnimatorController AnimatorControllerBTCopy(AnimatorController controller)
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

            if (def != null && def.stateMachine.states.Length >= 2)
            {
                // 新しい StateMachine を作成
                AnimatorStateMachine newStateMachine = new AnimatorStateMachine();
                newStateMachine.name = "MainCtrlTree_SM";

                // ステートを2つだけコピー
                var states = def.stateMachine.states.Take(2).ToArray();
                AnimatorState state1 = newStateMachine.AddState(
                    states[0].state.name,
                    states[0].position
                );
                AnimatorState state2 = newStateMachine.AddState(
                    states[1].state.name,
                    states[1].position
                );

                // MotionがBlendTreeなら新しいインスタンスを作成
                state1.motion = CopyMotion(states[0].state.motion);
                state2.motion = CopyMotion(states[1].state.motion);

                // スピードのコピー
                state1.speed = states[0].state.speed;
                state2.speed = states[1].state.speed;

                // デフォルトステートの設定
                newStateMachine.defaultState = state1;

                // トランジションを1つだけコピー
                var transition = states[0].state.transitions.FirstOrDefault();
                if (transition != null && transition.conditions.Length > 0)
                {
                    AnimatorStateTransition newTransition = state1.AddTransition(state2);
                    newTransition.hasExitTime = transition.hasExitTime;
                    newTransition.duration = transition.duration;
                    newTransition.offset = transition.offset;
                    newTransition.exitTime = transition.exitTime;
                    newTransition.isExit = transition.isExit;
                    newTransition.canTransitionToSelf = transition.canTransitionToSelf;

                    // 条件を1つだけコピー
                    var condition = transition.conditions[0];
                    newTransition.AddCondition(
                        condition.mode,
                        condition.threshold,
                        condition.parameter
                    );
                }

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

                // レイヤーを追加
                filteredLayers.Add(newLayer);
            }
            else
            {
                if (def != null && def.stateMachine.states.Length >= 1)
                {
                    // 新しい StateMachine を作成
                    AnimatorStateMachine newStateMachine = new AnimatorStateMachine();
                    newStateMachine.name = "MainCtrlTree_SM";

                    // ステートを2つだけコピー
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

                    // レイヤーを追加
                    filteredLayers.Add(newLayer);
                }
            }

            // レイヤーを適用
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
    }
}
#endif
