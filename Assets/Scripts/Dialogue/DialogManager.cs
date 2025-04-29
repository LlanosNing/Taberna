using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Para poder usar los elementos de la UI
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor.Rendering;

public class DialogManager : MonoBehaviour
{
    //La caja de texto para el di�logo
    public TextMeshProUGUI dialogText;
    //El nombre del personaje
    public TextMeshProUGUI dialogCharName;
    //El retrato del personaje que habla en ese momento
    public Image portrait;
    //Referencia a la caja de di�logos
    public GameObject dialogBox;
    //L�neas del di�logo
    public string[] dialogLines;
    public int lineIndex;
    public float typingTime;
    //La l�nea actual de di�logo
    public int currentLine;
    //Nombre del personaje que habla en ese momento
    private string charName;
    //El sprite del NPC
    private Sprite sNpc;

    UltimatePlayerController playerRef;
    TavernPlayerController playerTavernRef;
    Rigidbody playerRB;

    public bool canDialogueStart;

    public bool activatesScore;
    public GameObject score;

    public GameObject managerToActivate;

    public float dialogueCooldown;
    float dialogueCDCounter;

    public GameObject objectToActivatePostDialogue;

    //Hacemos una referencia (Singleton)
    public static DialogManager instance;
    private void Awake()
    {
        //Inicializamos el Singleton si est� vac�o
        if (instance == null) instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindWithTag("Player").GetComponent<UltimatePlayerController>();
        if (SceneManager.GetActiveScene().name == "InteriorTaberna")
            playerTavernRef = GameObject.FindWithTag("Player").GetComponent<TavernPlayerController>();
        else
        {
            playerRef = GameObject.FindWithTag("Player").GetComponent<UltimatePlayerController>();
            playerRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Si el cuadro de di�logo est� activo
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

                if (SceneManager.GetActiveScene().name != "InteriorTaberna")
                    playerRB.velocity = Vector3.zero;
                ////Vamos a la siguiente l�nea de di�logo
                //currentLine++;

                ////Si se ha terminado el di�logo
                //if (currentLine >= dialogLines.Length)
                //{
                //    //Desactivamos el cuadro de di�logo
                //    dialogBox.SetActive(false);
                //    //Permitimos que el jugador se mueva de nuevo
                //    if (SceneManager.GetActiveScene().name == "InteriorTaberna")
                //        playerTavernRef.canMove = true;
                //    else
                //        playerRef.canMove = true;
                //}
                ////Si el di�logo a�n no ha terminado
                //else
                //{
                //    //Comprobamos si hay un cambio de personaje en el di�logo
                //    CheckIfName(sNpc);
                //    //Muestra la l�nea de di�logo actual
                //    //dialogText.text = dialogLines[currentLine];
                //    StartCoroutine(ShowLine());
                //    dialogCharName.text = charName;
                //}
            }
        }

        if (dialogueCDCounter >= 0)
        {
            dialogueCDCounter -= Time.deltaTime;
        }
    }

    //M�todo que muestra el di�logo pasado por par�metro
    public void ShowDialog(string[] newLines, Sprite theSNpc)
    {
        if (!dialogBox.activeInHierarchy && dialogueCDCounter <= 0)
        {
            //El contenido de las l�neas de di�logo del Manager pasa a ser el de las l�neas de di�logo tras haber activado un di�logo
            dialogLines = newLines;
            //Vamos a la primera l�nea de di�logo
            currentLine = 0;
            //Asignamos el Sprite del NPC
            sNpc = theSNpc;
            //Comprobamos si hay un cambio de personaje en el di�logo
            CheckIfName(sNpc);
            //Muestro la l�nea de di�logo actual
            dialogText.text = dialogLines[currentLine];

            dialogCharName.text = charName;
            //Activamos el cuadro de di�logo
            dialogBox.SetActive(true);
            //Hacemos que el jugador no se pueda mover
            if (SceneManager.GetActiveScene().name == "InteriorTaberna")
                playerTavernRef.canMove = false;
            else
                playerRef.canMove = false;
        }
    }

    //M�todo para conocer si hay un cambio de personaje en el di�logo
    public void CheckIfName(Sprite theSNpc)
    {
        //Si la l�nea empieza por n-
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

            //Salto a la siguiente l�nea de di�logo
            lineIndex++;
        }
    }

    private void StartDialogue()
    {
        if(dialogueCDCounter <= 0)
        {
            lineIndex = 0;
            canDialogueStart = true;
            dialogBox.SetActive(true);
            CheckIfName(sNpc);
            StartCoroutine(ShowLine());
        }
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

            if(objectToActivatePostDialogue != null)
            {
                objectToActivatePostDialogue.SetActive(true);
            }
            lineIndex = 0;

            if(activatesScore && !score.activeInHierarchy)
            {
                score.SetActive(true);
            }

            if(managerToActivate != null)
            {
                managerToActivate.SetActive(true);
            }

            dialogueCDCounter = dialogueCooldown;
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
