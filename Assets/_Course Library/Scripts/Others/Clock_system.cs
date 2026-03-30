using System; // DateTimeクラスを使用するための名前空間をインポート
using System.Collections; // コレクションを使用するための名前空間をインポート
using System.Collections.Generic; // ジェネリックコレクションを使用するための名前空間をインポート
using UnityEngine; // Unityの基本機能を使用するための名前空間をインポート

public class Clock_system : MonoBehaviour // Clock_systemクラスを定義し、MonoBehaviourを継承
{
    public bool sec;   // 秒針の有無を示すフラグ
    public bool secTick;   // 秒針を秒ごとに動かすかを示すフラグ

    public GameObject hour; // 時針のゲームオブジェクト
    public GameObject minute; // 分針のゲームオブジェクト
    public GameObject second; // 秒針のゲームオブジェクト

    void Start() // 初期化処理を行うStartメソッド
    {
        if (!sec) // 秒針が不要な場合
            Destroy(second); // 秒針のゲームオブジェクトを破壊
    }

    void Update() // 毎フレーム呼び出されるUpdateメソッド
    {
        DateTime dt = DateTime.Now; // 現在の日時を取得

        hour.transform.eulerAngles = new Vector3(-(float)dt.Hour / 12 * -360 + -(float)dt.Minute / 60 * -30, 0,0); // 時針の回転角度を設定
        minute.transform.eulerAngles = new Vector3(-(float)dt.Minute / 60 * -360, 0,0); // 分針の回転角度を設定
        if (sec) // 秒針が必要な場合
        {
            if (secTick) // 秒針を秒ごとに動かす場合
                second.transform.eulerAngles = new Vector3(-(float)dt.Second / 60 * -360, 0,0); // 秒針の回転角度を設定
            else // 秒針を滑らかに動かす場合
                second.transform.eulerAngles = new Vector3(-(float)dt.Second / 60 * -360 + -(float)dt.Millisecond / 60 / 1000 * -360, 0,0); // 秒針の回転角度を設定
        }
    }
}
