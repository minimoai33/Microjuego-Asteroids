using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject asteroidPrefab;//Prefab de los asteroides

    public float spawnRatePerMinute = 30f;//Ratio de asteroides por minuto

    public float spawnRateIncrement = 1f;//Incremento de la frecuencia de aparición

    public float spawnNext = 0;

    public float maxTimeLife = 4f;

    public float xlimit; //Límite de aparición en el ejeX

    
    
    //Start borrado porque no va a hacer falta


    void Update()
    {
        //Aquí gestionaremos nuestro generador de asteroides

        //Vamos a instanciar enemigos si ha pasado el tiempo suficiente entre el tiempo actual y la generación del último asteroide

        if (Time.time > spawnNext)
        {
            spawnNext = Time.time + 60 / spawnRatePerMinute; //El siguiente punto de spawn va a ser siempre el tiempo actual más los segundos que correspondan al siguiente spawn

            spawnRatePerMinute += spawnRateIncrement; //Incrementamos el ratio para hacer que los asteroides aparezcan más rápido

            float rand = Random.Range(-xlimit,xlimit);

            Vector2 spawnPosition = new Vector2(rand , 8f); //Posición en la que vamos a spawnear

            GameObject meteor = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity); 

            Destroy(meteor,maxTimeLife);//Tiempo de autodestrucción del meteorito pa que la escena no se llene de basura
        }
    }
}
