// RotationModifierPlatform.cs
using UnityEngine;

public class RotationModifierPlatform : MonoBehaviour
{
    // チェックボックスで台のタイプを指定
    [Tooltip("この台がダーツの子の回転を反転させる台の場合にチェック")]
    public bool isInvertPlatform = false;

    [Tooltip("この台がダーツの子の回転をリセットする台の場合にチェック")]
    public bool isResetPlatform = false;

    void Awake()
    {
        // Inspectorでの設定が不正でないかチェック
        if (isInvertPlatform && isResetPlatform)
        {
             Debug.LogError("ERROR: RotationModifierPlatform on '" + gameObject.name + "' has both isInvertPlatform and isResetPlatform checked. Please select only one type for the platform.");
        }
         else if (!isInvertPlatform && !isResetPlatform)
         {
             Debug.LogWarning("WARNING: RotationModifierPlatform on '" + gameObject.name + "' has neither isInvertPlatform nor isResetPlatform checked. This platform will not modify rotation.");
         }
    }


    // 衝突時に呼ばれるメソッド (ColliderがIsTrigger=falseの場合)
    void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクト（ダーツの矢ルートオブジェクト "dart" を想定）から DartRotationController スクリプトを探す
        // 衝突したGameObject自体に DartRotationController がアタッチされているか確認
        DartRotationController dartController = collision.gameObject.GetComponent<DartRotationController>();

        // もし衝突相手が子オブジェクトで、親（"dart"）に DartRotationController がついているなら、親を取得してGetComponentでも良い
        // 今回は衝突するのはルートオブジェクト "dart" という前提なので、最初のGetComponentで十分なはずです。
        // if (dartController == null && collision.gameObject.transform.parent != null)
        // {
        //      dartController = collision.gameObject.transform.parent.GetComponent<DartRotationController>();
        // }


        if (dartController != null)
        {
            // 衝突したのが DartRotationController を持つオブジェクト（ダーツの矢ルート "dart"）だった場合
            Debug.Log($"DEBUG: Platform '{gameObject.name}' collided with Dart '{collision.gameObject.name}'.");

            // 台のタイプ（チェックボックス）に応じてダーツの回転コントローラースクリプトのメソッドを呼び出す
            if (isInvertPlatform && !isResetPlatform) // 反転台の場合
            {
                Debug.Log($"DEBUG: Platform '{gameObject.name}' is an Invert Platform. Calling InvertRightHandAttachYZRotation().");
                // ここで呼び出すメソッド名を変更
                dartController.InvertRightHandAttachYZRotation();
            }
            else if (isResetPlatform && !isInvertPlatform) // リセット台の場合
            {
                 Debug.Log($"DEBUG: Platform '{gameObject.name}' is a Reset Platform. Calling ResetRightHandAttachRotation().");
                 // ここで呼び出すメソッド名は変更なし
                dartController.ResetRightHandAttachRotation();
            }
             else if (isInvertPlatform && isResetPlatform) // 両方にチェックが入っている場合はエラー表示（Awakeでもチェックしているが念のため）
            {
                 Debug.LogError("ERROR: RotationModifierPlatform on '" + gameObject.name + "' has both isInvertPlatform and isResetPlatform checked. Please select only one.");
            }
             else // どちらにもチェックが入っていない場合
             {
                 Debug.LogWarning("WARNING: RotationModifierPlatform on '" + gameObject.name + "' is configured as neither Invert nor Reset. No action taken.");
             }
        }
         else
         {
             // 衝突したオブジェクトが DartRotationController を持っていない場合
              Debug.Log($"DEBUG: Platform '{gameObject.name}' collided with an object ('{collision.gameObject.name}') that does not have DartRotationController.");
         }
    }

     // トリガー進入時に呼ばれるメソッド (ColliderがIsTrigger=trueの場合)
     /*
    void OnTriggerEnter(Collider other)
    {
        // 衝突したオブジェクト（ダーツの矢ルートオブジェクト "dart" を想定）から DartRotationController スクリプトを探す
        DartRotationController dartController = other.GetComponent<DartRotationController>();

        // もし衝突相手がダーツの子オブジェクトで、親（"dart"）にDartRotationControllerがついているなら、親を取得してGetComponentでも良い
        // if (dartController == null && other.transform.parent != null)
        // {
        //      dartController = other.transform.parent.GetComponent<DartRotationController>();
        // }

        if (dartController != null)
        {
            Debug.Log($"DEBUG: Platform '{gameObject.name}' triggered with Dart '{other.gameObject.name}'.");

            if (isInvertPlatform && !isResetPlatform) // 反転台の場合
            {
                Debug.Log($"DEBUG: Platform '{gameObject.name}' is an Invert Platform. Calling InvertRightHandAttachYZRotation().");
                 // ここで呼び出すメソッド名を変更
                dartController.InvertRightHandAttachYZRotation();
            }
            else if (isResetPlatform && !isInvertPlatform) // リセット台の場合
            {
                 Debug.Log($"DEBUG: Platform '{gameObject.name}' is a Reset Platform. Calling ResetRightHandAttachRotation().");
                 // ここで呼び出すメソッド名は変更なし
                dartController.ResetRightHandAttachRotation();
            }
             else if (isInvertPlatform && isResetPlatform) // 両方にチェックが入っている場合はエラー表示
            {
                 Debug.LogError("ERROR: RotationModifierPlatform on '" + gameObject.name + "' has both isInvertPlatform and isResetPlatform checked. Please select only one.");
            }
             else // どちらにもチェックが入っていない場合
             {
                 Debug.LogWarning("WARNING: RotationModifierPlatform on '" + gameObject.name + "' is configured as neither Invert nor Reset. No action taken.");
             }
        }
        else
         {
              Debug.Log($"DEBUG: Platform '{gameObject.name}' triggered with an object ('{other.gameObject.name}') that does not have DartRotationController.");
         }
    }
    */
}