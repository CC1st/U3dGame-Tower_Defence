using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankerControl : MonoBehaviour
{
    private List<GameObject> enemys = new List<GameObject>();
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemys.Add(col.gameObject);
        }
    }
    void OnTriggerExit(Collider col)
    {
        enemys.Remove(col.gameObject);
    }

    public float attackRateTime = 1;
    private float timer = 0;
    public GameObject bulletPrefab;
    public Transform firePosition;
    private int i;
    public Transform head;
    public bool isMisso = false;
    public int damage = 70;
    public LineRenderer laserRender;
    public GameObject laserEffect;

    void Start()
    {
        timer = attackRateTime;
        i = 0;
    }
    void Update()
    {
        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = head.position.y;
            head.LookAt(targetPosition);
        }
        if (isMisso == false)
        {
            timer += Time.deltaTime;
            if (enemys.Count > 0 && timer >= attackRateTime)
            {
                timer = 0;
                Attack();
            }
        }
        else if(enemys.Count>0)
        {
            if (laserRender.enabled == false)
            {
                laserRender.enabled = true;
                laserEffect.SetActive(true);

            }
            if (enemys[0] == null)
            {
                UpdateEnemys();
            }
            if (enemys.Count > 0)
            {
                laserRender.SetPositions(new Vector3[] { firePosition.position, enemys[0].transform.position });
                enemys[0].GetComponent<Enemy>().TakeDamage(damage * Time.deltaTime);
                laserEffect.transform.position = enemys[0].transform.position;
                Vector3 pos = transform.position;
                pos.y = enemys[0].transform.position.y;
                laserEffect.transform.LookAt(pos);
            }
        }
        else
        {
            laserRender.enabled = false;
            laserEffect.SetActive(false);
        }
    }
    void Attack()
    {
        if (enemys[0] == null)
        {
            UpdateEnemys();           
        }
        if (enemys.Count > 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            bullet.GetComponent<Bullet>().SetTarget(enemys[i].transform);
        }
        else
        {
            timer = attackRateTime;
        }
    }

    void UpdateEnemys()
    {
        List<int> emptyIndex = new List<int>();
        for(int index = 0; index < enemys.Count; index++)
        {
            if (enemys[index] == null)
            {
                emptyIndex.Add(index);
            }
        }
        for(int i = 0; i < emptyIndex.Count; i++)
        {
            enemys.RemoveAt(emptyIndex[i]-i);
        }
    }
}
