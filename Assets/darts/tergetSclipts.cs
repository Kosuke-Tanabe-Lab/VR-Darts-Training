using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;

public class DisplayCollisionDistance : MonoBehaviour
{
    public TextMeshPro textMeshPro; // QuadのTextMeshProコンポーネント
    public Transform targetObject; // 衝突を認識する対象オブジェクト
    public Transform childObject; // 衝突されるオブジェクトの子オブジェクト
    public InputActionReference leftButtonAction; // 左側ボタンのInputActionの参照
    public InputActionReference rightButtonAction; // 右側ボタンのInputActionの参照
    public float longPressDuration = 2f; // 長押しの持続時間（秒）
    public int frameDelay = 15; // リセット後の遅延フレーム数

    private Transform collidedObject; // 衝突したオブジェクト
    private bool collisionOccurred = false; // 衝突が発生したかどうか
    private bool isPressing = false;
    private float pressTime = 0f;

    // 初期状態を保持するための変数
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;
    private string initialText;

    private void Start()
    {
        // 初期状態を記録
        initialPosition = targetObject.position;
        initialRotation = targetObject.rotation;
        initialScale = targetObject.localScale;
        initialText = textMeshPro.text;
    }

    private void OnEnable()
    {
        leftButtonAction.action.performed += OnButtonPressed;
        leftButtonAction.action.canceled += OnButtonReleased;
        rightButtonAction.action.performed += OnButtonPressed;
        rightButtonAction.action.canceled += OnButtonReleased;
    }

    private void OnDisable()
    {
        leftButtonAction.action.performed -= OnButtonPressed;
        leftButtonAction.action.canceled -= OnButtonReleased;
        rightButtonAction.action.performed -= OnButtonPressed;
        rightButtonAction.action.canceled -= OnButtonReleased;
    }

    void Update()
    {
        if (isPressing)
        {
            pressTime += Time.deltaTime;
            if (pressTime >= longPressDuration)
            {
                ResetState();
                isPressing = false;
                pressTime = 0f;
            }
        }

        if (collisionOccurred && collidedObject != null && childObject != null)
        {
            // 子オブジェクトと衝突したオブジェクトのyz平面上の位置を取得
            Vector3 childPosition = new Vector3(0, childObject.position.y, childObject.position.z);
            Vector3 collidedPosition = new Vector3(0, collidedObject.position.y, collidedObject.position.z);

            // yz平面上の距離を計算
            float distanceInMeters = Vector3.Distance(childPosition, collidedPosition);
            float distanceInCentimeters = distanceInMeters * 100; // メートルをセンチメートルに変換

            // 小数第一位を四捨五入
            float roundedDistance = Mathf.Round(distanceInCentimeters * 10) / 10;

            // TextMeshProに距離を表示（センチメートル単位）
            textMeshPro.text = "Distance: " + roundedDistance.ToString("F1") + " cm";
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 衝突が発生した際の処理
        if (collision.transform == targetObject)
        {
            collidedObject = collision.transform;
            collisionOccurred = true;

            // targetObjectのすべてのコンポーネントを無効化（Rendererを除く）
            Component[] components = targetObject.GetComponents<Component>();
            foreach (Component component in components)
            {
                if (component is Behaviour && !(component is Renderer))
                {
                    ((Behaviour)component).enabled = false;
                }
                else if (component is Collider)
                {
                    ((Collider)component).enabled = false;
                }
                else if (component is Rigidbody)
                {
                    Rigidbody rb = (Rigidbody)component;
                    rb.isKinematic = true;
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
        }
    }

    private void OnButtonPressed(InputAction.CallbackContext context)
    {
        isPressing = true;
        pressTime = 0f;
    }

    private void OnButtonReleased(InputAction.CallbackContext context)
    {
        isPressing = false;
        pressTime = 0f;
    }

    private void ResetState()
    {
        // targetObjectを初期状態にリセット
        targetObject.position = initialPosition;
        targetObject.rotation = initialRotation;
        targetObject.localScale = initialScale;

        // TextMeshProを初期状態にリセット
        textMeshPro.text = initialText;

        // 衝突フラグをリセット
        collisionOccurred = false;

        // targetObjectのコンポーネントを再度有効化する前に遅延を追加
        StartCoroutine(ReenableComponentsWithDelay());
    }

    private IEnumerator ReenableComponentsWithDelay()
    {
        // フレームカウンターを設定して遅延を待機
        for (int i = 0; i < frameDelay; i++)
        {
            yield return null; // 1フレーム待つ
        }

        // targetObjectのコンポーネントを再度有効化
        Component[] components = targetObject.GetComponents<Component>();
        foreach (Component component in components)
        {
            if (component is Behaviour && !(component is Renderer))
            {
                ((Behaviour)component).enabled = true;
            }
            else if (component is Collider)
            {
                ((Collider)component).enabled = true;
            }
            else if (component is Rigidbody)
            {
                ((Rigidbody)component).isKinematic = false;
            }
        }
    }
}
