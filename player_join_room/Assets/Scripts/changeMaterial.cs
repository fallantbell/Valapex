using UnityEngine;
using System.Collections;
 
/// <summary>
/// 残影效果脚本
/// </summary>
public class changeMaterial : MonoBehaviour
{
    // 设置残影的颜色
    public Color shadowColor;
 
    //残影持续时间
    public float duration = 0.0001f;
    //符合要求后，多少时间间隔创建线的残影
    public float interval = 0.1f;
 
    //模型所拥有的网格数据
    SkinnedMeshRenderer[] meshRender;
 
    // 使用 X-ray shader
    Shader ghostShader;
 
    void Start()
    {
        shadowColor=new Color(
            r: 1f,
            g: 0f,
            b: 1f,
            a: 1
        ); 
        // 获取模型身上所有的 SkinnedMeshRenderer
        meshRender = this.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        // 并获取 X-ray shader
        ghostShader = Resources.Load<Shader>("Xray");
    }
 
    // 计时，和位置参数
    // private float lastTime = 0;
    // private Vector3 lastPos = Vector3.zero;
 
    void Update()
    {
        
 
        //人物有位移才创建残影，不然不创建
        // if (lastPos == this.transform.position)
        // {
        //     return;
        // }
 
        // lastPos = this.transform.position;
 
        // 更新时间在满足间隔的时间创建
        // if (Time.time - lastTime < interval)
        // {
        //     return;
        // }
        // lastTime = Time.time;
 
        // 如果没有 SkinnedMeshRenderer 返回，不必创建残影
        if (meshRender == null) {
 
            return;
        }
 
        // 创建所有 SkinnedMeshRenderer 的残影
        for (int i = 0; i < meshRender.Length; i++)
        {
            // 创建 Mesh ,并烘焙 SkinnedMeshRenderer 的mesh(网格数据)
            Mesh mesh = new Mesh();
            meshRender[i].BakeMesh(mesh);
 
            // 创建物体，并设置不在场景中显示
            GameObject go = new GameObject();
            go.hideFlags = HideFlags.HideAndDontSave;
 
            // 在物体上挂载定时销毁脚本，设置销毁时间
            GhostItem item = go.AddComponent<GhostItem>();
            item.duration = duration;
            item.deleteTime = Time.time + duration;
 
            // 在物体上添加 MeshFilter 组件，并网格数据给MeshFilter
            MeshFilter filter = go.AddComponent<MeshFilter>();
            filter.mesh = mesh;
 
            // 在物体上添加 MeshRenderer，物体赋值上对应的材质，并把 X_ray shader挂载，并设置shader参数
            MeshRenderer meshRen = go.AddComponent<MeshRenderer>();
            meshRen.material = meshRender[i].material;
            meshRen.material.shader = ghostShader;
            meshRen.material.SetFloat("_Pow", 2.0f);
            shadowColor.a = 1;
            meshRen.material.SetColor("_GhostColor", shadowColor);
 
            // 设置物体的位置旋转和比例，为对应 SkinnedMeshRenderer 物体的位置旋转和比例
            go.transform.localScale = meshRender[i].transform.localScale;
            go.transform.position = meshRender[i].transform.position;
            go.transform.rotation = meshRender[i].transform.rotation;
 
            // 给 GhostItem  meshRenderer 参数赋值
            item.meshRenderer = meshRen;
            
        }
    }
}
