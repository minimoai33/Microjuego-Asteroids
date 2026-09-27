using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float thrustForce = 10f;//Fuerza de empuje

    public float rotationSpeed = 120f;//Factor de Giro

    public GameObject gun, bulletPrefab; //Spawner de balas(gun), y prefab de las balas

    private Rigidbody _rigid;

    //Límites de la cámara
    private float limitePorLaDerecha, limitePorLaIzquierda, limitePorArriba, limitePorAbajo; //Límites de la camara

    //Elementos de la interfaz de pausa
    public GameObject reanudar, pause, reinicio;

    public static int SCORE = 0;//Variable compartida de puntuación del jugador

    
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();

        //Ocultamos los elementos de la pantalla de pausa al comenzar la partida
        reanudar.SetActive(false);
        pause.SetActive(false);
        reinicio.SetActive(false);

        //Asigna las funciones a los botones de la interfaz
        Button reanudador = reanudar.GetComponent<Button>();
        reanudador.onClick.AddListener(Continuar);

        Button reiniciador = reinicio.GetComponent<Button>();
        reiniciador.onClick.AddListener(ComenzarDeCero);
    }

    
    void Update()
    {
        //Detectamos el movimiento hacia delante o hacia atrás
        float rotation = Input.GetAxis("Horizontal")*Time.deltaTime;
        float thrust = Input.GetAxis("Vertical")*Time.deltaTime;
        

        //Tenemos la nave inicialmente alineada hacia la derecha
        Vector3 thrustDirection = transform.right;

        _rigid.AddForce(thrustDirection * thrust * thrustForce);//Direccion*Decisión si hacia alante o atrás*fuerza de empuje

        transform.Rotate(Vector3.forward, -rotation*rotationSpeed);//Vector sobre el que se aplica el giro y angulo de giro.
        

        limitePorArriba = Camera.main.transform.position.y + Camera.main.orthographicSize; //Limite Superior sumando la posición de nuestra camara en el eje Y y la distancia desde la mitad hacia arriba de esta.(para poder compararlo con los ejes de la nave)

        limitePorAbajo = Camera.main.transform.position.y - Camera.main.orthographicSize; //Limite Inferior sumando la posición de nuestra camara en el eje Y y la distancia desde la mitad hacia abajo de esta.(para poder compararlo con los ejes de la nave)

        limitePorLaDerecha = Camera.main.transform.position.x + (Camera.main.orthographicSize*Camera.main.aspect); //Limite por la derecha sumando la posición de nuestra camara en el eje X y la distancia desde la mitad hacia la derecha de esta.(para poder compararlo con los ejes de la nave)

        limitePorLaIzquierda = Camera.main.transform.position.x - (Camera.main.orthographicSize*Camera.main.aspect); //Limite por la izquierda sumando la posición de nuestra camara en el eje X y la distancia desde la mitad hacia la izquierda de esta.(para poder compararlo con los ejes de la nave)

        if (transform.position.x > limitePorLaDerecha) //Si la nave sobrepasa el límite por derecha intercambiamos su posición al inicio del límite por izquierda
        {
            SetTransformX(limitePorLaIzquierda);
        }else if (transform.position.x < limitePorLaIzquierda) //Si la nave sobrepasa el límite por izquierda intercambiamos su posición al inicio del límite por derecha
        {
            SetTransformX(limitePorLaDerecha);
        }else if (transform.position.y > limitePorArriba) //Si la nave sobrepasa el límite superior intercambiamos su posición al inicio del límite inferior
        {
            SetTransformY(limitePorAbajo);
        }else if (transform.position.y < limitePorAbajo) //Si la nave sobrepasa el límite inferior intercambiamos su posición al inicio del límite superior
        {
            SetTransformY(limitePorArriba);
        }



        void SetTransformX(float n) //Función para cambiar la posición de la nave en el eje X
        {
            transform.position = new Vector3(n, transform.position.y, transform.position.z);
        }

        void SetTransformY(float n) //Función para cambiar la posición de la nave en el eje Y
        {
            transform.position = new Vector3(transform.position.x, n, transform.position.z);
        }

        //Modifica el comportamiento de las balas(KeyCode.Space tecla espacio)
        if (Input.GetKeyDown(KeyCode.Space)) {

            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position,Quaternion.identity);//Instancio un nuevo objeto de tipo bala si pulso el espacio
            //Instantiate necesita la posición donde va a aparecer y la rotación inicial (posición del bulletSpawner, y un giro por defecto )

            //Ahora hay que configurar la dirección de la bala(Hay que decirle que la bala siempre apunte hacia el arma)
            Bullet balaScript = bullet.GetComponent<Bullet>();

            balaScript.targetVector = transform.right;//Vector al que está asociada la nave

        }  

        //Si pulsas esc se para el juego y te vas a la pantalla de menú
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            activarPausa();

        }

    }
    //Función que se encarga de obtener la información cuando este objeto colisiona (la nave)
    private void OnCollisionEnter(Collision collision) //El atributo collision tiene toda la información del objeto cuando ha colisionado con otro
    {
        //Podemos preguntar que tag es el objeto con el que hemos colisionado
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SCORE = 0;//Reinicia el score cuando la nave colisiona con un meteorito
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Reinicia la partida

        }
        else
        {
            Debug.Log("He colisionado con otra cosa....");
        }

    }

    
    //Continua la partida y oculta el menú de pausa
    void Continuar()
    {
        pause.SetActive(false);
        reanudar.SetActive(false);
        reinicio.SetActive(false);
        Time.timeScale = 1f;
    }

    //Reinicia la partida desde el principio
    void ComenzarDeCero()
    {
        pause.SetActive(false);
        reanudar.SetActive(false);
        reinicio.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    }

    //Muestra el menú de pausa
    void activarPausa()
    {
        pause.SetActive(true);
        reanudar.SetActive(true);
        reinicio.SetActive(true);
    }
}
