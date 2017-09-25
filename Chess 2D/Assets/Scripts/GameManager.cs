using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {

	public enum States {OutOfGame, WhiteTurn, BlackTurn, Undefined};
	public static States myState = States.OutOfGame;

	public enum Phases {ChoosePiece, ChooseDestination, ChoosePromotion};
	public static Phases myPhase = Phases.ChoosePiece;

	public int turnsSinceGameStarted = 0;

	private Vector3[] locations = new Vector3[64];	// declares and initializes an array of locations where the chess pieces need to go in world space.

	private int[] lastBoardState = new int[64];

	private int chosenPiece;
	private int chosenDestination;
	private int chosenPromotion;

	private UIManager ui;
	private List<int> aMoves;

//	void Something () // compares the chessBoard with the lastBoardState and for every changed value it searches for possible next moves.
//	{
//		for (int i = 0; i < ActionMaster.chessBoard.Length; i++) {
//			if (lastBoardState[i] != ActionMaster.chessBoard[i]) {
//				 ActionMaster.AvailableMoves(i);
//			}
//		}
//	}

	void Start () {

		ui = GameObject.FindObjectOfType<UIManager>();

		for (int i = 0; i < ui.buttons.Length; i++) {
			ui.buttons[i] = GameObject.Find("Button " + i.ToString());
		}
		for (int i = 0; i < ActionMaster.chessBoard.Length; i++) {	// for loop goes through the array of Vector3's and sets their values.
			locations[i] = ui.buttons[i].transform.position;
			locations[i].z = -5;
		}
	}

	void Update () {
		DecideTurn(); 
	}

	void DecideTurn () {
		if (myState != States.OutOfGame) {
			if (turnsSinceGameStarted % 2 == 0) { // it's white player's turn.
				myState = States.WhiteTurn;
			} else { // it's black player's turn.
				myState = States.BlackTurn;
			}
		}
	}

	public void Turn (int input){
		if ((myState == States.WhiteTurn && ActionMaster.chessBoard[input] < 6) || (myState == States.BlackTurn && (ActionMaster.chessBoard[input] >=7 || ActionMaster.chessBoard[input] == 0))) {
			if (myPhase == Phases.ChoosePiece) {
				chosenPiece = input;
				if (ActionMaster.chessBoard[chosenPiece] > 0) {
					myPhase = Phases.ChooseDestination;
					Debug.Log ("Trying to move " + (ui.chessPieces [ActionMaster.chessBoard [chosenPiece] - 1].ToString ()) + " from location " + chosenPiece + ".");
				} else if (ActionMaster.chessBoard[input] == 0) {
					Debug.Log ("You are trying to select an empty field.");
				}
			} else if (myPhase == Phases.ChooseDestination) {
				chosenDestination = input;
				Debug.Log ("To location " + chosenDestination); 
				if (ActionMaster.AvailableMoves(chosenPiece).Contains(chosenDestination)) {
					MovePiece (chosenPiece, chosenDestination);
							if (ActionMaster.chessBoard[chosenPiece] == 6 && chosenDestination / 8 == 7) {
								myPhase = Phases.ChoosePromotion;
								ui.promotionPanelWhite.SetActive(true);
								return;
							}
							if (ActionMaster.chessBoard[chosenPiece] == 12 && chosenDestination / 8 == 0) {
								myPhase = Phases.ChoosePromotion;
								ui.promotionPanelBlack.SetActive(true);
								return;
							}
//					turnsSinceGameStarted++;
					myPhase = Phases.ChoosePiece;
					Debug.Log ("Move sucessful");
				} else {
					Debug.Log ("Move unsucessful");
					myPhase = Phases.ChoosePiece;
				}
			} else if (myPhase == Phases.ChoosePromotion) {
				chosenPromotion = input;	
				ActionMaster.chessBoard[chosenDestination] = chosenPromotion;
				ui.promotionPanelWhite.SetActive(false);
				ui.promotionPanelBlack.SetActive(false);
//				turnsSinceGameStarted++;
				myPhase = Phases.ChoosePiece;
				Debug.Log ("Move sucessful");

			}
		} else if (myState == States.BlackTurn) {
			if (myPhase == Phases.ChoosePiece) {
				// TODO
			} else if (myPhase == Phases.ChooseDestination) {
				// TODO
			} 
		}
	}

	void MovePiece (int origin, int destination)
	{
		int pieceToMove = ActionMaster.chessBoard[origin];
		ActionMaster.chessBoard [origin] = 0;
		ActionMaster.chessBoard[destination] = pieceToMove;
		BoardUpdate ();
	}

//	public bool ReceiveInput (int input) {
//			public void ReceiveInput (int locationNumber)
//	{
//		if (whiteTurn) {
//			if (originPhase && PiecesSetter.chessBoard[locationNumber] > 0 && PiecesSetter.chessBoard[locationNumber] < 7) { // if white player is trying to select a white piece
//				origin = locationNumber;
//				originPhase = false;
////				print ("I want to move " + ps.chessPieces [ps.chessBoard[origin] - 1] + " from location " + origin + ".");
//			} else if (!originPhase && (PiecesSetter.chessBoard[locationNumber] == 0 || PiecesSetter.chessBoard[locationNumber] > 6)) { // if white player is trying to move selected piece onto an empty or black piece
//				destination = locationNumber;
//				originPhase = true;
//				MovePiece(origin, destination);
//				turnsSinceGameStarted++;
////				print ("To location " + locationNumber + ".");
//			}
//		} else  if (!whiteTurn) {
//			if (originPhase && PiecesSetter.chessBoard[locationNumber] > 6) { // if black player is trying to select a black piece
//				origin = locationNumber;
//				originPhase = false;
////				print ("I want to move " + ps.chessPieces [ps.chessBoard[locationNumber] - 1] + " from location " + origin + ".");
//			} else if (!originPhase && (PiecesSetter.chessBoard[locationNumber] == 0 || PiecesSetter.chessBoard[locationNumber] < 7)) { // if black player is trying to move selected piece onto an empty or white piece
//				destination = locationNumber;
//				originPhase = true;
//				MovePiece(origin, destination);
//				turnsSinceGameStarted++;
////				print ("To location " + locationNumber + ".");
//			}
//		}
//	}
//		aMoves = ActionMaster.AvailableMoves(input);

	public void SetBoard () {

		// black pieces are set
		ActionMaster.chessBoard[56] = 9;
		ActionMaster.chessBoard[57] = 11;
		ActionMaster.chessBoard[58] = 10;
		ActionMaster.chessBoard[59] = 8;
		ActionMaster.chessBoard[60] = 7;
		ActionMaster.chessBoard[61] = 10;
		ActionMaster.chessBoard[62] = 11;
		ActionMaster.chessBoard[63] = 9;

		for (int i = 48; i <= 55; i++) {	// black pawns are set
			ActionMaster.chessBoard[i] = 12;
		}

		for (int i = 8; i <= 15; i++) {		// white pawns are set
			ActionMaster.chessBoard[i] = 6;
		}

		// white pieces are set
		ActionMaster.chessBoard[0] = 3;
		ActionMaster.chessBoard[1] = 5;
		ActionMaster.chessBoard[2] = 4;
		ActionMaster.chessBoard[3] = 2;
		ActionMaster.chessBoard[4] = 1;
		ActionMaster.chessBoard[5] = 4;
		ActionMaster.chessBoard[6] = 5;
		ActionMaster.chessBoard[7] = 3;
	}

	public void ClearBoard (){
		for (int i = 0; i < ActionMaster.chessBoard.Length; i++) {	// forloop goes through the chessboard array and sets each value to 0 (empty)
			ActionMaster.chessBoard[i] = 0;
		}
	}

		public void RestartGame () {
		ClearBoard();
		SetBoard();
		BoardUpdate();
		myState = States.WhiteTurn;
		myPhase = Phases.ChoosePiece;
		turnsSinceGameStarted = 0;
	}

	void ClearLocation (Vector3 target) {	// takes a Vector3 as an argument, searches for any children in the Pieces GameObject with that Vector3 position, and destroys them.
		foreach(Transform child in ui.pieces.transform) {
			if (child.transform.position == target){
				Destroy(child.gameObject);
			}
		}
	}

	public void BoardUpdate (){
		Quaternion qi = Quaternion.identity;

		for (int i = 0; i < ActionMaster.chessBoard.Length; i++) {	// for loop goes through the values of the chessBoard array, and it instantiates the pieces according to their respective values
			switch (ActionMaster.chessBoard [i]) {
				case 0:
					ClearLocation(locations[i]);
					break;
				case 1:
					ClearLocation(locations[i]);
					GameObject whiteKing = Instantiate (ui.chessPieces[0], locations[i], qi) as GameObject;
					whiteKing.transform.parent = ui.pieces.transform;
					break;
				case 2:
					ClearLocation(locations[i]);
					GameObject whiteQueen = Instantiate (ui.chessPieces[1], locations[i], qi) as GameObject;
					whiteQueen.transform.parent = ui.pieces.transform;
					break;
				case 3:
					ClearLocation(locations[i]);
					GameObject whiteCastle = Instantiate (ui.chessPieces[2], locations[i], qi) as GameObject;
					whiteCastle.transform.parent = ui.pieces.transform;
					break;
				case 4:
					ClearLocation(locations[i]);
					GameObject whiteBishop = Instantiate (ui.chessPieces[3], locations[i], qi) as GameObject;
					whiteBishop.transform.parent = ui.pieces.transform;
					break;
				case 5:
					ClearLocation(locations[i]);
					GameObject whiteKnight = Instantiate (ui.chessPieces[4], locations[i], qi) as GameObject;
					whiteKnight.transform.parent = ui.pieces.transform;
					break;
				case 6:
					ClearLocation(locations[i]);
					GameObject whitePawn = Instantiate (ui.chessPieces[5], locations[i], qi) as GameObject;
					whitePawn.transform.parent = ui.pieces.transform;
					break;
				case 7:
					ClearLocation(locations[i]);
					GameObject blackKing = Instantiate (ui.chessPieces[6], locations[i], qi) as GameObject;
					blackKing.transform.parent = ui.pieces.transform;
					break;
				case 8:
					ClearLocation(locations[i]);
					GameObject blackQueen = Instantiate (ui.chessPieces[7], locations[i], qi) as GameObject;
					blackQueen.transform.parent = ui.pieces.transform;
					break;
				case 9:
					ClearLocation(locations[i]);
					GameObject blackCastle = Instantiate (ui.chessPieces[8], locations[i], qi) as GameObject;
					blackCastle.transform.parent = ui.pieces.transform;
					break;
				case 10:
					ClearLocation(locations[i]);
					GameObject blackBishop = Instantiate (ui.chessPieces[9], locations[i], qi) as GameObject;
					blackBishop.transform.parent = ui.pieces.transform;
					break;
				case 11:
					ClearLocation(locations[i]);
					GameObject blackKnight = Instantiate (ui.chessPieces[10], locations[i], qi) as GameObject;
					blackKnight.transform.parent = ui.pieces.transform;
					break;
				case 12:
					ClearLocation(locations[i]);
					GameObject blackPawn = Instantiate (ui.chessPieces[11], locations[i], qi) as GameObject;
					blackPawn.transform.parent = ui.pieces.transform;
					break;
			}
		}
	}
}