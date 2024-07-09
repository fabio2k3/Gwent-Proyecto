using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.DebugUI.Table;

public class GameFlow : MonoBehaviour
{
    public static bool avoidCampOrc; // se pueden invocar cartas en el campo de los Orcos
    public static bool avoidCampWarrior; // se pueden invocar cartas en el campo de los Warriors

    /* numero que genera quien empieza
     0 => Warriors
     1 => Orcos*/
    public static int whoStart; 

    private int orcWinsCount = 0; // Contador de partidas ganadas por los orcos
    private int warriorWinsCount = 0; // Contador de partidas ganadas por los warriors

    public TextMeshProUGUI scoreWarrior; // Contador Warriors
    public TextMeshProUGUI scoreOrc; // Contador Orcos
    public TextMeshProUGUI textLeaderOrc; // Texto efecto Lider Orcos

    public GameObject roundOfWarriors; // ronda Warriors
    public GameObject roundOfOrcs; // ronda Orcos
    public GameObject WarriorsWin; // Ganan los Warriors
    public GameObject OrcWin; // Ganan los Orcos
    public GameObject TourOfOrcs; // Turno de los Orcos (WINNERS)
    public GameObject TourOfWarriors; // Turno de los Warrior (WINNERS)
    public GameObject TiedRound; // Ronda Empatada

    public List<string> ClimateCards = new List<string>(); // Lista de las cartas de CLIMA

    private bool orcGave; // Orcos cedieron el Turno
    private bool warriorsGave; // Warriors Cedieron el Turno
    private bool weHaveWinner; // Warriors o Orcos ganaron la ronda
    private bool effectOrcLeader = true; // verificar mas tarde si se activo el efecto del lider
    private bool startNewRound; // empezo una nueva ronda
    public static bool tourOrcs; // turno de los orcos
    public static bool tourWarriors; // turno de los warriors
    public static bool canTakeACard; // puedes tomar una carta
   
    public static int movement = 0;

    public NewBehaviourScript cardHandOrc; 
    public NewBehaviourScript cardHandWarrior;

    public Button warriorEffectLeader; // Boton para activar el efecto del lider

    void Start()
    {
        scoreWarrior = GameObject.Find("ScoreWarriors").GetComponent<TextMeshProUGUI>();
        scoreOrc = GameObject.Find("ScoreOrcs").GetComponent<TextMeshProUGUI>();

        whoStart = UnityEngine.Random.Range(0, 2);

        if (whoStart == 0)
            tourWarriors = true;
        if (whoStart == 1)
            tourOrcs = true;

        warriorEffectLeader.onClick.AddListener(OnAddCardsButtonPressed);
    }

    void Update()
    {    
        if(movement == 2 && canTakeACard)
        {
            movement = 0;
            canTakeACard = false;
        }

        #region Clear Effect !!!!
        if(ClearEffect.removeClimateCard)
        {
            if (DragAndDrop.climateCards[ClearEffect.positionOfRemoveClimate] != null)
            {
                string cardName = DragAndDrop.climateCards[ClearEffect.positionOfRemoveClimate].name;
                GameObject cloneObject = DragAndDrop.climateCards[ClearEffect.positionOfRemoveClimate];

                string originalObjectName = cardName.EndsWith("(Clone)") ? cardName.Replace("(Clone)", "") : cardName;

                if (cardHandOrc.objectNames.Contains(cardName))
                {
                    cardHandOrc.objectNames.Remove(cardName);
                    // Eliminar el objeto de hand si coincide con el objeto original
                    cardHandOrc.hand.RemoveAll(obj => obj.name == originalObjectName);
                }
                else if (cardHandWarrior.objectNames.Contains(cardName))
                {
                    cardHandWarrior.objectNames.Remove(cardName);
                    // Eliminar el objeto de hand si coincide con el objeto original
                    cardHandWarrior.hand.RemoveAll(obj => obj.name == originalObjectName);
                }

                DragAndDrop.climateCards[cloneObject.GetComponent<Cards>().climateInvocated] = null;
            }
        }
        #endregion

        #region Winner of the GAME

        if (orcWinsCount >= 2)
        {
            OrcWin.SetActive(true);

            if(Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
        if (warriorWinsCount >= 2)
        {
            WarriorsWin.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }

        #endregion

        #region Clear the Board
        if (weHaveWinner && Input.GetKeyDown(KeyCode.Return))
        {
            for (int row = 0; row < DragAndDrop.gameObjectsCards.GetLength(0); row++)
            {
                for (int col = 0; col < DragAndDrop.gameObjectsCards.GetLength(1); col++)
                {
                    if (DragAndDrop.gameObjectsCards[row, col] != null)
                    {
                        string cardName = DragAndDrop.gameObjectsCards[row, col].name;
                        GameObject clonedObject = DragAndDrop.gameObjectsCards[row, col];

                        string originalObjectName = cardName.EndsWith("(Clone)") ? cardName.Replace("(Clone)", "") : cardName;

                        if (cardHandOrc.objectNames.Contains(cardName))
                        {
                            cardHandOrc.objectNames.Remove(cardName);
                            // Eliminar el objeto de hand si coincide con el objeto original
                            cardHandOrc.hand.RemoveAll(obj => obj.name == originalObjectName);
                        }
                        else if (cardHandWarrior.objectNames.Contains(cardName))
                        {
                            cardHandWarrior.objectNames.Remove(cardName);
                            // Eliminar el objeto de hand si coincide con el objeto original
                            cardHandWarrior.hand.RemoveAll(obj => obj.name == originalObjectName);
                        }

                        DragAndDrop.gameObjectsCards[row, col].transform.position = new Vector3(DragAndDrop.gameObjectsCards[row, col].transform.position.x, DragAndDrop.gameObjectsCards[row, col].transform.position.y, 0f);

                        DragAndDrop.gameObjectsCards[row, col] = null;
                    }                    
                }
            }

            for (int pos = 0; pos < 3; pos++)
            {
                if (DragAndDrop.climateCards[pos] != null)
                {
                    string cardName = DragAndDrop.climateCards[pos].name;
                    GameObject cloneObject = DragAndDrop.climateCards[pos];

                    string originalObjectName = cardName.EndsWith("(Clone)") ? cardName.Replace("(Clone)", "") : cardName;

                    if (cardHandOrc.objectNames.Contains(cardName))
                    {
                        cardHandOrc.objectNames.Remove(cardName);
                        // Eliminar el objeto de hand si coincide con el objeto original
                        cardHandOrc.hand.RemoveAll(obj => obj.name == originalObjectName);
                    }
                    else if (cardHandWarrior.objectNames.Contains(cardName))
                    {
                        cardHandWarrior.objectNames.Remove(cardName);
                        // Eliminar el objeto de hand si coincide con el objeto original
                        cardHandWarrior.hand.RemoveAll(obj => obj.name == originalObjectName);

                    }

                    DragAndDrop.climateCards[pos].transform.position = new Vector3(DragAndDrop.climateCards[pos].transform.position.x, DragAndDrop.climateCards[pos].transform.position.y, 0f);

                    DragAndDrop.climateCards[pos] = null;
                }

            }

            for (int row = 0; row < DragAndDrop.decoyGameObjects.GetLength(0); row++)
            {
                for (int col = 0; col < DragAndDrop.decoyGameObjects.GetLength(1); col++)
                {
                    if (DragAndDrop.decoyGameObjects[row, col] != null)
                    {
                        string cardName = DragAndDrop.decoyGameObjects[row, col].name;
                        GameObject clonedObject = DragAndDrop.decoyGameObjects[row, col];

                        string originalObjectName = cardName.EndsWith("(Clone)") ? cardName.Replace("(Clone)", "") : cardName;

                        if (cardHandOrc.objectNames.Contains(cardName))
                        {
                            cardHandOrc.objectNames.Remove(cardName);
                            // Eliminar el objeto de hand si coincide con el objeto original
                            cardHandOrc.hand.RemoveAll(obj => obj.name == originalObjectName);
                        }
                        else if (cardHandWarrior.objectNames.Contains(cardName))
                        {
                            cardHandWarrior.objectNames.Remove(cardName);
                            // Eliminar el objeto de hand si coincide con el objeto original
                            cardHandWarrior.hand.RemoveAll(obj => obj.name == originalObjectName);
                        }

                        DragAndDrop.decoyGameObjects[row, col].transform.position = new Vector3(DragAndDrop.decoyGameObjects[row, col].transform.position.x, DragAndDrop.decoyGameObjects[row, col].transform.position.y, 0f);

                        DragAndDrop.decoyGameObjects[row, col] = null;
                    }
                }
            }


            weHaveWinner = false;
            roundOfOrcs.SetActive(false);
            roundOfWarriors.SetActive(false);
            TiedRound.SetActive(false);
            startNewRound = true;
        }
        #endregion

        #region Check Winner of the Round
        if (warriorsGave && orcGave)
        {
            if (CalculateScoreOrcs() > CalculateScoreWarriors())
            {
                roundOfOrcs.transform.position = new Vector3(roundOfOrcs.transform.position.x, roundOfOrcs.transform.position.y, 100f);
                roundOfOrcs.SetActive(true);
                orcWinsCount++;
                weHaveWinner = true;
                warriorsGave = false;
                orcGave = false;
                whoStart = 1;
                canTakeACard = true;
            }
                
            else if(CalculateScoreOrcs() < CalculateScoreWarriors())
            {
                roundOfWarriors.transform.position = new Vector3(roundOfWarriors.transform.position.x, roundOfWarriors.transform.position.y, 100f);
                roundOfWarriors.SetActive(true);
                warriorWinsCount++;
                weHaveWinner = true;
                warriorsGave = false;
                orcGave = false;
                whoStart = 0;
                canTakeACard = true;
            }
            else
            {
                if(effectOrcLeader)
                {
                    textLeaderOrc.gameObject.SetActive(true);

                    if(Input.GetKeyDown(KeyCode.Return))
                    {
                        textLeaderOrc.gameObject.SetActive(false);
                        Debug.Log("Gana Orcos");
                        roundOfOrcs.SetActive(true);
                        orcWinsCount++;
                        weHaveWinner = true;
                        warriorsGave = false;
                        orcGave = false;
                        effectOrcLeader = false;
                    }
                    if(Input.GetKeyDown(KeyCode.Escape))
                    { 
                        textLeaderOrc.gameObject.SetActive(false);
                        TiedRound.SetActive(true);
                        orcWinsCount++;
                        warriorWinsCount++;
                        weHaveWinner = true;
                        warriorsGave = false;
                        orcGave = false;
                    }
                }
            }
        }
        #endregion

        else
        {
            #region Pass Turn
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (orcGave)
                    warriorsGave = true;
                else if (warriorsGave)
                    orcGave = true;

                whoStart = (whoStart == 0) ? 1 : 0;

                if (whoStart == 0)
                    tourWarriors = true;
                if (whoStart == 1)
                    tourOrcs = true;
            }

            if (whoStart == 0)
            {
                avoidCampOrc = true;
                avoidCampWarrior = false;
            }
            else if (whoStart == 1)
            {
                avoidCampOrc = false;
                avoidCampWarrior = true;
            }
            #endregion

            scoreWarrior.text = "Score: " + CalculateScoreWarriors();
            scoreOrc.text = "Score: " + CalculateScoreOrcs();

            #region Give Up Turn
            if (Input.GetKeyDown(KeyCode.V) && !avoidCampWarrior)
            {
                warriorsGave = true;
                Debug.Log("W cedio");
                whoStart = 1;
            }
            if (Input.GetKeyDown(KeyCode.V) && !avoidCampOrc)
            {
                orcGave = true;
                Debug.Log("O cedio");
                whoStart = 0;
            }
            #endregion
        }

        #region Image about WHO Play
        if ((tourWarriors || (whoStart == 0 && startNewRound)))
        {
            TourOfWarriors.SetActive(true);
            tourWarriors = false;
        }
        if (tourOrcs || (whoStart == 1 && startNewRound))
        {
            TourOfOrcs.SetActive(true);
            tourOrcs = false;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(startNewRound)
            {
                startNewRound = false;
            }
            else
            {
                TourOfOrcs.SetActive(false);
                TourOfWarriors.SetActive(false);
            }
        }
        #endregion
    }

    // Funcion para Calcular el Score de los Orcos
    public int CalculateScoreOrcs()
    {
        int scoreOrc = 0;
        for (int row = 0; row < 3; row++)
        {
            for(int col = 0; col < 5; col++)
            {
                GameObject card = DragAndDrop.gameObjectsCards[row,col];

                if (card != null)
                    scoreOrc += card.GetComponent<Cards>().attack;
            }
        }

        return scoreOrc;
    }

    // Funcion para Calcular el Score de los Warriors
    public int CalculateScoreWarriors()
    {
        int scoreWarrior = 0;
        for (int row = 3; row < 6; row++)
        {
            for (int col = 0; col < 5; col++)
            {
                GameObject card = DragAndDrop.gameObjectsCards[row, col];

                if (card != null)
                    scoreWarrior += card.GetComponent<Cards>().attack;
            }
        }

        return scoreWarrior;
    }

    // Funcion para activar el efecto del lider
    public void OnAddCardsButtonPressed()
    {
        if (cardHandWarrior.hand.Count < 10)
        {
            cardHandWarrior.AddCardsFromDeckButton();
        }
        else
        {
            Debug.Log("No se pueden agregar más cartas a la mano del Guerrero.");
        }
    }
}

