using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Transform[] position;
    private int index=0;
    public float speed = 1;
    public GameObject dieEffectPrefab;
    private float hp;
    public Slider sliderHp;
    public float totleHp=100;
    // Start is called before the first frame update
    void Start()
    {
        position = WayPoint.position;
        hp = totleHp;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        if (index > position.Length - 1) return;
        transform.Translate((position[index].position - transform.position).normalized * Time.deltaTime * speed);
        if (Vector3.Distance(position[index].position, transform.position) < 0.2f)
        {
            index++;
        }
        if (index > position.Length - 1)
        {
            ReachDistination();
        }
    }
    //抵达终点
    void ReachDistination()
    {
        GameManager.Instance.Failed();
        GameObject.Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        EnemySpawner.countAliveEnemy--;
    }

    public void TakeDamage(float damage)
    {
        if (hp <= 0) return;
        hp -= damage;
        sliderHp.value =(float)hp / totleHp;
        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject effect = GameObject.Instantiate(dieEffectPrefab, transform.position, transform.rotation);
        Destroy(effect, 1.5f);
        Destroy(this.gameObject);
    }
}
