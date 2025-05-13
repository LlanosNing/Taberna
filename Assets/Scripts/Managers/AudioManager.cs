using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    //Creamos un array donde guardamos los sonidos a reproducir
    public AudioSource[] sfx;
    //Referencias a la música del juego
    public AudioSource music;

    //Hacemos el Singleton de este script
    public static AudioManager aMRef;

    private void Awake()
    {
        //Si la referencia del Singleton esta vacía
        if (aMRef == null)
            //La rrellenamos con todo el contenido de este código (para que todo sea accesible)
            aMRef = this;
    }

    private void Start()
    {
        music.Play();
    }

    //Método para reproducir los sonidos
    public void PlaySFX(int soundToPlay) //soundToPlay = sera el sonido número X del array que queremos reproducir
    {
        //Si ya estaba reproduciendo el sonido, lo paramos
        sfx[soundToPlay].Stop();
        //Alteramos un poco el sonido cada vez que se vaya a reproducir
        sfx[soundToPlay].pitch = Random.Range(.95f, 1.05f);
        //Reproducir el sonido pasado por parámetro
        sfx[soundToPlay].Play();
    }
}
