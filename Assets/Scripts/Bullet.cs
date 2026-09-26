using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{

    public float speed = 10f;

    public float maxLifeTime = 3f;
    public Vector3 targetVector;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, maxLifeTime); //Esta función nos permite decirle a unity que objeto queremos destruir(incluso se puede establecer un cronómetro de cuando se debe destruir ese objeto)
    }                        //GameObject en este caso somos nosotros mismos 

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed*targetVector*Time.deltaTime); //Modifica el componente transform en lo que es el posicionamiento del objeto(Lo estamos teletransportando)
    } //El target vector se asegura que la dirección en la que corre la bala está alineada con la nave

    private void OnCollisionEnter(Collision collision) //Gestiona la colisión entre la bala y el asteroide
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //En esta función cada vez que el objeto colisione(bala) incrementa tu puntuación
            IncreaseScore();
            Destroy(collision.gameObject); //Si la bala colisiona con el asteroide este se destruye
            Destroy(gameObject);//La bala en el momento de la colisión también se autodestruye
        }
    }

    private void IncreaseScore() //Incrementa los puntos del jugador y actualiza el texto del canvas
    {
        Player.SCORE++;
        UpdateScoreText();
    }

    private void UpdateScoreText() //Incrementa la puntuación en el texto
    {
        GameObject go = GameObject.FindGameObjectWithTag("UI");//Busca un gameObject por su tag
        go.GetComponent<Text>().text = "Puntos: " + Player.SCORE;//Modifica su texto para incrementar la puntuación
    }
}
