using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Para poder usar los elementos de la UI
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor.Rendering;

public class DialogManager : MonoBehaviour
{
    //La caja de texto para el diálogo
    public TextMeshProUGUI dialogText;
    //El nombre del personaje
    public TextMeshProUGUI dialogCharName;
    //El retrato del personaje que habla en ese momento
    public Image portrait;
    //Referencia a la caja de diálogos
    public GameObject dialogBox;
    //Líneas del diálogo
    public string[] dialogLines;
    public int lineIndex;
    public float typingTime;
    //La línea actual de diálogo
    public int currentLine;
    //Nombre del personaje que habla en ese momento
    private string charName;
    //El sprite del NPC
    private Sprite sNpc;

    UltimatePlayerController playerRef;
    TavernPlayerController playerTavernRef;

    public bool canDialogueStart;

    //Hacemos una referencia (Singleton)
    public static DialogManager instance;
    private void Awake()
    {
        //Inicializamos el Singleton si está vacío
        if (instance == null) instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindWithTag("Player").GetComponent<UltimatePlayerController>();
        if (SceneManager.GetActiveScene().name == "InteriorTaberna")
            playerTavernRef = GameObject.FindWithTag("Player").GetComponent<TavernPlayerController>();
        else
            playerRef = GameObject.FindWithTag("Player").GetComponent<UltimatePlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Si el cuadro de diálogo está activo
        if(dialogBox.activeInHierarchy)
        {
            if ((Input.GetButtonUp("Fire1") || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Jump")) && Time.timeScale == 1f)
            {
                if (!canDialogueStart)
                {
                    StartDialogue();
                }
                else if (dialogText.text == dialogLines[lineIndex])
                {
                    NextDialogueLine();
                }
                else
                {
                    StopAllCoroutines();
                    dialogText.text = dialogLines[lineIndex];
                }
                ////Vamos a la siguiente línea de diálogo
                //currentLine++;

                ////Si se ha terminado el diálogo
                //if (currentLine >= dialogLines.Length)
                //{
                //    //Desactivamos el cuadro de diálogo
                //    dialogBox.SetActive(false);
                //    //Permitimos que el jugador se mueva de nuevo
                //    if (SceneManager.GetActiveScene().name == "InteriorTaberna")
                //        playerTavernRef.canMove = true;
                //    else
                //        playerRef.canMove = true;
                //}
                ////Si el diálogo aún no ha terminado
                //else
                //{
                //    //Comprobamos si hay un cambio de personaje en el diálogo
                //    CheckIfName(sNpc);
                //    //Muestra la línea de diálogo actual
                //    //dialogText.text = dialogLines[currentLine];
                //    StartCoroutine(ShowLine());
                //    dialogCharName.text = charName;
                //}
            }
        }
    }

    //Método que muestra el diálogo pasado por parámetro
    public void ShowDialog(string[] newLines, Sprite theSNpc)
    {
        if (!dialogBox.activeInHierarchy)
        {
            //El contenido de las líneas de diálogo del Manager pasa a ser el de las líneas de diálogo tras haber activado un diálogo
            dialogLines = newLines;
            //Vamos a la primera línea de diálogo
            currentLine = 0;
            //Asignamos el Sprite del NPC
            sNpc = theSNpc;
            //Comprobamos si hay un cambio de personaje en el diálogo
            CheckIfName(sNpc);
            //Muestro la línea de diálogo actual
            dialogText.text = dialogLines[currentLine];

            dialogCharName.text = charName;
            //Activamos el cuadro de diálogo
            dialogBox.SetActive(true);
            //Hacemos que el jugador no se pueda mover
            if (SceneManager.GetActiveScene().name == "InteriorTaberna")
                playerTavernRef.canMove = false;
            else
                playerRef.canMove = false;
        }
    }

    //Método para conocer si hay un cambio de personaje en el diálogo
    public void CheckIfName(Sprite theSNpc)
    {
        //Si la línea empieza por n-
        if(dialogLines[lineIndex].StartsWith("n-"))
        {
            //Obtenemos el nombre del personaje que habla en ese momento
            charName = dialogLines[lineIndex].Replace("n-", "");

            dialogCharName.text = charName;
            //Si es distinto de los nombres de los personajes principales
            if (charName != "Fede")
                //Ponemos el sprite del npc en concreto
                portrait.sprite = theSNpc;
            //Si es el nombre de un personaje principal
            else
            //Ponemos el sprite de ese personaje
            {
                if (SceneManager.GetActiveScene().name == "InteriorTaberna")
                    portrait.sprite = playerTavernRef.thePlayerSprite;
                else
                    portrait.sprite = playerRef.thePlayerSprite;
            }

            //Salto a la siguiente línea de diálogo
            lineIndex++;
        }
    }

    private void StartDialogue()
    {
        lineIndex = 0;
        canDialogueStart = true;
        dialogBox.SetActive(true);
        CheckIfName(sNpc);
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogLines.Length)
        {
            CheckIfName(sNpc);
            StartCoroutine(ShowLine());
        }
        else
        {
            canDialogueStart = false;
            dialogBox.SetActive(false);
            if (SceneManager.GetActiveScene().name == "InteriorTaberna")
                playerTavernRef.canMove = true;
            else
                playerRef.canMove = true;
            lineIndex = 0;
        }
    }
    private IEnumerator ShowLine()
    {
        dialogText.text = string.Empty;

        foreach (char ch in dialogLines[lineIndex])
        {
            dialogText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
    }
}
