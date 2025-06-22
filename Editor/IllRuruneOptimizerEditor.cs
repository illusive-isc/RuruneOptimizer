using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;

namespace jp.illusive_isc.RuruneOptimizer
{
    [CustomEditor(typeof(IllRuruneOptimizer))]
    [AddComponentMenu("")]
    internal class IllRuruneOptimizerEditor : Editor
    {
        SerializedProperty petFlg;
        SerializedProperty ClothFlg;
        SerializedProperty HairFlg;
        SerializedProperty TailFlg;
        SerializedProperty TPSFlg;
        SerializedProperty ClairvoyanceFlg;
        SerializedProperty colliderJumpFlg;
        SerializedProperty pictureFlg;
        SerializedProperty BreastSizeFlg;
        SerializedProperty LightGunFlg;
        SerializedProperty WhiteBreathFlg;
        SerializedProperty BubbleBreathFlg;
        SerializedProperty WaterStampFlg;
        SerializedProperty eightBitFlg;
        SerializedProperty PenCtrlFlg;
        SerializedProperty HeartGunFlg;
        SerializedProperty FaceGestureFlg;
        SerializedProperty FaceLockFlg;
        SerializedProperty FaceValFlg;
        SerializedProperty kamitukiFlg;
        SerializedProperty nadeFlg;
        SerializedProperty blinkFlg;
        SerializedProperty controller;
        SerializedProperty menu;
        SerializedProperty param;
        SerializedProperty controllerDef;
        SerializedProperty menuDef;
        SerializedProperty paramDef;
        SerializedProperty IKUSIA_emote;
        SerializedProperty heelFlg1;
        SerializedProperty heelFlg2;
        SerializedProperty ClothFlg1;
        SerializedProperty ClothFlg2;
        SerializedProperty ClothFlg3;
        SerializedProperty ClothFlg4;
        SerializedProperty ClothFlg5;
        SerializedProperty ClothFlg6;
        SerializedProperty ClothFlg7;
        SerializedProperty ClothFlg8;
        SerializedProperty TailFlg1;
        SerializedProperty BreastSizeFlg1;
        SerializedProperty BreastSizeFlg2;
        SerializedProperty BreastSizeFlg3;
        SerializedProperty IKUSIA_emote1;
        SerializedProperty HairFlg10;
        SerializedProperty HairFlg11;
        SerializedProperty HairFlg12;
        SerializedProperty HairFlg20;
        SerializedProperty HairFlg22;
        SerializedProperty HairFlg30;
        SerializedProperty HairFlg40;
        SerializedProperty HairFlg50;
        SerializedProperty HairFlg51;
        SerializedProperty HairFlg60;

        private void OnEnable()
        {
            // フィールド名は元のクラスの変数名と一致させる
            petFlg = serializedObject.FindProperty("petFlg");
            ClothFlg = serializedObject.FindProperty("ClothFlg");
            HairFlg = serializedObject.FindProperty("HairFlg");
            TailFlg = serializedObject.FindProperty("TailFlg");
            TPSFlg = serializedObject.FindProperty("TPSFlg");
            ClairvoyanceFlg = serializedObject.FindProperty("ClairvoyanceFlg");
            colliderJumpFlg = serializedObject.FindProperty("colliderJumpFlg");
            pictureFlg = serializedObject.FindProperty("pictureFlg");
            BreastSizeFlg = serializedObject.FindProperty("BreastSizeFlg");
            LightGunFlg = serializedObject.FindProperty("LightGunFlg");
            WhiteBreathFlg = serializedObject.FindProperty("WhiteBreathFlg");
            BubbleBreathFlg = serializedObject.FindProperty("BubbleBreathFlg");
            WaterStampFlg = serializedObject.FindProperty("WaterStampFlg");
            eightBitFlg = serializedObject.FindProperty("eightBitFlg");
            PenCtrlFlg = serializedObject.FindProperty("PenCtrlFlg");
            HeartGunFlg = serializedObject.FindProperty("HeartGunFlg");
            FaceGestureFlg = serializedObject.FindProperty("FaceGestureFlg");
            FaceLockFlg = serializedObject.FindProperty("FaceLockFlg");
            FaceValFlg = serializedObject.FindProperty("FaceValFlg");
            kamitukiFlg = serializedObject.FindProperty("kamitukiFlg");
            nadeFlg = serializedObject.FindProperty("nadeFlg");
            blinkFlg = serializedObject.FindProperty("blinkFlg");
            controller = serializedObject.FindProperty("controller");
            menu = serializedObject.FindProperty("menu");
            param = serializedObject.FindProperty("param");
            controllerDef = serializedObject.FindProperty("controllerDef");
            menuDef = serializedObject.FindProperty("menuDef");
            paramDef = serializedObject.FindProperty("paramDef");
            IKUSIA_emote = serializedObject.FindProperty("IKUSIA_emote");
            heelFlg1 = serializedObject.FindProperty("heelFlg1");
            heelFlg2 = serializedObject.FindProperty("heelFlg2");
            ClothFlg1 = serializedObject.FindProperty("ClothFlg1");
            ClothFlg2 = serializedObject.FindProperty("ClothFlg2");
            ClothFlg3 = serializedObject.FindProperty("ClothFlg3");
            ClothFlg4 = serializedObject.FindProperty("ClothFlg4");
            ClothFlg5 = serializedObject.FindProperty("ClothFlg5");
            ClothFlg6 = serializedObject.FindProperty("ClothFlg6");
            ClothFlg7 = serializedObject.FindProperty("ClothFlg7");
            ClothFlg8 = serializedObject.FindProperty("ClothFlg8");
            TailFlg1 = serializedObject.FindProperty("TailFlg1");
            BreastSizeFlg1 = serializedObject.FindProperty("BreastSizeFlg1");
            BreastSizeFlg2 = serializedObject.FindProperty("BreastSizeFlg2");
            BreastSizeFlg3 = serializedObject.FindProperty("BreastSizeFlg3");
            IKUSIA_emote1 = serializedObject.FindProperty("IKUSIA_emote1");
            HairFlg10 = serializedObject.FindProperty("HairFlg1");
            HairFlg11 = serializedObject.FindProperty("HairFlg11");
            HairFlg12 = serializedObject.FindProperty("HairFlg12");
            HairFlg20 = serializedObject.FindProperty("HairFlg2");
            HairFlg22 = serializedObject.FindProperty("HairFlg22");
            HairFlg30 = serializedObject.FindProperty("HairFlg3");
            HairFlg40 = serializedObject.FindProperty("HairFlg4");
            HairFlg50 = serializedObject.FindProperty("HairFlg5");
            HairFlg51 = serializedObject.FindProperty("HairFlg51");
            HairFlg60 = serializedObject.FindProperty("HairFlg6");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(heelFlg1, new GUIContent("ヒールON"));
            EditorGUILayout.PropertyField(heelFlg2, new GUIContent("ハイヒールON"));

            EditorGUILayout.PropertyField(ClothFlg, new GUIContent("衣装メニューのみ削除"));
            if (!ClothFlg.boolValue)
            {
                GUI.enabled =
                    ClothFlg1.boolValue =
                    ClothFlg2.boolValue =
                    ClothFlg3.boolValue =
                    ClothFlg4.boolValue =
                    ClothFlg5.boolValue =
                    ClothFlg6.boolValue =
                    ClothFlg7.boolValue =
                    ClothFlg8.boolValue =
                        false;
            }
            EditorGUILayout.PropertyField(ClothFlg1, new GUIContent("  ├ ジャケット削除"));

            EditorGUILayout.PropertyField(ClothFlg2, new GUIContent("  ├ シャツ＆スカート削除"));
            EditorGUILayout.PropertyField(ClothFlg3, new GUIContent("  ├ アクセサリ削除"));
            EditorGUILayout.PropertyField(ClothFlg4, new GUIContent("  ├ string削除"));
            EditorGUILayout.PropertyField(ClothFlg5, new GUIContent("  ├ グローブ削除"));
            EditorGUILayout.PropertyField(ClothFlg6, new GUIContent("  ├ ソックス削除"));
            EditorGUILayout.PropertyField(ClothFlg7, new GUIContent("  ├ 靴削除"));
            EditorGUILayout.PropertyField(ClothFlg8, new GUIContent("  └ 下着削除"));
            GUI.enabled = true;
            EditorGUILayout.PropertyField(BreastSizeFlg, new GUIContent("バストサイズ変更削除"));
            if (!BreastSizeFlg.boolValue)
            {
                GUI.enabled = false;
                BreastSizeFlg1.boolValue = false;
                BreastSizeFlg2.boolValue = false;
                BreastSizeFlg3.boolValue = false;
            }
            EditorGUILayout.PropertyField(BreastSizeFlg1, new GUIContent("  ├ smallにする"));
            EditorGUILayout.PropertyField(BreastSizeFlg2, new GUIContent("  ├ 100にする"));
            EditorGUILayout.PropertyField(BreastSizeFlg3, new GUIContent("  └ 瑞希100にする"));
            GUI.enabled = true;
            {
                var RuruneOptimizer = (IllRuruneOptimizer)target;
                if (BreastSizeFlg1.boolValue != RuruneOptimizer.BreastSizeFlg1)
                {
                    BreastSizeFlg2.boolValue = false;
                    BreastSizeFlg3.boolValue = false;
                }
                else if (BreastSizeFlg2.boolValue != RuruneOptimizer.BreastSizeFlg2)
                {
                    BreastSizeFlg1.boolValue = false;
                    BreastSizeFlg3.boolValue = false;
                }
                else if (BreastSizeFlg3.boolValue != RuruneOptimizer.BreastSizeFlg3)
                {
                    BreastSizeFlg1.boolValue = false;
                    BreastSizeFlg2.boolValue = false;
                }
            }
            EditorGUILayout.PropertyField(
                HairFlg,
                new GUIContent("髪毛/ヘッドフォンをメニューから削除")
            );
            if (!HairFlg.boolValue)
                GUI.enabled =
                    HairFlg10.boolValue =
                    HairFlg11.boolValue =
                    HairFlg20.boolValue =
                    HairFlg12.boolValue =
                    HairFlg30.boolValue =
                    HairFlg60.boolValue =
                    HairFlg50.boolValue =
                    HairFlg51.boolValue =
                    HairFlg40.boolValue =
                        false;
            if (HairFlg60.boolValue)
                GUI.enabled =
                    HairFlg10.boolValue =
                    HairFlg11.boolValue =
                    HairFlg20.boolValue =
                    HairFlg12.boolValue =
                    HairFlg30.boolValue =
                        false;
            EditorGUILayout.PropertyField(HairFlg10, new GUIContent("  │   ├ ぱっつんON"));
            if (!HairFlg10.boolValue)
                GUI.enabled = HairFlg11.boolValue = false;
            EditorGUILayout.PropertyField(HairFlg11, new GUIContent("  │   │   └ ショートON"));
            if (HairFlg.boolValue && !HairFlg60.boolValue)
                GUI.enabled = true;
            EditorGUILayout.PropertyField(HairFlg20, new GUIContent("  │   ├ 前髪左分けON"));
            {
                var RuruneOptimizer = (IllRuruneOptimizer)target;
                if (HairFlg10.boolValue != RuruneOptimizer.HairFlg1)
                    HairFlg20.boolValue = false;
                else if (HairFlg20.boolValue != RuruneOptimizer.HairFlg2)
                    HairFlg10.boolValue = false;
            }

            EditorGUILayout.PropertyField(HairFlg22, new GUIContent("  │   ├ 髪留めON"));
            EditorGUILayout.PropertyField(HairFlg12, new GUIContent("  │   ├ 前髪サイドON"));
            EditorGUILayout.PropertyField(HairFlg30, new GUIContent("  │   └ サイドON"));
            if (HairFlg.boolValue)
                GUI.enabled = true;
            EditorGUILayout.PropertyField(HairFlg60, new GUIContent("  └ hair削除"));
            EditorGUILayout.PropertyField(HairFlg50, new GUIContent("      ├ ヘッドホン削除"));
            if (!HairFlg50.boolValue)
                GUI.enabled = HairFlg51.boolValue = false;
            EditorGUILayout.PropertyField(HairFlg51, new GUIContent("      │   └ particle削除"));
            if (HairFlg.boolValue)
                GUI.enabled = true;
            EditorGUILayout.PropertyField(HairFlg40, new GUIContent("      └ hair2削除"));
            GUI.enabled = true;
            EditorGUILayout.PropertyField(TailFlg, new GUIContent("尻尾削除"));
            EditorGUILayout.PropertyField(TailFlg1, new GUIContent("  └ リボン削除"));
            if (TailFlg.boolValue)
                TailFlg1.boolValue = true;
            EditorGUILayout.PropertyField(petFlg, new GUIContent("ペット削除"));
            EditorGUILayout.PropertyField(TPSFlg, new GUIContent("TPS削除"));
            EditorGUILayout.PropertyField(ClairvoyanceFlg, new GUIContent("透視削除"));
            EditorGUILayout.PropertyField(
                colliderJumpFlg,
                new GUIContent("コライダー・ジャンプ削除")
            );
            EditorGUILayout.PropertyField(pictureFlg, new GUIContent("撮影ギミック削除"));
            EditorGUILayout.PropertyField(LightGunFlg, new GUIContent("ライトガン削除"));
            EditorGUILayout.PropertyField(WhiteBreathFlg, new GUIContent("ホワイトブレス削除"));
            EditorGUILayout.PropertyField(BubbleBreathFlg, new GUIContent("バブルブレス削除"));
            EditorGUILayout.PropertyField(WaterStampFlg, new GUIContent("ウォータースタンプ削除"));
            EditorGUILayout.PropertyField(eightBitFlg, new GUIContent("8bit削除"));
            EditorGUILayout.PropertyField(PenCtrlFlg, new GUIContent("ペン操作削除"));
            EditorGUILayout.PropertyField(HeartGunFlg, new GUIContent("ハートガン削除"));
            EditorGUILayout.PropertyField(
                FaceGestureFlg,
                new GUIContent("デフォルトの表情プリセット削除(faceEmoなど使う場合)")
            );
            EditorGUILayout.PropertyField(FaceLockFlg, new GUIContent("表情固定機能削除"));
            EditorGUILayout.PropertyField(FaceValFlg, new GUIContent("顔差分変更機能削除"));
            EditorGUILayout.PropertyField(
                blinkFlg,
                new GUIContent("まばたきをメニューから削除して常にON")
            );
            EditorGUILayout.PropertyField(
                nadeFlg,
                new GUIContent("なでギミックをメニューから削除して常にON")
            );
            EditorGUILayout.PropertyField(
                kamitukiFlg,
                new GUIContent("噛みつきをメニューから削除して常にON")
            );
            EditorGUILayout.PropertyField(
                IKUSIA_emote,
                new GUIContent("IKUSIA_emoteをメニューのみ削除")
            );
            if (!IKUSIA_emote.boolValue)
            {
                GUI.enabled = IKUSIA_emote1.boolValue = false;
            }
            EditorGUILayout.PropertyField(IKUSIA_emote1, new GUIContent("  └ 手動のAFKは残す"));
            GUI.enabled = true;

            // Execute ボタンの追加
            if (GUILayout.Button("Execute"))
            {
                IllRuruneOptimizer script = (IllRuruneOptimizer)target;
                VRCAvatarDescriptor descriptor =
                    script.transform.root.GetComponent<VRCAvatarDescriptor>();
                if (descriptor != null)
                {
                    try
                    {
                        script.Execute(descriptor);
                    }
                    catch (System.Exception)
                    {
                        Debug.LogWarning("変換に失敗しました。再実行します。");
                        script.Execute(descriptor);
                    }
                }
                else
                {
                    Debug.LogWarning("VRCAvatarDescriptor が見つかりません。");
                }
            }
            EditorGUILayout.Space();
            GUILayout.TextField(
                "生成する元Asset",
                new GUIStyle
                {
                    fontStyle = FontStyle.Bold,
                    fontSize = 24,
                    normal = new GUIStyleState { textColor = Color.white },
                }
            );
            GUI.enabled = false;
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(controllerDef, new GUIContent("Animator Controller"));
            EditorGUILayout.PropertyField(menuDef, new GUIContent("Expressions Menu"));
            EditorGUILayout.PropertyField(paramDef, new GUIContent("Expression Parameters"));
            GUI.enabled = true;
            EditorGUILayout.Space();
            GUILayout.TextField(
                "生成されたAsset",
                new GUIStyle
                {
                    fontStyle = FontStyle.Bold,
                    fontSize = 24,
                    normal = new GUIStyleState { textColor = Color.white },
                }
            );
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(controller, new GUIContent("Animator Controller"));
            EditorGUILayout.PropertyField(menu, new GUIContent("Expressions Menu"));
            EditorGUILayout.PropertyField(param, new GUIContent("Expression Parameters"));

            // 変更内容の適用
            serializedObject.ApplyModifiedProperties();
        }

        // ▼ 右クリックメニューで作成できるようにするためのメニュー項目 ▼

        // Validate メソッドで対象が VRC アバターのルートであり、さらにアタッチされている Animator の avatar が "ruruneAvatar" であることをチェック
        [MenuItem("GameObject/illusive_tools/Create IllRuruneOptimizer Object", true)]
        private static bool ValidateCreateIllRuruneOptimizerObject(MenuCommand menuCommand)
        {
            GameObject contextGO = menuCommand.context as GameObject;
            if (contextGO == null)
            {
                contextGO = Selection.activeGameObject;
            }
            if (contextGO == null)
            {
                // どちらも null ならエラーを出すか、false を返す
                return false;
            }
            // 対象が VRCAvatarDescriptor を持っているか
            if (contextGO.GetComponent<VRCAvatarDescriptor>() == null)
                return false;
            // さらに、親に VRCAvatarDescriptor が存在しない（＝ルートである）かをチェック
            if (
                contextGO.transform.parent != null
                && contextGO.transform.parent.GetComponent<VRCAvatarDescriptor>() != null
            )
                return false;
            // Animator コンポーネントが存在し、その avatar プロパティの名前が "ruruneAvatar" であるかをチェック
            Animator animator = contextGO.GetComponent<Animator>();
            if (animator == null)
                return false;
            if (animator.avatar == null)
                return false;
            if (animator.avatar.name != "ruruneAvatar")
                return false;
            return true;
        }

        // 対象が条件を満たす場合のみ、メニュー項目が有効となる
        [MenuItem("GameObject/illusive_tools/Create IllRuruneOptimizer Object", false, 10)]
        private static void CreateIllRuruneOptimizerObject(MenuCommand menuCommand)
        {
            // 新しい GameObject を作成し、IllRuruneOptimizer コンポーネントを追加
            GameObject go = new GameObject("IllRuruneOptimizer");
            go.AddComponent<IllRuruneOptimizer>();

            // 右クリックで選択されたオブジェクト（VRCアバターのルート）の子として配置
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }
    }
}
