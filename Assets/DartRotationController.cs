// DartRotationController.cs
using UnityEngine;

public class DartRotationController : MonoBehaviour
{
    // Inspectorから回転を操作したい子オブジェクト（RightHandAttach）を設定します
    [Tooltip("回転を操作したいダーツの子オブジェクト（通常は RightHandAttach のような名前）を設定してください")]
    public Transform rightHandAttachTransform;

    private Quaternion originalRightHandAttachLocalRotation; // RightHandAttachの反転前の元のローカル回転を保存
    private bool isRightHandAttachRotationInvertedYZ = false; // RightHandAttachのY軸とZ軸が現在反転しているかを示すフラグ

    void Awake()
    {
        // RightHandAttachTransformがInspectorで設定されているか確認
        if (rightHandAttachTransform == null)
        {
            Debug.LogError("ERROR: RightHandAttachTransform is not assigned on Dart object '" + gameObject.name + "'!");
            // オブジェクト名で子オブジェクトを探す場合は以下のコードを使いますが、
            // Inspector設定の方がオブジェクト名変更に強く推奨です。
            // Transform foundChild = transform.Find("RightHandAttach"); // 例: 子オブジェクト名が RightHandAttach の場合
            // if (foundChild != null)
            // {
            //     rightHandAttachTransform = foundChild;
            //     Debug.LogWarning("RightHandAttachTransform found by name on '" + gameObject.name + "'. Consider assigning in Inspector for robustness.");
            // }
            // else
            // {
            //      Debug.LogError("ERROR: Child object for RightHandAttachTransform not found on '" + gameObject.name + "'. Please assign in Inspector or check child name.");
            // }
             return; // 参照が設定されていない場合は以降の処理を行わない
        }

        // ゲームオブジェクトが生成された時点の子オブジェクトのローカル回転を元の回転として保存します。
        SaveOriginalRightHandAttachRotation();
    }

    // 子オブジェクト（RightHandAttach）の元のローカル回転を保存するメソッド
    public void SaveOriginalRightHandAttachRotation()
    {
         // rightHandAttachTransform が null の場合は何もしない
        if (rightHandAttachTransform == null) return;

        originalRightHandAttachLocalRotation = rightHandAttachTransform.localRotation;
        isRightHandAttachRotationInvertedYZ = false; // 元の状態なので反転フラグはfalse
        Debug.Log($"DEBUG: '{gameObject.name}' (controlling '{rightHandAttachTransform.name}'): Original Rotation Saved: {originalRightHandAttachLocalRotation.eulerAngles}");
    }

    // 子オブジェクト（RightHandAttach）のY軸とZ軸のローカル回転を反転させるメソッド
    public void InvertRightHandAttachYZRotation()
    {
        // rightHandAttachTransform が null の場合は何もしない
        if (rightHandAttachTransform == null) return;

        if (!isRightHandAttachRotationInvertedYZ)
        {
            // 子オブジェクトの現在のローカルオイラー角を取得
            Vector3 currentEulerAngles = rightHandAttachTransform.localEulerAngles;

            // Y軸とZ軸の角度を符号反転させる
            float invertedY = -currentEulerAngles.y;
            float invertedZ = -currentEulerAngles.z;

            // 必要に応じて0～360度の範囲に調整
            if (invertedY < 0) invertedY += 360;
            if (invertedY >= 360) invertedY -= 360;
            if (invertedZ < 0) invertedZ += 360;
            if (invertedZ >= 360) invertedZ -= 360;


            // 新しいローカルオイラー角を設定 (X軸はそのまま)
            rightHandAttachTransform.localEulerAngles = new Vector3(currentEulerAngles.x, invertedY, invertedZ);

            isRightHandAttachRotationInvertedYZ = true; // 反転状態になったことを記録
            Debug.Log($"DEBUG: '{gameObject.name}' (controlling '{rightHandAttachTransform.name}'): YZ Rotation Inverted to: {rightHandAttachTransform.localEulerAngles}");
        }
        else
        {
            Debug.Log($"DEBUG: '{gameObject.name}' (controlling '{rightHandAttachTransform.name}'): YZ Rotation is already inverted. No change made.");
        }
    }

    // 子オブジェクト（RightHandAttach）の回転を元の状態にリセットするメソッド
    public void ResetRightHandAttachRotation()
    {
        // rightHandAttachTransform が null の場合は何もしない
        if (rightHandAttachTransform == null) return;

        if (isRightHandAttachRotationInvertedYZ)
        {
            // 保存しておいた子オブジェクトの元のローカル回転に戻す
            transform.localRotation = originalRightHandAttachLocalRotation;

            isRightHandAttachRotationInvertedYZ = false; // 反転状態を解除
            Debug.Log($"DEBUG: '{gameObject.name}' (controlling '{rightHandAttachTransform.name}'): Rotation Reset to: {transform.localRotation.eulerAngles}");
        }
        else
        {
            Debug.Log($"DEBUG: '{gameObject.name}' (controlling '{rightHandAttachTransform.name}'): Rotation is already in original state. No change made.");
        }
    }
}