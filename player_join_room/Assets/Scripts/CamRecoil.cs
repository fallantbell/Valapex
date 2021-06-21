using UnityEngine;
using System.Collections;

public class CamRecoil : MonoBehaviour
{
    [Header("Recoil_Transform")]
    public Transform RecoilRotationTransform;
   
    [Space(10)]
    [Header("Recoil_Settings")]
    public float RotationDampTime;
    [Space(10)]
    public float Recoil1;
    
    [Space(10)]
    public Vector3 RecoilRotation;
    public Vector3 RecoilKickBack;

    public Vector3 RecoilRotation_Aim;
    public Vector3 RecoilKickBack_Aim;
    [Space(10)]
    public Vector3 CurrentRecoil1;

    public bool aim;
    private Quaternion initialRotation;
    private GunSystem gun;
    private IEnumerator WaitForSwitch(){
        yield return new WaitForSeconds(0.5f);
        gun = GetComponentInParent<GunSystem>();
        Recoil1 = gun.recoilAmount;
    }
    void Update(){
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
            StartCoroutine(WaitForSwitch());
    }
    void FixedUpdate()
    {
        CurrentRecoil1 = Vector3.Lerp(CurrentRecoil1, Vector3.zero, Recoil1 * Time.deltaTime);
        if(gun.shooting)
            RecoilRotationTransform.localRotation = Quaternion.Slerp(RecoilRotationTransform.localRotation, 
                                                    Quaternion.Euler(RecoilRotationTransform.localRotation.eulerAngles + CurrentRecoil1), RotationDampTime * Time.deltaTime);
        else
            RecoilRotationTransform.localRotation = Quaternion.Slerp(RecoilRotationTransform.localRotation, 
                                                    initialRotation, RotationDampTime * Time.deltaTime);
        
    }
    public void Fire()
    {
        if (aim == true)
        {
            CurrentRecoil1 += new Vector3(RecoilRotation_Aim.x, Random.Range(-RecoilRotation_Aim.y, RecoilRotation_Aim.y), Random.Range(-RecoilRotation_Aim.z, RecoilRotation_Aim.z));
        }
        if (aim == false)
        {
            CurrentRecoil1 += new Vector3(RecoilRotation.x, Random.Range(-RecoilRotation.y, RecoilRotation.y), Random.Range(-RecoilRotation.z, RecoilRotation.z));
        }
    }
    void Awake(){
        initialRotation = RecoilRotationTransform.localRotation;
        CurrentRecoil1 = initialRotation.eulerAngles;
        
    }
    void Start(){
        gun = GetComponentInParent<GunSystem>();
    }
    
}