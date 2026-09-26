using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float thrustForce = 10f;//Fuerza de empuje

    public float rotationSpeed = 120f;//Factor de Giro

    //Variables para que las balas salgan de la nave 
    public GameObject gun, bulletPrefab; //Spawner de balas(gun)

    private Rigidbody _rigid;

    public static int SCORE = 0;//Variable compartida de puntuación del jugador

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float rotation = Input.GetAxis("Horizontal")*Time.deltaTime;

        float thrust = Input.GetAxis("Vertical")*Time.deltaTime;//Para programar el movimiento de nuestra nave a este valor empuje la detección del movimiento cuando el usuario interactua con nuestro juego (Teclado o Mando) 
        //GetAxis asigna a teclas acciones mediante etiquetas Vertical sería como pulsar w y s

        //Tenemos la nave inicialmente alineada hacia la derecha

        Vector3 thrustDirection = transform.right;

        _rigid.AddForce(thrustDirection * thrust * thrustForce);//Direccion*Decisión si hacia alante o atrás*fuerza de empuje

        transform.Rotate(Vector3.forward, -rotation*rotationSpeed);//Vector sobre el que se aplica el giro y angulo de giro.
        //DeltaTime = numero que establece el tiempo que hay entre dos frames consecutivos precalculados por el motor de esta manera se ajusta a la velocidad de frames de nuestra tarjeta gráfica

        //Cambiamos de signo rotation para que gire a donde debe girar a izq y d derecha

        if (Input.GetKeyDown(KeyCode.Space)) {

            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position,Quaternion.identity);//Instancio un nuevo objeto de tipo bala si pulso el espacio
            //Instantiate necesita la posición donde va a aparecer y la rotación inicial (posición del bulletSpawner, y un giro por defecto )

            //Ahora hay que configurar la dirección de la bala(Hay que decirle que la bala siempre apunte hacia el arma)
            Bullet balaScript = bullet.GetComponent<Bullet>();

            balaScript.targetVector = transform.right;//Vector al que está asociada la nave

        }  //Modifica el comportamiento de las balas(KeyCode.Space tecla espacio)

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

        //Queremos que cuando colisione la nave con un asteroide reiniciemos la partida
        //SceneManager gestiona las escenas dentro de mi juego
        //Con LoadScene podemos cargar cualquier otra escena o nivel de nuestro juego(por ejemplo pasar de un nivel a otro)
    }

    //Con atravesamientos o con triggers, la función con la que se trabaja es con OnTriggerEnter
    //Es trigguer si marcamos la opción trigger en el collider
}
