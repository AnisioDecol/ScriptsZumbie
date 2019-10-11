using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poção : MonoBehaviour
{
    public GameObject particula;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Plataforma" ||
            collision.gameObject.name == "Plataforma 2" ||
            collision.gameObject.name == "Parede" ||
            collision.gameObject.name == "GroundCheck" ||
            collision.gameObject.name == "Inimigo")
        {
            Particula();
        }
    }


    public void Particula() {

        GameObject tempParticula = Instantiate(particula) as GameObject;
        tempParticula.transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        Destroy(gameObject);
        Player a = GameObject.Find("Player").GetComponent<Player>();
        a.controleAtirar = 0;
    }
}
