using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zumbi : Inimigo
{

    private void Start()
    {
        transform.Find("ColisãoAtaqueInimigo").gameObject.GetComponent<Collider2D>();
    }
    // Update is called once per frame
    void Update()
    {

        PararAndar();
      
        

        if (movendo)
        {
            if((player.position.x > transform.position.x && sprite.flipX) ||
                (player.position.x < transform.position.x && !sprite.flipX))
            {
                anim.SetBool("Walk", true);
                Flip();
            }
            else
            {
                anim.SetBool("Idle", true);
            }
        }//fim movendo

    }//fim Update

    private void FixedUpdate()
    {
        if (movendo)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.name == "Player")
        {
            Atacar();
        }
        if (collision.gameObject.name == "Poção(Clone)" && life <= 0)
        {
            anim.SetBool("Morte", true);
            float duracao = anim.GetCurrentAnimatorStateInfo(0).length;
            Destroy(gameObject, duracao);
        }
        if (collision.gameObject.name == "Poção(Clone)" && life > 0)
        {
            Poção a = GameObject.Find("Poção(Clone)").GetComponent<Poção>();
            a.Particula();
            life -= 1;
        }

    }

    void Atacar()
    {
        anim.SetTrigger("Ataque 1");
        Collider2D[] colliders = new Collider2D[1];
        transform.Find("ColisãoAtaqueInimigo").gameObject.GetComponent<Collider2D>()
            .OverlapCollider(new ContactFilter2D(), colliders);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != null && colliders[i].gameObject.CompareTag("Player"))
            {
                Player a = GameObject.Find("Player").GetComponent<Player>();
                a.vidaAtual -= 0.03f;
            }
        }
    }

    void PararAndar()
    {
        float distance = PlayerDistance();

        Collider2D[] collider = new Collider2D[1];
        rb.OverlapCollider(new ContactFilter2D(), collider);
        for (int i = 0; i < collider.Length; i++)
        {
            if (collider[i] != null && collider[i].gameObject.CompareTag("Player"))
            {
                movendo = false;
                Atacar();
                break;
            }
            else
                movendo = (distance <= distanceAtaque);
        }

    }
}
