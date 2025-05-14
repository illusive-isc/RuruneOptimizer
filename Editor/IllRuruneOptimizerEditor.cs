using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;

namespace jp.illusive_isc.RuruneOptimizer
{
    [CustomEditor(typeof(IllRuruneOptimizer))]
    [AddComponentMenu("")]
    internal class IllRuruneOptimizerEditor : Editor
    {
        SerializedProperty petFlgProp;
        SerializedProperty ClothFlgProp;
        SerializedProperty HairFlgProp;
        SerializedProperty TailFlgProp;
        SerializedProperty TPSFlgProp;
        SerializedProperty ClairvoyanceFlgProp;
        SerializedProperty colliderJumpFlgProp;
        SerializedProperty pictureFlgProp;
        SerializedProperty BreastSizeFlgProp;
        SerializedProperty LightGunFlgProp;
        SerializedProperty WhiteBreathFlgProp;
        SerializedProperty BubbleBreathFlgProp;
        SerializedProperty WaterStampFlgProp;
        SerializedProperty eightBitFlgProp;
        SerializedProperty PenCtrlFlgProp;
        SerializedProperty HeartGunFlgProp;
        SerializedProperty FaceGestureFlgProp;
        SerializedProperty FaceLockFlgProp;
        SerializedProperty FaceValFlgProp;
        SerializedProperty kamitukiFlgProp;
        SerializedProperty nadeFlgProp;
        SerializedProperty blinkFlgProp;
        SerializedProperty controllerProp;
        SerializedProperty menuProp;
        SerializedProperty paramProp;
        SerializedProperty controllerDefProp;
        SerializedProperty menuDefProp;
        SerializedProperty paramDefProp;
        SerializedProperty IKUSIA_emoteProp;

        private void OnEnable()
        {
            // フィールド名は元のクラスの変数名と一致させる
            petFlgProp = serializedObject.FindProperty("petFlg");
            ClothFlgProp = serializedObject.FindProperty("ClothFlg");
            HairFlgProp = serializedObject.FindProperty("HairFlg");
            TailFlgProp = serializedObject.FindProperty("TailFlg");
            TPSFlgProp = serializedObject.FindProperty("TPSFlg");
            ClairvoyanceFlgProp = serializedObject.FindProperty("ClairvoyanceFlg");
            colliderJumpFlgProp = serializedObject.FindProperty("colliderJumpFlg");
            pictureFlgProp = serializedObject.FindProperty("pictureFlg");
            BreastSizeFlgProp = serializedObject.FindProperty("BreastSizeFlg");
            LightGunFlgProp = serializedObject.FindProperty("LightGunFlg");
            WhiteBreathFlgProp = serializedObject.FindProperty("WhiteBreathFlg");
            BubbleBreathFlgProp = serializedObject.FindProperty("BubbleBreathFlg");
            WaterStampFlgProp = serializedObject.FindProperty("WaterStampFlg");
            eightBitFlgProp = serializedObject.FindProperty("eightBitFlg");
            PenCtrlFlgProp = serializedObject.FindProperty("PenCtrlFlg");
            HeartGunFlgProp = serializedObject.FindProperty("HeartGunFlg");
            FaceGestureFlgProp = serializedObject.FindProperty("FaceGestureFlg");
            FaceLockFlgProp = serializedObject.FindProperty("FaceLockFlg");
            FaceValFlgProp = serializedObject.FindProperty("FaceValFlg");
            kamitukiFlgProp = serializedObject.FindProperty("kamitukiFlg");
            nadeFlgProp = serializedObject.FindProperty("nadeFlg");
            blinkFlgProp = serializedObject.FindProperty("blinkFlg");
            controllerProp = serializedObject.FindProperty("controller");
            menuProp = serializedObject.FindProperty("menu");
            paramProp = serializedObject.FindProperty("param");
            controllerDefProp = serializedObject.FindProperty("controllerDef");
            menuDefProp = serializedObject.FindProperty("menuDef");
            paramDefProp = serializedObject.FindProperty("paramDef");
            IKUSIA_emoteProp = serializedObject.FindProperty("IKUSIA_emote");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            GUIStyle boxStyle = new(GUI.skin.box)
            {
                fontSize = 12,
                alignment = TextAnchor.UpperLeft,
                padding = new RectOffset(10, 10, 10, 10),
            };

            GUILayout.Box(
                "選択無しで実行するだけで不要な[パラメーター2bit]と[オブジェクト]\n"
                    + "が削除されcheckを入れることで該当項目が、削除されます\n"
                    + "不要な[networkSyncedパラメーター67bit]の同期checkを外します\n"
                    + "括弧内の数字は削除されるパラメーターの容量になります\n"
                    + "＊＊＊ツールには、元に戻す機能はありません＊＊＊\n",
                boxStyle,
                GUILayout.ExpandWidth(true),
                GUILayout.Height(95)
            );

            EditorGUILayout.PropertyField(petFlgProp, new GUIContent("ペット削除(31Bits)"));
            EditorGUILayout.PropertyField(ClothFlgProp, new GUIContent("衣装削除(7Bits)"));
            if (!ClothFlgProp.boolValue)
            {
                GUI.enabled = false;
                TailFlgProp.boolValue = false;
            }

            EditorGUILayout.PropertyField(TailFlgProp, new GUIContent("尻尾メッシュ削除"));
            GUI.enabled = true;
            EditorGUILayout.PropertyField(
                HairFlgProp,
                new GUIContent("髪毛/ヘッドフォン削除(8Bits)")
            );

            EditorGUILayout.PropertyField(TPSFlgProp, new GUIContent("TPS削除(1Bits)"));
            EditorGUILayout.PropertyField(ClairvoyanceFlgProp, new GUIContent("透視削除(1Bits)"));
            EditorGUILayout.PropertyField(
                colliderJumpFlgProp,
                new GUIContent("コライダー・ジャンプ削除")
            );
            EditorGUILayout.PropertyField(
                pictureFlgProp,
                new GUIContent("撮影ギミック削除(6Bits)")
            );
            EditorGUILayout.PropertyField(
                BreastSizeFlgProp,
                new GUIContent("バストサイズ削除(8Bits)")
            );
            EditorGUILayout.PropertyField(
                LightGunFlgProp,
                new GUIContent("ライトガン削除(13Bits)")
            );
            EditorGUILayout.PropertyField(
                WhiteBreathFlgProp,
                new GUIContent("ホワイトブレス削除(1Bits)")
            );
            EditorGUILayout.PropertyField(
                BubbleBreathFlgProp,
                new GUIContent("バブルブレス削除(1Bits)")
            );
            EditorGUILayout.PropertyField(
                WaterStampFlgProp,
                new GUIContent("ウォータースタンプ削除(1Bits)")
            );
            EditorGUILayout.PropertyField(eightBitFlgProp, new GUIContent("8bit削除(1Bits)"));
            EditorGUILayout.PropertyField(PenCtrlFlgProp, new GUIContent("ペン操作削除(5Bits)"));
            EditorGUILayout.PropertyField(HeartGunFlgProp, new GUIContent("ハートガン削除(1Bits)"));
            EditorGUILayout.PropertyField(
                FaceGestureFlgProp,
                new GUIContent("デフォルトの表情プリセット削除(faceEmoなど使う場合)")
            );
            EditorGUILayout.PropertyField(FaceLockFlgProp, new GUIContent("顔lock"));
            EditorGUILayout.PropertyField(FaceValFlgProp, new GUIContent("顔差分"));
            EditorGUILayout.PropertyField(
                blinkFlgProp,
                new GUIContent("まばたきをメニューから削除して常にON")
            );
            EditorGUILayout.PropertyField(
                nadeFlgProp,
                new GUIContent("なでギミックをメニューから削除して常にON")
            );
            EditorGUILayout.PropertyField(
                kamitukiFlgProp,
                new GUIContent("噛みつきをメニューから削除して常にON")
            );
            EditorGUILayout.PropertyField(
                IKUSIA_emoteProp,
                new GUIContent("IKUSIA_emoteをメニューのみ削除")
            );
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
            EditorGUILayout.PropertyField(controllerDefProp, new GUIContent("Animator Controller"));
            EditorGUILayout.PropertyField(menuDefProp, new GUIContent("Expressions Menu"));
            EditorGUILayout.PropertyField(paramDefProp, new GUIContent("Expression Parameters"));
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
            EditorGUILayout.PropertyField(controllerProp, new GUIContent("Animator Controller"));
            EditorGUILayout.PropertyField(menuProp, new GUIContent("Expressions Menu"));
            EditorGUILayout.PropertyField(paramProp, new GUIContent("Expression Parameters"));

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
