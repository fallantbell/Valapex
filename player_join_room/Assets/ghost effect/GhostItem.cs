using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostItem : MonoBehaviour
{
    //持续时间
    public float duration;
    //销毁时间
    public float deleteTime;
    // 物体上的 MeshRenderer ，主要是为了 动态修改材质颜色 alpha 值，产生渐隐效果
    public MeshRenderer meshRenderer;
 
    void Update()
    {
        float tempTime = deleteTime - Time.time;
        if (tempTime <= 0)
        {//到时间就销毁
            GameObject.Destroy(this.gameObject);
        }
        else if (meshRenderer.material)
        {
            // 这里根据所剩时间的比例，来产生残影渐隐的效果
            float rate = tempTime / duration;//计算生命周期的比例
            Color cal = meshRenderer.material.GetColor("_GhostColor");
            cal.a *= rate;//设置透明通道
            meshRenderer.material.SetColor("_GhostColor", cal);
        }
 
    }
}
