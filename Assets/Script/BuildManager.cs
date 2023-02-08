using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public TurretData Tanker1;
    public TurretData Tanker2;
    public TurretData Tanker3;

    private TurretData selectTanker;
    private MapCube selectMapCube;
    private int money = 1000;
    public Text moneyText;
    public Animator moneyAnimator;

    public GameObject buttonUpgradeCanves;
    public Button buttonUpgrade;
    private Animator upgradeCanvasAnimator;

    void ChangeMoney(int change = 0)
    {
        money += change;
        moneyText.text = "￥" + money;
    }
    void Start()
    {
        upgradeCanvasAnimator = buttonUpgradeCanves.GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {                 
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();//取到所点击的对象
                    if (selectTanker != null && mapCube.tankerGo == null)
                    {                    
                        if (money > selectTanker.cost)
                        {     
                            //建造炮台
                            ChangeMoney(-selectTanker.cost);
                            mapCube.BuildTanker(selectTanker);
                        }
                        else
                        {
                            //钱不够
                            moneyAnimator.SetTrigger("flash");
                        }
                    }
                    else if(mapCube.tankerGo != null)  
                    {
                        //升级炮台
                        if (selectMapCube == mapCube&&buttonUpgradeCanves.activeInHierarchy)
                        {
                            StartCoroutine(HideUpgradeUI());
                        }
                        else
                        {
                            ShowUpgradeUI(mapCube.transform.position, mapCube.isUpgaded);
                        }
                        selectMapCube = mapCube;
                    }

                }
            }
        }
    }
    public void OnTanker1(bool isOn)
    {
        if (isOn)
        {
            selectTanker = Tanker1;
        }
    }
    public void OnTanker2(bool isOn)
    {    
        if (isOn)
        {
            selectTanker = Tanker2;
        }
    }
    public void OnTanker3(bool isOn)
    {
        if (isOn)
        {
            selectTanker = Tanker3;
        }
    }
    void ShowUpgradeUI(Vector3 pos, bool isDisableUpgade=false)
    {
        StopCoroutine("HideUpgradeUI");
        buttonUpgradeCanves.SetActive(false);
        buttonUpgradeCanves.SetActive(true);
        pos.y += 3.49f;
        buttonUpgradeCanves.transform.position = pos;
        buttonUpgrade.interactable = !isDisableUpgade;
    } 
    IEnumerator HideUpgradeUI()
    {
        upgradeCanvasAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(0.8f);
        buttonUpgradeCanves.SetActive(false);
    }
    public void OnUpgradeButtonDown()
    {
        if (money >= selectMapCube.turretData.costUpgrade)
        {
            ChangeMoney(-selectMapCube.turretData.costUpgrade);
            selectMapCube.UpgradeTanker();
        }
        else
        {
            moneyAnimator.SetTrigger("flash");
        }
        StartCoroutine(HideUpgradeUI());
    }
    public void OnDestroyButtonDown()
    {
        selectMapCube.DestroyTanker();
        StartCoroutine(HideUpgradeUI());
    }
}
