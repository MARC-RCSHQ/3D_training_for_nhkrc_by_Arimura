using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basement_behavior : MonoBehaviour
{
    // Start is called before the first frame update

    void Start () {
      // 現在使用されているマテリアルを取得
      Material mat = this.GetComponent<Renderer>().material;
      // マテリアルの色設定に赤色を設定
      mat.color = new Color(0.0f,0.5f,0.5f,0.8f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
