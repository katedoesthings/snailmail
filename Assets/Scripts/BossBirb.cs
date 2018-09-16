using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBirb : MonoBehaviour {
    public float attackEndDistance;
    public float attackY;
    public float attackTime = 5;
    public float attackDelay = 10;
    public float flameTime = 2;
    public AudioSource caw;
    public int maxHealth;

    public ParticleSystem flamez;

    private Vector3 attackStart;
    private Vector3 downTarget;
    private Vector3 fwdTarget;
    private SnailMovement snail;

    private float lastAttackTime;
    private float attackStartTime;
    private bool attacking;
    private Vector3 startpos;
    private float flameStart = -100;
    private int curHealth;

	// Use this for initialization
	void Start () {
        startpos = transform.position;
        lastAttackTime = Time.time;
        curHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        if (!snail) {
            snail = FindObjectOfType<SnailMovement>();
        }
        
        if (snail) {
            if (snail.transform.position.y > attackY && !attacking) {
                if (Time.time - lastAttackTime > attackDelay) {
                    lastAttackTime = Time.time;
                    Vector3 v = snail.transform.position - transform.position;
                    v.y = 0;
                    transform.rotation = Quaternion.LookRotation(v, Vector3.up);
                    attackStart = transform.position;
                    downTarget = snail.transform.position;
                    fwdTarget = transform.position + transform.forward * (Vector3.Distance(transform.position, snail.transform.position) + attackEndDistance);
                    attacking = true;
                    caw.Play();
                }
            }
            if (Time.time - lastAttackTime < attackTime && attacking) {
                float progress = (Time.time - lastAttackTime) / attackTime;

                if (progress < 0.5f) {
                    transform.position = Vector3.Lerp(attackStart, downTarget, progress * 2);
                } else {
                    transform.position = Vector3.Lerp(downTarget, fwdTarget, progress * 2 - 1);
                }
            } else {
                Vector3 v = snail.transform.position - transform.position;
                v.y = 0;
                transform.rotation = Quaternion.LookRotation(v, Vector3.up);
                attacking = false;
            }
        } else {
            transform.position = startpos;
        }

        if (Time.time - flameStart > flameTime) {
            var em = flamez.emission;
            em.enabled = false;
        }
	}

    private void OnTriggerEnter(Collider collision) {
        if (collision.GetComponent<SnailSection>()) {
            Checkpoint.active?.Respawn();
        }
    }

    public void Damage() {
        var em = flamez.emission;
        em.enabled = true;
        flameStart = Time.time;
        curHealth--;
        if (curHealth <= 0) {
            Destroy(this);
            GetComponent<Collider>().isTrigger = false;
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().angularVelocity = Random.onUnitSphere;
        }
    }
}
