using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject asteroidPrefab;//Prefab de los asteroides

    public float spawnRatePerMinute = 30f;//Ratio de asteroides por minuto

    public float spawnRateIncrement = 1f;//Para que vaya aumentando el número de asteroides

    public float spawnNext = 0;

    public float maxTimeLife = 4f;

    public float xlimit; //Variable pa jugar con nuestros límites del escenario(Se editará en unity)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    //Start borrado porque no va a hacer falta

    // Update is called once per frame
    void Update()
    {
        //Aquí gestionaremos nuestro generador de asteroides

        //Vamos a instanciar enemigos si ha pasado el tiempo suficiente entre el tiempo actual y la generación del último asteroide

        if (Time.time > spawnNext)
        {
            spawnNext = Time.time + 60 / spawnRatePerMinute; //El siguiente punto de spawn va a ser siempre el tiempo actual más los segundos que correspondan al siguiente spawn

            spawnRatePerMinute += spawnRateIncrement; //Incrementamos el ratio para hacer que los asteroides aparezcan más rápido

            float rand = Random.Range(-xlimit,xlimit);

            Vector2 spawnPosition = new Vector2(rand , 8f); //Posición respecto a lo que tengamos en nuestra escena 

            GameObject meteor = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity); //Posición en la que vamos a spawnear sin ningún  giro

            //Hay dos formas de decidir donde vamos a spawnear, lo podemos asociar a un GameObject vacío dentro de la escena y obtener su componente transform y de ahí lo generamos o directamente podemos decirle donde queremos que lo genere creando nuestro propio vector de posición

            Destroy(meteor,maxTimeLife);//Tiempo de autodestrucción del meteorito pa que la escena no se llene de basura
        }
    }
}
