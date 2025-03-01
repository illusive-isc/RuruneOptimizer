using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components; // VRCAvatarDescriptor を使用するため

namespace jp.illusive_isc.RuruneOptimizer
{
    [CustomEditor(typeof(IllRuruneOptimizer))]
    public class IllRuruneOptimizerEditor : Editor
    {
        // 各シリアライズプロパティを保持する変数
        SerializedProperty petFlgProp;
        SerializedProperty ClothFlgProp;
        SerializedProperty HairFlgProp;
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

        private void OnEnable()
        {
            // フィールド名は元のクラスの変数名と一致させる
            petFlgProp = serializedObject.FindProperty("petFlg");
            ClothFlgProp = serializedObject.FindProperty("ClothFlg");
            HairFlgProp = serializedObject.FindProperty("HairFlg");
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
        }

        public override void OnInspectorGUI()
        {
            // シリアライズオブジェクトの更新
            serializedObject.Update();

            // 各フィールドをカスタムラベル付きで表示
            EditorGUILayout.PropertyField(petFlgProp, new GUIContent("ペット削除フラグ"));
            EditorGUILayout.PropertyField(ClothFlgProp, new GUIContent("衣装削除フラグ"));
            EditorGUILayout.PropertyField(HairFlgProp, new GUIContent("髪毛削除フラグ"));
            EditorGUILayout.PropertyField(TPSFlgProp, new GUIContent("TPS削除フラグ"));
            EditorGUILayout.PropertyField(ClairvoyanceFlgProp, new GUIContent("透視削除フラグ"));
            EditorGUILayout.PropertyField(
                colliderJumpFlgProp,
                new GUIContent("コライダー・ジャンプ削除フラグ")
            );
            EditorGUILayout.PropertyField(pictureFlgProp, new GUIContent("撮影ギミック削除フラグ"));
            EditorGUILayout.PropertyField(
                BreastSizeFlgProp,
                new GUIContent("バストサイズ削除フラグ")
            );
            EditorGUILayout.PropertyField(LightGunFlgProp, new GUIContent("ライトガン削除フラグ"));
            EditorGUILayout.PropertyField(
                WhiteBreathFlgProp,
                new GUIContent("ホワイトブレス削除フラグ")
            );
            EditorGUILayout.PropertyField(
                BubbleBreathFlgProp,
                new GUIContent("バブルブレス削除フラグ")
            );
            EditorGUILayout.PropertyField(
                WaterStampFlgProp,
                new GUIContent("ウォータースタンプ削除フラグ")
            );
            EditorGUILayout.PropertyField(eightBitFlgProp, new GUIContent("8bit削除フラグ"));
            EditorGUILayout.PropertyField(PenCtrlFlgProp, new GUIContent("ペン操作削除フラグ"));
            EditorGUILayout.PropertyField(HeartGunFlgProp, new GUIContent("ハートガン削除フラグ"));

            // Execute ボタンの追加
            if (GUILayout.Button("Execute"))
            {
                IllRuruneOptimizer script = (IllRuruneOptimizer)target;
                VRCAvatarDescriptor descriptor =
                    script.transform.root.GetComponent<VRCAvatarDescriptor>();
                if (descriptor != null)
                {
                    script.Execute(descriptor);
                }
                else
                {
                    Debug.LogWarning("VRCAvatarDescriptor が見つかりません。");
                }
            }
            EditorGUILayout.Space();
            // ヘッダー部分：太字、24pt、白文字のテキストを表示
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
