using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components; // VRCAvatarDescriptor を使用するため

namespace jp.illusive_isc.RuruneOptimizer
{
    [CustomEditor(typeof(IllRuruneOptimizer))]
    [AddComponentMenu("")]
    internal class IllRuruneOptimizerEditor : Editor
    {
        // 各シリアライズプロパティを保持する変数
        SerializedProperty petFlgProp;
        SerializedProperty ClothFlgProp;
        SerializedProperty HairFlgProp;
        SerializedProperty TailFlgProp;
        SerializedProperty ClothDelFlgProp;
        SerializedProperty HairDelFlgProp;
        SerializedProperty HeadphoneFlgProp;
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
        SerializedProperty controllerProp;
        SerializedProperty menuProp;
        SerializedProperty paramProp;
        SerializedProperty controllerDefProp;
        SerializedProperty menuDefProp;
        SerializedProperty paramDefProp;

        private void OnEnable()
        {
            // フィールド名は元のクラスの変数名と一致させる
            petFlgProp = serializedObject.FindProperty("petFlg");
            ClothFlgProp = serializedObject.FindProperty("ClothFlg");
            HairFlgProp = serializedObject.FindProperty("HairFlg");
            ClothDelFlgProp = serializedObject.FindProperty("ClothDelFlg");
            HairDelFlgProp = serializedObject.FindProperty("HairDelFlg");
            TailFlgProp = serializedObject.FindProperty("TailFlg");
            HeadphoneFlgProp = serializedObject.FindProperty("HeadphoneFlg");
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
            controllerProp = serializedObject.FindProperty("controller");
            menuProp = serializedObject.FindProperty("menu");
            paramProp = serializedObject.FindProperty("param");
            controllerDefProp = serializedObject.FindProperty("controllerDef");
            menuDefProp = serializedObject.FindProperty("menuDef");
            paramDefProp = serializedObject.FindProperty("paramDef");
        }

        public override void OnInspectorGUI()
        {
            // シリアライズオブジェクトの更新
            serializedObject.Update();

            // 各フィールドをカスタムラベル付きで表示
            EditorGUILayout.PropertyField(petFlgProp, new GUIContent("ペット削除"));
            EditorGUILayout.PropertyField(ClothFlgProp, new GUIContent("衣装削除"));
            if (!ClothFlgProp.boolValue)
            {
                GUI.enabled = false;
                ClothDelFlgProp.boolValue = false;
            }

            EditorGUILayout.PropertyField(
                ClothDelFlgProp,
                new GUIContent("衣装のメッシュを取り除く")
            );
            EditorGUILayout.PropertyField(TailFlgProp, new GUIContent("尻尾メッシュ削除"));
            GUI.enabled = true;
            EditorGUILayout.PropertyField(HairFlgProp, new GUIContent("髪毛削除"));
            if (!HairFlgProp.boolValue)
            {
                GUI.enabled = false;
                HairDelFlgProp.boolValue = false;
            }

            EditorGUILayout.PropertyField(
                HairDelFlgProp,
                new GUIContent("髪毛のメッシュを取り除く")
            );
            GUI.enabled = true;
            EditorGUILayout.PropertyField(HeadphoneFlgProp, new GUIContent("ヘッドフォン削除"));
            EditorGUILayout.PropertyField(TPSFlgProp, new GUIContent("TPS削除"));
            EditorGUILayout.PropertyField(ClairvoyanceFlgProp, new GUIContent("透視削除"));
            EditorGUILayout.PropertyField(
                colliderJumpFlgProp,
                new GUIContent("コライダー・ジャンプ削除")
            );
            EditorGUILayout.PropertyField(pictureFlgProp, new GUIContent("撮影ギミック削除"));
            EditorGUILayout.PropertyField(BreastSizeFlgProp, new GUIContent("バストサイズ削除"));
            EditorGUILayout.PropertyField(LightGunFlgProp, new GUIContent("ライトガン削除"));
            EditorGUILayout.PropertyField(WhiteBreathFlgProp, new GUIContent("ホワイトブレス削除"));
            EditorGUILayout.PropertyField(BubbleBreathFlgProp, new GUIContent("バブルブレス削除"));
            EditorGUILayout.PropertyField(
                WaterStampFlgProp,
                new GUIContent("ウォータースタンプ削除")
            );
            EditorGUILayout.PropertyField(eightBitFlgProp, new GUIContent("8bit削除"));
            EditorGUILayout.PropertyField(PenCtrlFlgProp, new GUIContent("ペン操作削除"));
            EditorGUILayout.PropertyField(HeartGunFlgProp, new GUIContent("ハートガン削除"));

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
