using UnityEngine;

public class BoostInXAxis : MonoBehaviour
{
    public float boostFactor = 1.0f; // ブーストの係数

    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        // グローバルのX軸プラス方向にブーストを適用
        Vector3 boost = new Vector3(boostFactor, 0, 0);

        // 力を追加
        rb.AddForce(boost, ForceMode.Acceleration);
    }
}
