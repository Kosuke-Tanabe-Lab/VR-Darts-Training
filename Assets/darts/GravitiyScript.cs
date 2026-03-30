using UnityEngine;

public class GravityScript : MonoBehaviour
{
    // 重力を調整可能にするためにpublic変数にする
    public Vector3 gravity = new Vector3(0, -9.81f, 0);

    void FixedUpdate()
    {
        // 重力を適用する
        GetComponent<Rigidbody>().AddForce(gravity * GetComponent<Rigidbody>().mass);
    }
}
