using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Score score;

    private void Start() {
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
    }

    public delegate void EventDelegate();
    public event EventDelegate destroyEvent;

    public void OnDestroy()
    {
        if (destroyEvent != null)
            destroyEvent();
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.transform.tag == "Bullet")
        {
            this.gameObject.GetComponent<EnemyMove>().enabled = false;
            this.gameObject.GetComponent<ParticleSystem>().Play();
            StartCoroutine(waitfordie());
        }
    }

    private IEnumerator waitfordie()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
