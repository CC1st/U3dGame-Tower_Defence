using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour
{
    [HideInInspector]
    public GameObject tankerGo;
    [HideInInspector]
    public TurretData turretData;
    public GameObject buildEffect;
    public GameObject destroyEffect;
    private Renderer renderer;
    [HideInInspector]
    public bool isUpgaded = false;
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public void BuildTanker(TurretData turretData)
    {
        this.turretData = turretData;
        isUpgaded = false;
        tankerGo = GameObject.Instantiate(turretData.turretPrefab, transform.position+new Vector3(0,0.5f,0), Quaternion.identity);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        Destroy(effect, 1.5f);
    }

    public void UpgradeTanker()
    {
        if (isUpgaded == true) return;        
        Destroy(tankerGo);
        isUpgaded = true;
        tankerGo = GameObject.Instantiate(turretData.turretUpgradePrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        Destroy(effect, 1.5f);      
    }

    public void DestroyTanker()
    {
        Destroy(tankerGo);
        isUpgaded = false;
        turretData = null;
        tankerGo = null;
        GameObject effect = GameObject.Instantiate(destroyEffect, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        Destroy(effect, 1.5f);
    }
    void OnMouseEnter()
    {
        if (tankerGo == null && EventSystem.current.IsPointerOverGameObject()==false)
        {
            renderer.material.color = Color.red;
        }
    }
    void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }
}
