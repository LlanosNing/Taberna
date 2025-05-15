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
    //Nombre del personaje que habla en ese momento
    private string charName;
    //El sprite del NPC
    private Sprite[] sNpc;

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
        {
            playerRef = GameObject.FindWithTag("Player").GetComponent<UltimatePlayerController>();
            playerRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        }
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

                if (SceneManager.GetActiveScene().name != "InteriorTaberna")
                    playerRB.velocity = Vector3.zero;
            }
        }

        if (dialogueCDCounter >= 0)
        {
            dialogueCDCounter -= Time.deltaTime;
        }
    }

    //Método que muestra el diálogo pasado por parámetro
    public void ShowDialog(string[] newLines, Sprite[] theSNpc)
    {
        if (!dialogBox.activeInHierarchy && dialogueCDCounter <= 0)
        {
            //El contenido de las líneas de diálogo del Manager pasa a ser el de las líneas de diálogo tras haber activado un diálogo
            dialogLines = newLines;
            //Vamos a la primera línea de diálogo
            lineIndex = 0;
            //Asignamos el Sprite del NPC
            sNpc = theSNpc;

            StartDialogue();

            //Hacemos que el jugador no se pueda mover
            if (SceneManager.GetActiveScene().name == "InteriorTaberna")
                playerTavernRef.canMove = false;
            else
                playerRef.canMove = false;
        }
    }

    //Método para conocer si hay un cambio de personaje en el diálogo
    public void CheckIfName()
    {
        //Si la línea empieza por n-
        if(dialogLines[lineIndex].StartsWith("n-"))
        {
            //Obtenemos el nombre del personaje que habla en ese momento
            charName = dialogLines[lineIndex].Replace("n-", "");

            dialogCharName.text = charName;

            //Salto a la siguiente línea de diálogo
            lineIndex++;
        }
    }
    public void CheckIfEmotion(Sprite[] sprites)
    {
        if (dialogLines[lineIndex].StartsWith("e-"))
        {
            if(charName != "Fede")
            {
                portrait.sprite = sprites[int.Parse(dialogLines[lineIndex].Replace("e-", ""))];
            }
            else
            {
                if(SceneManager.GetActiveScene().name == "InteriorTaberna")
                {
                    portrait.sprite = playerTavernRef.thePlayerSprites[int.Parse(dialogLines[lineIndex].Replace("e-", ""))];
                }
                else
                {
                    portrait.sprite = playerRef.thePlayerSprites[int.Parse(dialogLines[lineIndex].Replace("e-", ""))];
                }
            }

            //Salto a la siguiente línea de diálogo
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
            CheckIfName();
            CheckIfEmotion(sNpc);
            portrait.sprite = sNpc[0];
            StartCoroutine(ShowLine());
        }
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogLines.Length)
        {
            CheckIfName();
            CheckIfEmotion(sNpc);
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
            AudioManager.aMRef.PlaySFX(7);
            yield return new WaitForSeconds(typingTime);
        }
    }
}
