using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderAttractor : MonoBehaviour {
    [SerializeField] private float force = 1f;
    [SerializeField] private Zone zone; 

    private List<GameObject> planetsToCheck = new List<GameObject>();
    private new CircleCollider2D collider;

    void Start() {
        collider = GetComponent<CircleCollider2D>();
        collider.radius = zone.GetRadius();
    }

    void Update() {
        foreach(GameObject planet in planetsToCheck) {
            float distanceFromCenter = Vector3.Distance(new Vector3(planet.transform.position.x, planet.transform.position.y, 0), new Vector3(transform.position.x, transform.position.y, 0));
            float distFromBorder = Mathf.Abs(distanceFromCenter - collider.radius);
            if(distFromBorder > .5 * planet.transform.localScale.x) continue;
            Vector3 dir = transform.position - planet.transform.position;
            float dist = dir.magnitude;
            planet.GetComponent<Rigidbody2D>().AddForce(dir.normalized * force / dist);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == Planet.LAYER) {
            planetsToCheck.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.layer == Planet.LAYER) {
            planetsToCheck.Remove(collision.gameObject);
        }
    }
}
