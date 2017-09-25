using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

	public GameObject pieces;

	public GameObject restartGameButton;
	public GameObject turnCounterText;
	public GameObject playerText;
	public GameObject turnText;
	public GameObject signatureText;

	public GameObject promotionPanelWhite;
	public GameObject promotionPanelBlack;

	public GameObject[] buttons = new GameObject [64];
	public GameObject[] chessPieces = new GameObject [12];

	//chessPieces [0] = whiteKing,
	//chessPieces [1] = whiteQueen, 
	//chessPieces [2] = whiteCastle, 
	//chessPieces [3] = whiteBishop, 
	//chessPieces [4] = whiteKnight, 
	//chessPieces [5] = whitePawn, 
	//chessPieces [6] = blackKing, 
	//chessPieces [7] = blackQueen, 
	//chessPieces [8] = blackCastle, 
	//chessPieces [9] = blackBishop, 
	//chessPieces [10] = blackKnight, 
	//chessPieces [11] = blackPawn;

	private GameManager gm;

	void Start () {
		gm = GameObject.FindObjectOfType<GameManager>();
	}

	void Update ()
	{
		Text rgbText = restartGameButton.GetComponent<Text>();
		Text tcText = turnCounterText.GetComponent<Text>();
		Text pText = playerText.GetComponent<Text>();
		Text tText = turnText.GetComponent<Text>();
		Text sText = signatureText.GetComponent<Text>();
//		string numberOfTurns = (gm.turnsSinceGameStarted + 1).ToString();

		if (GameManager.myState == GameManager.States.OutOfGame) {
			rgbText.text = ("Start");
			tcText.text = ("");
			pText.text = ("");
			tText.text = ("");
			sText.text = ("by Filip Ivanovski");
		} else {
			rgbText.text = ("Restart");
			tcText.text = ("Turn: " + (gm.turnsSinceGameStarted + 1).ToString());
			pText.text = ("Player:");
			sText.text = ("");
			if (GameManager.myState == GameManager.States.WhiteTurn) {
				tText.text = ("White");
				tText.color = Color.white;
			} else if (GameManager.myState == GameManager.States.BlackTurn) {
				tText.text = ("Black");
				tText.color = Color.black;
			}
		}
	}
}
