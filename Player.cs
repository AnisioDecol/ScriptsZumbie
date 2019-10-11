using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumforce;

    //Vida
    public Image barraVida;
    public float vidaMaxima = 5;
    public float vidaAtual;

    //Baú
    public GameObject ponte;
    public GameObject ponte2;
    public GameObject bau;
    public GameObject bau2;
    public GameObject bau3;
    public Text pegouBau;
    public int pegouBau2;

    public Button menu;

    public bool bauCamera;

    //Atirar
    public GameObject atirar;
    public int controleAtirar = 0;

    private Rigidbody2D rb;
    private bool facingRight = true;
    private Animator anim;
    public bool noChao = false;



    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        vidaAtual = vidaMaxima;
    }

    // Update is called once per frame
    void Update()
    {


        barraVida.rectTransform.sizeDelta = new Vector2(vidaAtual / vidaMaxima * 35.5f, 3);

        if (Input.GetKeyUp(KeyCode.W) && noChao == true || Input.GetKeyUp(KeyCode.UpArrow) && noChao == true)
        {
            anim.SetBool("Jump", true);
            rb.AddForce(new Vector2(0, jumforce));
            noChao = false;
        }

        if(vidaAtual <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

        //Atirar
        if (Input.GetMouseButtonDown(0) && facingRight && controleAtirar == 0 || Input.GetKeyDown(KeyCode.Space) && facingRight && controleAtirar == 0)
        {
            GameObject objeto = Instantiate(atirar, transform.position, transform.rotation) as GameObject;
            objeto.GetComponent<Rigidbody2D>().AddForce(new Vector2(600, 50));
            controleAtirar = 1;
        }
        else if (Input.GetMouseButtonDown(0) && !facingRight && controleAtirar == 0  || Input.GetKeyDown(KeyCode.Space) && !facingRight && controleAtirar == 0)
        {
            GameObject objeto = Instantiate(atirar, transform.position, transform.rotation) as GameObject;
            objeto.GetComponent<Rigidbody2D>().AddForce(new Vector2(-600, 50));
            controleAtirar = 1;
        }


    }

    void FixedUpdate()
    {
        if (noChao)
        {
            Movimento();
        }
        else
        {
            anim.SetBool("Jump", true);
            Movimento();
        }
        

    }

    void Movimento()
    {
        float h = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(h));

        rb.velocity = new Vector2(h * speed, rb.velocity.y);
        if (h > 0 && !facingRight)
        {
            Flip();
        }
        else if (h < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;

        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.name == "Baú")
        {
            pegouBau.text = "Novo caminho desbloqueado!";
            ponte.SetActive(true);
            bau.SetActive(false);
            bauCamera = true;
        }

        if (collision.gameObject.name == "Baú 2" && pegouBau2 == 0)
        {
            Destroy(bau2);
            pegouBau.text = "Ainda falta um baú para desbloquear um novo caminho!";
            pegouBau2 += 1;
        }

        if (collision.gameObject.name == "Baú 3" && pegouBau2 == 1)
        {
            Destroy(bau3);
            pegouBau.text = "Novo caminho desbloqueado!";
            pegouBau2 += 1;
            Plataforma();
        }


        if (collision.gameObject.name == "Plataforma" || collision.gameObject.name == "Plataforma 2")
        {
            pegouBau.text = " ";
        }

        if (collision.gameObject.name == "Bandeira")
        {
            pegouBau.text = "Você concluiu o jogo!";
            Time.timeScale = 0f;
            menu.gameObject.SetActive(true);
        }

    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "GroundCheck" || collision.gameObject.name == "Plataforma" || collision.gameObject.name == "Plataforma 2")
        {
            noChao = true;
            anim.SetBool("Jump", false);
        }
    }

    private void Plataforma()
    {
        if(pegouBau2 == 2)
        {
            ponte2.SetActive(true);
            bauCamera = true;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "Agua" && noChao == false)
        {
            noChao = true;
            anim.SetBool("Jump", false);
        }

        if (collision.gameObject.name == "Agua")
        {
            anim.SetBool("Water", true);
            DPSLife();

        }
        if (collision.gameObject.name != "Agua")
        {
            anim.SetBool("Water", false);

        }
    }

    private void DPSLife()
    {
        for (int i = 0; i < 1000; i++)
        {
            vidaAtual -= Time.deltaTime * 0.001f;
        }
        
    }


}
